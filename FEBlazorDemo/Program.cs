using Blazored.LocalStorage;
using FEBlazorDemo;
using Ganss.XSS;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SeComps;
using SeComps.UtilLib;
using System.Net.Http.Headers;
using System.Reflection;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register JwtBearerTokenHandler
builder.Services.AddTransient<JwtBearerTokenHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped(sp => new HtmlSanitizer
{
    AllowedAttributes = { "class" }
});
builder.Services.AddScoped<HttpClient>(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7028/")
    };
    return httpClient;
});

builder.Services.AddSingleton<StateManager>();
await builder.Build().RunAsync();
