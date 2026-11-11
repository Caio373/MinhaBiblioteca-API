namespace MinhaBiblioteca.Models;

public class Livro : MaterialBiblioteca, Emprestimo
{
    public string Autor { get; set; } = string.Empty;
    public int Paginas { get; set; }

    // aki o prazo eh 14 dias pro livro
    public override int CalcularPrazo()
    {
        return 14;
    }

    public override string Descricao()
    {
        return $"Livro: {Titulo} - Autor: {Autor}";
    }

    public string Emprestar()
    {
        return $"O livro '{Titulo}' pode ser emprestado por {CalcularPrazo()} dias.";
    }
}