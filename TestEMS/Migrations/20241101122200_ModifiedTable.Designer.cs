﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestEMS.Data;

#nullable disable

namespace TestEMS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241101122200_ModifiedTable")]
    partial class ModifiedTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestEMS.Models.ActivityTypes", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityId"));

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsActive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActivityId");

                    b.ToTable("ActivityTypes");
                });

            modelBuilder.Entity("TestEMS.Models.EmployeeData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsActive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmployeeData");
                });

            modelBuilder.Entity("TestEMS.Models.MyActivity", b =>
                {
                    b.Property<string>("ActivityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActivityDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActivityTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ActivityTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("AssignedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("AssignedTo")
                        .HasColumnType("int");

                    b.Property<string>("PageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("ToDoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActivityId");

                    b.HasIndex("ActivityTypeId");

                    b.HasIndex("AssignedBy");

                    b.HasIndex("AssignedTo");

                    b.HasIndex("ProjectId");

                    b.ToTable("MyActivity");
                });

            modelBuilder.Entity("TestEMS.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<string>("IsActive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("TestEMS.Models.ToDo", b =>
                {
                    b.Property<string>("ToDoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ActivityTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("AssignedTo")
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ToDoId");

                    b.HasIndex("ActivityTypeId");

                    b.HasIndex("AssignedTo");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ProjectId");

                    b.ToTable("ToDo");
                });

            modelBuilder.Entity("TestEMS.Models.MyActivity", b =>
                {
                    b.HasOne("TestEMS.Models.ActivityTypes", "ActivityType")
                        .WithMany()
                        .HasForeignKey("ActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestEMS.Models.EmployeeData", "AssignedByEmployeeData")
                        .WithMany()
                        .HasForeignKey("AssignedBy");

                    b.HasOne("TestEMS.Models.EmployeeData", "AssignedToEmployeeData")
                        .WithMany()
                        .HasForeignKey("AssignedTo");

                    b.HasOne("TestEMS.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActivityType");

                    b.Navigation("AssignedByEmployeeData");

                    b.Navigation("AssignedToEmployeeData");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TestEMS.Models.ToDo", b =>
                {
                    b.HasOne("TestEMS.Models.ActivityTypes", "ActivityType")
                        .WithMany()
                        .HasForeignKey("ActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestEMS.Models.EmployeeData", "AssignedByEmployeeData")
                        .WithMany()
                        .HasForeignKey("AssignedTo");

                    b.HasOne("TestEMS.Models.EmployeeData", "AddedByEmployeeData")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("TestEMS.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActivityType");

                    b.Navigation("AddedByEmployeeData");

                    b.Navigation("AssignedByEmployeeData");

                    b.Navigation("Project");
                });
#pragma warning restore 612, 618
        }
    }
}