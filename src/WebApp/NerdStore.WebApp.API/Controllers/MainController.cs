using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.API.Controllers;

public abstract class MainController : ControllerBase
{
    private readonly DomainNotificationHandler _notifications;
    protected readonly IMediatorHandler _mediatorHandler;

    protected Guid ClienteId = Guid.NewGuid();

    protected MainController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
    {
        _notifications = (DomainNotificationHandler)notifications;
        _mediatorHandler = mediatorHandler;
    }

    protected bool OperacaoValida()
    {
        return (!_notifications.TemNotificacao());
    }

    protected IEnumerable<string> ObterMensagensErro()
    {
        return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
    }

    protected void NotificarErro(string codigo, string mensagem)
    {
        _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
    }

    protected IActionResult CustomBadRequest(object message = null)
    {
        return BadRequest(new
        {
            erros = ObterMensagensErro(),
            mensagem = message
        });
    }
}