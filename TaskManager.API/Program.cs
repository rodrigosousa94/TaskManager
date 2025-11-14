using Microsoft.EntityFrameworkCore;
using System.Collections;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


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


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        finalConnectionString,
        new MySqlServerVersion(new Version(8, 0, 39))
    )
);


builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
