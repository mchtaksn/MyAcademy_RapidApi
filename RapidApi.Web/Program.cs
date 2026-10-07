using RapidApi.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Booking.com (RapidAPI) için typed HttpClient.
// BaseAddress ve x-rapidapi-key / x-rapidapi-host header'ları burada
// tek yerden set ediliyor; BookingService sadece relative path kullanıyor.
builder.Services.AddHttpClient<IBookingService, BookingService>(client =>
{
    client.BaseAddress = new Uri("https://booking-com15.p.rapidapi.com/api/v1/");
    client.DefaultRequestHeaders.Add("x-rapidapi-key", builder.Configuration["RapidApi:Key"]);
    client.DefaultRequestHeaders.Add("x-rapidapi-host", builder.Configuration["RapidApi:Host"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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