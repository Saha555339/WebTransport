﻿// <auto-generated />
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebTransport.DataBase;

namespace WebTransport.Migrations
{
    [DbContext(typeof(TransportContext))]
    [Migration("20220329190924_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("WebTransport.DataBase.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("WebTransport.DataBase.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("WebTransport.DataBase.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<List<int>>("StopsId")
                        .HasColumnType("integer[]");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("WebTransport.DataBase.Stop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("DistrictId")
                        .HasColumnType("integer");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("WebTransport.DataBase.District", b =>
                {
                    b.HasOne("WebTransport.DataBase.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("WebTransport.DataBase.Route", b =>
                {
                    b.HasOne("WebTransport.DataBase.City", "City")
                        .WithMany("Routes")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("WebTransport.DataBase.Stop", b =>
                {
                    b.HasOne("WebTransport.DataBase.District", "District")
                        .WithMany("Stops")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("WebTransport.DataBase.City", b =>
                {
                    b.Navigation("Districts");

                    b.Navigation("Routes");
                });

            modelBuilder.Entity("WebTransport.DataBase.District", b =>
                {
                    b.Navigation("Stops");
                });
#pragma warning restore 612, 618
        }
    }
}