using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RAC.BusinessLogic;
using RAC.RACModels;

// ReSharper disable InconsistentNaming

namespace RAC_Tests
{
    [TestClass]
    public class DocumentList_Tests
    {

        [TestMethod]
        public void SaveDocumentsLocal_ExistingFile()
        {
            /*
             * Setup
             */
            cleanDocumentList();
            CreateFile("file10.txt", 666);
            CreateFile("file20.txt", 667);
            CreateFile("file30.txt", 668);

            // Document 1
            var fs1 = new FileStream("file10.txt", FileMode.Open, FileAccess.Read);
            string filename1 = Path.GetFileName(fs1.Name);

            // Document 2
            var fs2 = new FileStream("file20.txt", FileMode.Open, FileAccess.Read);
            string filename2 = Path.GetFileName(fs2.Name);

            // Document 3
            var fs3 = new FileStream("file30.txt", FileMode.Open, FileAccess.Read);
            string filename3 = Path.GetFileName(fs3.Name);

            var testRAC = new RACRequest { Id = 999 };

            /*
             * Expected Values
             */

            var expectedDocumentListSize = 3;

            var expectedFile1Name = "file10.txt";
            var expectedFile2Name = "file20.txt";
            var expectedFile3Name = "file30.txt";

            var expectedFile1Size = 666;
            var expectedFile2Size = 667;
            var expectedFile3Size = 668;

            var expectedCode1 = ERRORS.SUCCESS;
            var expectedCode2 = ERRORS.SUCCESS;
            var expectedCode3 = ERRORS.SUCCESS;

            /*
             * Actual Values
             */
            ERRORS actualCode1 = DocumentList.Instance.SaveDocumentsLocal(filename1, fs1, (int)fs1.Length, testRAC, false);
            ERRORS actualCode2 = DocumentList.Instance.SaveDocumentsLocal(filename2, fs2, (int)fs2.Length, testRAC, false);
            ERRORS actualCode3 = DocumentList.Instance.SaveDocumentsLocal(filename3, fs3, (int)fs3.Length, testRAC, false);

            List<UploadedDocument> actualDocumetList = DocumentList.Instance.GetDocumentList();
            int actualDocumentListSize = actualDocumetList.Count;

            string actualFile1Name = actualDocumetList.ElementAt(0).Name;
            string actualFile2Name = actualDocumetList.ElementAt(1).Name;
            string actualFile3Name = actualDocumetList.ElementAt(2).Name;

            int actualFile1Size = actualDocumetList.ElementAt(0).Binary.Length;
            int actualFile2Size = actualDocumetList.ElementAt(1).Binary.Length;
            int actualFile3Size = actualDocumetList.ElementAt(2).Binary.Length;

            /*
             * Testing
             */
            Assert.AreEqual(expectedDocumentListSize, actualDocumentListSize,
                "GetDocumentsGetDocuments_NonEmptyList: List Size");

            Assert.AreEqual(expectedFile1Name, actualFile1Name, "GetDocumentsGetDocuments_NonEmptyList: File1 Name");
            Assert.AreEqual(expectedFile1Size, actualFile1Size, "GetDocumentsGetDocuments_NonEmptyList: File1 Size");
            Assert.AreEqual(expectedCode1, actualCode1, "GetDocumentsGetDocuments_NonEmptyList: Code1");

            Assert.AreEqual(expectedFile2Name, actualFile2Name, "GetDocumentsGetDocuments_NonEmptyList: File2 Name");
            Assert.AreEqual(expectedFile2Size, actualFile2Size, "GetDocumentsGetDocuments_NonEmptyList: File2 Size");
            Assert.AreEqual(expectedCode2, actualCode2, "GetDocumentsGetDocuments_NonEmptyList: Code2");

            Assert.AreEqual(expectedFile3Name, actualFile3Name, "GetDocumentsGetDocuments_NonEmptyList: File3 Name");
            Assert.AreEqual(expectedFile3Size, actualFile3Size, "GetDocumentsGetDocuments_NonEmptyList: File3 Size");
            Assert.AreEqual(expectedCode3, actualCode3, "GetDocumentsGetDocuments_NonEmptyList: Code3");

        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SaveDocumentsLocal_NonExistingFile()
        {
            /*
             * Setup
             */

            // Document 1
            var fs1 = new FileStream("file50.txt", FileMode.Open, FileAccess.Read);
            string filename1 = Path.GetFileName(fs1.Name);

            // Document 2
            var fs2 = new FileStream("file60.txt", FileMode.Open, FileAccess.Read);
            string filename2 = Path.GetFileName(fs2.Name);

            // Document 3
            var fs3 = new FileStream("file70.txt", FileMode.Open, FileAccess.Read);
            string filename3 = Path.GetFileName(fs3.Name);

            var testRAC = new RACRequest { Id = 999 };
            DocumentList.Instance.SaveDocumentsLocal(filename1, fs1, (int)fs1.Length, testRAC, false);
            DocumentList.Instance.SaveDocumentsLocal(filename2, fs2, (int)fs2.Length, testRAC, false);
            DocumentList.Instance.SaveDocumentsLocal(filename3, fs3, (int)fs3.Length, testRAC, false);

            /*
             * Expected Values
             */

            var expectedDocumentListSize = 0;

            /*
             * Actual Values
             */
            List<UploadedDocument> actualDocumetList = DocumentList.Instance.GetDocumentList();
            int actualDocumentListSize = actualDocumetList.Count;

            /*
             * Testing
             */
            Assert.AreEqual(expectedDocumentListSize, actualDocumentListSize,
                "GetDocumentsGetDocuments_NonEmptyList: List Size");

            /*
             * cleanup
             */

            for (int i = 0; i < actualDocumetList.Count; i++)
            {
                DocumentList.Instance.RemoveDocumentsLocal(i);
            }
        }

        [TestMethod]
        public void RemoveDocumentsLocal_NonEmptyList()
        {
            /*
             * Setup
             */
            CreateFile("file80.txt", 666);
            CreateFile("file90.txt", 667);
            CreateFile("file100.txt", 668);

            // Document 1
            var fs1 = new FileStream("file80.txt", FileMode.Open, FileAccess.Read);
            string filename1 = Path.GetFileName(fs1.Name);

            // Document 2
            var fs2 = new FileStream("file90.txt", FileMode.Open, FileAccess.Read);
            string filename2 = Path.GetFileName(fs2.Name);

            // Document 3
            var fs3 = new FileStream("file100.txt", FileMode.Open, FileAccess.Read);
            string filename3 = Path.GetFileName(fs3.Name);

            var testRAC = new RACRequest { Id = 999 };

            DocumentList.Instance.SaveDocumentsLocal(filename1, fs1, (int)fs1.Length, testRAC, false);
            DocumentList.Instance.SaveDocumentsLocal(filename2, fs2, (int)fs2.Length, testRAC, false);
            DocumentList.Instance.SaveDocumentsLocal(filename3, fs3, (int)fs3.Length, testRAC, false);

            /*
             * Expected Values
             */

            var expectedDocumentListSize = 2;

            var expectedFile1Name = "file80.txt";
            var expectedFile2Name = "file90.txt";

            var expectedFile1Size = 666;
            var expectedFile2Size = 667;

            var expectedCode = ERRORS.SUCCESS;

            /*
             * Actual Values
             */

            ERRORS actualCode = DocumentList.Instance.RemoveDocumentsLocal(2);
            List<UploadedDocument> actualDocumetList = DocumentList.Instance.GetDocumentList();
            int actualDocumentListSize = actualDocumetList.Count;

            string actualFile1Name = actualDocumetList.ElementAt(0).Name;
            string actualFile2Name = actualDocumetList.ElementAt(1).Name;

            int actualFile1Size = actualDocumetList.ElementAt(0).Binary.Length;
            int actualFile2Size = actualDocumetList.ElementAt(1).Binary.Length;

            /*
             * Testing
             */
            Assert.AreEqual(expectedDocumentListSize, actualDocumentListSize,
                "RemoveDocumentsLocal_NonEmptyList: List Size");
            Assert.AreEqual(expectedCode, actualCode, "RemoveDocumentsLocal_NonEmptyList: Removal Code");
            Assert.AreEqual(expectedFile1Name, actualFile1Name, "RemoveDocumentsLocal_NonEmptyList: File1 Name");
            Assert.AreEqual(expectedFile1Size, actualFile1Size, "RemoveDocumentsLocal_NonEmptyList: File1 Size");

            Assert.AreEqual(expectedFile2Name, actualFile2Name, "RemoveDocumentsLocal_NonEmptyList: File2 Name");
            Assert.AreEqual(expectedFile2Size, actualFile2Size, "RemoveDocumentsLocal_NonEmptyList: File2 Size");
        }

        [TestMethod]
        public void RemoveDocumentsLocal_IndexOutOfBounds()
        {
            /*
             * Setup
             */
            cleanDocumentList();
            CreateFile("file1.txt", 666);
            CreateFile("file2.txt", 667);

            // Document 1
            var fs1 = new FileStream("file1.txt", FileMode.Open, FileAccess.Read);
            string filename1 = Path.GetFileName(fs1.Name);

            // Document 2
            var fs2 = new FileStream("file2.txt", FileMode.Open, FileAccess.Read);
            string filename2 = Path.GetFileName(fs2.Name);

            var testRAC = new RACRequest { Id = 999 };

            DocumentList.Instance.SaveDocumentsLocal(filename1, fs1, (int)fs1.Length, testRAC, false);
            DocumentList.Instance.SaveDocumentsLocal(filename2, fs2, (int)fs2.Length, testRAC, false);

            /*
             * Expected Values
             */

            var expectedDocumentListSize = 2;

            var expectedFile1Name = "file1.txt";
            var expectedFile2Name = "file2.txt";

            var expectedFile1Size = 666;
            var expectedFile2Size = 667;

            var expectedCode = ERRORS.DOCUMENT_REMOVAL_FAIL;

            /*
             * Actual Values
             */

            ERRORS actualCode = DocumentList.Instance.RemoveDocumentsLocal(4);

            List<UploadedDocument> actualDocumetList = DocumentList.Instance.GetDocumentList();
            int actualDocumentListSize = actualDocumetList.Count;

            string actualFile1Name = actualDocumetList.ElementAt(0).Name;
            string actualFile2Name = actualDocumetList.ElementAt(1).Name;

            int actualFile1Size = actualDocumetList.ElementAt(0).Binary.Length;
            int actualFile2Size = actualDocumetList.ElementAt(1).Binary.Length;

            /*
             * Testing
             */
            Assert.AreEqual(expectedDocumentListSize, actualDocumentListSize,
                "RemoveDocumentsLocal_NonEmptyList: List Size");

            Assert.AreEqual(expectedCode, actualCode, "RemoveDocumentsLocal_NonEmptyList: Removal Code");

            Assert.AreEqual(expectedFile1Name, actualFile1Name, "RemoveDocumentsLocal_NonEmptyList: File1 Name");
            Assert.AreEqual(expectedFile1Size, actualFile1Size, "RemoveDocumentsLocal_NonEmptyList: File1 Size");

            Assert.AreEqual(expectedFile2Name, actualFile2Name, "RemoveDocumentsLocal_NonEmptyList: File2 Name");
            Assert.AreEqual(expectedFile2Size, actualFile2Size, "RemoveDocumentsLocal_NonEmptyList: File2 Size");
        }

        [TestMethod]
        public void RemoveDocumentsLocal_NoFiles()
        {
            /*
             * Expected Values
             */
            cleanDocumentList();
            var expectedDocumentListSize = 0;

            var expectedCode = ERRORS.DOCUMENT_REMOVAL_FAIL;

            /*
             * Actual Values
             */

            ERRORS actualCode = DocumentList.Instance.RemoveDocumentsLocal(0);

            List<UploadedDocument> actualDocumetList = DocumentList.Instance.GetDocumentList();
            int actualDocumentListSize = actualDocumetList.Count;

            /*
             * Testing
             */
            Assert.AreEqual(expectedDocumentListSize, actualDocumentListSize,
                "RemoveDocumentsLocal_NonEmptyList: List Size");

            Assert.AreEqual(expectedCode, actualCode, "RemoveDocumentsLocal_NonEmptyList: Removal Code");
        }

        [TestMethod]
        public void SaveChangesToDB_RACWithFiles()
        {
            ///*
            // * Setup
            // */
            //cleanDocumentList();
            //Candidate user = getTestUser();
            //CreateFile("file5.txt", 666);
            //CreateFile("file6.txt", 667);
            //CreateFile("file7.txt", 668);

            //// Document 1
            //var fs1 = new FileStream("file5.txt", FileMode.Open, FileAccess.Read);
            //string filename1 = Path.GetFileName(fs1.Name);
            //var br1 = new BinaryReader(fs1);
            //UploadedDocument file1 =
            //    UploadedDocumentBll.LoadDocumentFromDisk(filename1,
            //        br1.ReadBytes((int)fs1.Length), new RACRequest
            //        {
            //            Id = 9999
            //        }, false);

            //// Document 2
            //var fs2 = new FileStream("file6.txt", FileMode.Open, FileAccess.Read);
            //string filename2 = Path.GetFileName(fs2.Name);
            //var br2 = new BinaryReader(fs2);
            //UploadedDocument file2 =
            //    UploadedDocumentBll.LoadDocumentFromDisk(filename2,
            //        br2.ReadBytes((int)fs2.Length), new RACRequest
            //        {
            //            Id = 9999
            //        }, false);

            //// Document 3
            //var fs3 = new FileStream("file7.txt", FileMode.Open, FileAccess.Read);
            //string filename3 = Path.GetFileName(fs3.Name);
            //var br3 = new BinaryReader(fs3);
            //UploadedDocument file3 =
            //    UploadedDocumentBll.LoadDocumentFromDisk(filename3,
            //        br3.ReadBytes((int)fs3.Length), new RACRequest
            //        {
            //            Id = 9999
            //        }, false);

            //DbContext.Context.User.Add(user);
            //DbContext.Context.SaveChanges();
            //user.RACRequest.UploadedDocuments = new List<UploadedDocument>();
            //user.RACRequest.UploadedDocuments.Add(file1);
            //user.RACRequest.UploadedDocuments.Add(file2);
            //user.RACRequest.UploadedDocuments.Add(file3);

            ///*
            // * Expected Values
            // */

            //var expectedDocumentListSize = 3;

            //var expectedFile1Name = "file5.txt";
            //var expectedFile2Name = "file6.txt";
            //var expectedFile3Name = "file7.txt";

            //var expectedFile1Size = 666;
            //var expectedFile2Size = 667;
            //var expectedFile3Size = 668;

            ///*
            // * Actual Values
            // */
            //DocumentList.Instance.SaveChangesToDb(user.RACRequest);
            //int id = DbContext.Context.User.Max(u => u.Id);
            //List<UploadedDocument> actualDocumetList =
            //    ((Candidate)DbContext.Context.User.Where(u => u.Id == id).FirstOrDefault()).RACRequest
            //    .UploadedDocuments.ToList();

            //int actualDocumentListSize = actualDocumetList.Count;

            //string actualFile1Name = actualDocumetList.ElementAt(0).Name;
            //string actualFile2Name = actualDocumetList.ElementAt(1).Name;
            //string actualFile3Name = actualDocumetList.ElementAt(2).Name;

            //int actualFile1Size = actualDocumetList.ElementAt(0).Binary.Length;
            //int actualFile2Size = actualDocumetList.ElementAt(1).Binary.Length;
            //int actualFile3Size = actualDocumetList.ElementAt(2).Binary.Length;

            ///*
            // * Testing
            // */
            //Assert.AreEqual(expectedDocumentListSize, actualDocumentListSize,
            //    "GetDocumentsGetDocuments_NonEmptyList: List Size");

            //Assert.AreEqual(expectedFile1Name, actualFile1Name, "GetDocumentsGetDocuments_NonEmptyList: File1 Name");
            //Assert.AreEqual(expectedFile1Size, actualFile1Size, "GetDocumentsGetDocuments_NonEmptyList: File1 Size");

            //Assert.AreEqual(expectedFile2Name, actualFile2Name, "GetDocumentsGetDocuments_NonEmptyList: File2 Name");
            //Assert.AreEqual(expectedFile2Size, actualFile2Size, "GetDocumentsGetDocuments_NonEmptyList: File2 Size");

            //Assert.AreEqual(expectedFile3Name, actualFile3Name, "GetDocumentsGetDocuments_NonEmptyList: File3 Name");
            //Assert.AreEqual(expectedFile3Size, actualFile3Size, "GetDocumentsGetDocuments_NonEmptyList: File3 Size");

            ///*
            // * Cleanup
            // */

            //Factories.UploadDocumentTestFactory.Dispose(file1);
            //Factories.UploadDocumentTestFactory.Dispose(file2);
            //Factories.UploadDocumentTestFactory.Dispose(file3);
            //Factories.CandidateTestFactory.Dispose(user);
        }

        public Candidate getTestUser()
        {
            //var p = new ProgramBLL();
            //var RACProgramId = 4;

            //var can = new Candidate
            //{
            //    FirstName = "Cody",
            //    LastName = "Berube",
            //    Email = "test@gmail.com",
            //    HomePhone = "8197710091",
            //    WorkPhone = "8197710091",
            //    UserType = 1,
            //    Street = "36 Rue What",
            //    City = "Gatineau",
            //    Province = provinces.QC.ToString(),
            //    Country = "Canada",
            //    PreferredMethodOfContact = 1,
            //    IsArchived = false,
            //    IsDeleted = false,
            //    IsPrivacyPolicyAccepted = true,
            //    DateRegistered = DateTime.Now,
            //    RegistrationIP = "1.1.1.1"
            //};

            //can.RACRequest = new RACRequest
            //{
            //    StartDate = DateTime.Now,
            //    SubmissionDate = DateTime.Now,
            //    Status = (int)RACStatus.Started,
            //    ProgramId = RACProgramId,
            //    UploadedDocuments = null,
            //    IsGenEdOnly = false,
            //    Program = ProgramBLL.GetProgramById(RACProgramId)
            //};
            return null;
        }

        private static void CreateFile(string name, int size)
        {
            File.WriteAllBytes(name, new byte[size]);
        }

        public void cleanDocumentList()
        {
            /*
             * cleanup
             */

            while (DocumentList.Instance.GetDocumentList().Count != 0)
            {
                DocumentList.Instance.RemoveDocumentsLocal(0);
            }
        }
    }
}