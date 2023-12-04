﻿// <auto-generated />
using System;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateOfBirth")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

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
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpecializationId")
                        .HasColumnType("int");

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

                    b.HasIndex("SpecializationId");

                    b.ToTable("AspNetUsers", null, t =>
                        {
                            t.HasCheckConstraint("CK_AspNetUser_Email", "Email LIKE '%[a-zA-Z0-9.!#$%&''*+/=?^_`{|}~-]%@[a-zA-Z0-9-]%'");

                            t.HasCheckConstraint("CK_AspNetUser_Gender", "Gender IN ('Male', 'Female')");
                        });

                    b.HasData(
                        new
                        {
                            Id = "0d893228-14b9-446d-b727-442a0d353458",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "49898182-20cd-4d75-a2d6-e6d95d1d565e",
                            DateOfBirth = "18/03/1999",
                            Email = "sragmahmoud4@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Mahmoud",
                            Gender = "Male",
                            LastName = "Serag",
                            LockoutEnabled = false,
                            PasswordHash = "Admin123",
                            PhoneNumber = "+201064560413",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8b0d81f1-6574-4e03-998a-bd48b76765bf",
                            TwoFactorEnabled = false
                        });
                });

            modelBuilder.Entity("Core.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayId")
                        .HasColumnType("int");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Core.Models.AppointmentTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsBooked")
                        .HasColumnType("bit");

                    b.Property<int>("TimeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("TimeId");

                    b.ToTable("AppointmentTimes");
                });

            modelBuilder.Entity("Core.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentTimeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DiscountId")
                        .HasColumnType("int");

                    b.Property<int>("FinalPrice")
                        .HasColumnType("int");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentTimeId")
                        .IsUnique();

                    b.HasIndex("DiscountId")
                        .IsUnique()
                        .HasFilter("[DiscountId] IS NOT NULL");

                    b.HasIndex("PatientId");

                    b.HasIndex("StatusId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Core.Models.BookingStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("Id");

                    b.ToTable("BookingStatus", t =>
                        {
                            t.HasCheckConstraint("CK_BookingStatus_Name", "Name IN ('Pending', 'Completed', 'Cancelled')");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Completed"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("Core.Models.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("Id");

                    b.ToTable("Days", t =>
                        {
                            t.HasCheckConstraint("CK_Day_Name", "Name IN ('Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday')");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Saturday"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sunday"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Monday"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Tuesday"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Wednesday"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Thursday"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Friday"
                        });
                });

            modelBuilder.Entity("Core.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiscountCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DiscountTypeId")
                        .HasColumnType("int");

                    b.Property<int>("DiscountValue")
                        .HasColumnType("int");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DiscountCode")
                        .IsUnique();

                    b.HasIndex("DiscountTypeId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Core.Models.DiscountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("Id");

                    b.ToTable("DiscountTypes", t =>
                        {
                            t.HasCheckConstraint("CK_DiscountType_Name", "Name IN ('Percentage', 'Value')");
                        });
                });

            modelBuilder.Entity("Core.Models.ExaminationPrice", b =>
                {
                    b.Property<string>("DoctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("DoctorId");

                    b.ToTable("ExaminationPrices");
                });

            modelBuilder.Entity("Core.Models.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("Core.Models.Time", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TimeValue")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("TimeValue")
                        .IsUnique();

                    b.ToTable("Times");
                });

            modelBuilder.Entity("Core.Models.UserBookingTracking", b =>
                {
                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ApprovedBookingCount")
                        .HasColumnType("int");

                    b.HasKey("PatientId");

                    b.ToTable("UserBookingTrackings");
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

                    b.HasData(
                        new
                        {
                            Id = "c4082530-a0e2-442d-8ddf-3473948e2add",
                            Name = "Patient"
                        },
                        new
                        {
                            Id = "f8cacecb-c5d7-4a10-a360-a42c4c8cc7aa",
                            Name = "Doctor"
                        },
                        new
                        {
                            Id = "1b53555c-e5e3-4e66-ab07-26eb7f5f57e3",
                            Name = "Admin"
                        });
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

            modelBuilder.Entity("Core.Models.ApplicationUser", b =>
                {
                    b.HasOne("Core.Models.Specialization", "Specialization")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecializationId");

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("Core.Models.Appointment", b =>
                {
                    b.HasOne("Core.Models.Day", "Day")
                        .WithMany("Appointments")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.ApplicationUser", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Core.Models.AppointmentTime", b =>
                {
                    b.HasOne("Core.Models.Appointment", "Appointment")
                        .WithMany("AppointmentTimes")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Time", "Time")
                        .WithMany("AppointmentTimes")
                        .HasForeignKey("TimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Time");
                });

            modelBuilder.Entity("Core.Models.Booking", b =>
                {
                    b.HasOne("Core.Models.AppointmentTime", "AppointmentTime")
                        .WithOne("Booking")
                        .HasForeignKey("Core.Models.Booking", "AppointmentTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Discount", "Discount")
                        .WithOne("Booking")
                        .HasForeignKey("Core.Models.Booking", "DiscountId");

                    b.HasOne("Core.Models.ApplicationUser", "Patient")
                        .WithMany("Bookings")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Core.Models.BookingStatus", "BookingStatus")
                        .WithMany("Bookings")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppointmentTime");

                    b.Navigation("BookingStatus");

                    b.Navigation("Discount");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Core.Models.Discount", b =>
                {
                    b.HasOne("Core.Models.DiscountType", "DiscountType")
                        .WithMany("Discounts")
                        .HasForeignKey("DiscountTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiscountType");
                });

            modelBuilder.Entity("Core.Models.ExaminationPrice", b =>
                {
                    b.HasOne("Core.Models.ApplicationUser", "Doctor")
                        .WithOne("ExaminationPrice")
                        .HasForeignKey("Core.Models.ExaminationPrice", "DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Core.Models.UserBookingTracking", b =>
                {
                    b.HasOne("Core.Models.ApplicationUser", "Patient")
                        .WithOne("UserBookingTracking")
                        .HasForeignKey("Core.Models.UserBookingTracking", "PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
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
                    b.HasOne("Core.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Core.Models.ApplicationUser", null)
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

                    b.HasOne("Core.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Core.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.ApplicationUser", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Bookings");

                    b.Navigation("ExaminationPrice")
                        .IsRequired();

                    b.Navigation("UserBookingTracking")
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.Appointment", b =>
                {
                    b.Navigation("AppointmentTimes");
                });

            modelBuilder.Entity("Core.Models.AppointmentTime", b =>
                {
                    b.Navigation("Booking")
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.BookingStatus", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Core.Models.Day", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Core.Models.Discount", b =>
                {
                    b.Navigation("Booking")
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.DiscountType", b =>
                {
                    b.Navigation("Discounts");
                });

            modelBuilder.Entity("Core.Models.Specialization", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("Core.Models.Time", b =>
                {
                    b.Navigation("AppointmentTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
