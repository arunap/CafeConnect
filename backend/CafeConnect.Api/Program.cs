using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using CafeConnect.Api.Middleware;
using CafeConnect.Application;
using CafeConnect.Infrastructure;
using CafeConnect.Infrastructure.DatabaseContext;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure log4net
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfraServices(builder.Configuration);

builder.Services.AddControllers()
  .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Where(m => m.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage);
            var response = new { Success = false, Message = "Validation errors occurred", Errors = errors };
            return new BadRequestObjectResult(response);
        };
    })
   .AddJsonOptions(options =>
   {
       options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
       options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
   });


// ensures that all generated URLs are lowercase.
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "Cafe Connect API",
    Description = "Cafe Connect System API is designed to streamline and manage the daily operations of a cafe",
    TermsOfService = new Uri("https://example.com/terms"),
}));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "React.Client", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


var app = builder.Build();

app.UseExceptionMiddleware();

// run database migrations
using (var sp = app.Services.CreateScope())
{
    var databaseService = sp.ServiceProvider.GetRequiredService<CafeDbContext>();
    databaseService.Database.Migrate();
}

// seed cafes and employees
await app.Services.SeedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
  {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cafe Connect API v1");
      c.RoutePrefix = string.Empty;
  });
}

app.UseCors("React.Client");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
