﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TowerLocatorApp.DataAccess;

#nullable disable

namespace TowerLocatorApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230713165656_BaseMigration")]
    partial class BaseMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TowerLocatorApp.Models.BTSModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("RouteId")
                        .HasColumnType("integer");

                    b.Property<double>("accuracy")
                        .HasColumnType("double precision");

                    b.Property<double>("altitude")
                        .HasColumnType("double precision");

                    b.Property<string>("arfcn")
                        .HasColumnType("text");

                    b.Property<double>("asu")
                        .HasColumnType("double precision");

                    b.Property<double>("bearing")
                        .HasColumnType("double precision");

                    b.Property<string>("cdma_dbm")
                        .HasColumnType("text");

                    b.Property<string>("cdma_ecio")
                        .HasColumnType("text");

                    b.Property<double>("cell_id")
                        .HasColumnType("double precision");

                    b.Property<double?>("cqi")
                        .HasColumnType("double precision");

                    b.Property<string>("csi_rsrp")
                        .HasColumnType("text");

                    b.Property<string>("csi_rsrq")
                        .HasColumnType("text");

                    b.Property<string>("csi_sinr")
                        .HasColumnType("text");

                    b.Property<double>("dbm")
                        .HasColumnType("double precision");

                    b.Property<string>("device")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("discovered_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ec_no")
                        .HasColumnType("text");

                    b.Property<string>("evdo_dbm")
                        .HasColumnType("text");

                    b.Property<string>("evdo_ecio")
                        .HasColumnType("text");

                    b.Property<string>("evdo_snr")
                        .HasColumnType("text");

                    b.Property<double>("lac")
                        .HasColumnType("double precision");

                    b.Property<double>("lat")
                        .HasColumnType("double precision");

                    b.Property<double>("lon")
                        .HasColumnType("double precision");

                    b.Property<double>("mcc")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("measured_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("mnc")
                        .HasColumnType("double precision");

                    b.Property<bool>("neighboring")
                        .HasColumnType("boolean");

                    b.Property<string>("net_type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("psc")
                        .HasColumnType("double precision");

                    b.Property<double?>("rnc")
                        .HasColumnType("double precision");

                    b.Property<string>("rscp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("rsrp")
                        .HasColumnType("double precision");

                    b.Property<double?>("rsrq")
                        .HasColumnType("double precision");

                    b.Property<double>("rssi")
                        .HasColumnType("double precision");

                    b.Property<string>("rssnr")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("short_cell_id")
                        .HasColumnType("double precision");

                    b.Property<double>("speed")
                        .HasColumnType("double precision");

                    b.Property<string>("ss_rsrp")
                        .HasColumnType("text");

                    b.Property<string>("ss_rsrq")
                        .HasColumnType("text");

                    b.Property<string>("ss_sinr")
                        .HasColumnType("text");

                    b.Property<double>("ta")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("BTSSet");
                });

            modelBuilder.Entity("TowerLocatorApp.Models.DetailedMapPointModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Point>("Coordinates")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<double>("Elevation")
                        .HasColumnType("double precision");

                    b.Property<int>("RouteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("MapPoints");
                });

            modelBuilder.Entity("TowerLocatorApp.Models.RouteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<LineString>("Line")
                        .HasColumnType("geometry");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("TowerLocatorApp.Models.BTSModel", b =>
                {
                    b.HasOne("TowerLocatorApp.Models.RouteModel", "Route")
                        .WithMany("AssociatedTowers")
                        .HasForeignKey("RouteId");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("TowerLocatorApp.Models.DetailedMapPointModel", b =>
                {
                    b.HasOne("TowerLocatorApp.Models.RouteModel", "Route")
                        .WithMany("RoutePoints")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("TowerLocatorApp.Models.RouteModel", b =>
                {
                    b.Navigation("AssociatedTowers");

                    b.Navigation("RoutePoints");
                });
#pragma warning restore 612, 618
        }
    }
}
