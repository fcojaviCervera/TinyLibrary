using Microsoft.EntityFrameworkCore;
using TinyLibrary.Application;
using TinyLibrary.WebApi.Filters;
using TinyLibrary.Infrastructure;
using TinyLibrary.Infrastructure.Persistence;
using TinyLibrary.WebApi.ErrorHandling;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();   

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    context.Database.Migrate();
}

app.UseExceptionHandler();
 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
