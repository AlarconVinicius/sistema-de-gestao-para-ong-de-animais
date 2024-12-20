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
    [DbContext(typeof(OrganizationDbContext))]
    [Migration("20240713162647_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChavePix")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Especie")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Fotos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Porte")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Raca")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("tbl_animais", (string)null);
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.Colaborador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("tbl_colaboradores", (string)null);
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.ONG", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChavePix")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("tbl_ongs", (string)null);
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.Animal", b =>
                {
                    b.HasOne("SGONGA.WebAPI.Business.Models.ONG", "ONG")
                        .WithMany("Animais")
                        .HasForeignKey("TenantId")
                        .IsRequired();

                    b.Navigation("ONG");
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.Colaborador", b =>
                {
                    b.HasOne("SGONGA.WebAPI.Business.Models.ONG", "ONG")
                        .WithMany("Colaboradores")
                        .HasForeignKey("TenantId")
                        .IsRequired();

                    b.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("ColaboradorId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Endereco")
                                .IsRequired()
                                .HasMaxLength(254)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Email");

                            b1.HasKey("ColaboradorId");

                            b1.ToTable("tbl_colaboradores");

                            b1.WithOwner()
                                .HasForeignKey("ColaboradorId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("ONG");
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.ONG", b =>
                {
                    b.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Contato", "Contato", b1 =>
                        {
                            b1.Property<Guid>("ONGId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ONGId");

                            b1.ToTable("tbl_ongs");

                            b1.WithOwner()
                                .HasForeignKey("ONGId");

                            b1.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Email", "Email", b2 =>
                                {
                                    b2.Property<Guid>("ContatoONGId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Endereco")
                                        .IsRequired()
                                        .HasMaxLength(254)
                                        .HasColumnType("nvarchar(254)")
                                        .HasColumnName("Email");

                                    b2.HasKey("ContatoONGId");

                                    b2.ToTable("tbl_ongs");

                                    b2.WithOwner()
                                        .HasForeignKey("ContatoONGId");
                                });

                            b1.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Telefone", "Telefone", b2 =>
                                {
                                    b2.Property<Guid>("ContatoONGId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Numero")
                                        .IsRequired()
                                        .HasMaxLength(15)
                                        .HasColumnType("varchar(100)")
                                        .HasColumnName("Telefone");

                                    b2.HasKey("ContatoONGId");

                                    b2.ToTable("tbl_ongs");

                                    b2.WithOwner()
                                        .HasForeignKey("ContatoONGId");
                                });

                            b1.Navigation("Email")
                                .IsRequired();

                            b1.Navigation("Telefone")
                                .IsRequired();
                        });

                    b.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Endereco", "Endereco", b1 =>
                        {
                            b1.Property<Guid>("ONGId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CEP")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Complemento")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Estado")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Rua")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.HasKey("ONGId");

                            b1.ToTable("tbl_ongs");

                            b1.WithOwner()
                                .HasForeignKey("ONGId");
                        });

                    b.Navigation("Contato")
                        .IsRequired();

                    b.Navigation("Endereco")
                        .IsRequired();
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.ONG", b =>
                {
                    b.Navigation("Animais");

                    b.Navigation("Colaboradores");
                });
#pragma warning restore 612, 618
        }
    }
}
