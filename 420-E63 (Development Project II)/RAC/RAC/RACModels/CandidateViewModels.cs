﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAC.RACModels
{
    public class CandidateHomeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
        public List<NotificationsViewModel> Notifications { get; set; }
    }
}