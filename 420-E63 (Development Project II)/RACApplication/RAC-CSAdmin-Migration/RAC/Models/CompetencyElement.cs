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
    
    public partial class CompetencyElement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompetencyElement()
        {
            this.RACRequestCompetencyElements = new HashSet<RACRequestCompetencyElements>();
        }
    
        public int Id { get; set; }
        public int CompetencyId { get; set; }
        public int ElementMinistryDataId { get; set; }
        public System.DateTime DateAdded { get; set; }
        public Nullable<System.DateTime> DateExpired { get; set; }
    
        public virtual Competency Competency { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RACRequestCompetencyElements> RACRequestCompetencyElements { get; set; }
        public virtual ElementMinistryData ElementMinistryData { get; set; }
    }
}
