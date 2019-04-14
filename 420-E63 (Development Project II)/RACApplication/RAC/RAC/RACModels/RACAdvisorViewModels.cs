using System.Collections.Generic;

namespace RAC.RACModels
{
    public class RACAdvisorHomeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<NotificationsViewModel> Notifications { get; set; }
    }
}