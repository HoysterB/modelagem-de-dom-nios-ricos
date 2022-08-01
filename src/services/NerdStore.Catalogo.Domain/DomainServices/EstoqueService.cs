using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Interfaces.DomainServices;
using NerdStore.Catalogo.Domain.Interfaces.Repository;
using NerdStore.Core.Communication.Mediator;

namespace NerdStore.Catalogo.Domain.DomainServices;

public class EstoqueService : IEstoqueService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMediatorHandler _mediatorHandler;
    public EstoqueService(IProdutoRepository produtoRepository, IMediatorHandler mediatorHandler)
    {
        _produtoRepository = produtoRepository;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterPorId(produtoId);

        if (produto == null) return false;

        if (produto.PossuiEstoque(quantidade) is false) return false;

        produto.DebitarEstoque(quantidade);

        //TODO: Parametrizar a quantidade de estoque baixo
        if (produto.QuantidadeEstoque < 10)
        {
            await _mediatorHandler.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produtoId, produto.QuantidadeEstoque));
        }

        _produtoRepository.Atualizar(produto);
        return await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterPorId(produtoId);

        if (produto == null) return false;

        produto.ReporEstoque(quantidade);

        _produtoRepository.Atualizar(produto);
        return await _produtoRepository.UnitOfWork.Commit();
    }

    public void Dispose()
    {
        _produtoRepository.Dispose();
    }
}