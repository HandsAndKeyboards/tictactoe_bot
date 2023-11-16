using Microsoft.OpenApi.Models;
using TicTacToeBot.Bots;
using TicTacToeBot.Bots.MCTSbot;
using TicTacToeBot.Bots.MDTFbot;
using TicTacToeBot.Config;

var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
var programLogger = loggerFactory.CreateLogger<Program>();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

var config = CreateBotConfig();
if (config == null)
    return;
StartBotByEnv();

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
    // c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{builder.Environment.ApplicationName}.xml");
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



return;

BotConfig? CreateBotConfig()
{
    var sessionId = Environment.GetEnvironmentVariable("SESSION_ID");
    if (string.IsNullOrEmpty(sessionId))
    {
        programLogger.LogCritical("env variable \"SESSION_ID\" not defined");
        return null;
    }

    var botUrl = Environment.GetEnvironmentVariable("BOT_URL");
    if (string.IsNullOrEmpty(botUrl))
    {
        programLogger.LogCritical("env variable \"BOT_URL\" not defined");
        return null;
    }

    var mediatorUrl = Environment.GetEnvironmentVariable("MEDIATOR_URL");
    if (string.IsNullOrEmpty(mediatorUrl))
    {
        programLogger.LogCritical("env variable \"MEDIATOR_URL\" not defined");
        return null;
    }
    
    var botId = Environment.GetEnvironmentVariable("BOT_ID");
    if (string.IsNullOrEmpty(botId))
    {
        programLogger.LogCritical("env variable \"BOT_ID\" not defined");
        return null;
    }
    
    var password = Environment.GetEnvironmentVariable("BOT_PASSWORD");
    if (string.IsNullOrEmpty(password))
    {
        programLogger.LogCritical("env variable \"BOT_PASSWORD\" not defined");
        return null;
    }

    return new BotConfig
    {
        BotUrl = botUrl,
        MediatorUrl = botId,
        SessionId = sessionId,
        BotId = botId,
        Password = password,
    };
}

void StartBotByEnv()
{
    ITicTacToeBot bot;
    
    var figure = GetFigure();
    var botType = GetBotType();
    switch (botType)
    {
        case "MDTF": 
            bot = new Bot(figure);
            break;
        case "MCST":
            bot = new MCTSRunner();
            break;
        default:
            throw new ApplicationException("");
    }

    builder.Services.AddSingleton(bot);
}

char GetFigure() =>
    Environment.GetEnvironmentVariable("BOT_FIGURE")?.ElementAt(0) 
        ?? throw new ApplicationException("env variable BOT_FIGURE is not presented or contains incorrect value");

string GetBotType() =>
    Environment.GetEnvironmentVariable("PLAYING_BOT_TYPE") 
        ?? throw new ApplicationException("env variable PLAYING_BOT_TYPE is not presented or contains incorrect value");
