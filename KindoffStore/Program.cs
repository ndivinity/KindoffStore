using BusinessLogic;
using Databasing;
using Microsoft.EntityFrameworkCore;

namespace KindoffStore;

class Program
{
    [STAThread] public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContextFactory<DatabaseContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("KindoffStoreSqliteConnection"));
        });

        #region Singleton yada-yada
        builder.Services.AddSingleton<ProductManager>();
        #endregion

        WebApplication app = builder.Build();

		#region Database creation and migrations
		using (IServiceScope scope = app.Services.CreateScope())
        {
            DatabaseContext dctx = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            dctx.Database.Migrate();
            dctx.Database.EnsureCreated();
        }
        #endregion

        

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();   
    }
}
