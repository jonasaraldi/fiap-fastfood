using Carter;
using FastFood.Atendimento.Endpoints.IoC;
using FastFood.WebApi.Configurations;
using FastFood.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAtendimentoModuleDependencies(builder.Configuration);
builder.Services.AddHealthCheck(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthCheck();
app.UseHttpsRedirection();
app.MapCarter();
app.UseGlobalExceptionHandlerMiddleware();
app.UseAtendimentoModule();

app.Run();