namespace MinhaBiblioteca.Models;

public abstract class MaterialBiblioteca
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int AnoPublicacao { get; set; }

    // Metodo abstrato, cada clace implemeta o seu
    public abstract int CalcularPrazo();

    // Metodo virtual que pode ser subescrito na class filha
    public virtual string Descricao()
    {
        return $"{Id} - {Titulo} ({AnoPublicacao})";
    }
}