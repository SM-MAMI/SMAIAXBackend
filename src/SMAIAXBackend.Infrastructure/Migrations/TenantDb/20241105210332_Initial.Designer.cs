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
    [Migration("20241105210332_Initial")]
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

                    b.ToTable("Measurement", "domain");
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

                    b.ToTable("Metadata", "domain");
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.Policy", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("HouseholdSize")
                        .HasColumnType("integer")
                        .HasColumnName("householdSize");

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

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("userId");

                    b.HasKey("Id")
                        .HasName("pK_Policy");

                    b.ToTable("Policy", "domain", t =>
                        {
                            t.Property("State")
                                .HasColumnName("policy_state");
                        });
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

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("userId");

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

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("userId");

                    b.HasKey("Id")
                        .HasName("pK_SmartMeter");

                    b.ToTable("SmartMeter", "domain");
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.RelationshipHelpers.PolicySmartMeter", b =>
                {
                    b.Property<Guid>("PolicyId")
                        .HasColumnType("uuid")
                        .HasColumnName("policyId");

                    b.Property<Guid>("SmartMeterId")
                        .HasColumnType("uuid")
                        .HasColumnName("smartMeterId");

                    b.HasKey("PolicyId", "SmartMeterId")
                        .HasName("pK_PolicySmartMeter");

                    b.HasIndex("SmartMeterId")
                        .HasDatabaseName("iX_PolicySmartMeter_smartMeterId");

                    b.ToTable("PolicySmartMeter", "domain");
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
                                .HasColumnName("CurrentPhase1");

                            b1.Property<double>("CurrentPhase2")
                                .HasColumnType("double precision")
                                .HasColumnName("CurrentPhase2");

                            b1.Property<double>("CurrentPhase3")
                                .HasColumnType("double precision")
                                .HasColumnName("CurrentPhase3");

                            b1.Property<double>("NegativeActiveEnergyTotal")
                                .HasColumnType("double precision")
                                .HasColumnName("NegativeActiveEnergyTotal");

                            b1.Property<double>("NegativeActivePower")
                                .HasColumnType("double precision")
                                .HasColumnName("NegativeActivePower");

                            b1.Property<double>("PositiveActiveEnergyTotal")
                                .HasColumnType("double precision")
                                .HasColumnName("PositiveActiveEnergyTotal");

                            b1.Property<double>("PositiveActivePower")
                                .HasColumnType("double precision")
                                .HasColumnName("PositiveActivePower");

                            b1.Property<double>("ReactiveEnergyQuadrant1Total")
                                .HasColumnType("double precision")
                                .HasColumnName("ReactiveEnergyQuadrant1Total");

                            b1.Property<double>("ReactiveEnergyQuadrant3Total")
                                .HasColumnType("double precision")
                                .HasColumnName("ReactiveEnergyQuadrant3Total");

                            b1.Property<DateTime>("Timestamp")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("Timestamp");

                            b1.Property<double>("TotalPower")
                                .HasColumnType("double precision")
                                .HasColumnName("TotalPower");

                            b1.Property<string>("Uptime")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Uptime");

                            b1.Property<double>("VoltagePhase1")
                                .HasColumnType("double precision")
                                .HasColumnName("VoltagePhase1");

                            b1.Property<double>("VoltagePhase2")
                                .HasColumnType("double precision")
                                .HasColumnName("VoltagePhase2");

                            b1.Property<double>("VoltagePhase3")
                                .HasColumnType("double precision")
                                .HasColumnName("VoltagePhase3");

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
                                .HasColumnName("City");

                            b1.Property<string>("Continent")
                                .HasColumnType("text")
                                .HasColumnName("Continent");

                            b1.Property<string>("Country")
                                .HasColumnType("text")
                                .HasColumnName("Country");

                            b1.Property<string>("State")
                                .HasColumnType("text")
                                .HasColumnName("State");

                            b1.Property<string>("StreetName")
                                .HasColumnType("text")
                                .HasColumnName("StreetName");

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

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.Policy", b =>
                {
                    b.OwnsOne("SMAIAXBackend.Domain.Model.ValueObjects.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("PolicyId")
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

                            b1.HasKey("PolicyId");

                            b1.ToTable("Policy", "domain");

                            b1.WithOwner()
                                .HasForeignKey("PolicyId")
                                .HasConstraintName("fK_Policy_Policy_id");
                        });

                    b.Navigation("Location")
                        .IsRequired();
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
                                .HasColumnName("LocationResolution");

                            b1.Property<string>("Locations")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("policyFilter_Locations");

                            b1.Property<int>("MaxHouseHoldSize")
                                .HasColumnType("integer")
                                .HasColumnName("MaxHouseHoldSize");

                            b1.Property<decimal>("MaxPrice")
                                .HasColumnType("numeric")
                                .HasColumnName("MaxPrice");

                            b1.Property<string>("MeasurementResolution")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("MeasurementResolution");

                            b1.Property<int>("MinHouseHoldSize")
                                .HasColumnType("integer")
                                .HasColumnName("MinHouseHoldSize");

                            b1.HasKey("PolicyRequestId");

                            b1.ToTable("PolicyRequest", "domain");

                            b1.WithOwner()
                                .HasForeignKey("PolicyRequestId")
                                .HasConstraintName("fK_PolicyRequest_PolicyRequest_id");
                        });

                    b.Navigation("PolicyFilter")
                        .IsRequired();
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.RelationshipHelpers.PolicySmartMeter", b =>
                {
                    b.HasOne("SMAIAXBackend.Domain.Model.Entities.Policy", null)
                        .WithMany("SmartMeters")
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_PolicySmartMeter_Policy_policyId");

                    b.HasOne("SMAIAXBackend.Domain.Model.Entities.SmartMeter", null)
                        .WithMany("Policies")
                        .HasForeignKey("SmartMeterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_PolicySmartMeter_SmartMeter_smartMeterId");
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.Policy", b =>
                {
                    b.Navigation("SmartMeters");
                });

            modelBuilder.Entity("SMAIAXBackend.Domain.Model.Entities.SmartMeter", b =>
                {
                    b.Navigation("Metadata");

                    b.Navigation("Policies");
                });
#pragma warning restore 612, 618
        }
    }
}
