using dciSphere.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Database.Configuration;
internal class BankEntityConfiguration : IEntityTypeConfiguration<Bank>
{
    public void Configure(EntityTypeBuilder<Bank> builder)
    {
         builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Accounts)
            .WithOne(x => x.Bank)
            .HasForeignKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
