using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.ValueObjects;
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
                new Produto(string.Empty, "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "imagem.jpg", new Dimensoes(10, 10, 10)));

            Assert.Equal("O campo nome do produto não pode estar vazio", ex.Message);
        }
    }
}