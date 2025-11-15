using Microsoft.EntityFrameworkCore;
using System.Collections;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Troca variáveis de ambiente no connection string
string ReplaceEnvVars(string conn)
{
    foreach (DictionaryEntry env in Environment.GetEnvironmentVariables())
    {
        string key = "${" + env.Key + "}";
        if (conn.Contains(key))
            conn = conn.Replace(key, env.Value?.ToString());
    }
    return conn;
}

var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var finalConnectionString = ReplaceEnvVars(rawConnectionString);

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        finalConnectionString,
        new MySqlServerVersion(new Version(8, 0, 39))
    )
);

// Serviços
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔥 Configuração de CORS correta
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔥 Ativar CORS antes dos controllers
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
