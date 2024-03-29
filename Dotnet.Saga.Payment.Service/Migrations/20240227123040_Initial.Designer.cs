﻿// <auto-generated />
using System;
using Dotnet.Saga.Payment.Service.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dotnet.Saga.Payment.Service.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240227123040_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dotnet.Saga.Payment.Service.Entities.PaymentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PaymentIdent")
                        .HasColumnType("char(36)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("tb_payment", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
