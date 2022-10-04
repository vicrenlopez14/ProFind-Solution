using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebService.Data;

// using WebService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ProFindContext>(options =>
    options.UseMySql(builder.Configuration["DatabaseConnectionString"], new MySqlServerVersion(new Version(8, 0, 27)))
        .LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging().EnableDetailedErrors());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomOperationIds(description =>
        description.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
});

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddHttpLogging(logging =>
{
    // Customize HTTP logging.
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("My-Request-Header");
    logging.ResponseHeaders.Add("My-Response-Header");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});


var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(o =>
    {
        o.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            swagger.Servers = new List<OpenApiServer>
                { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
        });
    });
    app.UseSwaggerUI(c => c.DisplayOperationId());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();