using GalacticEmpire.Client.States;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("blazorWASM", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddHttpClient("PublicGalacticServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("blazorWASM"));
            builder.Services.AddMudServices();

            if (builder.HostEnvironment.IsDevelopment())
            {
                builder.Services.AddOidcAuthentication(options =>
                {
                    // Configure your authentication provider options here.
                    // For more information, see https://aka.ms/blazor-standalone-auth
                    builder.Configuration.Bind("oidc", options.ProviderOptions);
                    //options.ProviderOptions.Authority = "https://localhost:44331";
                    //options.ProviderOptions.ClientId = "blazorWASM"; // The client ID
                    //options.ProviderOptions.ResponseType = "code";
                });
            }
            else
            {
                builder.Services.AddOidcAuthentication(options =>
                {
                    // Configure your authentication provider options here.
                    // For more information, see https://aka.ms/blazor-standalone-auth
                    builder.Configuration.Bind("oidc_azure", options.ProviderOptions);
                    //options.ProviderOptions.Authority = "https://localhost:44331";
                    //options.ProviderOptions.ClientId = "blazorWASM"; // The client ID
                    //options.ProviderOptions.ResponseType = "code";
                });
            }

            

            builder.Services.AddApiAuthorization();

            builder.Services.AddScoped<EmpireState>();

            await builder.Build().RunAsync();
        }
    }
}
