using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAC.Models
{
    public class RACAdvisorHomeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public int UserType { get; set; }
        public List<NotificationsViewModel> Notifications { get; set; }
    }
}