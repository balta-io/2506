using CleanStore.Domain.AccountContext.Entities;
using CleanStore.Domain.AccountContext.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanStore.Infrastructure.SharedContext.Data.Mappings;

public class AccountMap : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        #region Table and Primary Key

        builder
            .ToTable("Account");

        builder
            .HasKey(x => x.Id)
            .HasName("PK_Account");

        #endregion

        #region Columns
        builder.OwnsOne(x => x.Email, email =>
        {
            email.HasIndex(e => e.Address)
                .HasDatabaseName("IX_Account_Email")
                .IsUnique();

            email.Property(x => x.Address)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(Email.MaxLength)
                .HasColumnName("Email");

            email.Property(x => x.Hash)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .HasColumnName("EmailHash");
        });
        #endregion
    }
}