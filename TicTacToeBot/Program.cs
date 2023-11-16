using Microsoft.OpenApi.Models;
using TicTacToeBot;
using TicTacToeBot.Config;
using TicTacToeBot.Models;
using TicTacToeBot.Services;
using TicTacToeBot.Bots.MDTFbot;
using TicTacToeBot.Bots.MCTSbot;

var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
var programLogger = loggerFactory.CreateLogger<Program>();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

var config = CreateBotConfig();
if (config == null)
    return;
StartBotByENV();

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
    if (sessionId == null)
    {
        programLogger.LogCritical("env variable \"SESSION_ID\" not defined");
        return null;
    }

    var botUrl = Environment.GetEnvironmentVariable("BOT_URL");
    if (botUrl == null)
    {
        programLogger.LogCritical("env variable \"BOT_URL\" not defined");
        return null;
    }

    var mediatorUrl = Environment.GetEnvironmentVariable("MEDIATOR_URL");
    if (mediatorUrl == null)
    {
        programLogger.LogCritical("env variable \"MEDIATOR_URL\" not defined");
        return null;
    }

    return new BotConfig
    {
        botUrl = botUrl!,
        mediatorUrl = mediatorUrl!,
        SessionId = sessionId!
    };

}

async Task StartBot(BotConfig botConfig)
{
    var mediator = new MediatorService(botConfig);
    var figure = await mediator.RegistrationBot();
    var bot = new Bot(figure == Figure.X ? 'x' : 'o');
    builder.Services.AddSingleton(bot);
}

void StartBotByENV()
{
    var figure = Environment.GetEnvironmentVariable("BOT_FIGURE");

    var bot = new Bot(figure[0]);
    builder.Services.AddSingleton(bot);

    var MCTS_bot = new MCTSRunner();
    builder.Services.AddSingleton(MCTS_bot);
}