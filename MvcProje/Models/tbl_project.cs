//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcProje.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectContent { get; set; }
        public string ProjectImg { get; set; }
        public Nullable<System.DateTime> ProjectAddedDatetime { get; set; }
        public string ProjectAddedAdmin { get; set; }
        public Nullable<int> ProjectOrder { get; set; }
        public Nullable<int> ProjectStatus { get; set; }
        public string ProjectCategory { get; set; }
    }
}
