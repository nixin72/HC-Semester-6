using System;
using System.Diagnostics;
using RAC.BusinessLogic;
using RAC.RACModels;

namespace RAC_Tests.Factories
{
    internal static class UploadDocumentTestFactory
    {
        /*
         * UploadDocumentFactory is used to construct an UploadedDocument object (including a RAC Request, Program,
         * Candidate) that is to be used for testing purposes inside `UploadDocuments_Test.cs`. This removes a ton
         * of boilerplate code that would have to occur in order for tests to be conducted properly. This class also
         * handles adding, and deleting dummy objects from the database. This ensures nothing is left behind after
         * testing.
         */

        public static UploadedDocument Construct(string documentName, int documentByteSize, bool isForDb = false)
        {
            /*
             * Creates an UploadedDocument object based on the passed in parameters, then immediatly pushes the data
             * into the database. Doesn't use `UploadedDocumentBll` to avoid errors in the buisness logic, and to avoid
             * circular testing. Creates default objects for Candidate, Program, and RACRequest.
             *
             * Parameters:
             * documentName - A string representing the name of the document, including the file extention.
             * documentByteSize - The size of the document in bytes.
             * isForDb - Default false. If true, pushes to the database. False, just returns the object.
             *
             * Returns:
             * The `UploadedDocument` object that has been pushed into the database. `null` is returned if an error
             * occurs.
             */

            var candidate = new Candidate
            {
                Id = 999999,
                City = "Gatineau",
                Country = "Canada",
                Email = "testUser@rac.ca",
                FirstName = "Morgan",
                LastName = "Wavy",
                HomePhone = "8195557689",
                UserType = 1,

            };

            // This grabs the "Microsoft Networks and Security Administrator" program
            Program program = DbContext.Context.Program.Find(2);

            var racRequest = new RACRequest
            {
                Id = 999999,
                StartDate = DateTime.Now,
                IsGenEdOnly = false,
                Candidate = candidate,
                ProgramId = program.Id,
                Program = program
            };

            var uploadedDocument = new UploadedDocument
            {
                Binary = new byte[documentByteSize],
                Id = 999999,
                Name = documentName,
                RACRequestId = racRequest.Id,
                RACRequest = racRequest
            };

            if (isForDb)
            {
                DbContext.Context.UploadedDocument.Add(uploadedDocument);
                DbContext.Context.SaveChanges();
            }

            return uploadedDocument;
        }

        public static bool Dispose(UploadedDocument uploadedDocument)
        {
            /*
             * Removes the given uploaded document from the database, as well as it's associated Candidate, and RAC
             * Request (the program is taken from the actual database, **must** not dispose.
             *
             * Parameters:
             * uploadedDocument - The document object to be disposed of. This document should of been made with the
             * `Construct` method within this Class.
             *
             * Returns:
             * A boolean representing if there was an error during the removal of the document.
             */

            RACRequest racRequest = uploadedDocument.RACRequest;
            Candidate candidate = racRequest.Candidate;

            try
            {
                DbContext.Context.UploadedDocument.Remove(uploadedDocument);
                DbContext.Context.RACRequest.Remove(racRequest);
                DbContext.Context.User.Remove(candidate);
                DbContext.Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Dispose(RACRequest racRequest)
        {
            /*
             * Removes the given RAC Request, and Candidate from the database. This is used over the _other_
             * Dispose moethod when the document has been deleted without deleting the Request/Candidate.
             *
             * See also:
             * UploadDocuments_Test.RemoveDocuments_Success()
             *
             * Parameters:
             * racRequest - The RAC Request that owns the document.
             *
             */
            Candidate candidate = racRequest.Candidate;

            try
            {
                DbContext.Context.RACRequest.Remove(racRequest);
                DbContext.Context.User.Remove(candidate);
                DbContext.Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

