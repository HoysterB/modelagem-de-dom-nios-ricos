using NerdStore.Core.Messages;

namespace NerdStore.Core.Mediator;

public interface IMediatorHandler
{
    Task PublicarEvento<T>(T evento) where T : Event;
}