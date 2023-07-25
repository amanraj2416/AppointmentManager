﻿// <auto-generated />
using System;
using AppointmentManagerWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppointmentManagerWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230719103332_updateAppointment")]
    partial class updateAppointment
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppointmentManagerWebApp.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime?>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AppointmentTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DoctorIdFk")
                        .HasColumnType("int");

                    b.Property<string>("PatientEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DoctorIdFk");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("AppointmentManagerWebApp.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"));

                    b.Property<int>("AppointmentSlotTime")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("DayEndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("DayStartTime")
                        .HasColumnType("time");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DoctorId");

                    b.HasIndex("UserId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("AppointmentManagerWebApp.Models.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("AppointmentManagerWebApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AppointmentManagerWebApp.Models.Appointment", b =>
                {
                    b.HasOne("AppointmentManagerWebApp.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorIdFk");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("AppointmentManagerWebApp.Models.Doctor", b =>
                {
                    b.HasOne("AppointmentManagerWebApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
