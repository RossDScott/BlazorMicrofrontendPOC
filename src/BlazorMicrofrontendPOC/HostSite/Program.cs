using HostSite.Data;
using HostSite.Microfrontend;
using HostSite.Options;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();

builder.Configuration.AddJsonFile("MicrofrontendRegistration.json", false);
builder.Services.Configure<MicrofrontendRegistrationOptions>(
    builder.Configuration.GetSection(MicrofrontendRegistrationOptions.MicrofrontendRegistration));

//This is the microfrontend builder. Based on the config it will load the assemblies, dependencies
//and register the DI
var componentLoader = new RegistrationBuilder(builder);
builder.Services.AddSingleton(componentLoader);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();