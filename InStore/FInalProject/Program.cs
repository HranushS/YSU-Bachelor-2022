using main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinalProjectWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString(name: "DbConnection")));


//builder.Services.AddIdentityCore<Users>()
//        .AddRoles<IdentityRole>()
//        .AddEntityFrameworkStores<AppDbContext>()
//        .AddDefaultTokenProviders();

//builder.Services.AddAuthentication(o =>
//{
//    o.DefaultScheme = IdentityConstants.ApplicationScheme;
//    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
//})
//.AddIdentityCookies(o => { });



//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequiredRole", policy =>
//          policy.RequireRole("Admin", "Customer", "Manager"));
//});


builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc();
//builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
builder.Services.AddSession();
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

app.UseRouting();app.UseAuthentication();

app.UseAuthorization();
app.UseSession();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
