using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.API.Extensions;

public class ErrorFilter : IAsyncResultFilter
{
    private readonly DomainNotificationHandler _notifications;
    public ErrorFilter(INotificationHandler<DomainNotification> notifications)
    {
        _notifications = (DomainNotificationHandler)notifications;
    }
 
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var notifications = await Task.FromResult(_notifications.ObterNotificacoes());
        notifications.ForEach(c => context.ModelState.AddModelError(string.Empty ,c.Value));

        await next();
    }
}