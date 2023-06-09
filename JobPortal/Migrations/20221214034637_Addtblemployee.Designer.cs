﻿// <auto-generated />
using JobPortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221214034637_Addtblemployee")]
    partial class Addtblemployee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("JobSite.Models.Employee", b =>
                {
                    b.Property<int>("EId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EId"), 1L, 1);

                    b.Property<int>("EAge")
                        .HasColumnType("int");

                    b.Property<long>("Mobno")
                        .HasColumnType("bigint");

                    b.HasKey("EId");

                    b.ToTable("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
