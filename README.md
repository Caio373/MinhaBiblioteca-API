Nome do projeto: API Gerenciamento de Biblioteca

Integrantes:

Caio Fagundes

Descrição do sistema

Essa é uma API basica para gerenciar uma biblioteca da faculdade. O sistema dexa a jente cadastrar, listar, atualizar e excluir livros e revistas em memoria. O projeto usa os conceitos de POO como herança (tudo herda de MaterialBiblioteca), polimorfismo, interfaces e o padrão de projeto Repository registrado como Singleton pra não sumir os dados muito rapido.

Tecnologias utilizadas

C#

ASP.NET Core Web API (.NET 10)

Swagger (pra testar a API)

Como executar

Abre a pasta do projeto no terminal (VS Code).

Roda o comando pra iniciar a API:

dotnet run


Segura o Ctrl e clica no link do localhost que vai apareser no terminal.

No navegador, coloca /swagger no final do link (exemplo: http://localhost:5000/swagger) pra abrir a tela de testes.

Principais endpoints

GET /api/Materiais : Mostra todos os materiais cadastrados.

GET /api/Materiais/{id} : Busca um material pelo ID gerado.

POST /api/Materiais/livro : Cadastra um livro novo.

POST /api/Materiais/revista : Cadastra uma revista nova.

PUT /api/Materiais/livro/{id} : Atualiza os dados de um livro.

DELETE /api/Materiais/{id} : Apaga o material do sistema.

GET /api/Materiais/{id}/descricao : Retorna o resumo do material e o prazo calculado por polimorfismo.

POST /api/Materiais/{id}/emprestar : Testa c o material tem a interface IEmprestimo.

Exemplos de testes

1. Cadastrando um Livro (POST /api/Materiais/livro)
Coloca esse JSON no corpo:

{
  "titulo": "Clean Code",
  "anoPublicacao": 2008,
  "autor": "Robert C. Martin",
  "paginas": 431
}


A resposta deve ser o código 201 Created.

2. Ver o polimorfismo fucionando (GET /api/Materiais/1/descricao)
Coloque o ID 1 e execute. A resposta vai vir com o prazo de 14 dias calculado direto da classe do livro:

{
  "id": 1,
  "tipo": "Livro",
  "descricao": "Livro: Clean Code - Autor: Robert C. Martin",
  "prazoEmprestimo": 14
}


3. Teste de Emprestimo (POST /api/Materiais/1/emprestar)
Teste com o ID de um livro, tem q retornar 200 OK disendo que pode ser emprestado.
Se testar com o ID de uma Revista, vai dar 400 Bad Request pq revista n tem a interface de emprestimo.
