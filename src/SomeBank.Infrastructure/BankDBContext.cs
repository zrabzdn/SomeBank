using SomeBank.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace SomeBank.Infrastructure
{
    public class BankDBContext : DbContext
    {
        public BankDBContext() : base() { }
        public BankDBContext(DbContextOptions<BankDBContext> options) : base(options) { }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BankDB;Trusted_Connection=True;");
            }
        }
    }
}