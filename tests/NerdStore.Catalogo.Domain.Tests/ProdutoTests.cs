using System;
using NerdStore.Core.DomainObjects;
using Xunit;

namespace NerdStore.Catalogo.Domain.Tests
{
    public class ProdutoTests
    {
        [Fact]
        public void Produto_Validar_ValidacoesDevemRetornarExceptions()
        {
            var ex = Assert.Throws<DomainException>(() =>
            new Produto(string.Empty, "Descricao", false, 100, DateTime.Now, "image", Guid.NewGuid(), new Dimensoes(1, 3, 4)));

            Assert.Equal("O campo nome do produto não pode estar vazio", ex.Message);
        }
    }
}
