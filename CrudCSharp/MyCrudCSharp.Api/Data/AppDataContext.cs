using Mcrisoft.EntityFrameworkCore;

// Microsoft.EntityFrameworkCore.Sqlite
// Dentro do projeto:
// dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.31
// dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 7.0.0
// dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.31
// dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.0

// Configuração com banco de dados
public class AppDataContext : DbContext {

    public DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOpionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite("Datta Source=Ecommerce.db");
    }

}