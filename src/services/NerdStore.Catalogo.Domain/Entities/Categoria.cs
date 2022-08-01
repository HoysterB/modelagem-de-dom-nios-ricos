using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain.Entities;

public class Categoria : Entity
{
    public string Nome { get; private set; }
    public int Codigo { get; private set; }

    public ICollection<Produto> Produtos { get; set; }

    protected Categoria() { }

    public Categoria(string nome, int codigo)
    {
        Nome = nome;
        Codigo = codigo;

        EhValido();
    }

    public override string ToString()
    {
        return $"{Nome} - {Codigo}";
    }

    public override bool EhValido()
    {
        Validacoes.ValidarSeVazio(Nome, "O campo nome da categoria não pode estar vazio");
        Validacoes.ValidarSeIgual(Codigo, 0, "O Campo codigo não pode ser 0");

        return true;
    }
}