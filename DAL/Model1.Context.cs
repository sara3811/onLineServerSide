﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class onLineEntities1 : DbContext
    {
        public onLineEntities1()
            : base("name=onLineEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<activityTime> activityTimes { get; set; }
        public virtual DbSet<business> businesses { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<customersInLine> customersInLines { get; set; }
        public virtual DbSet<ranx> ranges { get; set; }
        public virtual DbSet<service> services { get; set; }
        public virtual DbSet<swap> swaps { get; set; }
        public virtual DbSet<unusual> unusuals { get; set; }
    }
}