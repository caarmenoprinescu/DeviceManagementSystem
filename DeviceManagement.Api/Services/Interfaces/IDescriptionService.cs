namespace DeviceManagement.Api.Services.Interfaces;

public interface IDescriptionService
{
    Task<string> GenerateDescriptionAsync(string prompt);
}