//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WamApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class Page
    {
        public Page()
        {
            this.Region = new HashSet<Region>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public Nullable<int> applicationId { get; set; }
    
        public virtual Application Application { get; set; }
        public virtual ICollection<Region> Region { get; set; }
    }
}
