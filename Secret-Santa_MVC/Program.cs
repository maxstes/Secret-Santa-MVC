using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.DBClass;
using Secret_Santa_MVC.SignalRApp;
using Secret_Santa_MVC.Tools;
using Secret_Santa_MVC.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddSignalR();

builder.Services.AddDbContext<SantaContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    });

builder.Services.AddTransient<LogicGame>();
builder.Services.AddTransient<Commands>();
builder.Services.AddTransient<PlayTools>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "login");
builder.Services.AddAuthorization();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<long>>(opt =>
{
#warning !Костыль 
    opt.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<SantaContext>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddDefaultTokenProviders();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Secret-Santa", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

});




var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();
//app.UseMvc();
app.UseHttpsRedirection();
app.UseRouting();

app.UseDefaultFiles();
app.UseStaticFiles();


app.UseAuthentication();
app.UseAuthorization();
app.UseCors("cors");



app.Map("/data", [Authorize] () => new { message = "Hello World" });


app.MapHub<ChatHub>("/chat");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}");

app.Run();
