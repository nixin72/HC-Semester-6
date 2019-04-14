using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RAC.BusinessLogic;
using RAC.RACModels;
using RAC_Tests.Factories;

// ReSharper disable InconsistentNaming

namespace RAC_Tests
{
    [TestClass]
    public class UploadedDocuments_Test
    {
        // I'm not convinced that we're not going to need these tests in the future. Hold off on deleting these for
        // now. - Max

        //[TestClass]
        //public class ValidateDocument_TestClass
        //{
        //    private readonly UploadedDocument _document = new UploadedDocument
        //    {
        //        Name = "bigdocumentfile.zip",
        //        Binary = new byte[11534336]
        //    };

        //    [TestMethod]
        //    public void ValidateDocument_FileTooLarge()
        //    {
        //        Assert.IsFalse(UploadedDocumentBll.ValidateFileSize(_document.Binary),
        //            "DOCUMENT_TOO_LARGE: Document was accepted despite being greater than 10MB.");
        //    }

        //    [TestMethod]
        //    public void ValidateDocument_FileSizeNull()
        //    {
        //        Assert.IsFalse(UploadedDocumentBll.ValidateFileSize(null),
        //            "DOCUMENT_NULL_BYTES: Document was accepted despite bytes being null.");
        //    }

        //    [TestMethod]
        //    public void ValidateDocument_WrongFileType()
        //    {
        //        Assert.IsFalse(UploadedDocumentBll.ValidateFileType(_document.Name),
        //            "DOCUMENT_WRONG_FILE_TYPE: Document was accepted despite not being an accepted file type.");
        //    }

        //    [TestMethod]
        //    public void ValidateDocument_NullFilename()
        //    {
        //        Assert.IsFalse(UploadedDocumentBll.ValidateFileType(null),
        //            "DOCUMENT_NULL_NAME: Document was accepted despite not having a filename.");
        //    }

        //    [TestMethod]
        //    public void ValidateDocument_NoFileExtention()
        //    {
        //        Assert.IsFalse(UploadedDocumentBll.ValidateFileType("paystubs"),
        //            "DOCUMENT_NO_EXTENTION: Document was accepted despite not having a file extention.");
        //    }
        //}

        [TestClass]
        public class UploadDocument_TestClass
        {
            [TestMethod]
            public void LoadDocument_DocumentExists()
            {
                try
                {
                    CreateFile("file.txt", 666);
                    var fs = new FileStream("file.txt", FileMode.Open, FileAccess.Read);
                    string filename = Path.GetFileName(fs.Name);
                    var br = new BinaryReader(fs);
                    UploadedDocument document =
                        UploadedDocumentBll.LoadDocumentFromDisk(filename,
                            br.ReadBytes((int) fs.Length), new RACRequest
                            {
                                Id = 9999
                            }, false);
                    Assert.IsNotNull(document, "DOCUMENT_IS_NULL: Document was not read from file system.");
                    Assert.AreEqual("file.txt", document.Name,
                        "DOCUMENT_NAME_INCORRECT: Document name was not loaded correctly.");
                    Assert.AreEqual(666, document.Binary.Length,
                        "DOCUMENT_SIZE_INCORRECT: Documentment size was not read correctly.");
                }
                catch (IOException)
                {
                    Assert.Fail("DOCUMENT_CREATION_FAILED: Document could not be read/wrote to disk.");
                }
            }

            [TestMethod]
            public void LoadDocument_DocumentDoesNotExist()
            {
                UploadedDocument noNameDocument = UploadedDocumentBll.LoadDocumentFromDisk("", new byte[1000], null, false);
                UploadedDocument noSizeDocument =
                    UploadedDocumentBll.LoadDocumentFromDisk("notafile.jpeg", new byte[0], null, false);
                Assert.IsNull(noNameDocument,
                    "DOCUMENT_IS_NOT_NULL: Document that doesn't exist was read from file system.");
                Assert.IsNull(noSizeDocument,
                    "DOCUMENT_IS_NOT_NULL: Document that doesn't exist was read from file system.");
            }

            [TestMethod]
            public void LoadDocument_DocumentNameInvalid()
            {
                try
                {
                    CreateFile("my_mixtape.flac", 666);
                    var fs = new FileStream("my_mixtape.flac", FileMode.Open, FileAccess.Read);
                    string filename = Path.GetFileName(fs.Name);
                    var br = new BinaryReader(fs);
                    UploadedDocument document =
                        UploadedDocumentBll.LoadDocumentFromDisk(filename,
                            br.ReadBytes((int) fs.Length), new RACRequest
                            {
                                Id = 9999
                            }, false);
                    Assert.IsNull(document,
                        "DOCUMENT_WRONG_FILE_TYPE: Document was accepted despite not being an accepted file type.");
                }
                catch (IOException)
                {
                    Assert.Fail("DOCUMENT_CREATION_FAILED: Document could not be read/wrote to disk.");
                }
            }

