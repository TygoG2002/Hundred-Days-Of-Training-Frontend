using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HundredDays;
using HundredDays.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Uri apiBaseUri;

if (builder.HostEnvironment.IsDevelopment())
{
    apiBaseUri = new Uri("http://localhost:5254/");
}
else
{
    apiBaseUri = new Uri(builder.HostEnvironment.BaseAddress);
}

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = apiBaseUri
    });

builder.Services.AddScoped<WorkoutApi>();

await builder.Build().RunAsync();
