using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Microsoft.Ajax.Utilities;
using RAC.RACModels;

namespace RAC.BusinessLogic
{
    public static class UploadedDocumentBll
    {
        /*
         * Contains all business logic code for the UploadedDocument classes.
         */
        private const int ONE_MEGABYTE = 1000000;
        private const int ONE_KILOBYTE = 1000;

        public static UploadedDocument LoadDocumentFromDisk(string filename, byte[] fileStreamBytes,
            RACRequest racRequest, Boolean isAddedByRACAdvisor)
        {
            /*
             * Gets a filename, and a stream of bytes. Checks to make sure they are valid,
             * checks if the document has a valid size and type, then returns the new
             * document object.
             *
             * See also:
             * ValidateFileSize()
             * ValidateFileType
             *
             * Parameters:
             * string filename = The name of the uploaded document.
             * byte[] fileStreamBytes = The bytes of the file as streamed by the binary reader.
             * RACRequest racRequest = The racRequest object this document is attached to.
             * 
             * 
             * Returns:
             * A completed UploadedDocument object with a name, an associated RACRequest, and the bytes.
             * Returns null if the upload failed.
             * 
             */

            if (filename.IsNullOrWhiteSpace() || fileStreamBytes.Length <= 0)
            {
                return null;
            }

            if (!IsFileSizeValid(fileStreamBytes) || !IsFileTypeValid(filename))
            {
                return null;
            }

            if (racRequest == null)
            {
                return null;
            }

            return new UploadedDocument
            {
                Name = filename,
                Binary = fileStreamBytes,
                RACRequestId = racRequest.Id,
                RACRequest = racRequest,
                IsAddedByRACAdvisor = isAddedByRACAdvisor
            };
        }

        public static ERRORS UploadDocument(UploadedDocument document)
        {
            /*
             * Takes in a document, sends it to the database to be stored.
             * 
             * Parameters:
             * UploadedDocument document = A completed document <seealso> LoadDocumentFromDisk()
             * 
             * Returns:
             * SUCCESS - The document was uploaded
             * DOCUMENT_IS_NULL - The passed in document was null
             */

            if (document == null)
            {
                return ERRORS.DOCUMENT_IS_NULL;
            }

            DbContext.Context.UploadedDocument.Add(document);
            DbContext.Context.SaveChanges();
            return ERRORS.SUCCESS;
        }


        public static List<UploadedDocument> GetUploadedDocuments(int racId, Boolean isAddedByRACAdvisor)
        {
            /*
             * Gets a list of uploaded documents for a specific RAC
             *
             * Parameters:
             * int racId = The id of the RAC 
             * 
             * Returns:
             * A list of uploaded documents for a specific RAC. The list will be empty if there are no documents uploaded for that RAC.
             * 
             */

            var documentList = new List<UploadedDocument>();

            IQueryable<UploadedDocument> documents =
                DbContext.Context.UploadedDocument.Where(u => u.RACRequestId == racId && u.IsAddedByRACAdvisor == isAddedByRACAdvisor);

            foreach (UploadedDocument doc in documents)
            {
                documentList.Add(new UploadedDocument
                {
                    Binary = doc.Binary,
                    Name = doc.Name,
                    DateUploaded = doc.DateUploaded,
                    //TODO Fetch the RAC Request from the DB
                    RACRequest = null,
                    RACRequestId = doc.RACRequestId,
                    Id = doc.Id,
                    IsOpened = doc.IsOpened,
                    IsAddedByRACAdvisor = isAddedByRACAdvisor
                });
            }

            return documentList;
        }


        public static void SetDocumentRead(UploadedDocument doc)
        {
            if ((doc.IsAddedByRACAdvisor && !RACAdvisorBLL.CheckIsRACAdvisor())|| (!doc.IsAddedByRACAdvisor && RACAdvisorBLL.CheckIsRACAdvisor()))
            {
                if (!doc.IsOpened)
                {
                    doc.IsOpened = true;

                    var dbDocList = DbContext.Context.UploadedDocument.Where(d => d.Id == doc.Id);
                    dbDocList.First().IsOpened = true;
                    DbContext.Context.SaveChanges();
                }
            }
        }
        public static UploadedDocument RetrieveDocument(int documentId)
        {
            /*
             * Retrieves a document from the database, based on a passed in documentId
             * 
             * Parameters:
             * int documentId = The ID of the document we wish to retrieve.
             * 
             * Returns:
             * The retrieved document.
             */

            return DbContext.Context.UploadedDocument
                .FirstOrDefault(doc => doc.Id == documentId);
        }

