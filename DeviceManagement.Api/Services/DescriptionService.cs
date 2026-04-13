using Mscc.GenerativeAI;
using DeviceManagement.Api.Services.Interfaces;

namespace DeviceManagement.Api.Services;

public class DescriptionService(IConfiguration configuration) : IDescriptionService
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<string> GenerateDescriptionAsync(string prompt)
    {
        var apiKey = _configuration["Gemini:ApiKey"];
        var model = _configuration["Gemini:Model"];

        var googleAI = new GoogleAI(apiKey);
        var gemini = googleAI.GenerativeModel(model);

        var response = await gemini.GenerateContent(prompt);
        return response.Text;
    }
}