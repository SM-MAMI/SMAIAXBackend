﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SMAIAXBackend.Infrastructure.DbContexts;

#nullable disable

namespace SMAIAXBackend.Infrastructure.Migrations.TenantDb
{
    [DbContext(typeof(TenantDbContext))]
    [Migration("20241109172921_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.Measurement", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("SmartMeterId")
                        .HasColumnType("uuid")
                        .HasColumnName("smartMeterId");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id")
                        .HasName("pK_Measurement");

                    b.ToTable("Measurement", "domain", t =>
                        {
                            t.Property("Timestamp")
                                .HasColumnName("measurement_timestamp");
                        });
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.Metadata", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("HouseholdSize")
                        .HasColumnType("integer")
                        .HasColumnName("householdSize");

                    b.Property<Guid>("SmartMeterId")
                        .HasColumnType("uuid")
                        .HasColumnName("smartMeterId");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("validFrom");

                    b.HasKey("Id")
                        .HasName("pK_Metadata");

                    b.HasIndex("SmartMeterId")
                        .HasDatabaseName("iX_Metadata_smartMeterId");

                    b.HasIndex("ValidFrom")
                        .IsUnique()
                        .HasDatabaseName("iX_Metadata_validFrom");

                    b.ToTable("Metadata", "domain");
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.Policy", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("LocationResolution")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("locationResolution");

                    b.Property<string>("MeasurementResolution")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("measurementResolution");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<Guid>("SmartMeterId")
                        .HasColumnType("uuid")
                        .HasColumnName("smartMeterId");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.HasKey("Id")
                        .HasName("pK_Policy");

                    b.ToTable("Policy", "domain");
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.PolicyRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsAutomaticContractingEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("isAutomaticContractingEnabled");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.HasKey("Id")
                        .HasName("pK_PolicyRequest");

                    b.ToTable("PolicyRequest", "domain");
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.SmartMeter", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pK_SmartMeter");

                    b.ToTable("SmartMeter", "domain");
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.Measurement", b =>
                {
                    b.OwnsOne("SMAIAXBackend.Domain.Model.ValueObjects.MeasurementData", "Data", b1 =>
                        {
                            b1.Property<Guid>("MeasurementId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<double>("CurrentPhase1")
                                .HasColumnType("double precision")
                                .HasColumnName("currentPhase1");

                            b1.Property<double>("CurrentPhase2")
                                .HasColumnType("double precision")
                                .HasColumnName("currentPhase2");

                            b1.Property<double>("CurrentPhase3")
                                .HasColumnType("double precision")
                                .HasColumnName("currentPhase3");

                            b1.Property<double>("NegativeActiveEnergyTotal")
                                .HasColumnType("double precision")
                                .HasColumnName("negativeActiveEnergyTotal");

                            b1.Property<double>("NegativeActivePower")
                                .HasColumnType("double precision")
                                .HasColumnName("negativeActivePower");

                            b1.Property<double>("PositiveActiveEnergyTotal")
                                .HasColumnType("double precision")
                                .HasColumnName("positiveActiveEnergyTotal");

                            b1.Property<double>("PositiveActivePower")
                                .HasColumnType("double precision")
                                .HasColumnName("positiveActivePower");

                            b1.Property<double>("ReactiveEnergyQuadrant1Total")
                                .HasColumnType("double precision")
                                .HasColumnName("reactiveEnergyQuadrant1Total");

                            b1.Property<double>("ReactiveEnergyQuadrant3Total")
                                .HasColumnType("double precision")
                                .HasColumnName("reactiveEnergyQuadrant3Total");

                            b1.Property<DateTime>("Timestamp")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("timestamp");

                            b1.Property<double>("TotalPower")
                                .HasColumnType("double precision")
                                .HasColumnName("totalPower");

                            b1.Property<string>("Uptime")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("uptime");

                            b1.Property<double>("VoltagePhase1")
                                .HasColumnType("double precision")
                                .HasColumnName("voltagePhase1");

                            b1.Property<double>("VoltagePhase2")
                                .HasColumnType("double precision")
                                .HasColumnName("voltagePhase2");

                            b1.Property<double>("VoltagePhase3")
                                .HasColumnType("double precision")
                                .HasColumnName("voltagePhase3");

                            b1.HasKey("MeasurementId");

                            b1.ToTable("Measurement", "domain");

                            b1.WithOwner()
                                .HasForeignKey("MeasurementId")
                                .HasConstraintName("fK_Measurement_Measurement_id");
                        });

                    b.Navigation("Data")
                        .IsRequired();
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.Metadata", b =>
                {
                    b.HasOne("SMAIAXBackend.Domain.Model.Entities.SmartMeter", "SmartMeter")
                        .WithMany("Metadata")
                        .HasForeignKey("SmartMeterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_Metadata_SmartMeter_smartMeterId");

                    b.OwnsOne("SMAIAXBackend.Domain.Model.ValueObjects.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("MetadataId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("City")
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<string>("Continent")
                                .HasColumnType("text")
                                .HasColumnName("continent");

                            b1.Property<string>("Country")
                                .HasColumnType("text")
                                .HasColumnName("country");

                            b1.Property<string>("State")
                                .HasColumnType("text")
                                .HasColumnName("state");

                            b1.Property<string>("StreetName")
                                .HasColumnType("text")
                                .HasColumnName("streetName");

                            b1.HasKey("MetadataId");

                            b1.ToTable("Metadata", "domain");

                            b1.WithOwner()
                                .HasForeignKey("MetadataId")
                                .HasConstraintName("fK_Metadata_Metadata_id");
                        });

                    b.Navigation("Location")
                        .IsRequired();

                    b.Navigation("SmartMeter");
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.PolicyRequest", b =>
                {
                    b.OwnsOne("SMAIAXBackend.Domain.Model.ValueObjects.PolicyFilter", "PolicyFilter", b1 =>
                        {
                            b1.Property<Guid>("PolicyRequestId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("LocationResolution")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("locationResolution");

                            b1.Property<string>("Locations")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("locations");

                            b1.Property<int>("MaxHouseHoldSize")
                                .HasColumnType("integer")
                                .HasColumnName("maxHouseHoldSize");

                            b1.Property<decimal>("MaxPrice")
                                .HasColumnType("numeric")
                                .HasColumnName("maxPrice");

                            b1.Property<string>("MeasurementResolution")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("measurementResolution");

                            b1.Property<int>("MinHouseHoldSize")
                                .HasColumnType("integer")
                                .HasColumnName("minHouseHoldSize");

                            b1.HasKey("PolicyRequestId");

                            b1.ToTable("PolicyRequest", "domain");

                            b1.WithOwner()
                                .HasForeignKey("PolicyRequestId")
                                .HasConstraintName("fK_PolicyRequest_PolicyRequest_id");
                        });

                    b.Navigation("PolicyFilter")
                        .IsRequired();
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.SmartMeter", b =>
                {
                    b.Navigation("Metadata");
                });
#pragma warning restore 612, 618
        }
    }
}
