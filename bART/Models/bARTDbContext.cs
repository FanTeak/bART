using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bART.Models
{
    public class bARTDbContext : DbContext
    {
        public bARTDbContext(DbContextOptions<bARTDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(AccountConfigure);
            modelBuilder.Entity<Contact>(ContactConfigure);
        }

        private void AccountConfigure(EntityTypeBuilder<Account> builder)
        {
            builder.HasAlternateKey(a => a.Name);
            builder.HasOne(a => a.Incident).WithMany(i => i.Accounts).OnDelete(DeleteBehavior.SetNull);
        }

        private void ContactConfigure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasAlternateKey(c => c.Email);
            builder.HasOne(c=>c.Account).WithMany(a=>a.Contacts).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
