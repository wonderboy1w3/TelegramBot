using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramBot.WebHook.Andozalar;

namespace TelegramBot.WebHook.Xizmatlar;

public class WebHookConfiguration : IHostedService
{
    private readonly ILogger<WebHookConfiguration> logger;
    private readonly BotSozlamasi botSozlamasi;
    private readonly IServiceProvider serviceProvider;
    public WebHookConfiguration(
        ILogger<WebHookConfiguration> logger,
        IConfiguration configuration, 
        IServiceProvider serviceProvider)
    {
        this.logger = logger;
        this.serviceProvider = serviceProvider;
        this.botSozlamasi = configuration.GetSection("BotSozlamalari").Get<BotSozlamasi>();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var scope = serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        var hostAddress = $"{botSozlamasi.HostAddress}/bot/{botSozlamasi.Token}";
        this.logger.LogInformation("Bot ishlashni boshladi oka!");

        await botClient.SetWebhookAsync(
            url: hostAddress,
            allowedUpdates: Array.Empty<UpdateType>(),
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var scope = serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        this.logger.LogInformation("Bot ishlashdan to'xtadi oka!");

        await botClient.DeleteWebhookAsync();
    }
}
