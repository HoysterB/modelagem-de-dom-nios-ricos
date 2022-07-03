using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;

namespace NerdStore.WebApp.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VitrineController : ControllerBase
{
    private readonly IProdutoAppService _produtoAppService;
    public VitrineController(IProdutoAppService produtoAppService)
    {
        _produtoAppService = produtoAppService;
    }

    [HttpGet]
    public async Task<IActionResult> Vitrine()
    {
        return Ok(await _produtoAppService.ObterTodos());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ProdutoDetalhe(Guid id)
    {
        return Ok(await _produtoAppService.ObterPorId(id));
    }
}