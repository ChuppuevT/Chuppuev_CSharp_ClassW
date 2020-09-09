﻿using AbstractFoodDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFoodDatabaseImplement
{
    public class AbstractFoodDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=(local)\SQLEXPRESS;Initial Catalog=AbstractFoodDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Food> Foods { set; get; }
        public virtual DbSet<Kit> Kits { set; get; }
        public virtual DbSet<KitFood> KitComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public DbSet<Client> Clients { set; get; }
        public DbSet<Implementer> Implementers { set; get; }
        public DbSet<MessageInfo> MessageInfoes { set; get; }
    }
}
