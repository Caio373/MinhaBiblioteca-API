namespace MinhaBiblioteca.Models;

public class Revista : MaterialBiblioteca
{
    public int Edicao { get; set; }
    public int MesPublicacao { get; set; }

    // revista so pode 7 dias
    public override int CalcularPrazo()
    {
        return 7;
    }

    public override string Descricao()
    {
        return $"Revista: {Titulo} - Edição: {Edicao}";
    }
}