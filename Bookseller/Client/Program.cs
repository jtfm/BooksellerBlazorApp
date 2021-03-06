using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Bookseller.Client.Services;

namespace Bookseller.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services
                .AddHttpClient<IBookDataService, BookDataService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));

            await builder.Build().RunAsync();
        }
    }
}
