﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using localinezationBackend.Services.Context;

#nullable disable

namespace localinezationBackend.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("localinezationBackend.Models.MediaItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CoverArt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("OriginalLanguage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Platform")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("MediaInfo");
                });

            modelBuilder.Entity("localinezationBackend.Models.RequestReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsVideo")
                        .HasColumnType("bit");

                    b.Property<string>("Src")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TranslationRequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TranslationRequestId");

                    b.ToTable("RequestReference");
                });

            modelBuilder.Entity("localinezationBackend.Models.TranslationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGuest")
                        .HasColumnType("bit");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TranslatedText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TranslationRequestId")
                        .HasColumnType("int");

                    b.Property<int>("TranslatorUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TranslationRequestId");

                    b.ToTable("Translations");
                });

            modelBuilder.Entity("localinezationBackend.Models.TranslationRequestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MediaId")
                        .HasColumnType("int");

                    b.Property<string>("RequestDialogue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestLanguage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestorUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MediaId");

                    b.ToTable("TranslationRequests");
                });

            modelBuilder.Entity("localinezationBackend.Models.UserModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("localinezationBackend.Models.RequestReference", b =>
                {
                    b.HasOne("localinezationBackend.Models.TranslationRequestModel", "TranslationRequest")
                        .WithMany("RequestReferences")
                        .HasForeignKey("TranslationRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TranslationRequest");
                });

            modelBuilder.Entity("localinezationBackend.Models.TranslationModel", b =>
                {
                    b.HasOne("localinezationBackend.Models.TranslationRequestModel", "TranslationRequest")
                        .WithMany("Translations")
                        .HasForeignKey("TranslationRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TranslationRequest");
                });

            modelBuilder.Entity("localinezationBackend.Models.TranslationRequestModel", b =>
                {
                    b.HasOne("localinezationBackend.Models.MediaItemModel", "Media")
                        .WithMany("TranslationRequests")
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Media");
                });

            modelBuilder.Entity("localinezationBackend.Models.MediaItemModel", b =>
                {
                    b.Navigation("TranslationRequests");
                });

            modelBuilder.Entity("localinezationBackend.Models.TranslationRequestModel", b =>
                {
                    b.Navigation("RequestReferences");

                    b.Navigation("Translations");
                });
#pragma warning restore 612, 618
        }
    }
}
