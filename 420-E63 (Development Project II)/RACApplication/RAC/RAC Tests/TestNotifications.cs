//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using RAC.BusinessLogic;
//using RAC.RACModels;
//using RAC_Tests.Factories;

//namespace RAC_Tests {
//    [TestClass]
//    public class TestNotifications
//    {
       
        

//        [TestMethod]
//        public void TestSendNotificationStartRACRequest()
//        {
//            var candidate = new Candidate();
//            RACNotification notification = new RACNotification();
//            RACNotification fromDB = null;
//            try
//            {
//                candidate = CreateObjectForTest();
//                notification = new RACNotification();
//                notification.CandidateId = candidate.Id;
//                notification.Id = -1;
//                notification.Time = DateTime.Now;
//                notification.NotificationType = (short) Messages.NEW_RAC_STARTED;

//                DbContext.Context.RACNotification.Add(notification);
//                DbContext.Context.SaveChanges();

//                fromDB =
//                    DbContext.Context.RACNotification.FirstOrDefault(x => x.Id == notification.Id);
//                Assert.AreEqual(notification.Id, fromDB.Id,
//                    "Test failed: Notification id is incorrect. Expected: " + notification.Id + " Actual: " +
//                    fromDB.Id);
//            }
//            catch (Exception e)
//            {
//                Assert.Fail(e.Message);
//            }
//            finally
//            {
//                Dispose(candidate);
//            }

//        }

//        [TestMethod]
//        public void TestSendNotificationSubmittedRACRequest()
//        {
//            var candidate = new Candidate();
//            RACNotification notification = new RACNotification();
//            RACNotification fromDB = null;
            
//            try
//            {
//                candidate = CreateObjectForTest();
//                notification = new RACNotification();
//                notification.CandidateId = candidate.Id;
//                notification.Id = -1;
//                notification.Time = DateTime.Now;
//                notification.NotificationType = (short)Messages.RAC_REQUEST_SUBMITTED;

//                DbContext.Context.RACNotification.Add(notification);
//                DbContext.Context.SaveChanges();

//                fromDB =
//                    DbContext.Context.RACNotification.FirstOrDefault(x => x.Id == notification.Id);
//                Assert.AreEqual(notification.Id, fromDB.Id,
//                    "Test failed: Notification id is incorrect. Expected: " + notification.Id + " Actual: " +
//                    fromDB.Id);
//            }
//            catch (Exception e)
//            {
//                Assert.Fail(e.Message);
//            }
//            finally
//            {
//                Dispose(candidate);
//            }
//        }

//        [TestMethod]
//        public void TestSendNotificationChangeProgram()
//        {
//            var candidate = new Candidate();
//            RACNotification notification = new RACNotification();
//            RACNotification fromDB = null;
//            try
//            {
//                candidate = CreateObjectForTest();
//                notification = new RACNotification();
//                notification.CandidateId = candidate.Id;
//                notification.Id = -1;
//                notification.Time = DateTime.Now;
//                notification.NotificationType = (short)Messages.RAC_PROGRAM_CHANGE;

//                DbContext.Context.RACNotification.Add(notification);
//                DbContext.Context.SaveChanges();

//                fromDB =
//                    DbContext.Context.RACNotification.FirstOrDefault(x => x.Id == notification.Id);
//                Assert.AreEqual(notification.Id, fromDB.Id,
//                    "Test failed: Notification id is incorrect. Expected: " + notification.Id + " Actual: " +
//                    fromDB.Id);
//            }
//            catch (Exception e)
//            {
//                Assert.Fail(e.Message);
//            }
//            finally
//            {
//                Dispose(candidate);
//            }
//        }

//        [TestMethod]
//        public void TestSendNotificationDeleteAccount()
//        {
//            var candidate = new Candidate();
//            RACNotification notification = new RACNotification();
//            try
//            {
//                candidate = CreateObjectForTest();
//                notification = new RACNotification();
//                notification.CandidateId = candidate.Id;
//                notification.Id = -1;
//                notification.Time = DateTime.Now;
//                notification.NotificationType = (short)Messages.CANDIDATE_ACCOUNT_DELETED;

//                DbContext.Context.RACNotification.Add(notification);
//                DbContext.Context.SaveChanges();

//                RACNotification fromDB =
//                    DbContext.Context.RACNotification.FirstOrDefault(x => x.Id == notification.Id);
//                Assert.AreEqual(notification.Id, fromDB.Id,
//                    "Test failed: Notification id is incorrect. Expected: " + notification.Id + " Actual: " +
//                    fromDB.Id);
//            }
//            catch (Exception e)
//            {
//                Assert.Fail(e.Message);
//            }
//            finally
//            {
//                Dispose(candidate);
//            }
//        }

//        public static Candidate CreateObjectForTest()
//        {
//            var c = new Candidate
//            {
//                FirstName = "Andrew",
//                LastName = "Ha",
//                Email = "Test@Email.com",
//                HomePhone = "123-123-1234",
//                WorkPhone = null,
//                UserType = (int)userType.Candidate,
//                City = "England",
//                Street = "Street",
//                Country = "Canada",
//                DateRegistered = DateTime.Now,
//                Province = CandidateBLL.GetProvinceEnumString("QC"),
//                PreferredMethodOfContact = (int) PreferredMethodOfContact.Email,
//                IsArchived = false,
//                RegistrationIP = "127.0.0.1",
//                IsDeleted = false,
//                IsPrivacyPolicyAccepted = true
//            };

//            //Going to database to get latest Program
//            int intId = DbContext.Context.Program.Max(u => u.Id);
//            ProgramBLL p = new ProgramBLL();
//            c.RACRequest = new RACRequest
//            {
//                Id = -1,
//                StartDate = DateTime.Now,
//                Status = (int)RACStatus.Started,
//                ProgramId = intId,
//                UploadedDocuments = null,
//                IsGenEdOnly = false,
//                Program = ProgramBLL.GetProgramById(intId)
//            };

//            DbContext.Context.User.Add(c);
//            DbContext.Context.SaveChanges();

//            return c;
//        }

//        public static bool Dispose(Candidate candidate)
//        {
//            /*
//             * Removes the given candidate from the database.
//             *
//             * Parameters:
//             * candidate - The candidate object to be disposed of. This candidate should of been made with the
//             * `Construct` method within this Class.
//             *
//             * Returns:
//             * A boolean representing if there was an error during the removal of the document.
//             */

//            try
//            {
//                var ListOfNotifications =
//                    DbContext.Context.RACNotification.Where(x => x.CandidateId == candidate.Id);

//                foreach (var notification in ListOfNotifications)
//                {
//                    DbContext.Context.RACNotification.Remove(notification);
//                }

//                var cand = (Candidate)DbContext.Context.User.First(c => c.Email == candidate.Email);

//                foreach (RACRequestCompetency rrc in cand.RACRequest.RACRequestCompetencies)
//                {
//                    DbContext.Context.CompetencyComment.RemoveRange(rrc.CompetencyComments);
//                    DbContext.Context.RACRequestCompetencyElement.RemoveRange(rrc.RACRequestCompetencyElements);
//                }

//                DbContext.Context.RACRequestCompetency.RemoveRange(cand.RACRequest.RACRequestCompetencies);
//                DbContext.Context.RACRequest.Remove(cand.RACRequest);
//                DbContext.Context.User.Remove(cand);
//                DbContext.Context.SaveChanges();

//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }


//    }
//}
