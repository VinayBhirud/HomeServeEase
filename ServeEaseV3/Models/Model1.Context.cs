﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServeEaseV2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dacProjectEntities : DbContext
    {
        public dacProjectEntities()
            : base("name=dacProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<address> addresses { get; set; }
        public virtual DbSet<appointment> appointments { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<payment_details> payment_details { get; set; }
        public virtual DbSet<review> reviews { get; set; }
        public virtual DbSet<service_providers> service_providers { get; set; }
        public virtual DbSet<transaction> transactions { get; set; }
    }
}