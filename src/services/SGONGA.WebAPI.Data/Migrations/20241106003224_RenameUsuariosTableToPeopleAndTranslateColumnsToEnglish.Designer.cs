﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SGONGA.WebAPI.Data.Context;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations
{
    [DbContext(typeof(ONGDbContext))]
    [Migration("20241106003224_RenameUsuariosTableToPeopleAndTranslateColumnsToEnglish")]
    partial class RenameUsuariosTableToPeopleAndTranslateColumnsToEnglish
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SGONGA.WebAPI.Business.Animals.Entities.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Castrado")
                        .HasColumnType("bit");

                    b.Property<string>("ChavePix")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Especie")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Idade")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Porte")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Raca")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Sexo")
                        .HasColumnType("bit");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("tbl_animais", (string)null);
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.People.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPhoneNumberVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("SubscribeToNewsletter")
                        .HasColumnType("bit");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tbl_people", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.Adopter", b =>
                {
                    b.HasBaseType("SGONGA.WebAPI.Business.People.Entities.Person");

                    b.ToTable("tbl_adopters", (string)null);
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.People.Entities.NGO", b =>
                {
                    b.HasBaseType("SGONGA.WebAPI.Business.People.Entities.Person");

                    b.Property<string>("PixKey")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.ToTable("tbl_ngos", (string)null);
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.Animals.Entities.Animal", b =>
                {
                    b.HasOne("SGONGA.WebAPI.Business.People.Entities.NGO", "ONG")
                        .WithMany("Animals")
                        .HasForeignKey("TenantId")
                        .HasPrincipalKey("TenantId")
                        .IsRequired();

                    b.Navigation("ONG");
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.People.Entities.Person", b =>
                {
                    b.OwnsOne("SGONGA.WebAPI.Business.People.ValueObjects.Document", "Document", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(14)
                                .HasColumnType("varchar(14)")
                                .HasColumnName("Document");

                            b1.HasKey("PersonId");

                            b1.ToTable("tbl_people");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("SGONGA.WebAPI.Business.People.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(254)
                                .HasColumnType("varchar(254)")
                                .HasColumnName("Email");

                            b1.HasKey("PersonId");

                            b1.ToTable("tbl_people");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("SGONGA.WebAPI.Business.People.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("varchar(11)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("PersonId");

                            b1.ToTable("tbl_people");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("SGONGA.WebAPI.Business.People.ValueObjects.Site", "Site", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("varchar(200)")
                                .HasColumnName("Site");

                            b1.HasKey("PersonId");

                            b1.ToTable("tbl_people");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("SGONGA.WebAPI.Business.People.ValueObjects.Slug", "Slug", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("UrlPath")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Slug");

                            b1.HasKey("PersonId");

                            b1.ToTable("tbl_people");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("Document")
                        .IsRequired();

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("PhoneNumber")
                        .IsRequired();

                    b.Navigation("Site")
                        .IsRequired();

                    b.Navigation("Slug")
                        .IsRequired();
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.Adopter", b =>
                {
                    b.HasOne("SGONGA.WebAPI.Business.People.Entities.Person", null)
                        .WithOne()
                        .HasForeignKey("SGONGA.WebAPI.Business.Models.Adopter", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.People.Entities.NGO", b =>
                {
                    b.HasOne("SGONGA.WebAPI.Business.People.Entities.Person", null)
                        .WithOne()
                        .HasForeignKey("SGONGA.WebAPI.Business.People.Entities.NGO", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.People.Entities.NGO", b =>
                {
                    b.Navigation("Animals");
                });
#pragma warning restore 612, 618
        }
    }
}
