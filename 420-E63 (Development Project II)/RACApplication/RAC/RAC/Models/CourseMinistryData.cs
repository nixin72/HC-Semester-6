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
    
    public partial class CourseMinistryData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourseMinistryData()
        {
            this.Courses = new HashSet<Course>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> MinistryId { get; set; }
        public string MinistryCode { get; set; }
        public int CourseType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
