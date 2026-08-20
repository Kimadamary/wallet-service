using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletService.Domain.Entities;





namespace WalletService.Infrastructure.Persistence.Configurations
{
   public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder) {


            builder.HasKey(c => c.Id);

            builder.Property(c => c.Mid).IsRequired().HasMaxLength(50);

            builder.HasIndex(c => c.Mid).IsUnique();

            builder.Property(c => c.FullName).IsRequired().HasMaxLength(250);
            
            builder.Property(c => c.ParticipantId).HasMaxLength(100);

            builder.HasIndex(c => c.ParticipantId).IsUnique().HasFilter("[ParticipantId] IS NOT NULL");

        }

    }
    
}
