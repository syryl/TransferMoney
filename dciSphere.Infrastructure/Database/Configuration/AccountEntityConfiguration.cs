using dciSphere.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Database.Configuration;
internal class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
        builder.Property(x => x.BankId).IsRequired();

        builder.HasOne(x => x.Bank)
            .WithMany(x => x.Accounts)
            .HasForeignKey(x => x.BankId);

        builder.ComplexProperty(x => x.Balance, c =>
        {
            c.Property(p => p.Currency).IsRequired();
            c.Property(p => p.Amount).IsRequired();
        });
    }
}
