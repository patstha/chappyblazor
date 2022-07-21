using Blazor.Analytics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<IDapperDataAccess, DapperDataAccess>((services) =>
{
    return new DapperDataAccess(
        services.GetRequiredService<IConfiguration>().GetConnectionString("Default"),
        services.GetRequiredService<ILogger<DapperDataAccess>>());
});
builder.Services.AddTransient(service => new DataService(
    new DapperDataAccess(
        service.GetRequiredService<IConfiguration>().GetConnectionString("Default"),
        service.GetRequiredService<ILogger<DapperDataAccess>>())));
builder.Services.AddGoogleAnalytics("GTM-KN8RD79");
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
