﻿// <auto-generated />
using System;
using AthleteHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AthleteHub.Infrastructure.Migrations
{
    [DbContext(typeof(AthleteHubDbContext))]
    [Migration("20240623162934_makingCertificateNullable")]
    partial class makingCertificateNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AthleteHub.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Athlete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("SubscribtionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Athletes");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.AthleteActiveSubscribtion", b =>
                {
                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<int>("SubscribtionId")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("SubscribtionEndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("SubscribtionStartDate")
                        .HasColumnType("date");

                    b.HasKey("AthleteId", "SubscribtionId");

                    b.HasIndex("SubscribtionId");

                    b.ToTable("AthleteActiveSubscribtions");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.AthleteCoach", b =>
                {
                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<int>("CoachId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCurrentlySubscribed")
                        .HasColumnType("bit");

                    b.HasKey("AthleteId", "CoachId");

                    b.HasIndex("CoachId");

                    b.ToTable("AthletesCoaches");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.AthleteFavouriteCoach", b =>
                {
                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<int>("CoachId")
                        .HasColumnType("int");

                    b.HasKey("AthleteId", "CoachId");

                    b.HasIndex("CoachId");

                    b.ToTable("AthletesFavouriteCoaches");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.AthleteSubscribtionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("SubscribtionEndDate")
                        .HasColumnType("date");

                    b.Property<int>("SubscribtionId")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("SubscribtionStartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("AthletesSubscribtionsHistory");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Coach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Certificate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSuspended")
                        .HasColumnType("bit");

                    b.Property<decimal?>("OverallRating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("RatingsCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.CoachBlockedAthlete", b =>
                {
                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<int>("CoachId")
                        .HasColumnType("int");

                    b.HasKey("AthleteId", "CoachId");

                    b.HasIndex("CoachId");

                    b.ToTable("CoachesBlockedAthletees");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.CoachRating", b =>
                {
                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<int>("CoachId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.HasKey("AthleteId", "CoachId");

                    b.HasIndex("CoachId");

                    b.ToTable("CoachesRatings");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoachId")
                        .HasColumnType("int");

                    b.Property<int>("ContentType")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Measurement", b =>
                {
                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<decimal?>("BMI")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int?>("BenchPressWeight")
                        .HasColumnType("int");

                    b.Property<decimal?>("BodyFatPercentage")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int?>("DeadliftWeight")
                        .HasColumnType("int");

                    b.Property<int?>("SquatWeight")
                        .HasColumnType("int");

                    b.Property<decimal>("WeightInKg")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("AthleteId", "Date");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AthleteId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.PostContent", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "ContentId");

                    b.HasIndex("ContentId");

                    b.ToTable("PostContents");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Subscribtion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoachId")
                        .HasColumnType("int");

                    b.Property<int>("DurationInMonths")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(8, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.ToTable("Subscribtions");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.SubscribtionFeature", b =>
                {
                    b.Property<int>("SubscribtionId")
                        .HasColumnType("int");

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.HasKey("SubscribtionId", "FeatureId");

                    b.HasIndex("FeatureId");

                    b.ToTable("SubscribtionFeatures");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Athlete", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.ApplicationUser", "ApplicationUser")
                        .WithOne("Athlete")
                        .HasForeignKey("AthleteHub.Domain.Entities.Athlete", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.AthleteActiveSubscribtion", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.Athlete", "Athlete")
                        .WithMany("AthletesActiveSubscribtions")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AthleteHub.Domain.Entities.Subscribtion", "Subscribtion")
                        .WithMany("AthletesActiveSubscribtions")
                        .HasForeignKey("SubscribtionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Subscribtion");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.AthleteCoach", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.Athlete", "Athlete")
                        .WithMany("AthletesCoaches")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AthleteHub.Domain.Entities.Coach", "Coach")
                        .WithMany("AthleteCoaches")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.AthleteFavouriteCoach", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.Athlete", "Athlete")
                        .WithMany("AthletesFavouriteCoaches")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AthleteHub.Domain.Entities.Coach", "Coach")
                        .WithMany("AthletesFavouriteCoaches")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Coach", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.ApplicationUser", "ApplicationUser")
                        .WithOne("Coach")
                        .HasForeignKey("AthleteHub.Domain.Entities.Coach", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.CoachBlockedAthlete", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.Athlete", "Athlete")
                        .WithMany("CoachesBlockedAthletees")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AthleteHub.Domain.Entities.Coach", "Coach")
                        .WithMany("CoachesBlockedAthletees")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.CoachRating", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.Athlete", "Athlete")
                        .WithMany("CoachesRatings")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AthleteHub.Domain.Entities.Coach", "Coach")
                        .WithMany("CoachesRatings")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Content", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.Coach", "Coach")
                        .WithMany("Contents")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Measurement", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.Athlete", "Athlete")
                        .WithMany("Measurements")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Post", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.Athlete", "Athlete")
                        .WithMany("Posts")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.PostContent", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.Content", "Content")
                        .WithMany("PostsContents")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AthleteHub.Domain.Entities.Post", "Post")
                        .WithMany("PostsContents")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Subscribtion", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.Coach", "Coach")
                        .WithMany("Subscribtions")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.SubscribtionFeature", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.Feature", "Feature")
                        .WithMany("SubscribtionsFeatures")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AthleteHub.Domain.Entities.Subscribtion", "Subscribtion")
                        .WithMany("SubscribtionsFeatures")
                        .HasForeignKey("SubscribtionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feature");

                    b.Navigation("Subscribtion");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AthleteHub.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AthleteHub.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.ApplicationUser", b =>
                {
                    b.Navigation("Athlete")
                        .IsRequired();

                    b.Navigation("Coach")
                        .IsRequired();
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Athlete", b =>
                {
                    b.Navigation("AthletesActiveSubscribtions");

                    b.Navigation("AthletesCoaches");

                    b.Navigation("AthletesFavouriteCoaches");

                    b.Navigation("CoachesBlockedAthletees");

                    b.Navigation("CoachesRatings");

                    b.Navigation("Measurements");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Coach", b =>
                {
                    b.Navigation("AthleteCoaches");

                    b.Navigation("AthletesFavouriteCoaches");

                    b.Navigation("CoachesBlockedAthletees");

                    b.Navigation("CoachesRatings");

                    b.Navigation("Contents");

                    b.Navigation("Subscribtions");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Content", b =>
                {
                    b.Navigation("PostsContents");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Feature", b =>
                {
                    b.Navigation("SubscribtionsFeatures");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Post", b =>
                {
                    b.Navigation("PostsContents");
                });

            modelBuilder.Entity("AthleteHub.Domain.Entities.Subscribtion", b =>
                {
                    b.Navigation("AthletesActiveSubscribtions");

                    b.Navigation("SubscribtionsFeatures");
                });
#pragma warning restore 612, 618
        }
    }
}
