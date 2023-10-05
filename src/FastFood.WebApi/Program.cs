using Carter;
using FastFood.Atendimento.Endpoints.IoC;
using FastFood.Atendimento.Infrastructure.IoC;
using FastFood.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAtendimentoModuleDependencies(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();
app.UseMiddleware<GlobalExceptionHandler>();
app.UseAtendimentoModule();
app.Run();