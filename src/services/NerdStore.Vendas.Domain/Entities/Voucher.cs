using NerdStore.Core.DomainObjects;
using NerdStore.Vendas.Domain.Enums;

namespace NerdStore.Vendas.Domain.Entities;

public class Voucher : Entity
{
    public string Codigo { get; private set; }
    public decimal? Percentual { get; private set; }
    public decimal? ValorDesconto { get; private set; }
    public int Quantidade { get; private set; }
    public ETipoDescontoVoucher TipoDescontoVoucher { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataUtilizacao { get; private set; }
    public DateTime DataValidade { get; private set; }
    public bool Ativo { get; private set; }
    public bool Utilizado { get; private set; }

    public ICollection<Pedido> Pedidos { get; set; }
    public override bool EhValido()
    {
        throw new NotImplementedException();
    }
}