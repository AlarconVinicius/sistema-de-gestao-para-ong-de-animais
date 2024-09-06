using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Models.DomainObjects;

namespace SGONGA.WebAPI.Data.Mappings;
public class SolicitacaoCadastroMapping : IEntityTypeConfiguration<SolicitacaoCadastro>
{
    public void Configure(EntityTypeBuilder<SolicitacaoCadastro> builder)
    {
        builder.ToTable("tbl_solicitacoes_cadastro");

        builder.HasKey(a => a.Id);

        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();

        builder.Property(o => o.NomeOng)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(o => o.InstagramOng)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Property(c => c.DocumentoOng)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnType("varchar(11)");

        builder.Property(o => o.ChavePixOng)
               .IsRequired(false)
               .HasMaxLength(100)
               .HasColumnType("varchar(100)");

        builder.OwnsOne(o => o.ContatoOng, c =>
        {
            c.OwnsOne(c => c.Telefone, t =>
            {
                t.Property(t => t.Numero)
                    .HasColumnName("TelefoneOng")
                    .HasColumnType("Telefone")
                    .IsRequired()
                    .HasMaxLength(Telefone.ComprimentoMaxNumero)
                    .HasColumnType($"varchar({Telefone.ComprimentoMaxNumero})");
            });

            c.OwnsOne(c => c.Email, t =>
            {
                t.Property(t => t.Endereco)
                    .HasColumnName("EmailOng")
                    .IsRequired()
                    .HasMaxLength(Email.ComprimentoMaxEndereco)
                    .HasColumnType($"varchar({Email.ComprimentoMaxEndereco})");
            });
        });

        builder.OwnsOne(o => o.EnderecoOng, e =>
        {
            e.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            e.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            e.Property(e => e.CEP)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            e.Property(e => e.Logradouro)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            e.Property(e => e.Bairro)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            e.Property(e => e.Numero)
                .IsRequired();

            e.Property(e => e.Complemento)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            e.Property(e => e.Referencia)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");
        });

        builder.Property(o => o.NomeResponsavel)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(c => c.DocumentoResponsavel)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnType("varchar(11)");

        builder.Property(c => c.DataNascimentoResponsavel)
                .IsRequired();

        builder.OwnsOne(o => o.ContatoResponsavel, c =>
        {
            c.OwnsOne(c => c.Telefone, t =>
            {
                t.Property(t => t.Numero)
                    .HasColumnName("TelefoneResponsavel")
                    .HasColumnType("Telefone")
                    .IsRequired()
                    .HasMaxLength(Telefone.ComprimentoMaxNumero)
                    .HasColumnType($"varchar({Telefone.ComprimentoMaxNumero})");
            });

            c.OwnsOne(c => c.Email, t =>
            {
                t.Property(t => t.Endereco)
                    .HasColumnName("EmailResponsavel")
                    .IsRequired()
                    .HasMaxLength(Email.ComprimentoMaxEndereco)
                    .HasColumnType($"varchar({Email.ComprimentoMaxEndereco})");
            });
        });

        builder.Property(c => c.Status)
                .IsRequired();
    }
}