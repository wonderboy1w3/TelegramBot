using Telegram.Bot;
using TelegramBot.WebHook.Andozalar;
using TelegramBot.WebHook.Xizmatlar;

var builder = WebApplication.CreateBuilder(args);

var section = builder.Configuration.GetSection("BotSozlamalari");
builder.Services.Configure<BotSozlamasi>(section);
var botSozlamasi = section.Get<BotSozlamasi>();

builder.Services.AddHttpClient("telegram-bot")
    .AddTypedClient<ITelegramBotClient>(httpClient => 
    new TelegramBotClient(botSozlamasi.Token, httpClient));

builder.Services.AddScoped<UpdateHandler>();
builder.Services.AddHostedService<WebHookConfiguration>();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();

app.MapControllerRoute(
    name: "telegram-bot",
    pattern: $"bot/{botSozlamasi.Token}",
    new { controller = "WebHook", action = "Post" });

app.MapControllers();
app.Run();
