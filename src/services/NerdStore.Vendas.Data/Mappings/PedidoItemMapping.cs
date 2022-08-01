using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain.Entities;

namespace NerdStore.Vendas.Data.Mappings;

public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.HasKey(c => c.Id);


        builder.Property(c => c.ProdutoNome)
            .IsRequired()
            .HasColumnType("varchar(250)");

        // 1 : N => Pedido : Pagamento
        builder.HasOne(c => c.Pedido)
            .WithMany(c => c.PedidosItems);

        builder.ToTable("PedidoItems");
    }
}