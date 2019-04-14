using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAC.RACModels
{
    public class CandidateCustom : RACUser
    {
        public String Street { get; set; }
        public String City { get; set; }
        public String Province { get; set; }
        public String Country { get; set; }
        public int PreferredMethodOfContact { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsArchived { get; set; }
        public Boolean IsPrivacyPolicyAccepted { get; set; }
        public DateTime DateRegistered { get; set; }
        public String RegistrationIP { get; set; }
    }
}