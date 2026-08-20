using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WalletService.Domain.Entities;






namespace WalletService.Application.Abstractions
{
    
    public interface IApplicationDbContext
    {
        DbSet<Client> Clients { get; }
        DbSet<Wallet> Wallets { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }




}
