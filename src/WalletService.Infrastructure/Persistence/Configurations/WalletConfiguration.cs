using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletService.Domain.Entities;
using WalletService.Domain.Enums;




namespace WalletService.Infrastructure.Persistence.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Code).IsRequired().HasMaxLength(100);

            builder.HasIndex(w => w.Code).IsUnique();

            builder.Property(w => w.AccountNumber).HasMaxLength(50);

            builder.Property(w => w.Status).HasConversion<int>().IsRequired();

            builder.HasIndex(w => new { w.ClientId }).IsUnique().HasFilter("[Status] IN (1, 2, 3)");

            builder.HasOne(w => w.Client).WithMany(c => c.Wallets).HasForeignKey(w => w.ClientId).OnDelete(DeleteBehavior.Restrict);







        }
    }
}
