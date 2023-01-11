using FrontEnd;
using FrontEnd.Auth;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FrontEnd.Configuration;
using FrontEnd.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var configuration = builder.Configuration;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient());
builder.Services.AddConfReposirioHTTP(configuration);
builder.Services.AddScoped<IMostrarMensajes, MostrarMensajes>();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<ProveedorAutenticacionJWT>();

builder.Services.AddScoped<AuthenticationStateProvider, ProveedorAutenticacionJWT>(
              provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());

builder.Services.AddScoped<ILoginService, ProveedorAutenticacionJWT>(
              provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
