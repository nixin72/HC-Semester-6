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
    
    public partial class UploadedDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Binary { get; set; }
        public int RACRequestId { get; set; }
        public System.DateTime DateUploaded { get; set; }
        public bool IsAddedByRACAdvisor { get; set; }
        public bool IsOpened { get; set; }
    
        public virtual RACRequest RACRequest { get; set; }
    }
}
