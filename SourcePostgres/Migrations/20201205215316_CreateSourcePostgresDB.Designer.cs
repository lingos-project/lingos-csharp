﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SourcePostgres;

namespace SourcePostgres.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20201205215316_CreateSourcePostgresDB")]
    partial class CreateSourcePostgresDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Common.Locale", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("Deprecated")
                        .HasColumnType("boolean");

                    b.Property<bool>("Required")
                        .HasColumnType("boolean");

                    b.HasKey("Name");

                    b.ToTable("Locales");
                });

            modelBuilder.Entity("Common.Scope", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("Deprecated")
                        .HasColumnType("boolean");

                    b.HasKey("Name");

                    b.ToTable("Scopes");
                });

            modelBuilder.Entity("Common.Translation", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("LocaleName")
                        .HasColumnType("text");

                    b.Property<string>("ScopeName")
                        .HasColumnType("text");

                    b.Property<string>("Variant")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("none");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Key", "LocaleName", "ScopeName", "Variant");

                    b.HasIndex("LocaleName");

                    b.HasIndex("ScopeName");

                    b.ToTable("Translations");
                });

            modelBuilder.Entity("Common.Translation", b =>
                {
                    b.HasOne("Common.Locale", "Locale")
                        .WithMany()
                        .HasForeignKey("LocaleName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.Scope", "Scope")
                        .WithMany()
                        .HasForeignKey("ScopeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Locale");

                    b.Navigation("Scope");
                });
#pragma warning restore 612, 618
        }
    }
}
