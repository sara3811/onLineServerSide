﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BussinessSite
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class onLineEntities : DbContext
    {
        public onLineEntities()
            : base("name=onLineEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<activityTimes> activityTimes { get; set; }
        public virtual DbSet<businesses> businesses { get; set; }
        public virtual DbSet<categories> categories { get; set; }
        public virtual DbSet<customers> customers { get; set; }
        public virtual DbSet<customersInLine> customersInLine { get; set; }
        public virtual DbSet<ranges> ranges { get; set; }
        public virtual DbSet<services> services { get; set; }
        public virtual DbSet<swaps> swaps { get; set; }
        public virtual DbSet<unusuals> unusuals { get; set; }
    }
}
