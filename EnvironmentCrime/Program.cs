using EnvironmentCrime.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEnvironmentCrimeRepository, EFEnvironmentCrimeRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
builder.Services.AddSession();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<AppIdentityDbContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Home/Login";
	options.AccessDeniedPath = "/Home/AccessDenied";
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	DBInitializer.EnsurePopulated(services);
	await IdentityInitializer.EnsurePopulated(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
