using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjektNET.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(options =>
{
    options.AddConsole(); // For console output
    options.AddDebug();   // For debug output
});
// Dodanie kontekstu bazy danych
builder.Services.AddDbContext<ProjektNETDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjektNETDbContext") ?? throw new InvalidOperationException("Connection string 'ProjektNETDbContext' not found.")));

// Dodanie us?ug do kontenera
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Konfiguracja potoku ??da? HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Domy?lna warto?? HSTS to 30 dni. Mo?esz to zmieni? dla scenariuszy produkcyjnych, zobacz https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
builder.Services.AddLogging();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();