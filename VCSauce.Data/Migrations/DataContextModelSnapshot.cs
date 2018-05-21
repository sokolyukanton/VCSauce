﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using VCSauce.Data.Entities;

namespace VCSauce.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VCSauce.Data.Entities.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<int>("State");

                    b.Property<int>("Type");

                    b.Property<int?>("VersionId");

                    b.HasKey("Id");

                    b.HasIndex("VersionId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("VCSauce.Data.Entities.Repository", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<string>("StoragePath")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Path")
                        .IsUnique();

                    b.ToTable("Repositories");
                });

            modelBuilder.Entity("VCSauce.Data.Entities.Version", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Label")
                        .IsRequired();

                    b.Property<int?>("RepositoryId");

                    b.HasKey("Id");

                    b.HasIndex("RepositoryId");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("VCSauce.Data.Entities.File", b =>
                {
                    b.HasOne("VCSauce.Data.Entities.Version")
                        .WithMany("Files")
                        .HasForeignKey("VersionId");
                });

            modelBuilder.Entity("VCSauce.Data.Entities.Version", b =>
                {
                    b.HasOne("VCSauce.Data.Entities.Repository")
                        .WithMany("Versions")
                        .HasForeignKey("RepositoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
