using MediatR;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Data;
using NerdStore.Catalogo.Data.Repository;
using NerdStore.Catalogo.Domain.DomainServices;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Interfaces.DomainServices;
using NerdStore.Catalogo.Domain.Interfaces.Repository;
using NerdStore.Core.Mediator;

namespace NerdStore.WebApp.API.Setup;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Mediator
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

        //Catalogo
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoAppService, ProdutoAppService>();
        services.AddScoped<IEstoqueService, EstoqueService>();
        services.AddDbContext<CatalogoContext>(sp => sp.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


    }
}