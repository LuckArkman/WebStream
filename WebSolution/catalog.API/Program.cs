using catalog.API.Configurations;
using catalog.API.Controllers;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddUseCase();
builder.Services.AddAndConfigureControllers();
builder.Services.AddAndConfigureControllers()
    .AddUseCase()
    .DBConfiguration();

var app = builder.Build();
app.UseDocumentation();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }