using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.WebHook.Xizmatlar;

public class UpdateHandler
{
    private readonly ITelegramBotClient botClient;
    private readonly ILogger<UpdateHandler> logger;
    public UpdateHandler(ITelegramBotClient botClient, ILogger<UpdateHandler> logger)
    {
        this.logger = logger;
        this.botClient = botClient;
    }

    public async Task HandleUpdates(Update update, CancellationToken cancellationToken)
    {
        var handler = update switch
        {
            { Message: { } message } => BotOnSendMessageAsync(message, cancellationToken),
            { ChosenInlineResult: { } chosenInlineResult } => BotOnChoosenInlineResultReceiver(chosenInlineResult, cancellationToken),
            _ => UnKnownHandlerAsync()
        };

        await handler;
    }

    private async Task BotOnChoosenInlineResultReceiver(ChosenInlineResult chosenInlineResult, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("BotOnChoosenInlineResultReceiver shu metod ishlayapti oka!");

        await botClient.SendTextMessageAsync(
            chatId: chosenInlineResult.From.Id,
            text: "Nimadir!",
            cancellationToken: cancellationToken);
    }

    private async Task BotOnSendMessageAsync(Message message, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("BotOnSendMessageAsync shu metod ishlayapti oka!");

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Salom Dunyo!",
            cancellationToken: cancellationToken);
    }

    private async Task UnKnownHandlerAsync()
    {
        this.logger.LogInformation("Nega ko'rsatilgan ishni qilmaysan eeeee");
        await Task.CompletedTask;
    }
}
