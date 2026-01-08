using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CICD;

public static class ProductsEndpoints
{
    // Hardcoded in-memory list of products (no database)
    private static readonly List<Product> Products =
    [
        new(1, "Keyboard", 49.99m),
        new(2, "Mouse", 19.99m),
        new(3, "Monitor", 199.99m)
    ];

    public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
    {
        return app
            .MapGetProductsEndpoint()
            .MapGetProductByIdEndpoint();
    }

    public static IEndpointRouteBuilder MapGetProductsEndpoint(this IEndpointRouteBuilder app)
    {
        // GET /products - return all products
        app.MapGet("/products", () => Results.Ok(Products))
           .WithName("GetProducts");

        return app;
    }

    public static IEndpointRouteBuilder MapGetProductByIdEndpoint(this IEndpointRouteBuilder app)
    {
        // GET /products/{id} - return a single product by id
        app.MapGet("/products/{id:int}", (int id) =>
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            return product is not null ? Results.Ok(product) : Results.NotFound();
        }).WithName("GetProductById");

        return app;
    }
}
