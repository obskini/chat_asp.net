using chat.Classes;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Bind AppSettings configuration
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.AddSingleton(appSettings);

// Register HttpClient
builder.Services.AddHttpClient("TalkApi", (sp, client) =>
{
    var settings = sp.GetRequiredService<AppSettings>();
    client.BaseAddress = new Uri(settings.ApiBaseUrl);
});

// Register ChatChannelApi and IChatChannelApi in DI container
builder.Services.AddScoped<IChatChannelApi, ChatChannelApi>();

// Register ChatMessageApi and IChatMessageApi in DI container
builder.Services.AddScoped<IChatMessageApi, ChatMessageApi>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
