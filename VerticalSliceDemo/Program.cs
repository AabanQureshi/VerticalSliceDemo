using Application;
using Infrastructure;
using Scalar.AspNetCore;
using VerticalSliceDemo.Features;
using VerticalSliceDemo.Features.Product;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddFeatures();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.MapScalarApiReference(option =>
    {
        option.Title = "VerticalSliceDemo API Reference";
        option.WithTheme(ScalarTheme.DeepSpace);
        option.WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Http);
    });
}

app.UseHttpsRedirection();
app.MapProductEndpoints();

app.Run();
