using Microsoft.AspNetCore.Mvc;
using MinhaBiblioteca.Models;
using MinhaBiblioteca.Repositories;

namespace MinhaBiblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriaisController : ControllerBase
{
    private readonly IMaterialRepository _repository;

    // Injesão de dependencia do repositorio aki 
    public MateriaisController(IMaterialRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult ObterTodos()
    {
        return Ok(_repository.ObterTodos());
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var material = _repository.ObterPorId(id);
        if (material == null)
            return NotFound(); 
        
        return Ok(material);
    }

    [HttpPost("livro")]
    public IActionResult CadastrarLivro([FromBody] Livro livro)
    {
        // validasões basicas pro livro
        if (string.IsNullOrWhiteSpace(livro.Titulo)) return BadRequest("Título é obrigatório.");
        if (livro.AnoPublicacao < 0) return BadRequest("Ano não pode ser negativo.");
        if (string.IsNullOrWhiteSpace(livro.Autor)) return BadRequest("Autor é obrigatório.");
        if (livro.Paginas <= 0) return BadRequest("Páginas devem ser maior que 0.");

        var criado = _repository.Adicionar(livro);
        return Created($"/api/Materiais/{criado.Id}", criado);
    }

    [HttpPost("revista")]
    public IActionResult CadastrarRevista([FromBody] Revista revista)
    {
        // validasões da revista
        if (string.IsNullOrWhiteSpace(revista.Titulo)) return BadRequest("Título é obrigatório.");
        if (revista.AnoPublicacao < 0) return BadRequest("Ano não pode ser negativo.");
        if (revista.Edicao <= 0) return BadRequest("Edição deve ser maior que zero.");
        if (revista.MesPublicacao < 1 || revista.MesPublicacao > 12) return BadRequest("Mês inválido.");

        var criado = _repository.Adicionar(revista);
        return Created($"/api/Materiais/{criado.Id}", criado);
    }

    [HttpPut("livro/{id}")]
    public IActionResult AtualizarLivro(int id, [FromBody] Livro livro)
    {
        if (!_repository.Atualizar(id, livro))
            return NotFound();
        
        return Ok(livro);
    }

    [HttpPut("revista/{id}")]
    public IActionResult AtualizarRevista(int id, [FromBody] Revista revista)
    {
        if (!_repository.Atualizar(id, revista))
            return NotFound();
        
        return Ok(revista);
    }

    [HttpDelete("{id}")]
    public IActionResult Excluir(int id)
    {
        if (!_repository.Remover(id))
            return NotFound(); // se n dechar exclui pq n axou
        
        return NoContent();
    }

    [HttpGet("{id}/descricao")]
    public IActionResult ObterDescricao(int id)
    {
        var material = _repository.ObterPorId(id);
        if (material == null)
            return NotFound();

        // Polimorfismo acontesendo aki em baixo
        var resultado = new
        {
            id = material.Id,
            tipo = material.GetType().Name,
            descricao = material.Descricao(),
            prazoEmprestimo = material.CalcularPrazo()
        };

        return Ok(resultado);
    }

    [HttpPost("{id}/emprestar")]
    public IActionResult Emprestar(int id)
    {
        var material = _repository.ObterPorId(id);
        if (material == null)
            return NotFound();

        // verefica c a interface is not
        if (material is not Emprestimo emprestavel)
        {
            return BadRequest("Este material não pode ser emprestado.");
        }

        return Ok(emprestavel.Emprestar());
    }
}