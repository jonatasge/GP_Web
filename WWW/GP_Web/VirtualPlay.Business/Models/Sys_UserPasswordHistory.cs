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
    
    public partial class Sys_UserPasswordHistory
    {
        public int idUserPasswordHistory { get; set; }
        public int idUser { get; set; }
        public string dsPassword { get; set; }
        public int idUserCreate { get; set; }
        public System.DateTime dtCreate { get; set; }
        public int idUserLastUpdate { get; set; }
        public System.DateTime dtLastUpdate { get; set; }
    
        public virtual Sys_User Sys_User { get; set; }
        public virtual Sys_User Sys_User1 { get; set; }
        public virtual Sys_User Sys_User2 { get; set; }
    }
}