            [TestMethod]
            public void LoadDocument_RACRequestIsNull()
            {
                try
                {
                    CreateFile("resume.docx", 10000);
                    var fs = new FileStream("resume.docx", FileMode.Open, FileAccess.Read);
                    string filename = Path.GetFileName(fs.Name);
                    var br = new BinaryReader(fs);
                    UploadedDocument document =
                        UploadedDocumentBll.LoadDocumentFromDisk(filename,
                            br.ReadBytes((int) fs.Length), null, false);
                    Assert.IsNull(document,
                        "DOCUMENT_NO_RAC_REQUEST: Document was accepted despite not having an associated RAC Request.");
                }
                catch (IOException)
                {
                    Assert.Fail("DOCUMENT_CREATION_FAILED: Document could not be read/wrote to disk.");
                }
            }

            [TestMethod]
            public void UploadDocument_Success()
            {
                UploadedDocument document = UploadDocumentTestFactory.Construct("document.pdf", 10);
                try
                {
                    ERRORS returnValue = UploadedDocumentBll.UploadDocument(document);
                    Assert.AreEqual(ERRORS.SUCCESS, returnValue,
                        "DOCUMENT_UPLOAD_FAILED: Document was not uploaded dispite being a valid documnet.");

                    UploadDocumentTestFactory.Dispose(document);
                }
                catch (Exception ex)
                {
                    Assert.Fail("DATABASE_CONNECTION_FAILED: Failed to communicate with the database. The error is: " +
                                ex.Message + ex.InnerException);
                }
            }
        }

        [TestMethod]
        public void UploadDocument_DocumentIsNull()
        {
            try
            {
                ERRORS returnValue = UploadedDocumentBll.UploadDocument(null);
                Assert.AreEqual(ERRORS.DOCUMENT_IS_NULL, returnValue, "DOCUMENT_NULL_UPLOADED: Document was uploaded dispite being null.");
            }
            catch (Exception ex)
            {
                Assert.Fail("DATABASE_CONNECTION_FAILED: Failed to communicate with the database. The error is: " +
                            ex.Message);
            }
        }


        [TestClass]
        public class RetrieveDocument_TestClass
        {
            [TestMethod]
            public void RetrieveDocument_DocumentExists()
            {
                UploadedDocument document = UploadDocumentTestFactory.Construct("retrieve_document_exists.pdf", 3, true);
                try
                {
                    UploadedDocument retrieveDocument = UploadedDocumentBll.RetrieveDocument(document.Id);
                    Assert.IsNotNull(retrieveDocument, "DOCUMENT_NOT_FOUND: Existing document was not found.");
                }
                catch (Exception ex)
                {
                    Assert.Fail("DATABASE_CONNECTION_FAILED: Failed to communicate with the database. The error is: " +
                                ex.Message);
                }
                finally
                {
                    UploadDocumentTestFactory.Dispose(document);
                }
            }

            [TestMethod]
            public void RetrieveDocument_DocumentDoesNotExist()
            {
                try
                {
                    UploadedDocument document = UploadedDocumentBll.RetrieveDocument(0);
                    Assert.IsNull(document, "DOCUMENT_FOUND: Non-existant document was found.");
                }
                catch (Exception ex)
                {
                    Assert.Fail("DATABASE_CONNECTION_FAILED: Failed to communicate with the database. The error is: " +
                                ex.Message);
                }
            }

            [TestMethod]
            public void GetUploadedDocuments_DocumentsExist()
            {
                UploadedDocument document = UploadDocumentTestFactory.Construct("get_uploaded_documents_exists.txt", 1, true);
                int racId = document.RACRequest.Id;

                try
                {
                    List<UploadedDocument> documents = UploadedDocumentBll.GetUploadedDocuments(racId, false);
                    Assert.IsNotNull(documents,
                        "DOCUMENT_RETRIEVAL_FAIL: Documents were not returned from the database.");
                }
                catch (Exception ex)
                {
                    Assert.Fail("DATABASE_CONNECTION_FAILED: Failed to communicate with the database. The error is: " +
                                ex.Message);
                }
                finally
                {
                    UploadDocumentTestFactory.Dispose(document);
                }
            }

            [TestMethod]
            public void GetUploadedDocuments_DocumentsDoNoExist()
            {
                try
                {
                    List<UploadedDocument> documents = UploadedDocumentBll.GetUploadedDocuments(0, false);
                    Assert.AreEqual(0, documents.Count,
                        "DOCUMENT_RETRIEVED_NOTHING: Documents were returned from the database despite the RAC ID being invalid.");
                }
                catch (Exception ex)
                {
                    Assert.Fail("DATABASE_CONNECTION_FAILED: Failed to communicate with the database. The error is: " +
                                ex.Message);
                }
            }
        }

