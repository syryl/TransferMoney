using dciSphere.Abstraction.Application;
using dciSphere.Endpoints;
using dciSphere.Infrastructure;
using dciSphere.Infrastructure.Logging;
using dciSphere.Interaction;
using dciSphere.Messages;
using dciSphere.Messages.Banks;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Serilog;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
var appStartupOptions = new AppStartupOptions();

//Serilog
var serilogOptions = services.BindSettings<LoggingOptions>(builder.Configuration, "SerilogOptions");
var logger = SerilogInitializer.Initialize(serilogOptions);
Log.Logger = logger;
builder.Logging.AddSerilog(logger);

services.AddInfrastructure(builder.Configuration, appStartupOptions);
services.AddInteractions();
services.AddMessages();

Log.Information("Starting web application...");

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.ExposeApiV1();

app.Run();
