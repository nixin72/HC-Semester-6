using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAC.RACModels;

namespace RAC.RACModels
{
    public class RACUser
    {
        public String CSAdminId { get; set; }
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String HomePhone { get; set; }
        public String WorkPhone { get; set; }
        public int UserType { get; set; }
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
        public RAC.RACModels.Candidate CandidateProfile { get; set; }
    }
}