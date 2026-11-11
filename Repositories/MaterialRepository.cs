using MinhaBiblioteca.Models;

namespace MinhaBiblioteca.Repositories;

public class MaterialRepository : IMaterialRepository
{
    // Dicionario privado para guardar tudo em memoria
    private readonly Dictionary<int, MaterialBiblioteca> _materiais;

    public MaterialRepository()
    {
        _materiais = new Dictionary<int, MaterialBiblioteca>();
            
        // add uns dados pra testar no swager
        Adicionar(new Livro { Titulo = "Clean Code", AnoPublicacao = 2008, Autor = "Robert C. Martin", Paginas = 431 });
        Adicionar(new Revista { Titulo = "Tecnologia e Inovação", AnoPublicacao = 2026, Edicao = 15, MesPublicacao = 11 });
    }

    // gerador de id pra n repetir
    private int GerarNovoId()
    {
        if (_materiais.Count == 0)
            return 1;
        
        return _materiais.Keys.Max() + 1;
    }

    public IEnumerable<MaterialBiblioteca> ObterTodos()
    {
        return _materiais.Values;
    }

    public MaterialBiblioteca? ObterPorId(int id)
    {
        if (_materiais.ContainsKey(id))
            return _materiais[id];
        
        return null; // si nao axar, retorna nulo msm
    }

    public MaterialBiblioteca Adicionar(MaterialBiblioteca material)
    {
        material.Id = GerarNovoId();
        _materiais.Add(material.Id, material);
        return material;
    }

    public bool Atualizar(int id, MaterialBiblioteca material)
    {
        if (!_materiais.ContainsKey(id))
            return false;
        
        material.Id = id; // mantem o id da hrl
        _materiais[id] = material;
        return true;
    }

    public bool Remover(int id)
    {
        return _materiais.Remove(id);
    }
}