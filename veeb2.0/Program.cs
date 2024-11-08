﻿using Microsoft.EntityFrameworkCore.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Добавляем CORS-политику
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:3000") // Укажите здесь адрес вашего React-приложения
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName); // Используем полное имя типа
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Временно закомментируйте следующую строку, если HTTPS вызывает проблемы
// app.UseHttpsRedirection();

// Включаем использование CORS
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
