namespace NerdStore.Catalogo.Domain.Interfaces.DomainServices;

public interface IEstoqueService : IDisposable
{
    Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
    Task<bool> ReporEstoque(Guid produtoId, int quantidade);
}