        [TestClass]
        public class RemoveDocuments_TestClass
        {
            [TestMethod]
            public void RemoveDocuments_Success()
            {
                UploadedDocument document = UploadDocumentTestFactory.Construct("remove_documents_success.txt", 1, true);
                RACRequest racRequest = document.RACRequest;
                try
                {
                    ERRORS returnCode = UploadedDocumentBll.RemoveDocument(document.Id);
                    Assert.AreEqual(ERRORS.SUCCESS, returnCode,
                        "DOCUMENT_DELETION_FAILED: Document was not deleted from the database.");
                }
                catch (Exception ex)
                {
                    Assert.Fail("DATABASE_CONNECTION_FAILED: Failed to communicate with the database. The error is: " +
                                ex.Message);
                }
                finally
                {
                    UploadDocumentTestFactory.Dispose(racRequest);
                }
            }

            [TestMethod]
            public void RemoveDocuments_DocumentDoesNotExist()
            {
                try
                {
                    ERRORS returnCode = UploadedDocumentBll.RemoveDocument(0);
                    Assert.AreEqual(ERRORS.NO_DOCUMENT_WITH_ID, returnCode, "DOCUMENT_DELETION_ID: Document was \"deleted\" from the database.");
                }
                catch (Exception ex)
                {
                    Assert.Fail("DATABASE_CONNECTION_FAILED: Failed to communicate with the database. The error is: " +
                                ex.Message);
                }
            }
        }

        [TestClass]
        public class FileSizeWithUnit_TestClass
        {
            [TestMethod]
            public void FileSizeWithUnit_LessThanOne()
            {
                Assert.AreEqual("0.0 KB", UploadedDocumentBll.FileSizeWithUnit(0),
                    "FILE_SIZE_PARSING_FAILED: Size 0 did not return 0.0 KB.");

                Assert.AreEqual("0.0 KB", UploadedDocumentBll.FileSizeWithUnit(0.1),
                    "FILE_SIZE_PARSING_FAILED: Size 0.5 did not return 0.0 KB.");

                Assert.AreEqual("0.0 KB", UploadedDocumentBll.FileSizeWithUnit(0.9),
                    "FILE_SIZE_PARSING_FAILED: Size 0.9 did not return 0.0 KB.");

            }

            [TestMethod]
            public void FileSizeWithUnit_OneOrGreater()
            {
                Assert.AreEqual("0.001 KB", UploadedDocumentBll.FileSizeWithUnit(1),
                    "FILE_SIZE_PARSING_FAILED: Size 1 did not return 1.0 KB.");

                Assert.AreEqual("0.5 KB", UploadedDocumentBll.FileSizeWithUnit(500),
                    "FILE_SIZE_PARSING_FAILED: Size 500 did not return 0.5 KB.");

                Assert.AreEqual("0.999 KB", UploadedDocumentBll.FileSizeWithUnit(999),
                    "FILE_SIZE_PARSING_FAILED: Size 999 did not return 0.999 KB.");
            }

            [TestMethod]
            public void FileSizeWithUnit_Kilobytes()
            {
                Assert.AreEqual("1 KB", UploadedDocumentBll.FileSizeWithUnit(1000),
                    "FILE_SIZE_PARSING_FAILED: Size 1000 did not return 1.0 KB");

                Assert.AreEqual("500 KB", UploadedDocumentBll.FileSizeWithUnit(500000),
                    "FILE_SIZE_PARSING_FAILED: Size 500000 did not return 500.0 KB");

                Assert.AreEqual("999.999 KB", UploadedDocumentBll.FileSizeWithUnit(999999),
                    "FILE_SIZE_PARSING_FAILED: Size 999999 did not return 999.999 KB");
            }

            [TestMethod]
            public void FileSizeWithUnit_Megabytes()
            {

                Assert.AreEqual("1 MB", UploadedDocumentBll.FileSizeWithUnit(1000000),
                    "FILE_SIZE_PARSING_FAILED: Size 1000000 did not return 1 MB");

                Assert.AreEqual("1.5 MB", UploadedDocumentBll.FileSizeWithUnit(1500000),
                    "FILE_SIZE_PARSING_FAILED: Size 1500000 did not return 1.5 MB");

                Assert.AreEqual("2 MB", UploadedDocumentBll.FileSizeWithUnit(2000000),
                    "FILE_SIZE_PARSING_FAILED: Size 2000000 did not return 2 MB");
            }
        }

        [TestClass]
        public class GetFileTypeFromExtention_TestClass
        {
            [TestMethod]
            public void GetFileTypeFromExtention_IsNull()
            {
                Assert.AreEqual("None", UploadedDocumentBll.GetFileTypeFromExtention(null),
                    "FILE_TYPE_PARSING_FAILE: File type null did not return \"None\"");
            }

