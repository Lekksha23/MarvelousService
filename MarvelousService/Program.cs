using Marvelous.Contracts.Enums;
using MarvelousService.API.Extensions;
using MarvelousService.API.Infrastructure;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.DataLayer.Configuration;

var builder = WebApplication.CreateBuilder(args);
string _logDirectoryVariableName = "LOG_DIRECTORY";
string _connectionStringVariableName = "SERVICE_CONNECTION_STRING";

string configService = "https://piter-education.ru:6040";
string authService = "https://piter-education.ru:6042";

string logDirectory = builder.Configuration.GetValue<string>(_logDirectoryVariableName);
string connString = builder.Configuration.GetValue<string>(_connectionStringVariableName);

builder.Services.Configure<DbConfiguration>(opt =>
{
    opt.ConnectionString = connString;
});

var config = new ConfigurationBuilder()
           .SetBasePath(logDirectory)
           .AddXmlFile("NLog.config", optional: true, reloadOnChange: true)
           .Build();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterSwaggerAuth();

builder.Services.AddCustomAuth();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidation();
builder.Services.AddMassTransit();
builder.Services.RegisterMarvelousServiceRepositories();
builder.Services.RegisterMarvelousServiceServices();
builder.Services.RegisterMarvelousServiceHelpers();
builder.Services.RegisterMarvelousServiceClients();
builder.Services.RegisterMarvelousServiceAutomappers();
builder.Services.RegisterLogger(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Configuration[Microservice.MarvelousConfigs.ToString()] = configService;
app.Configuration[Microservice.MarvelousAuth.ToString()] = authService;

await app.Services.CreateScope().ServiceProvider.GetRequiredService<IInitializeHelper>().InitializeConfigs();

app.Run();
