using front_end.Handlers;
using front_end.Interfaces;
using front_end.Services;
using System.Net.Http;
namespace front_end
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            // HttpContext accessor for reading cookies/session/token
            builder.Services.AddHttpContextAccessor();

            // Register your token handler
            builder.Services.AddTransient<BearerTokenHandler>();

            // Register your API HttpClient with token handler
            builder.Services.AddHttpClient("HomeAwayAPI", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["HomeAwayAPI:BaseUrl"]);
            })
            .AddHttpMessageHandler<BearerTokenHandler>();

            builder.Services.AddScoped<IAuthService, AuthService>();



            // Register API services (Hotel, Room, Reservation)
            builder.Services.AddHttpClient<IHotelService, HotelService>();
            builder.Services.AddHttpClient<IRoomService, RoomService>();
            builder.Services.AddHttpClient<IReservationService, ReservationService>();


            var app = builder.Build();

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
}
