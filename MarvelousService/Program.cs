using MarvelousService.API.Infrastructure;
using MarvelousService.API.Extensions;
using MarvelousService.DataLayer.Configuration;
using System.Text.Json.Serialization;
using MarvelousService.API.ExceptionResponse;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

string _connectionStringVariableName = "SERVICE_CONNECTION_STRING";
string connString = builder.Configuration.GetValue<string>(_connectionStringVariableName);

builder.Services.Configure<DbConfiguration>(opt =>
{
    opt.ConnectionString = connString;
});

builder.Services.AddSwaggerGen(config =>
{
    config.EnableAnnotations();
});

builder.Services.AddMvc()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })

    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var exc = new ValidationExceptionResponse(context.ModelState);
            return new UnprocessableEntityObjectResult(exc);
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.RegisterMarvelousServiceRepositories();
//builder.Services.RegisterMarvelousServiceServices();
//builder.Services.RegisterMarvelousServiceAutomappers();

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
