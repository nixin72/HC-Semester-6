//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RAC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RACAdvisorNotifications
    {
        public int Id { get; set; }
        public short NotificationType { get; set; }
        public int CandidateId { get; set; }
        public System.DateTime Time { get; set; }
    
        public virtual Candidate Candidate { get; set; }
    }
}
