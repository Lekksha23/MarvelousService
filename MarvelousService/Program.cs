using MarvelousService.API.Extensions;
using MarvelousService.API.Infrastructure;
using MarvelousService.DataLayer.Configuration;

var builder = WebApplication.CreateBuilder(args);
string _logDirectoryVariableName = "LOG_DIRECTORY";
string _connectionStringVariableName = "SERVICE_CONNECTION_STRING";

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

builder.Services.RegisterMarvelousServiceRepositories();
builder.Services.RegisterMarvelousServiceServices();
builder.Services.RegisterMarvelousServiceAutomappers();
builder.Services.RegisterLogger(config);
builder.Services.AddMassTransit();

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
