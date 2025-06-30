using InventoryTracker.Data;
using Microsoft.EntityFrameworkCore;
using InventoryTracker.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>  
    options.UseInMemoryDatabase("InventoryDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => 
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    builder => 
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the middleware pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);

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