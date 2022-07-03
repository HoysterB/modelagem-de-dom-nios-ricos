using MediatR;
using NerdStore.Catalogo.Domain.Interfaces.Repository;

namespace NerdStore.Catalogo.Domain.Events;

public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
{
    private readonly IProdutoRepository _produtoRepository;
    public ProdutoEventHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterPorId(mensagem.AggregateId);

        //Enviar um e-mail para aquisição de novos produtos 
    }
}