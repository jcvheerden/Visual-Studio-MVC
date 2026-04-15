using Microsoft.EntityFrameworkCore;
using prjHealthCareSystem.Data;

var builder = WebApplication.CreateBuilder(args);


//Add services to the container.
builder.Services.AddControllersWithViews();

// ---> ADD THESE LINES RIGHT HERE <---
// Program.cs
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Try 5 times
            maxRetryDelay: TimeSpan.FromSeconds(30), // Wait up to 30 seconds between retries
            errorNumbersToAdd: null)
    ));


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Program.cs
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        
        // Give SQL Server 45 seconds to warm up in Docker
        logger.LogInformation("Waiting 45 seconds for SQL Server to boot...");
        Thread.Sleep(45000); 
        
        context.Database.EnsureCreated();
        logger.LogInformation("Database and tables created successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
