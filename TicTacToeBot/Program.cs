using Microsoft.OpenApi.Models;

var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
var programLogger = loggerFactory.CreateLogger<Program>();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var sessionId = Environment.GetEnvironmentVariable("SESSION_ID");
if (sessionId == null)
{
    programLogger.LogCritical("env variable \"SESSION_ID\" not defined");
    return;
}

var botUrl = Environment.GetEnvironmentVariable("BOT_URL");
if (botUrl == null)
{
    programLogger.LogCritical("env variable \"BOT_URL\" not defined");
    return;
}

var mediatorUrl = Environment.GetEnvironmentVariable("MEDIATOR_URL");
if (mediatorUrl == null)
{
    programLogger.LogCritical("env variable \"MEDIATOR_URL\" not defined");
    return;
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0.0",
        Title = "Tic-Tac-Toe bot",
        Description = "Tic-Tac-Toe bot (ASP.NET Core 7.0)"
    });
    c.CustomSchemaIds(type => type.FullName);
    c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{builder.Environment.ApplicationName}.xml");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicTacToeBot"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.Run();