        public static ERRORS RemoveDocument(int documentId)
        {
            /*
             * Removes a specific document from the database.
             *
             * Parameters:
             * int documentId - The document's id to pass through to the database.
             * 
             * Returns:
             * SUCCESS - The document deletion was successfull
             * NO_DOCUMENT_WITH_ID - The document Id did not exist in the database
             * 1 - The document exists and the deletion was successfull.
             *
             */
            UploadedDocument docToRemove = DbContext.Context.UploadedDocument
                .FirstOrDefault(d => d.Id == documentId);

            if (docToRemove == null)
            {
                return ERRORS.NO_DOCUMENT_WITH_ID;
            }

            DbContext.Context.UploadedDocument.Remove(docToRemove);
            DbContext.Context.SaveChanges();
            return ERRORS.SUCCESS;
        }

        public static string FileSizeWithUnit(double fileSizeInBytes)
        {
            /*
             * Convert a file size in bytes to Megabytes, or Kilobytes. Appends the proper unit to the end of the size.
             *
             * Parameters:
             * fileSizeInBytes - The file size of a document. This can be found by calling document.Binary.Length.
             *
             * Returns:
             * A string containing the converted file size, and the unit.
             */

            if (fileSizeInBytes < 1)
            {
                return "0.0 KB";
            }

            if (fileSizeInBytes >= ONE_MEGABYTE)
            {
                return fileSizeInBytes / ONE_MEGABYTE + " MB";
            }

            return fileSizeInBytes / ONE_KILOBYTE + " KB";
        }

        public static string GetFileTypeFromExtention(string fileExtention)
        {
            /*
             * Takes in a file extention (without the leading '.'), and returns the file type. Only valid file types are
             * handled, there is no current situation where one would need a non-valid file type (as of 2018-01-31).
             *
             * See also:
             * IsFileTypeValid
             *
             * Parameters:
             * fileExtention - The file extention as a string, without the leading period ('.').
             *
             * Returns:
             * The full file type associated with the extention
             */

            if (fileExtention.IsNullOrWhiteSpace())
            {
                return "None";
            }

            switch (fileExtention.ToLower())
            {
                case "bmp":
                    return "Bitmap Image";
                case "doc":
                case "docx":
                    return "Microsoft Word Document";
                case "jpg":
                case "jpeg":
                    return "JPEG Image";
                case "ods":
                    return "Open Document Spreadsheet";
                case "odt":
                    return "Open Document Text Document";
                case "pdf":
                    return "PDF Document";
                case "png":
                    return "PNG Image";
                case "rtf":
                    return "Real Text File";
                case "tif":
                case "tiff":
                    return "Tiff Image";
                case "txt":
                    return "Plain Text File";
                case "xls":
                case "xlsx":
                    return "Microsoft Excel Spreadsheet";
                default:
                    return "Unknown";
            }
        }

        private static bool IsFileSizeValid(byte[] documentBytes)
        {
            /* 
             * Validate that the file size of a single file is less than the
             * filesize maximum, 10MB. 
             * 
             * Parameters:
             * UploadedDocument document = A document object
             * 
             * Returns:
             * true if file is <= 10MB
             * false if file is > 10MB
             */

            return documentBytes?.Length <= Convert.ToInt32(WebConfigurationManager.AppSettings.Get("MaxFileSize"));
        }

        private static bool IsFileTypeValid(string filename)
        {
            /*
             * Validate that the file type of a single file is one of our
             * approved file type. Those filetypes are:
             * - bmp
             * - doc & docx
             * - jpg & jpeg
             * - ods
             * - odt
             * - pdf
             * - png
             * - rtf
             * - tif & tiff
             * - txt
             * - xls & xlsx 
             * 
             * Parameters:
             * filename = A documents filename
             * 
             * 
             * Returns:
             * true if the filetype is valid.
             * fale if the filetype is not valid.
             */

            if (!filename.Contains("."))
            {
                return false;
            }

            string documentFiletype = filename.Split('.')[1];
            switch (documentFiletype.ToLower())
            {
                case "bmp":
                case "doc":
                case "docx":
                case "jpg":
                case "jpeg":
                case "ods":
                case "odt":
                case "pdf":
                case "png":
                case "rtf":
                case "tif":
                case "tiff":
                case "txt":
                case "xls":
                case "xlsx":
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsAtLeastOneFileUploaded(Candidate user)
        {
            /*
             * Determines if at least one document has been uploaded to the database.
             * Called by `submitRacRequst()` in `candidate-home.js`.
             *
             * See also:
             * UploadedDocumentBll.GetUploadedDocuments();
             *
             * Returns:
             * JsonResult - { success: [true, false] }
             */

            int racRequestId = user.RACRequest.Id; 
            List<UploadedDocument> uploadedDocuments = GetUploadedDocuments(racRequestId, false);

            return uploadedDocuments.Count >= 1;
        }
    }
}