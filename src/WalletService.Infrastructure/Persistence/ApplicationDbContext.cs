using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WalletService.Application.Abstractions;
using WalletService.Domain.Entities;



namespace WalletService.Infrastructure.Persistence
{
    
    public class ApplicationDbContext : DbContext , IApplicationDbContext
    {

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Wallet> Wallets => Set<Wallet>();



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        




    }




    
   
}
