﻿// <auto-generated />
using System;
using JobAdvertisementAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobAdvertisementAppAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231219161313_user_admin_update")]
    partial class user_admin_update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CourseEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CourseStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organizer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EducationLevel")
                        .HasColumnType("int");

                    b.Property<string>("FieldOfStudy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Institution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PeriodOfEducationEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PeriodOfEducationStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Town")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.JobExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PeriodOdEmploymentEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PeriodOfEmploymentStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Responsibilities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("JobExperience");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.JobLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("JobLevel");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.JobType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("JobType");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailablePosts")
                        .HasColumnType("int");

                    b.Property<string>("Benefits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<int?>("JobLevelId")
                        .HasColumnType("int");

                    b.Property<int?>("JobTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("MaximumWege")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinimumWege")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requirements")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Responsibilities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TypeOfContractId")
                        .HasColumnType("int");

                    b.Property<string>("WorkingDays")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkingHours")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WorkingShiftId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("JobLevelId");

                    b.HasIndex("JobTypeId");

                    b.HasIndex("TypeOfContractId");

                    b.HasIndex("WorkingShiftId");

                    b.ToTable("Offer");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.TypeOfContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeOfContract");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CareerSummary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Occupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skills")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.UserLanguage", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("UserLanguage");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.WorkingShift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Shift")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkingShift");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Course", b =>
                {
                    b.HasOne("JobAdvertisementAppAPI.Models.User", "User")
                        .WithMany("Courses")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Education", b =>
                {
                    b.HasOne("JobAdvertisementAppAPI.Models.User", "User")
                        .WithMany("Education")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.JobExperience", b =>
                {
                    b.HasOne("JobAdvertisementAppAPI.Models.User", "User")
                        .WithMany("JobExperiences")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Offer", b =>
                {
                    b.HasOne("JobAdvertisementAppAPI.Models.Category", "Category")
                        .WithMany("Offers")
                        .HasForeignKey("CategoryId");

                    b.HasOne("JobAdvertisementAppAPI.Models.Company", "Company")
                        .WithMany("Offers")
                        .HasForeignKey("CompanyId");

                    b.HasOne("JobAdvertisementAppAPI.Models.JobLevel", "JobLevel")
                        .WithMany("Offers")
                        .HasForeignKey("JobLevelId");

                    b.HasOne("JobAdvertisementAppAPI.Models.JobType", "JobType")
                        .WithMany("Offers")
                        .HasForeignKey("JobTypeId");

                    b.HasOne("JobAdvertisementAppAPI.Models.TypeOfContract", "TypeOfContract")
                        .WithMany("Offers")
                        .HasForeignKey("TypeOfContractId");

                    b.HasOne("JobAdvertisementAppAPI.Models.WorkingShift", "WorkingShift")
                        .WithMany("Offer")
                        .HasForeignKey("WorkingShiftId");

                    b.Navigation("Category");

                    b.Navigation("Company");

                    b.Navigation("JobLevel");

                    b.Navigation("JobType");

                    b.Navigation("TypeOfContract");

                    b.Navigation("WorkingShift");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Profile", b =>
                {
                    b.HasOne("JobAdvertisementAppAPI.Models.User", "User")
                        .WithMany("Profiles")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.UserLanguage", b =>
                {
                    b.HasOne("JobAdvertisementAppAPI.Models.Language", "Language")
                        .WithMany("UserLanguages")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobAdvertisementAppAPI.Models.User", "User")
                        .WithMany("UserLanguages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Category", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Company", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.JobLevel", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.JobType", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.Language", b =>
                {
                    b.Navigation("UserLanguages");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.TypeOfContract", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.User", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Education");

                    b.Navigation("JobExperiences");

                    b.Navigation("Profiles");

                    b.Navigation("UserLanguages");
                });

            modelBuilder.Entity("JobAdvertisementAppAPI.Models.WorkingShift", b =>
                {
                    b.Navigation("Offer");
                });
#pragma warning restore 612, 618
        }
    }
}
