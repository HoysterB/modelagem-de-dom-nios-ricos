﻿using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Vendas.Domain.Entities;
using NerdStore.Vendas.Domain.Enums;
using NerdStore.Vendas.Domain.Interfaces.Repository;

namespace NerdStore.Vendas.Data.Repository;

public class PedidoRepository : IPedidoRepository
{
    private readonly VendasContext _context;

    public PedidoRepository(VendasContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Pedido> ObterPorId(Guid id)
    {
        return await _context.Pedidos.FindAsync(id);
    }

    public async Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId)
    {
        return await _context.Pedidos.AsNoTracking().Where(p => p.ClienteId == clienteId).ToListAsync();
    }

    public async Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId)
    {
        var pedido =
            await _context.Pedidos.FirstOrDefaultAsync(p =>
                p.ClienteId == clienteId && p.PedidoStatus == EPedidoStatus.Rascunho);

        if (pedido == null) return null;

        await _context.Entry(pedido)
            .Collection(i => i.PedidosItems).LoadAsync();

        if (pedido.VoucherId != null)
        {
            await _context.Entry(pedido)
                .Reference(i => i.Voucher).LoadAsync();
        }

        return pedido;
    }

    public void Adicionar(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
    }

    public void Atualizar(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
    }


    public async Task<PedidoItem> ObterItemPorId(Guid id)
    {
        return await _context.PedidosItems.FindAsync(id);
    }

    public async Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId)
    {
        return await _context.PedidosItems.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.PedidoId == pedidoId);
    }

    public void AdicionarItem(PedidoItem pedidoItem)
    {
        _context.PedidosItems.Add(pedidoItem);
    }

    public void AtualizarItem(PedidoItem pedidoItem)
    {
        _context.PedidosItems.Update(pedidoItem);
    }

    public void RemoverItem(PedidoItem pedidoItem)
    {
        _context.PedidosItems.Remove(pedidoItem);
    }

    public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
    {
        return await _context.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}