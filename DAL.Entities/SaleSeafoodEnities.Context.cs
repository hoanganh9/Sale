﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Repository.Pattern.Ef6;
    
    public partial class SaleSeafoodEntities : DataContext
    {
        public SaleSeafoodEntities()
            : base("name=SaleSeafoodEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserInfo> AspNetUserInfoes { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<SI_Category> SI_Category { get; set; }
        public virtual DbSet<SI_CategoryDetail> SI_CategoryDetail { get; set; }
        public virtual DbSet<SysAction> SysActions { get; set; }
        public virtual DbSet<SysConfig> SysConfigs { get; set; }
        public virtual DbSet<SysGenCode> SysGenCodes { get; set; }
        public virtual DbSet<SysGroupAction> SysGroupActions { get; set; }
        public virtual DbSet<SysMenu> SysMenus { get; set; }
        public virtual DbSet<SysLog> SysLogs { get; set; }
        public virtual DbSet<V_UserInfo> V_UserInfo { get; set; }
    }
}
