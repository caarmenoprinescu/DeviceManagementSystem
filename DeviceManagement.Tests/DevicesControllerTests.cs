using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;
using DeviceManagement.Api.Models;
using DeviceManagement.Api.DTOs;
using System.Net.Http.Json;

namespace DeviceManagement.Tests;

public class DevicesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;


    public DevicesControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    private async Task<Device> CreateTestDevice()
    {
        var device = GetValidDevice();

        var response = await _client.PostAsJsonAsync("/api/devices", device);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
       
        var created = await response.Content.ReadFromJsonAsync<Device>();
        if (created == null)
            throw new Exception("Failed to create test device");
        Assert.Equal("Test Device", created.Name);

        return created!;
    }


    private static DeviceDTO GetValidDevice(int userId = 1) => new()
    {
        Name = "Test Device",
        Manufacturer = "Test",
        Type = 0,
        OperatingSystem = "Android",
        OsVersion = "14.0",
        Processor = "Snapdragon",
        Ram = 8,
        Description = "Test description",
        UserId = userId
    };


    [Fact]
    public async Task GetAllDevices_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/devices");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDeviceById_Exists_ReturnsOk()
    {
        var created = await CreateTestDevice();

        var response = await _client.GetAsync($"/api/devices/{created!.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDeviceById_NotFound_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/devices/99999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateDevice_Valid_ReturnsCreated()
    {
        var device = GetValidDevice();

        var response = await _client.PostAsJsonAsync("/api/devices", device);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateDevice_Invalid_ReturnsBadRequest()
    {
        var device = GetValidDevice();
        device.Name = null!;

        var response = await _client.PostAsJsonAsync("/api/devices", device);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateDevice_Valid_ReturnsNoContent()
    {
        var created = await CreateTestDevice();

        var device = GetValidDevice();

        var response = await _client.PutAsJsonAsync($"/api/devices/{created!.Id}", device);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateDevice_Invalid_ReturnsBadRequest()
    {
        var created = await CreateTestDevice();

        var device = GetValidDevice();
        device.Name = null!;

        var response = await _client.PutAsJsonAsync($"/api/devices/{created!.Id}", device);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteDevice_Exists_ReturnsNoContent()
    {
        var created = await CreateTestDevice();

        var response = await _client.DeleteAsync($"/api/devices/{created!.Id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteDevice_NotFound_ReturnsNotFound()
    {
        var response = await _client.DeleteAsync($"/api/devices/123553");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetDevice_AfterDelete_ReturnsNotFound()
    {
        var created = await CreateTestDevice();

        var deleteResponse = await _client.DeleteAsync($"/api/devices/{created!.Id}");

        var getResponse = await _client.GetAsync($"/api/devices/{created!.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}