//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RAC.RACModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class ElementMinistryData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ElementMinistryData()
        {
            this.CompetencyElements = new HashSet<CompetencyElement>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> MinistryId { get; set; }
        public string MinistryCode { get; set; }
        public System.DateTime DateAdded { get; set; }
        public Nullable<System.DateTime> DateExpired { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompetencyElement> CompetencyElements { get; set; }
    }
}
