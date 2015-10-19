//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VirtualPlay.Business.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sys_System
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sys_System()
        {
            this.Sys_SmsLog = new HashSet<Sys_SmsLog>();
            this.Sys_UserLoginHistory = new HashSet<Sys_UserLoginHistory>();
            this.Sys_UserSystem = new HashSet<Sys_UserSystem>();
            this.Sys_WebmailLog = new HashSet<Sys_WebmailLog>();
            this.Sys_UserSession = new HashSet<Sys_UserSession>();
        }
    
        public int idSystem { get; set; }
        public string tpSystem { get; set; }
        public string nmSystem { get; set; }
        public string dsSystem { get; set; }
        public string cdSystem { get; set; }
        public Nullable<System.DateTime> dtExpire { get; set; }
        public int idUserCreate { get; set; }
        public System.DateTime dtCreate { get; set; }
        public int idUserLastUpdate { get; set; }
        public System.DateTime dtLastUpdate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_SmsLog> Sys_SmsLog { get; set; }
        public virtual Sys_SystemType Sys_SystemType { get; set; }
        public virtual Sys_User Sys_User { get; set; }
        public virtual Sys_User Sys_User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_UserLoginHistory> Sys_UserLoginHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_UserSystem> Sys_UserSystem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_WebmailLog> Sys_WebmailLog { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_UserSession> Sys_UserSession { get; set; }
    }
}
