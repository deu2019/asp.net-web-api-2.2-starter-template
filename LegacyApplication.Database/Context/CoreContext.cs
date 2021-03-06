﻿using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Reflection;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Core;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Models.Work;
using LegacyApplication.Shared.Configurations;

namespace LegacyApplication.Database.Context
{
    public class CoreContext : DbContext, IUnitOfWork
    {
        public CoreContext() : base(AppSettings.DefaultConnection)
        {
            //System.Data.Entity.Database.SetInitializer<CoreContext>(null);
#if DEBUG
            Database.Log = Console.Write;
            Database.Log = message => Trace.WriteLine(message);
#endif
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); //去掉默认开启的级联删除

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(UploadedFile)));
        }

        //Core
        public DbSet<UploadedFile> UploadedFiles { get; set; }

        //Work
        public DbSet<InternalMail> InternalMails { get; set; }
        public DbSet<InternalMailTo> InternalMailTos { get; set; }
        public DbSet<InternalMailAttachment> InternalMailAttachments { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        //HR
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobPostLevel> JobPostLevels { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<AdministrativeLevel> AdministrativeLevels { get; set; }
        public DbSet<TitleLevel> TitleLevels { get; set; }
        public DbSet<Nationality> Nationalitys { get; set; }
        
        
    }
}