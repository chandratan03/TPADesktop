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
    
    public partial class MediaSocial
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MediaSocial()
        {
            this.MediaSocialAccounts = new HashSet<MediaSocialAccount>();
        }
    
        public string MediaID { get; set; }
        public string MediaName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MediaSocialAccount> MediaSocialAccounts { get; set; }
    }
}
