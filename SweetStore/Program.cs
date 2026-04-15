using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Sweet")));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IImageService, ImageService>();

var app = builder.Build();
app.UseMiddleware<SweetStore.Middleware.ExceptionMiddleware>();
// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();

// Map Controllers
app.MapControllers();

app.Run();