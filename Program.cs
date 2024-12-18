using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure JSON options to handle circular references and null values
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

// Add Razor Pages support
builder.Services.AddRazorPages();

// Add DbContext and configure SQLite connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services for authentication and user management
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

// Add Google and Facebook authentication
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    })
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
        facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
    });

// Add Swagger for API documentation
builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Enable console logging

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Development environment: Detailed error pages and Swagger
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management API v1");
    });
}
else
{
    // Production environment: Generic error pages
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
    app.UseHsts();
}

// Middleware for HTTPS redirection and serving static files
app.UseHttpsRedirection();
app.UseStaticFiles();

// Middleware for routing
app.UseRouting();

// Middleware for authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Configure routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Add Razor Pages
app.MapRazorPages();

// Run the application
app.Run();