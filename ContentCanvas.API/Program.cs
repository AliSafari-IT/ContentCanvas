using ContentCanvas.API.Data;
using Microsoft.Extensions.DependencyInjection; // Make sure to include this
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    // Set the application URLs
    ApplicationName = typeof(Program).Assembly.FullName,
    WebRootPath = "wwwroot",
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Development
});

// Set URLs for Kestrel
builder.WebHost.UseUrls("https://localhost:5001", "http://localhost:5000");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MongoDbContext with IConfiguration
builder.Services.AddSingleton<MongoDbContext>(sp =>
    new MongoDbContext(sp.GetRequiredService<IConfiguration>()));

// Configure CORS to allow requests from your React app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000") // Replace with your React app's URL
                            .AllowAnyHeader()
                            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Use the CORS policy
app.UseCors("AllowSpecificOrigin");

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();