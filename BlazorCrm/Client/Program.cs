using BlazorCrm.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

//Register Syncfusion license 23.2.4 blazorcrm.client na allajv thn version an den doulevei
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Your License");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSyncfusionBlazor();

builder.Services.AddHttpClient("BlazorCrm.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorCrm.ServerAPI"));

await builder.Build().RunAsync();
