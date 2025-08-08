using Microsoft.OpenApi.Models;
using Toodo.API.Extensions;
using Toodo.API.Infrastructure;
using Toodo.Application.Extensions;
using Toodo.Infrastructure.Data;
using Toodo.Infrastructure.Extensions;
using Toodo.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddPresentationServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("api/identity").MapIdentityApi<ApplicationUser>();

app.MapControllers();

app.Run();