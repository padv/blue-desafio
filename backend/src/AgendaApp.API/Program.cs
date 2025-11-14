using AgendaApp.Application;
using AgendaApp.Infrastructure;
using AgendaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar camada de aplicação
builder.Services.AddApplicationServices();

// Adicionar camada de infrastructure 
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(            "http://localhost:5173",     
            "http://localhost:3000",     
            "http://frontend:3000",      
            "http://127.0.0.1:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

app.Urls.Clear();
app.Urls.Add("http://0.0.0.0:5000");

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); // Mude para seu DbContext
    await dbContext.Database.MigrateAsync();
}


app.Run();