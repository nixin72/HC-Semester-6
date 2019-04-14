using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using RAC.RACModels;

namespace RAC.BusinessLogic
{
    public class DocumentList
    {
        private static DocumentList _instance;
        private List<UploadedDocument> _documentList;

        public DocumentList()
        {
            _documentList = new List<UploadedDocument>();
        }

        public static DocumentList Instance => _instance ??
                                               (_instance = new DocumentList
                                               {
                                                   _documentList = new List<UploadedDocument>()
                                               });

        public List<UploadedDocument> GetDocumentList()
        {
            return _documentList;
        }

        public ERRORS SaveDocumentsLocal(String FileName, Stream InputStream, int ContentLength, RACRequest raq, Boolean isAddedByRACAdvisor)
        {
            try
            {
                string name = FileName;
                byte[] bytes;
                using (var br = new BinaryReader(InputStream))
                {
                    bytes = br.ReadBytes(ContentLength);
                }

                _instance._documentList.Add(UploadedDocumentBll.LoadDocumentFromDisk(name, bytes, raq, isAddedByRACAdvisor));
                return ERRORS.SUCCESS;
            }
            catch (FileNotFoundException f)
            {
                try
                {
                    using (StreamWriter file = new StreamWriter(HttpContext.Current.Server.MapPath("err.log"), true))
                    {
                        file.WriteLine(f.ToString());
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(@"Error could not be logged: " + e);
                }

                return ERRORS.FILE_NOT_FOUND;
            }
        }

        public ERRORS RemoveDocumentsLocal(int index)
        {
            /*
             * Summary:
             * Removes a document from local document list singleton. 
             * 
             * Parameters:
             * index - The index of the document within the document list
             * 
             * Returns:
             * SUCCESS - File was removed successfully
             * DOCUMENT_REMOVAL_FAIL - Document was still inside the local list, despite being found and removed
             */

            // If the index is larger than the document list count, then the document in question was invalid, and
            // is being removed because dropzone.js prevented the user from uploading.
            // This is not an error, just how the system handles removal.
            if (_instance._documentList.Count <= index)
            {
                return ERRORS.DOCUMENT_REMOVAL_FAIL;
            }

            UploadedDocument foundDocument = _instance._documentList.ElementAt(index);
            _instance._documentList.Remove(foundDocument);

            return _instance._documentList.IndexOf(foundDocument) != -1 ? ERRORS.DOCUMENT_REMOVAL_FAIL : ERRORS.SUCCESS;
        }

        public void SaveChangesToDb(RACRequest currentRac)
        {
            foreach (UploadedDocument document in Instance._documentList)
            {
                try
                {
                    document.DateUploaded = DateTime.Now;
                    document.IsOpened = false;
                    document.RACRequest = DbContext.Context.RACRequest.First(r => r.Id == document.RACRequestId);
                    DbContext.Context.UploadedDocument.Add(document);
                    DbContext.Context.SaveChanges();
                }
                catch (Exception e)
                {
                    DbContext.Context.UploadedDocument.Remove(document);
                    Debug.WriteLine(e.Message + " | " + e.InnerException);
                }
            }

            _instance = null;
        }
    }
}