using home_away.Interfaces;
using HomeAway.Service;

namespace home_away
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // HotelService
            //builder.Services.AddHttpClient<HotelService>(client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:7184");
            //});
            //builder.Services.AddHttpClient("HomeAwayAPI", client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:7184/api/");
            //});


            builder.Services.AddScoped<IHotelService, HotelService>();




            // BookingService  ✔ تمت إضافته
            //builder.Services.AddHttpClient<BookingService>(client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:7184");
            //});





            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(); // if you want session option
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Register BearerTokenHandler and typed HttpClient that uses it
            builder.Services.AddTransient<BearerTokenHandler>();

            builder.Services.AddHttpClient("HomeAwayAPI", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["HomeAwayAPI:BaseUrl"]); // e.g. https://localhost:7184/api/
            })
            .AddHttpMessageHandler<BearerTokenHandler>();

            // You can also register a typed client for convenience:
            builder.Services.AddHttpClient<IAuthService, AuthService>("HomeAwayAPIClient")
                .AddHttpMessageHandler<BearerTokenHandler>();

            var app = builder.Build();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();



            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
}