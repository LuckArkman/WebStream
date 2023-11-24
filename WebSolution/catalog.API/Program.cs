using catalog.API.Configurations;
using catalog.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUseCase()
    .AddAndConfigureControllers();

var app = builder.Build();

app.UseDocumentation();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }