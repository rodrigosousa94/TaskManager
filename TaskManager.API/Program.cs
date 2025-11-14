using Microsoft.EntityFrameworkCore;
using System.Collections;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ==========================
// Replace ${VAR} with environment variables
// ==========================
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

// ==========================
// Database
// ==========================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        finalConnectionString,
        ServerVersion.AutoDetect(finalConnectionString)
    )
);

// ==========================
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==========================
// CORS
// ==========================
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

// ==========================
// Auto-migrate on startup
// ==========================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ==========================
// Force Railway PORT
// ==========================
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");

// ==========================
app.MapControllers();
app.Run();
