//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace program_mvm
{
    using System;
    using System.Collections.Generic;
    
    public partial class MediaSocialAccount
    {
        public string MediaID { get; set; }
        public string CustomerID { get; set; }
        public string MediaAccountName { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual MediaSocial MediaSocial { get; set; }
    }
}