            [TestMethod]
            public void GetFileTypeFromExtention_IsEmpty()
            {
                Assert.AreEqual("None", UploadedDocumentBll.GetFileTypeFromExtention(""),
                    "FILE_TYPE_PARSING_FAILE: File type \"\" did not return \"None\"");
            }

            [TestMethod]
            public void GetFileTypeFromExtention_IsWhitespace()
            {
                Assert.AreEqual("None", UploadedDocumentBll.GetFileTypeFromExtention("   "),
                    "FILE_TYPE_PARSING_FAILE: File type \"   \" did not return \"None\"");
            }

            [TestMethod]
            public void GetFileTypeFromExtention_ValidFileType()
            {
                Assert.AreEqual("Bitmap Image", UploadedDocumentBll.GetFileTypeFromExtention("bmp"),
                    "FILE_TYPE_PARSING_FAILE: File type \"bmp\" did not return \"Bitmap Image\"");

                Assert.AreEqual("Microsoft Word Document", UploadedDocumentBll.GetFileTypeFromExtention("doc"),
                    "FILE_TYPE_PARSING_FAILE: File type \"doc\" did not return \"Microsoft Word Document\"");

                Assert.AreEqual("Microsoft Word Document", UploadedDocumentBll.GetFileTypeFromExtention("docx"),
                    "FILE_TYPE_PARSING_FAILE: File type \"docx\" did not return \"Microsoft Word Document\"");

                Assert.AreEqual("JPEG Image", UploadedDocumentBll.GetFileTypeFromExtention("jpg"),
                    "FILE_TYPE_PARSING_FAILE: File type \"jpg\" did not return \"JPEG Image\"");

                Assert.AreEqual("JPEG Image", UploadedDocumentBll.GetFileTypeFromExtention("jpeg"),
                    "FILE_TYPE_PARSING_FAILE: File type \"jpeg\" did not return \"JPEG Image\"");

                Assert.AreEqual("Open Document Spreadsheet", UploadedDocumentBll.GetFileTypeFromExtention("ods"),
                    "FILE_TYPE_PARSING_FAILE: File type \"ods\" did not return \"Open Document Spreadsheet\"");

                Assert.AreEqual("Open Document Text Document", UploadedDocumentBll.GetFileTypeFromExtention("odt"),
                    "FILE_TYPE_PARSING_FAILE: File type \"odt\" did not return \"Open Document Text Document\"");

                Assert.AreEqual("PDF Document", UploadedDocumentBll.GetFileTypeFromExtention("pdf"),
                    "FILE_TYPE_PARSING_FAILE: File type \"pdf\" did not return \"PDF Document\"");

                Assert.AreEqual("PNG Image", UploadedDocumentBll.GetFileTypeFromExtention("png"),
                    "FILE_TYPE_PARSING_FAILE: File type \"png\" did not return \"PNG Image\"");

                Assert.AreEqual("Real Text File", UploadedDocumentBll.GetFileTypeFromExtention("rtf"),
                    "FILE_TYPE_PARSING_FAILE: File type \"rtf\" did not return \"Real Text File\"");

                Assert.AreEqual("Tiff Image", UploadedDocumentBll.GetFileTypeFromExtention("tif"),
                    "FILE_TYPE_PARSING_FAILE: File type \"tif\" did not return \"Tiff Image\"");

                Assert.AreEqual("Tiff Image", UploadedDocumentBll.GetFileTypeFromExtention("tiff"),
                    "FILE_TYPE_PARSING_FAILE: File type \"tiff\" did not return \"Tiff Image\"");

                Assert.AreEqual("Plain Text File", UploadedDocumentBll.GetFileTypeFromExtention("txt"),
                    "FILE_TYPE_PARSING_FAILE: File type \"txt\" did not return \"Plain Text File\"");

                Assert.AreEqual("Microsoft Excel Spreadsheet", UploadedDocumentBll.GetFileTypeFromExtention("xls"),
                    "FILE_TYPE_PARSING_FAILE: File type \"xls\" did not return \"Microsoft Excel Speadsheet\"");

                Assert.AreEqual("Microsoft Excel Spreadsheet", UploadedDocumentBll.GetFileTypeFromExtention("xlsx"),
                    "FILE_TYPE_PARSING_FAILE: File type \"xlsx\" did not return \"Microsoft Excel Speadsheet\"");

                Assert.AreEqual("Unknown", UploadedDocumentBll.GetFileTypeFromExtention("flac"),
                    "FILE_TYPE_PARSING_FAILE: File type \"flac\" did not return \"Unknown\"");
            }
        }

        private static void CreateFile(string name, int size)
        {
            File.WriteAllBytes(name, new byte[size]);
        }
    }
}