﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bART.Models
{
    public class bARTDbContext : DbContext
    {
        public bARTDbContext(DbContextOptions<bARTDbContext> options) : base(options)
        {
            
        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(AccountConfigure);
            modelBuilder.Entity<Contact>(ContactConfigure);
        }

        private void AccountConfigure(EntityTypeBuilder<Account> builder)
        {
            builder.HasAlternateKey(a => a.Name);
        }

        private void ContactConfigure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasAlternateKey(c => c.Email);
        }
    }
}