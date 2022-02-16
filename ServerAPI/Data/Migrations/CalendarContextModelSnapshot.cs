﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerAPI.Data;

namespace ServerAPI.Migrations
{
    [DbContext(typeof(CalendarContext))]
    partial class CalendarContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("ServerAPI.Data.Models.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AdminPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Recipients")
                        .HasColumnType("TEXT");

                    b.HasKey("AccountID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ServerAPI.Data.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("IterationsFinished")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notifications")
                        .HasColumnType("TEXT");

                    b.Property<int>("RecurringType")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ServerAPI.Data.Models.Settings", b =>
                {
                    b.Property<int>("revisionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HostEmailPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("HostEmailUsername")
                        .HasColumnType("TEXT");

                    b.Property<int>("NotificationTime")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ServerName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("revisionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("revisionID");

                    b.ToTable("Settings");
                });
#pragma warning restore 612, 618
        }
    }
}
