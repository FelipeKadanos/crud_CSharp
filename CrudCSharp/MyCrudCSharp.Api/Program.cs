Console.WriteLine("Felipe Kadanos - 2026 - Address Api");

// dotnet tool install --dotnet-ef
// dotnet ef migrations add initial...
// dotnet ef datavase update

// dotnet restore

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

List<Produto> pdts = new() {
    new Produto { Nome = "Notebook" },
};

app.MapGet("/", () => "Exemplo Api Ecommerce!");

// GET: http://localhost/api/produto/listar
app.MapGet("/api/produto/listar", ([FromServices] AppDataContext ctx) => {

    if (ctx.Produtos.Count() == 0)
        return Results.BadRequest("Não há produtos cadastrados");
    
    return Results.Ok(ctx.Produtos.ToList());
});

// POST: http://localhost/api/produto/cadastrar
app.MapPost("/api/produto/cadastrar", ([FromBody] Produto? produto, [FromServices] AppDataContext ctx) => { // O ? diz que o produto pode ser nulo
    if (produto is null)
        return Results.BadRequest("Produto não foi enviado.");
    
    foreach (var pdt in pdts) {
        if (pdt.Nome == produto.Nome)
            return Results.BadRequest("Produto já cadastrado.");
    }
    
    pdts.Add(produto);
    return Results.Created("", produto);
    // return Results.Created($"/api/produto/{produto.Id}", produto);
});

// GET: http://localhost/api/produto/buscar/{nome}
app.MapGet("/api/produto/buscar/{nome}", ([FromRoute] string nome) => {
    Produto? produto = pdts.FirstOrDefault(p => p.Nome == nome);
    
    return (produto is null) ? Results.NotFound("Produto não encontrado") : Results.Ok(produto);
});

// DEL: http://localhost/api/produto/excluir/{nome}
app.MapDel("/api/produto/excluir/{id}", ([FromRoute] string id) => {
    Produto? produto = pdts.FirstOrDefault(p => p.Id == id);

    if (produto is null)
        return Results.NotFound("Produto não encontrado");

    pdts.Remove(produto)
    return Results.Ok(produto);
});

// PUT: http://localhost/api/produto/alterar/{nome}
app.MapPut("/api/produto/alterar/{id}", ([FromRoute] string id, [FromBody] Produto produto) => {
    Produto? produto = pdts.FirstOrDefault(p => p.Id == id);
    
    if (produto is null)
        return Results.NotFound("Produto não encontrado");

    
    return Results.Ok(produto);
});

app.Run();