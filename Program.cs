using Blazored.LocalStorage;
using Health_expert_B.Data;
using Health_expert_B.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();


builder.Services.Configure<Dalle2Options>(o => builder.Configuration.GetSection("OpenAI").Bind(o));
builder.Services.Configure<ChatOptions>(o => builder.Configuration.GetSection("OpenAI").Bind(o));
builder.Services.AddScoped<OpenAIService>();
builder.Services.AddSingleton<UserInfoService>();
builder.Services.AddSingleton<UserMealPrefrenceService>();
builder.Services.AddSingleton<UserWorkoutPrefrenceService>();

// Register HttpClient
builder.Services.AddScoped<HttpClient>(s =>
{
    var navigationManager = s.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
});

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
