﻿// <auto-generated />
using System;
using CoreRepo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoreRepo.Data.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    partial class CoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("core")
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoreRepo.Data.Receipt.ReceiptEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Receipts", "core");
                });

            modelBuilder.Entity("CoreRepo.Data.ReceiptLine.ReceiptLineEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<int>("AmountType")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyType")
                        .HasColumnType("int");

                    b.Property<Guid>("ReceiptId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ReceiptId");

                    b.ToTable("ReceiptLines", "core");
                });

            modelBuilder.Entity("CoreRepo.Data.ReceiptLineTag.ReceiptLineTagEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ReceiptLineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReceiptLineId");

                    b.ToTable("ReceiptLineTags", "core");
                });

            modelBuilder.Entity("CoreRepo.Data.ReceiptLine.ReceiptLineEntity", b =>
                {
                    b.HasOne("CoreRepo.Data.Receipt.ReceiptEntity", null)
                        .WithMany("ReceiptLines")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoreRepo.Data.ReceiptLineTag.ReceiptLineTagEntity", b =>
                {
                    b.HasOne("CoreRepo.Data.ReceiptLine.ReceiptLineEntity", null)
                        .WithMany("ReceiptLineTags")
                        .HasForeignKey("ReceiptLineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoreRepo.Data.Receipt.ReceiptEntity", b =>
                {
                    b.Navigation("ReceiptLines");
                });

            modelBuilder.Entity("CoreRepo.Data.ReceiptLine.ReceiptLineEntity", b =>
                {
                    b.Navigation("ReceiptLineTags");
                });
#pragma warning restore 612, 618
        }
    }
}