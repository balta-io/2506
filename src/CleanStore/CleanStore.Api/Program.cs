using CleanStore.Application;
using CleanStore.Infrastructure;
using CleanStore.Infrastructure.SharedContext.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    "Server=localhost,1433;Database=cleanstore;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";
builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseSqlServer(connectionString, m => m.MigrationsAssembly("CleanStore.Api")));
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.MapPost("/v1/accounts", async (
    ISender sender,
    CleanStore.Application.AccountContext.UseCases.Create.Command command) =>
{
    var result = await sender.Send(command);
    return TypedResults.Created($"v1/accounts/{result.Value.Id}", result);
});

app.MapGet("/v1/accounts/{id}", async (
    ISender sender,
    Guid id) =>
{
    var query = new CleanStore.Application.AccountContext.UseCases.Get.Query(id);
    var result = await sender.Send(query);
    return TypedResults.Ok(result);
});

app.Run();