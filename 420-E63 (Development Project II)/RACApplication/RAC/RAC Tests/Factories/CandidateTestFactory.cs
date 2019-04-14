using System;
using RAC.BusinessLogic;
using RAC.RACModels;

namespace RAC_Tests.Factories
{
    internal static class CandidateTestFactory
    {
        /*
        * CandidateTestFactory is used to construct a Candidate object
        * that is to be used for testing purposes inside `Candidate.cs`. This removes a ton
        * of boilerplate code that would have to occur in order for tests to be conducted properly. This class also
        * handles adding, and deleting dummy objects from the database. This ensures nothing is left behind after
        * testing.
        */

        public static RACUser Construct( string email = "testUser@rac.ca", string firstName = "Morgan",
            string lastName = "Wavy", string password = "p@ssw0rd", bool isForDb = false)
        {
            /*
             * Creates a Candidate object based on the passed in parameters, then immediatly pushes the data
             * into the database. Doesn't use `CandidateBLL` to avoid errors in the buisness logic, and to avoid
             * circular testing. Creates default objects for Candidate.
             *
             * Parameters:
             * firstName - Default Morgan. The candidates first name.
             * lastName - Default Wavy. The candidates last name.
             * isForDb - Default false. If true, pushes to the database. False, just returns the object.
             *
             * Returns:
             * The `Candidate` object that has been pushed into the database. `null` is returned if an error
             * occurs.
             */

            var user = new RACUser
            {
                Id = 999999,
                City = "Gatineau",
                Country = "Canada",
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                HomePhone = "8195557689",
                UserType = 1,
                DateRegistered = DateTime.Now,
                PreferredMethodOfContact = 0,
                IsArchived = false,
                IsDeleted = false,
                IsPrivacyPolicyAccepted = true,
                RegistrationIP = "127.0.0.1"
            };

            Program program = DbContext.Context.Program.Find(2);

            var racRequest = new RACRequest
            {
                Id = 999999,
                StartDate = DateTime.Now,
                IsGenEdOnly = false,
                Candidate = null,
                ProgramId = program.Id,
                Program = program
            };

            if (isForDb)
            {
                DbContext.Context.RACRequest.Add(racRequest);
                DbContext.Context.SaveChanges();
            }

            return null;
        }

        public static bool Dispose(RACUser candidate)
        {
            /*
             * Removes the given candidate from the database.
             *
             * Parameters:
             * candidate - The candidate object to be disposed of. This candidate should of been made with the
             * `Construct` method within this Class.
             *
             * Returns:
             * A boolean representing if there was an error during the removal of the document.
             */

            //try
            //{
            //    var cand = (Candidate) DbContext.Context.User.First(c => c.Email == candidate.Email);

            //    foreach (RACRequestCompetency rrc in cand.RACRequest.RACRequestCompetencies)
            //    {
            //        DbContext.Context.CompetencyComment.RemoveRange(rrc.CompetencyComments);
            //        DbContext.Context.RACRequestCompetencyElement.RemoveRange(rrc.RACRequestCompetencyElements);
            //    }

            //    DbContext.Context.RACRequestCompetency.RemoveRange(cand.RACRequest.RACRequestCompetencies);
            //    DbContext.Context.RACRequest.Remove(cand.RACRequest);
            //    DbContext.Context.User.Remove(cand);
            //    DbContext.Context.SaveChanges();

            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return false;
        }
    }
}
