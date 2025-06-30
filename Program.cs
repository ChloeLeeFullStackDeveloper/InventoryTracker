using InventoryTracker.Data;
using Microsoft.EntityFrameworkCore;
using InventoryTracker.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>  
    options.UseInMemoryDatabase("InventoryDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the middleware pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Products.AddRange(
        new Product { Name = "Laptop", Quantity = 5, Price = 1299.99m},
        new Product { Name = "Mouse", Quantity = 25, Price = 39.99m},
        new Product { Name = "Monitor", Quantity = 10, Price = 229.49m}
    );

    context.SaveChanges();
}

app.Run();