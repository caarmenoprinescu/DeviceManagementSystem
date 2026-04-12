using DeviceManagement.Api.Data;
using DeviceManagement.Api.Data.Interfaces;
using DeviceManagement.Api.Repositories;
using DeviceManagement.Api.Repositories.Interfaces;
using DeviceManagement.Api.Services;
using DeviceManagement.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("MyFrontendPolicy", policy => {
        policy.WithOrigins("http://localhost:4200") 
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddScoped<IDbConnectionFactory, DatabaseConfig>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors("MyFrontendPolicy");
app.MapControllers();
app.Run();
public partial class Program { }
