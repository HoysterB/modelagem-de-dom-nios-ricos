using MediatR;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Data;
using NerdStore.Catalogo.Data.Repository;
using NerdStore.Catalogo.Domain.DomainServices;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Interfaces.DomainServices;
using NerdStore.Catalogo.Domain.Interfaces.Repository;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Data;
using NerdStore.Vendas.Data.Repository;
using NerdStore.Vendas.Domain.Interfaces.Repository;

namespace NerdStore.WebApp.API.Setup;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Mediator
        services.AddScoped<IMediatorHandler, MediatorHandler>();
     
        //Notifications
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        //Catalogo
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoAppService, ProdutoAppService>();
        services.AddScoped<IEstoqueService, EstoqueService>();
        services.AddDbContext<CatalogoContext>(sp => sp.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();


        //Vendas
        services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddDbContext<VendasContext>(sp => sp.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}