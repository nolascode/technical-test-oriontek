using ClientsManagementApi.Extensions;
using ClientsManagementApi.Middlewares;
using Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NLog;

var builder = WebApplication.CreateBuilder(args);

//setting up nglog
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureDatabaseContext();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSwagger();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);


var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsProduction())
    app.UseHsts();
/*if(app.Environment.IsProduction())
    app.UseHsts();
else
    app.UseDeveloperExceptionPage();*/

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(sui =>
{
    sui.SwaggerEndpoint("/swagger/v1/swagger.json", "ORIONTEK");
});


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
});

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
