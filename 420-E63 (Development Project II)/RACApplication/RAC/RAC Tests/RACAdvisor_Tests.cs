//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using RAC.BusinessLogic;
//using RAC.CSAdminModel;
//using RAC.RACModels;
//using RAC_Tests.Factories;

//namespace RAC_Tests
//{
//    [TestClass]
//    public class RACAdvisor_Tests
//    {
//        [TestMethod]
//        public void GetNotifications_Success()
//        {
//            Candidate candidate = CandidateTestFactory.Construct(isForDb: true);
//            Notification.CreateNotification(candidate.Id, Messages.CANDIDATE_ACCOUNT_DELETED);
//            List<NotificationsViewModel> notifications = RACAdvisorBLL.GetNotifications();

//            Assert.IsNotNull(notifications);

//            Assert.AreEqual(candidate.Id, notifications.First().Candidate.Id,
//                "INCORRECT_NOTIFICAITON: Notification contained the incorrect Candidate ID.");

//            Assert.AreEqual((short) Messages.CANDIDATE_ACCOUNT_DELETED, notifications.First().NotificationType,
//                "NOTIFICATION_TYPE_ERROR: Notification did not match.");

//            Notification.DeleteNotification(notifications.Last().Id);
//            CandidateTestFactory.Dispose(candidate);
//        }

//        [TestMethod]
//        public void MapToHomeViewModel_NullUser()
//        {
//            Assert.IsNull(RACAdvisorBLL.MapToHomeViewModel(null, new RACAdvisorHomeViewModel()),
//                "VIEW_MODEL_MAPPING_USER_NULL: MapToHomeViewModel accepted a null User.");
//        }

//        [TestMethod]
//        public void MapToHomeViewModel_NullRvm()
//        {
//            Assert.IsNull(RACAdvisorBLL.MapToHomeViewModel(new User(), null),
//                "VIEW_MODEL_MAPPING_RVM_NULL: MapToHOmeViewModel accepted a null RVM.");
//        }

//        [TestMethod]
//        public void MapToHomeViewModel_BothNull()
//        {
//            Assert.IsNull(RACAdvisorBLL.MapToHomeViewModel(null, null),
//                "VIEW_MODEL_MAPPING_NULL: MapToHOmeViewModel accepted null parameters.");
//        }

//        [TestMethod]
//        public void MapToHomeViewModel_Success()
//        {
//            var user = new User
//            {
//                FirstName = "Morgan",
//                LastName = "Wavy",
//                Email = "rac@test.com",
//                Id = 999,
//                HomePhone = "5555555555",
//                UserType = 1,
//                WorkPhone = ""
//            };

//            var expected = new RACAdvisorHomeViewModel
//            {
//                //Email = user.Email,
//                //FirstName = user.FirstName,
//                //LastName = user.LastName,
//                //HomePhone = user.HomePhone,
//                //Id = user.Id,
//                //UserType = user.UserType,
//                //WorkPhone = user.WorkPhone
//            };

//            RACAdvisorHomeViewModel actual = RACAdvisorBLL.MapToHomeViewModel(user, new RACAdvisorHomeViewModel());

//            Assert.AreEqual(expected.Id, actual.Id,
//                "VIEW_MODEL_MAPPING_FAIL: User was not properly mapped to the view model.");
//        }
//    }
//}
