using MarvelousService.API.Extensions;
using MarvelousService.API.Infrastructure;
using MarvelousService.DataLayer.Configuration;

var builder = WebApplication.CreateBuilder(args);

string _connectionStringVariableName = "SERVICE_CONNECTION_STRING";
string _logDirectoryVariableName = "LOG_DIRECTORY";

string connString = builder.Configuration.GetValue<string>(_connectionStringVariableName);
string logDirectory = builder.Configuration.GetValue<string>(_logDirectoryVariableName);

builder.Services.Configure<DbConfiguration>(opt =>
{
    opt.ConnectionString = connString;
});

var config = new ConfigurationBuilder()
           .SetBasePath(logDirectory)
           .AddXmlFile("NLog.config", optional: true, reloadOnChange: true)
           .Build();


builder.Services.AddSwaggerGen(config =>
{
    config.EnableAnnotations();
});


builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterSwaggerAuth();

builder.Services.AddCustomAuth();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterMarvelousServiceRepositories();
builder.Services.RegisterMarvelousServiceServices();
builder.Services.RegisterMarvelousServiceAutomappers();

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
