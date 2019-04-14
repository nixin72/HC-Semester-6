using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RAC.BusinessLogic;
using RAC.RACModels;

using RAC_Tests.Factories;

namespace RAC_Tests
{
    [TestClass]
    public class Candidate_Test
    {
        private readonly int _programCode = RAC.BusinessLogic.DbContext.Context.Program.First(prg => prg.ProgramMinistryData.Name == "00000").Id; 

        [TestMethod]
        public void CreateCandidate_HappyPath()
        {
            Candidate candidate = CandidateTestFactory.Construct();

            try
            {
                List<ERRORS> returnCodes = CandidateBLL.Register(candidate, _programCode);

                // Expected results
                const int expectedCodeCount = 1;
                const ERRORS expectedCode = ERRORS.SUCCESS;

                // Actual Results
                ERRORS actualCode = returnCodes[0];
                int actualCodeCount = returnCodes.Count;

                Assert.AreEqual(expectedCode, actualCode);
                Assert.AreEqual(expectedCodeCount, actualCodeCount);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            finally
            {
                CandidateTestFactory.Dispose(candidate);
            }
        }

        [TestMethod]
        public void CreateCandidate_InvalidFirstName()
        {
            Candidate candidate = CandidateTestFactory.Construct(firstName: null);

            try
            {
                List<ERRORS> codes = CandidateBLL.Register(candidate, _programCode);

                // Expected results
                const int expectedCodeCount = 1;
                const ERRORS expectedCode = ERRORS.INVALID_FIRST_NAME_NULL;

                // Actual results
                ERRORS actualCode = codes[0];
                int actualCodeCount = codes.Count;

                Assert.AreEqual(expectedCode, actualCode);
                Assert.AreEqual(expectedCodeCount, actualCodeCount);
            }
            finally
            {
                CandidateTestFactory.Dispose(candidate);
            }
        }

        [TestMethod]
        public void CreateCandidate_InvalidLastName()
        {
            Candidate candidate = CandidateTestFactory.Construct(lastName: null);
            try
            {
                List<ERRORS> codes = CandidateBLL.Register(candidate, _programCode);

                // Expected results
                const int expectedCodeCount = 1;
                const ERRORS expectedCode = ERRORS.INVALID_LAST_NAME_NULL;

                // Actual results
                ERRORS actualCode = codes[0];
                int actualCodeCount = codes.Count;

                Assert.AreEqual(expectedCode, actualCode);
                Assert.AreEqual(expectedCodeCount, actualCodeCount);
            }
            finally
            {
                CandidateTestFactory.Dispose(candidate);
            }
        }

        [TestMethod]
        public void CreateCandidate_InvalidEmail_Null()
        {
            Candidate candidate = CandidateTestFactory.Construct(null);

            try
            {
                List<ERRORS> codes = CandidateBLL.Register(candidate, _programCode);

                // Expected results
                const int expectedCodeCount = 1;
                const ERRORS expectedCode = ERRORS.INVALID_EMAIL_NULL;

                // Actual results
                ERRORS actualCode = codes[0];
                int actualCodeCount = codes.Count;

                Assert.AreEqual(expectedCode, actualCode);
                Assert.AreEqual(expectedCodeCount, actualCodeCount);
            }
            finally
            {
                CandidateTestFactory.Dispose(candidate);
            }
        }

        [TestMethod]
        public void CreateCandidate_InvalidPassword()
        {
            Candidate candidate = CandidateTestFactory.Construct(password: null);
            try
            {
                List<ERRORS> codes = CandidateBLL.Register(candidate, 0);

                // Expected results
                const int expectedCodeCount = 1;
                const ERRORS expectedCode = ERRORS.INVALID_PASSWORD_NULL;

                // Actual results
                ERRORS actualCode = codes[0];
                int actualCodeCount = codes.Count;

                Assert.AreEqual(expectedCode, actualCode);
                Assert.AreEqual(expectedCodeCount, actualCodeCount);
            }
            finally
            {
                CandidateTestFactory.Dispose(candidate);
            }
        }

        [TestMethod]
        public void RetrieveCandidate_HappyPath()
        {
            Candidate candidate = CandidateTestFactory.Construct();
            try
            {
                List<ERRORS> codes = CandidateBLL.Register(candidate, _programCode);

                // Expected results
                const int expectedCodeCount = 1;
                const ERRORS expectedCode = ERRORS.SUCCESS;

                // Actual results
                ERRORS actualCode = codes[0];
                int actualCodeCount = codes.Count;
                var actualReturnedCandidate = (Candidate) CandidateBLL.GetUser(candidate.Email);

                Assert.AreEqual(expectedCode, actualCode);
                Assert.AreEqual(expectedCodeCount, actualCodeCount);
                Assert.AreEqual(candidate.Email, actualReturnedCandidate.Email);
                Assert.AreEqual(candidate.FirstName, actualReturnedCandidate.FirstName);
                Assert.AreEqual(candidate.LastName, actualReturnedCandidate.LastName);
                Assert.AreEqual(candidate.HomePhone, actualReturnedCandidate.HomePhone);
                Assert.AreEqual(candidate.Street, actualReturnedCandidate.Street);
                Assert.AreEqual(candidate.City, actualReturnedCandidate.City);
                Assert.AreEqual(candidate.Province, actualReturnedCandidate.Province);
                Assert.AreEqual(candidate.Country, actualReturnedCandidate.Country);
            }
            finally
            {
                CandidateTestFactory.Dispose(candidate);
            }
        }

        [TestMethod]
        public void RetrieveCandidate_InvalidUserAdded()
        {
            Candidate candidate = CandidateTestFactory.Construct(null, lastName: null);
            try
            {
                List<ERRORS> codes = CandidateBLL.Register(candidate, _programCode);

                // Expected results
                const int expectedCodeCount = 2;
                const ERRORS expectedCode1 = ERRORS.INVALID_LAST_NAME_NULL;
                const ERRORS expectedCode2 = ERRORS.INVALID_EMAIL_NULL;

                // Actual results
                ERRORS actualCode1 = codes[0];
                ERRORS actualCode2 = codes[1];
                int actualCodeCount = codes.Count;
                var actualReturnedCandidate = (Candidate) CandidateBLL.GetUser(candidate.Email);

                Assert.AreEqual(expectedCode1, actualCode1);
                Assert.AreEqual(expectedCode2, actualCode2);
                Assert.AreEqual(expectedCodeCount, actualCodeCount);
                Assert.AreEqual(null, actualReturnedCandidate);
            }
            finally
            {
                CandidateTestFactory.Dispose(candidate);
            }
        }

        [TestMethod]
        public void RetrieveCandidate_NonexistantUser()
        {
            Candidate candidate = CandidateTestFactory.Construct();

            try
            {
                CandidateBLL.Register(candidate, _programCode);

                // Actual results
                var actualReturnedCandidate = (Candidate) CandidateBLL.GetUser("thisisnotanemail");

                Assert.AreEqual(null, actualReturnedCandidate);
            }
            finally
            {
                CandidateTestFactory.Dispose(candidate);
            }
        }
    }
}