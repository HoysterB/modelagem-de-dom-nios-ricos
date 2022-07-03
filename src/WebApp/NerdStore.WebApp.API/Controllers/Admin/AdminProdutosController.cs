using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Dtos;
using NerdStore.Catalogo.Application.Services;

namespace NerdStore.WebApp.API.Controllers.Admin;

[ApiController]
[Route("api/v1/[controller]")]
public class AdminProdutosController : ControllerBase
{
    private readonly IProdutoAppService _produtoAppService;

    public AdminProdutosController(IProdutoAppService produtoAppService)
    {
        _produtoAppService = produtoAppService;
    }

    [HttpGet("obter-todos-produtos")]
    public async Task<IActionResult> ObterTodos()
    {
        return Ok(await _produtoAppService.ObterTodos());
    }

    [HttpPost("novo-produto")]
    public async Task<IActionResult> NovoProduto(ProdutoDto produtoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _produtoAppService.AdicionarProduto(produtoDto);

        return NoContent();
    }

    [HttpPost("nova-categoria")]
    public async Task<IActionResult> NovaCategoria(CategoriaDto categoriaDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _produtoAppService.AdicionarCategoria(categoriaDto);

        return NoContent();
    }

    [HttpGet("obter-categorias")]
    public async Task<IActionResult> ObterCategorias()
    {
        return Ok(await _produtoAppService.ObterCategorias());
    }

    [HttpPut("produtos-atualizar-estoque")]
    public async Task<IActionResult> AtualizarEstoque(Guid id, int quantidade)
    {
        if (quantidade > 0)
        {
            return Ok(await _produtoAppService.ReporEstoque(id, quantidade));
        }
        else
        {
            return Ok(await _produtoAppService.DebitarEstoque(id, quantidade));
        }
    }

    [HttpPut("editar-produto")]
    public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoDto produtoDto)
    {
        var produto = await _produtoAppService.ObterPorId(id);
        produtoDto.QuantidadeEstoque = produto.QuantidadeEstoque;

        ModelState.Remove("QuantidadeEstoque");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _produtoAppService.AtualizarProduto(produtoDto);

        return NoContent();
    }
}