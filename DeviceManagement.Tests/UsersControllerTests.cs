using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;
using DeviceManagement.Api.Models;
using DeviceManagement.Api.DTOs;
using System.Net.Http.Json;

namespace DeviceManagement.Tests;

public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;


    public UsersControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    private async Task<User> CreateTestUser()
    {
        var user = GetValidUser();

        var response = await _client.PostAsJsonAsync("/api/users", user);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content.ReadFromJsonAsync<User>();
        if (created == null)
            throw new Exception("Failed to create test user");
        Assert.Equal("Test User", created.Name);

        return created!;
    }


    private static UserDTO GetValidUser()=> new()
    {
        Name = "Test User",
        Role = "Test",
        Location = "Nowhere"
    };


    [Fact]
    public async Task GetAllUsers_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/users");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetUserById_Exists_ReturnsOk()
    {
        var created = await CreateTestUser();

        var response = await _client.GetAsync($"/api/users/{created!.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetUserById_NotFound_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/users/99999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateUser_Valid_ReturnsCreated()
    {
        var user = GetValidUser();

        var response = await _client.PostAsJsonAsync("/api/users", user);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateUser_Invalid_ReturnsBadRequest()
    {
        var user = GetValidUser();
        user.Name = null!;

        var response = await _client.PostAsJsonAsync("/api/users", user);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_Valid_ReturnsNoContent()
    {
        var created = await CreateTestUser();

        var user = GetValidUser();

        var response = await _client.PutAsJsonAsync($"/api/users/{created!.Id}", user);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_Invalid_ReturnsBadRequest()
    {
        var created = await CreateTestUser();

        var user = GetValidUser();
        user.Name = null!;

        var response = await _client.PutAsJsonAsync($"/api/users/{created!.Id}", user);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_Exists_ReturnsNoContent()
    {
        var created = await CreateTestUser();

        var response = await _client.DeleteAsync($"/api/users/{created!.Id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_NotFound_ReturnsNotFound()
    {
        var response = await _client.DeleteAsync($"/api/users/123553");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetUser_AfterDelete_ReturnsNotFound()
    {
        var created = await CreateTestUser();

        var deleteResponse = await _client.DeleteAsync($"/api/users/{created!.Id}");

        var getResponse = await _client.GetAsync($"/api/users/{created!.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}