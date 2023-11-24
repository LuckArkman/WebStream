using catalog.API.Configurations;
using catalog.API.Controllers;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAndConfigureControllers()
    .AddUseCase()
    .Configuration();


var app = builder.Build();

app.UseDocumentation();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }