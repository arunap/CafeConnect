using System.Text.Json;
using System.Text.Json.Serialization;
using CafeConnect.Application;
using CafeConnect.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfraServices(builder.Configuration);

builder.Services.AddControllers()
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

var app = builder.Build();

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

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
