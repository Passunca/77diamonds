//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace developerBackEndTest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemPhotoPropertySet
    {
        public int Id { get; set; }
        public int ItemPhotoId { get; set; }
        public int PropertyId { get; set; }
        public string Value { get; set; }
    
        public virtual Property Property { get; set; }
        public virtual ItemPhoto ItemPhoto { get; set; }
    }
}
