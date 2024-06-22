using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Areas.Identity.Data;
using WebApplication7.Data;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ManDBContextConnection") ?? throw new InvalidOperationException("Connection string 'ManDBContextConnection' not found.");

builder.Services.AddDbContext<ManDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<WebApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ManDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();
builder.Services.AddDbContext<WebApplication7Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplication7Context") ?? throw new InvalidOperationException("Connection string 'WebApplication7Context' not found.")));

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
app.MapRazorPages();

app.Run();
