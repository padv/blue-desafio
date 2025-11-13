using AgendaApp.Application;
using AgendaApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Adicionar camada de aplicação
builder.Services.AddApplicationServices();

// Adicionar camada de infrastructure 
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();