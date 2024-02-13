using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lab17_A
{
    public class Startup
    {
        // Este método se utiliza para configurar los servicios de la aplicación.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // Configura los servicios de MVC
            services.AddSession(); // Agrega el servicio de sesiones
        }

        // Este método se utiliza para configurar el pipeline de middleware de la aplicación.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
