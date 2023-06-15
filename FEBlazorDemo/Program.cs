using FEBlazorDemo;
using Ganss.XSS;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SeComps.UtilLib;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IHtmlSanitizer, HtmlSanitizer>(x =>
{
    // Configure sanitizer rules as needed here.
    // For now, just use default rules + allow class attributes
    var sanitizer = new Ganss.XSS.HtmlSanitizer();
    sanitizer.AllowedAttributes.Add("class");
    return sanitizer;
});

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7028/")
    });

builder.Services.AddSingleton<StateManager>();

await builder.Build().RunAsync();
