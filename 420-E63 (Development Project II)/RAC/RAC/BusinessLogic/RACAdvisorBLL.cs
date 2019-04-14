using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAC.Controllers;
using RAC.Models;
using RAC.RACModels;

namespace RAC.BusinessLogic
{
    public static class RACAdvisorBLL
    {
        public static List<NotificationsViewModel> GetNotifications()
        {
            List<RACNotification> notifications = DbContext.Context.RACNotification.ToList();
            {
                List<NotificationsViewModel> notificationsVm = MapNotificationsToViewModels(notifications);
                return notificationsVm;
            }
        }

        public static bool CheckIsRACAdvisor()
        {
            var user = (User) HttpContext.Current.Session["User"];
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
                    Candidate = notificatonInstance.Candidate,
                    Description = description,
                    Date = notificatonInstance.Time,
                    NotificationType = notificatonInstance.NotificationType
                });
            }

            //Sorting by date now
            notifications = notifications.OrderByDescending(x => x.Date).ToList();
            return notifications;
        }

        public static RACAdvisorHomeViewModel MapToHomeViewModel(User user, RACAdvisorHomeViewModel rvm)
        {
            if (user == null || rvm == null)
            {
                return null;
            }

            rvm.Id = user.Id;
            rvm.Email = user.Email;
            rvm.FirstName = user.FirstName;
            rvm.LastName = user.LastName;
            rvm.UserType = user.UserType;
            rvm.WorkPhone = user.WorkPhone;
            return rvm;
        }

        public static void SendUploadNotification(int id)
        {
            var user = (Candidate)DbContext.Context.User.First(u => u.Id == id);
            Notification.CreateNotification(user.Id, Messages.RAC_ADVISOR_UPLOAD_TO_CANDIDATE);
            DbContext.Context.SaveChanges();
        }
    }
}