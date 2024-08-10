using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Moq;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.Models;
using PermissionsAPI.Services.CQRS.Commands;
using PermissionsAPI.Services.CQRS.Handlers;
public class PermissionsApiIntegrationTests : IClassFixture<WebApplicationFactory<PermissionsAPI.Startup>>
{
    private readonly HttpClient _client;

    public PermissionsApiIntegrationTests(WebApplicationFactory<PermissionsAPI.Startup> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPermissions_ShouldReturnSuccessStatusCode()
    {
        // Arrange & Act
        var response = await _client.GetAsync("/api/permissions");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseString);
    }
}
