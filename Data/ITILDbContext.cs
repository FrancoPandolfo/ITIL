﻿using Microsoft.EntityFrameworkCore;
using ITIL.Data.Domain;

namespace ITIL.Data
{ 
    public class ITILDbContext : DbContext
    {
        public ITILDbContext(DbContextOptions<ITILDbContext> options):base(options)
        {                           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);        

            // Cambiar la convención de nombres de tablas.
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }        
        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Change> Changes { get; set; }
        public DbSet<ConfigurationItem> Configuration { get; set; }
    }
}
