using AthsEssGymBook.Client.Services.Contracts;
using AthsEssGymBook.Client.Services.Implementations;
using AthsEssGymBook.Client.States;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AthsEssGymBook.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<IdentityAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());
            builder.Services.AddScoped<IAuthorizeApi, AuthorizeApi>();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<Services.WeatherForecastClient>(client =>
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddHttpClient<Services.BookingsClient>(client =>
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            //builder.Services.AddHttpClient<Services.BookingsClient>(client =>
                //client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}
