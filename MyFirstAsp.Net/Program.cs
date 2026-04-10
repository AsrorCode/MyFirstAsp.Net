using MyFirstAsp.Net.Data;
using MyFirstAsp.Net.Interfaces;
using MyFirstAsp.Net.Repositories;
using MyFirstAsp.Net.Services;
using Scalar.AspNetCore; 

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVISLARNI RO'YXATDAN O'TKAZISH ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Scalar OpenAPI hujjatidan foydalanadi, shuning uchun AddSwaggerGen qoladi
builder.Services.AddSwaggerGen();

// Ma'lumotlar bazasi ulanishi (Dapper)
builder.Services.AddScoped<DapperContext>();

// Dependency Injection (Interfeyslar va Servislarni bog'lash)
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, IOrderService>();

var app = builder.Build();

// --- 2. MIDDLEWARES (SO'ROVLARNI BOSHQARISH) ---
if (app.Environment.IsDevelopment())
{
    // Swagger JSON faylini shakllantirish scalar buni o'qiydi
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });

    // Swagger UI o'rniga Scalar-dan foydalanamiz
    app.MapScalarApiReference();
}

app.UseAuthorization();
app.MapControllers();

app.Run();