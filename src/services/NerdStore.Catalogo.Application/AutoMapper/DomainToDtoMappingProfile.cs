using AutoMapper;
using NerdStore.Catalogo.Application.Dtos;
using NerdStore.Catalogo.Domain.Entities;

namespace NerdStore.Catalogo.Application.AutoMapper;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Categoria, CategoriaDto>();

        CreateMap<Produto, ProdutoDto>()
            .ForMember(dto => dto.Largura, o => o.MapFrom(s => s.Dimensoes.Largura))
            .ForMember(dto => dto.Altura, o => o.MapFrom(s => s.Dimensoes.Altura))
            .ForMember(dto => dto.Profundidade, o => o.MapFrom(s => s.Dimensoes.Profundidade));
    }
}