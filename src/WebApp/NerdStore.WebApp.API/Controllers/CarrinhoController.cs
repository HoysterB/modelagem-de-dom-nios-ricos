using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Application.Commands;

namespace NerdStore.WebApp.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CarrinhoController : MainController
{
    private readonly IProdutoAppService _produtoAppService;

    public CarrinhoController(INotificationHandler<DomainNotification> notifications,
        IMediatorHandler mediatorHandler, IProdutoAppService produtoAppService) : base(notifications, mediatorHandler)
    {
        _produtoAppService = produtoAppService;
    }

    [HttpPost("meu-carrinho")]
    public async Task<IActionResult> AdicionarItem(Guid produtoId, int quantidade)
    {
        var produto = await _produtoAppService.ObterPorId(produtoId);
        if (produto == null)
        {
            NotificarErro(nameof(AdicionarItem),"Pedido não encontrado");
            return CustomBadRequest();
        }

        if (produto.QuantidadeEstoque < quantidade)
        {
            NotificarErro(nameof(AdicionarItem), "Pedido sem estoque");
            return CustomBadRequest();
        }

        var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
        await _mediatorHandler.EnviarComando(command);

        if (OperacaoValida())
        {
            return NoContent();
        }

        return CustomBadRequest();
    }
}