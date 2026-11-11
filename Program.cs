using MinhaBiblioteca.Repositories; // Presisa importar a pasta dos repositorios

var builder = WebApplication.CreateBuilder(args);

// 1. Suporte a Controllers
builder.Services.AddControllers();

// 2. Geração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// arrumando o cors q o professor pedio no .md
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// registrando como singleto pro dicionario não sumir rapido
builder.Services.AddSingleton<IMaterialRepository, MaterialRepository>();

var app = builder.Build(); // aki é Build igual o prof passou

// 3. Swagger no ambiente de dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// abilitar o cors q a jente crio la em cima
app.UseCors("PermitirFrontend");

// 4. Mapeia os endpoints
app.MapControllers();

app.Run();