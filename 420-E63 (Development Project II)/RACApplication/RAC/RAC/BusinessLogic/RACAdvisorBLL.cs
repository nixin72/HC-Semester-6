using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAC.RACModels;

namespace RAC.BusinessLogic
{
    public static class RACAdvisorBLL
    {
        public static List<NotificationsViewModel> GetNotifications(int userT)
        {
            try
            {
                List<RACNotification> notifications =
                    DbContext.Context.RACNotification.Where(n => n.UserType == userT).ToList();
                {
                    List<NotificationsViewModel> notificationsVm = MapNotificationsToViewModels(notifications);
                    return notificationsVm;
                }
            }
            catch
            {
                return new List<NotificationsViewModel>();
            }
        }

        public static bool CheckIsRACAdvisor()
        {
            var user = (RACUser) HttpContext.Current.Session["User"];
            var isRACAdvisor = false;
            if (user == null)
            {
                return false;
            }
            if (user.UserType == (int)userType.RACAdvisor)
            {
                isRACAdvisor = true;
            }

            return isRACAdvisor;
        }

        private static List<NotificationsViewModel> MapNotificationsToViewModels(IEnumerable<RACNotification> notification)
        {
            var notifications = new List<NotificationsViewModel>();
            foreach (RACNotification notificatonInstance in notification)
            {
                var description = "";

                switch ((Messages) notificatonInstance.NotificationType)
                {
                    case Messages.NEW_RAC_STARTED:
                        description = "started";
                        break;
                    case Messages.RAC_REQUEST_SUBMITTED:
                        description = "completed";
                        break;
                    case Messages.RAC_PROGRAM_CHANGE:
                        description = "changed their program";
                        break;
                    case Messages.CANDIDATE_ACCOUNT_DELETED:
                        description = "deleted";
                        break;
                }


                notifications.Add(new NotificationsViewModel
                {
                    Id = notificatonInstance.Id,
                    Candidate = CandidateBLL.MapUserInformation(CandidateBLL.GetMembershipUserById(notificatonInstance.Candidate.CSAdminId)),
                    Description = description,
                    Date = notificatonInstance.Time,
                    NotificationType = notificatonInstance.NotificationType
                });
            }

            //Sorting by date now
            notifications = notifications.OrderByDescending(x => x.Date).ToList();
            return notifications;
        }

        public static RACAdvisorHomeViewModel MapToHomeViewModel(RACUser user, RACAdvisorHomeViewModel rvm)
        {
            if (user == null || rvm == null)
            {
                return null;
            }

            rvm.Id = user.Id;
            rvm.FirstName = user.FirstName;
            rvm.LastName = user.LastName;
            return rvm;
        }

        public static void SendUploadNotification(int id)
        {
            var user = (Candidate)DbContext.Context.Candidate.First(u => u.Id == id);
            Notification.CreateNotification(user.Id, Messages.RAC_ADVISOR_UPLOAD_TO_CANDIDATE, (int)userType.Candidate);
            DbContext.Context.SaveChanges();
        }
    }
}