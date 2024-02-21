﻿// <auto-generated />
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dciSphere.Infrastructure.Database;

#nullable disable

namespace dciSphere.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240215080917_UpdateBankEntity")]
    partial class UpdateBankEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("dciSphere.Core.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.ComplexProperty<Dictionary<string, object>>("Balance", "dciSphere.Core.Models.Account.Balance#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Amount")
                                .HasColumnType("int");

                            b1.Property<int>("Currency")
                                .HasColumnType("int");
                        });

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("dciSphere.Core.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("dciSphere.Core.Models.Account", b =>
                {
                    b.HasOne("dciSphere.Core.Models.Bank", "Bank")
                        .WithMany("Accounts")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("dciSphere.Core.Models.Bank", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}