using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using RAC.BusinessLogic;
using RAC.Models;
using RAC.RACModels;

namespace RAC.Controllers
{
    public class UploadDocumentController : Controller
    {
        /*
         * This class will handle all controller activities for all uploading documents activities.
         */

        // JSON_BOOLEAN_[TRUE, FALSE] represent a boolean value that would be given back to an AJAX request.
        private const string JSON_BOOLEAN_TRUE = "{ \"isSuccess\": true }";
        private const string JSON_BOOLEAN_FALSE = "{ \"isSuccess\": false}";

        [HttpGet]
        public JsonResult SaveChangesToDb()
        {
            /*
             * The method will save the instance list of documents to the database. 
             *
             * See also:
             * SaveChangesToDB()
             * uploaded-documents.js
             *
             * Parameters:
             * None
             * 
             * Returns:
             * An JsonResult that will be used as an error/success code
             * 
             */
            Candidate can = CandidateBLL.GetCandidateByUserType();
            RACRequest currentRac = can.RACRequest;
            DocumentList.Instance.SaveChangesToDb(currentRac);

            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                RACAdvisorBLL.SendUploadNotification(can.Id);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadDocument(int id, Boolean isAddedByRACAdvisor)
        {
            /*
             * The method will get a document from the database and return it as a FileResult
             *
             * See also:
             * GetUploadedDocuments()
             *
             * Parameters:
             * int id = The id of the document that will be searched for.
             * 
             * Returns:
             * A FileResult that will contain the information of the file found. Returns null if not file was found.
             * 
             */
            Candidate can = CandidateBLL.GetCandidateByUserType();

            var foundDoc = new UploadedDocument();

            RACRequest currentRac = can.RACRequest;
            List<UploadedDocument> docs = UploadedDocumentBll.GetUploadedDocuments(currentRac.Id, isAddedByRACAdvisor);

            foreach (UploadedDocument doc in docs)
            {
                if (doc.Id == id)
                {
                    foundDoc = doc;
                }
            }
            UploadedDocumentBll.SetDocumentRead(foundDoc);
            byte[] fileBytes = foundDoc.Binary;
            string fileName = foundDoc.Name;
            return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }

        public PartialViewResult _UploadDocuments()
        {
            return PartialView();
        }

        public PartialViewResult ShowViewDocuments(Boolean isAddedByRACAdvisor)
        {
            /*
             * The method will return the ViewDocuments partial view which displays all the documents that have been uploaded to date.
             *
             * See also:
             * GetUploadedDocuments()
             *
             * Parameters:
             * None
             * 
             * Returns:
             * A PartialView that displays all uploaded documents.
             * 
             */

            Candidate can = CandidateBLL.GetCandidateByUserType();
            RACRequest currentRac = can.RACRequest;
            return PartialView("_ViewDocuments", UploadedDocumentBll.GetUploadedDocuments(currentRac.Id, isAddedByRACAdvisor));
        }

        public PartialViewResult _ViewDocuments()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult DeleteFileFromDb(int id, Boolean isAddedByRACAdvisor)
        {
            /*
             * Used in the _ViewDocuments.cshtml this will remove a file from the database.
             *
             * See also:
             * GetUploadedDocuments()
             *
             * Parameters:
             * int id = The id of the document to remove.
             * 
             * Returns:
             * A PartialView that displays an updated list of all the documents.
             * 
             */
            UploadedDocumentBll.RemoveDocument(id);
            Candidate can = CandidateBLL.GetCandidateByUserType();
            RACRequest currentRac = can.RACRequest;
            return PartialView("_ViewDocuments", UploadedDocumentBll.GetUploadedDocuments(currentRac.Id, isAddedByRACAdvisor));
        }

        public ActionResult UploadDocumentTest()
        {
            return View(DocumentList.Instance.GetDocumentList());
        }

        [HttpPost]
        public JsonResult RemoveFileFromLocal(int ind)
        {
            /*
             * Used in the uploaded-documents.js this will remove a file from a local list. It will NOT affect the database. This is for whenever they 
             * drop or upload files but have not commited the upload yet.
             *
             * See also:
             * RemoveDocumentsLocal()
             *
             * Parameters:
             * int id = The id of the document to remove.
             * 
             * Returns:
             * A JsonResult that returns the success/error code.
             * 
             */

            ERRORS returnCode = DocumentList.Instance.RemoveDocumentsLocal(ind);

            switch (returnCode)
            {
                case ERRORS.SUCCESS:
                    return Json(JSON_BOOLEAN_TRUE);
                case ERRORS.DOCUMENT_REMOVAL_FAIL:
                    return Json(JSON_BOOLEAN_FALSE);
                // Any other codes should be assumed false.
                default:
                    return Json(JSON_BOOLEAN_FALSE);
            }
        }

        [HttpPost]
        public JsonResult IsTotalFileSizeValid(bool isAddedByRACAdvisor)
        {
            /*
             * Called in `uploaded-documents.js`. Gets the total file size of ALL documents the user has selected,
             * both uploaded/to-be uploaded, and checks if that total is less than 30 MB. If it is, then it returns
             * false. If it is not, then it returns true. Both inside a JSON object with a single key: success.
             *
             * See also:
             * UploadedDocumentBll.GetUploadedDocuments()
             * DocumentList.Instance.GetDocumentList()
             * UploadedDocumentBll.MAX_TOTAL_FILE_SIZE
             *
             * Parameters:
             * None
             *
             * Returns:
             * JsonResult - { success: [true, false] }
             */
            Candidate can = CandidateBLL.GetCandidateByUserType();
            int racRequestId = can.RACRequest.Id;
            List<UploadedDocument> uploadedDocuments = UploadedDocumentBll.GetUploadedDocuments(racRequestId, isAddedByRACAdvisor);

            int totalUploadedSize = uploadedDocuments.Sum(document => document.Binary.Length);
            int totalLocalSize = DocumentList.Instance.GetDocumentList().Sum(document => document.Binary.Length);
            int totalCombinedDocumentSize = totalUploadedSize + totalLocalSize;

            return Json(totalCombinedDocumentSize > Convert.ToInt32(WebConfigurationManager.AppSettings.Get("MaxTotalFileSize"))
                ? JSON_BOOLEAN_FALSE
                : JSON_BOOLEAN_TRUE);
        }

        public ActionResult SaveUploadedFileLocal(bool isAddedByRACAdvisor)
        {
            /*
             * Used by the form in _UploadDocuments.cshtml this will save the files to the instance list of documents. 
             * This controller is called whenever a user uploads a file. 
             * NO database manipulation is done here.
             *
             * See also:
             * SaveDocumentsLocal()
             *
             * Parameters:
             * None
             * 
             * Returns:
             * A JsonResult that returns the success/error code.
             * 
             */

            try
            {
                Candidate can = CandidateBLL.GetCandidateByUserType();
                RACRequest currRac = can.RACRequest;
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    DocumentList.Instance.SaveDocumentsLocal(file.FileName, file.InputStream, file.ContentLength, currRac, isAddedByRACAdvisor);
                }

                return Json(new {Message = ERRORS.SUCCESS});
            }
            catch (Exception ex)
            {
                return Json(new {Message = "Exception while saving file:" + ex.Message});
            }
        }
    }
}