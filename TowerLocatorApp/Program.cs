using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NetTopologySuite.IO.Converters;
using NetTopologySuite;
using TowerLocatorApp.DataAccess;
using TowerLocatorApp.DataAccess.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => options.AddPolicy(name:"Default",   /*CORS policy*/
    policy => { 
        policy.WithOrigins("https://localhost:44497").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin(); 
    }));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MapDB"), options => options.UseNetTopologySuite()));   /*stejne jako v AppDbContextu, musi byt i zde*/
builder.Services.AddSwaggerGen(config => {           /////
    config.AddServer(new OpenApiServer {
        Description = "Default",
        Url = "https://localhost:7072"
    });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        options.SerializerSettings.Converters.Add(new GeometryConverter(geometryFactory));
    });
builder.Services.AddScoped<RouteService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
     
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();             /*na testovani API*/
app.UseHttpsRedirection();
app.UseCors("Default");
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints => {         ////
    endpoints.MapControllers();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
