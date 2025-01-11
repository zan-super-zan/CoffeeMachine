﻿// <auto-generated />
using System;
using CoffeMachineAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeMachineAPI.Migrations
{
    [DbContext(typeof(CoffeeMachineContext))]
    [Migration("20250110130247_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CoffeMachineAPI.PostgresModel.MachineEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EventDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("CoffeMachineAPI.PostgresModel.MachineState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("CoffeeAmountGrams")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsPoweredOn")
                        .HasColumnType("boolean");

                    b.Property<long>("WaterAmountMl")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("MachineStates");
                });
#pragma warning restore 612, 618
        }
    }
}