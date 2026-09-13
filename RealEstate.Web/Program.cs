using RealEstate.Application;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Data.Seed;
using RealEstate.Infrastructure.DependencyInjection;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.  
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Seed the database with initial data
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<AppDbContext>();

            await PropertySeed.SeedAsync(context);
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}