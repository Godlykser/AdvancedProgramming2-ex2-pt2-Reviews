using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reviews.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ReviewsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReviewsContext") ?? throw new InvalidOperationException("Connection string 'ReviewsContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Reviews}/{action=Index}/{id?}");

app.Run();
