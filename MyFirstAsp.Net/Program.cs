
using MyFirstAsp.Net.Data;
using MyFirstAsp.Net.Interfaces;
using MyFirstAsp.Net.Repositories;
using MyFirstAsp.Net.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Controllerlarni qo'shish
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//2.Malumotlar bazasi va Service Repositorylarni ro'yxatdan o'tkazish

builder.Services.AddScoped<DapperContext>();

// Repository va Service-larni bog'lash
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<OrderService, OrderService>();

var app = builder.Build();

// 3. Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();