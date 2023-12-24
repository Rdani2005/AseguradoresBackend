﻿// <auto-generated />
using System;
using Beneficiary.Service.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Beneficiary.Service.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Beneficiary.Service.Entities.Insurance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CarrierCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("Beneficiary.Service.Entities.InsuranceInsured", b =>
                {
                    b.Property<Guid>("InsuredId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InsuranceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("InsuredId", "InsuranceId");

                    b.HasIndex("InsuranceId");

                    b.ToTable("InsuredInsurances");
                });

            modelBuilder.Entity("Beneficiary.Service.Entities.Insured", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("Date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Insuranced");
                });

            modelBuilder.Entity("Beneficiary.Service.Entities.InsuranceInsured", b =>
                {
                    b.HasOne("Beneficiary.Service.Entities.Insurance", "Insurance")
                        .WithMany("InsuredInsurances")
                        .HasForeignKey("InsuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Beneficiary.Service.Entities.Insured", "Insured")
                        .WithMany("InsuredInsurances")
                        .HasForeignKey("InsuredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Insurance");

                    b.Navigation("Insured");
                });

            modelBuilder.Entity("Beneficiary.Service.Entities.Insurance", b =>
                {
                    b.Navigation("InsuredInsurances");
                });

            modelBuilder.Entity("Beneficiary.Service.Entities.Insured", b =>
                {
                    b.Navigation("InsuredInsurances");
                });
#pragma warning restore 612, 618
        }
    }
}
