namespace CICD.Tests;

using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CICD;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class ProductsApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductsApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsOkAndNonEmptyList()
    {
        // Act
        var response = await _client.GetAsync("/products");

        // Assert
        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.NotEmpty(products!);
    }

    [Fact]
    public async Task GetProductById_WithExistingId_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/products/1");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var product = await response.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(product);
        Assert.Equal(1, product!.Id);
    }

    [Fact]
    public async Task GetProductById_WithNonExistingId_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync("/products/9999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
