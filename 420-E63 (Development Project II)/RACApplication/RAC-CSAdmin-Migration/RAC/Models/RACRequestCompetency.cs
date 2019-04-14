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
    
    public partial class RACRequestCompetency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RACRequestCompetency()
        {
            this.RACRequestCompetencyElements = new HashSet<RACRequestCompetencyElements>();
            this.CompetencyComments = new HashSet<CompetencyComment>();
        }
    
        public int Id { get; set; }
        public int RACRequestId { get; set; }
        public int CompetencyId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RACRequestCompetencyElements> RACRequestCompetencyElements { get; set; }
        public virtual RACRequest RACRequest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompetencyComment> CompetencyComments { get; set; }
        public virtual Competency Competency { get; set; }
    }
}