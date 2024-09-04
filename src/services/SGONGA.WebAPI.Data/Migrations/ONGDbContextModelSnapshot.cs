﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SGONGA.WebAPI.Data.Context;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations
{
    [DbContext(typeof(ONGDbContext))]
    partial class ONGDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.Colaborador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

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
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("Instagram")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("tbl_ongs", (string)null);
                });

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.SolicitacaoCadastro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChavePixOng")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimentoResponsavel")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentoOng")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("DocumentoResponsavel")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<Guid>("IdOng")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdResponsavel")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InstagramOng")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("NomeOng")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NomeResponsavel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("tbl_solicitacoes_cadastro", (string)null);
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

                    b.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Contato", "Contato", b1 =>
                        {
                            b1.Property<Guid>("ColaboradorId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ColaboradorId");

                            b1.ToTable("tbl_colaboradores");

                            b1.WithOwner()
                                .HasForeignKey("ColaboradorId");

                            b1.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Email", "Email", b2 =>
                                {
                                    b2.Property<Guid>("ContatoColaboradorId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Endereco")
                                        .IsRequired()
                                        .HasMaxLength(254)
                                        .HasColumnType("varchar(254)")
                                        .HasColumnName("Email");

                                    b2.HasKey("ContatoColaboradorId");

                                    b2.ToTable("tbl_colaboradores");

                                    b2.WithOwner()
                                        .HasForeignKey("ContatoColaboradorId");
                                });

                            b1.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Telefone", "Telefone", b2 =>
                                {
                                    b2.Property<Guid>("ContatoColaboradorId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Numero")
                                        .IsRequired()
                                        .HasMaxLength(15)
                                        .HasColumnType("varchar(15)")
                                        .HasColumnName("Telefone");

                                    b2.HasKey("ContatoColaboradorId");

                                    b2.ToTable("tbl_colaboradores");

                                    b2.WithOwner()
                                        .HasForeignKey("ContatoColaboradorId");
                                });

                            b1.Navigation("Email")
                                .IsRequired();

                            b1.Navigation("Telefone")
                                .IsRequired();
                        });

                    b.Navigation("Contato")
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
                                        .HasColumnType("varchar(254)")
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
                                        .HasColumnType("varchar(15)")
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

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("CEP")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("varchar(20)");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Complemento")
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Estado")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Logradouro")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<int>("Numero")
                                .HasColumnType("int");

                            b1.Property<string>("Referencia")
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

            modelBuilder.Entity("SGONGA.WebAPI.Business.Models.SolicitacaoCadastro", b =>
                {
                    b.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Contato", "ContatoOng", b1 =>
                        {
                            b1.Property<Guid>("SolicitacaoCadastroId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("SolicitacaoCadastroId");

                            b1.ToTable("tbl_solicitacoes_cadastro");

                            b1.WithOwner()
                                .HasForeignKey("SolicitacaoCadastroId");

                            b1.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Email", "Email", b2 =>
                                {
                                    b2.Property<Guid>("ContatoSolicitacaoCadastroId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Endereco")
                                        .IsRequired()
                                        .HasMaxLength(254)
                                        .HasColumnType("varchar(254)")
                                        .HasColumnName("EmailOng");

                                    b2.HasKey("ContatoSolicitacaoCadastroId");

                                    b2.ToTable("tbl_solicitacoes_cadastro");

                                    b2.WithOwner()
                                        .HasForeignKey("ContatoSolicitacaoCadastroId");
                                });

                            b1.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Telefone", "Telefone", b2 =>
                                {
                                    b2.Property<Guid>("ContatoSolicitacaoCadastroId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Numero")
                                        .IsRequired()
                                        .HasMaxLength(15)
                                        .HasColumnType("varchar(15)")
                                        .HasColumnName("TelefoneOng");

                                    b2.HasKey("ContatoSolicitacaoCadastroId");

                                    b2.ToTable("tbl_solicitacoes_cadastro");

                                    b2.WithOwner()
                                        .HasForeignKey("ContatoSolicitacaoCadastroId");
                                });

                            b1.Navigation("Email")
                                .IsRequired();

                            b1.Navigation("Telefone")
                                .IsRequired();
                        });

                    b.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Contato", "ContatoResponsavel", b1 =>
                        {
                            b1.Property<Guid>("SolicitacaoCadastroId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("SolicitacaoCadastroId");

                            b1.ToTable("tbl_solicitacoes_cadastro");

                            b1.WithOwner()
                                .HasForeignKey("SolicitacaoCadastroId");

                            b1.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Email", "Email", b2 =>
                                {
                                    b2.Property<Guid>("ContatoSolicitacaoCadastroId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Endereco")
                                        .IsRequired()
                                        .HasMaxLength(254)
                                        .HasColumnType("varchar(254)")
                                        .HasColumnName("EmailResponsavel");

                                    b2.HasKey("ContatoSolicitacaoCadastroId");

                                    b2.ToTable("tbl_solicitacoes_cadastro");

                                    b2.WithOwner()
                                        .HasForeignKey("ContatoSolicitacaoCadastroId");
                                });

                            b1.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Telefone", "Telefone", b2 =>
                                {
                                    b2.Property<Guid>("ContatoSolicitacaoCadastroId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Numero")
                                        .IsRequired()
                                        .HasMaxLength(15)
                                        .HasColumnType("varchar(15)")
                                        .HasColumnName("TelefoneResponsavel");

                                    b2.HasKey("ContatoSolicitacaoCadastroId");

                                    b2.ToTable("tbl_solicitacoes_cadastro");

                                    b2.WithOwner()
                                        .HasForeignKey("ContatoSolicitacaoCadastroId");
                                });

                            b1.Navigation("Email")
                                .IsRequired();

                            b1.Navigation("Telefone")
                                .IsRequired();
                        });

                    b.OwnsOne("SGONGA.WebAPI.Business.Models.DomainObjects.Endereco", "EnderecoOng", b1 =>
                        {
                            b1.Property<Guid>("SolicitacaoCadastroId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("CEP")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("varchar(20)");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Complemento")
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Estado")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Logradouro")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<int>("Numero")
                                .HasColumnType("int");

                            b1.Property<string>("Referencia")
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.HasKey("SolicitacaoCadastroId");

                            b1.ToTable("tbl_solicitacoes_cadastro");

                            b1.WithOwner()
                                .HasForeignKey("SolicitacaoCadastroId");
                        });

                    b.Navigation("ContatoOng")
                        .IsRequired();

                    b.Navigation("ContatoResponsavel")
                        .IsRequired();

                    b.Navigation("EnderecoOng")
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
