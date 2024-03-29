﻿// <auto-generated />
using System;
using CommonModelsLib.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BEAPIDemo.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20230615162908_test")]
    partial class test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CommonModelsLib.UserBase", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("userBases");
                });
#pragma warning restore 612, 618
        }
    }
}
