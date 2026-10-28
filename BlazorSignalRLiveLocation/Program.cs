using BlazorSignalRLiveLocation.Components;
using BlazorSignalRLiveLocation.Hubs;


var builder = WebApplication.CreateBuilder(args);

// This is required for Blazor Server interactivity
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddHubOptions(options =>
    {
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
        options.HandshakeTimeout = TimeSpan.FromSeconds(30);
        // This is the default, but ensure it's not too small
        options.MaximumReceiveMessageSize = 32 * 1024;
    });

// This is required for your custom LocationHub
builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// This maps your LocationHub to the URL used in the JavaScript
app.MapHub<LocationHub>("/locationhub");

app.Run();