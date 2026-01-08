using CICD;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Map products-related endpoints defined in the dedicated endpoints class
app.MapProductsEndpoints();

app.Run();

// Public partial Program class so the test project can use WebApplicationFactory<Program>
public partial class Program
{
}
