﻿// <auto-generated />
using BackendCodeForML.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackendCodeForML.Data.Migrations
{
    [DbContext(typeof(WYPredictionContext))]
    [Migration("20241206152630_AddedUserPredictionModel")]
    partial class AddedUserPredictionModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackendCodeForML.Models.DistrictData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AvgTemp")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Clay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PHLevel")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Phosphorus")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Potassium")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ProductionArea")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Rainfall")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RelativeHumidity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Sand")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SoilTemp")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EDistrict");
                });

            modelBuilder.Entity("BackendCodeForML.Models.RegisterModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BackendCodeForML.Models.UserPredictionDetailModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AvgTemp")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Clay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("DistrictName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PHLevel")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Phosphorus")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Potassium")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PredictionResult")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ProductionArea")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Rainfall")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RelativeHumidity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Sand")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SoilTemp")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("UserPredictionDetail");
                });

            modelBuilder.Entity("BackendCodeForML.Models.UserRoleModel", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("BackendCodeForML.Models.RegisterModel", b =>
                {
                    b.HasOne("BackendCodeForML.Models.UserRoleModel", "Role")
                        .WithMany("RegisteredUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BackendCodeForML.Models.UserRoleModel", b =>
                {
                    b.Navigation("RegisteredUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
