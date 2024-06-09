using Microsoft.EntityFrameworkCore;
using Practico_8.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<VideoRentalContext>(options =>
options.UseSqlServer("Data Source=DESKTOP-EM3IN3K\\SQLEXPRESS;Initial Catalog=VideoRental;Integrated Security=true; TrustServerCertificate=True"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
