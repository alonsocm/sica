using Application;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Persistence;
using Shared;
using WebAPI.Extensions;

var origenesPermitidos = "_originesPermitidos";

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddApplicationLayer();
builder.Services.AddSharedInfraestructure(configuration);
builder.Services.AddPersistenceInfrastructure(configuration, builder.Environment.EnvironmentName);
builder.Services.AddApiVersionExtensions();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: origenesPermitidos,
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                      });

});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodyBufferSize = int.MaxValue;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = long.MaxValue; // if don't set default value is: 30 MB
});

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = long.MaxValue; // if don't set default value is: 128 MB
    x.MultipartHeadersLengthLimit = int.MaxValue;
    x.ValueCountLimit = int.MaxValue;
});


var app = builder.Build();
app.UseCors(origenesPermitidos);

var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseErrorHandlingMiddleware();
app.MapControllers();
app.Run();
