using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var botClient = new TelegramBotClient("6297457299:AAGA_bl4x2lbFK8zOOlt0tUmG6lsVLyLdGQ");

using CancellationTokenSource cts = new();

ReceiverOptions receiver = new()
{
    AllowedUpdates = Array.Empty<UpdateType>(),
};

botClient.StartReceiving(
    updateHandler:HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiver,
    cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;
    var firstName = message.Chat.FirstName;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    // Echo received message text

    //Message sentMessage = await botClient.SendTextMessageAsync(
    //    chatId: chatId,
    //    text: "You said:\n" + messageText,
    //    cancellationToken: cancellationToken);


    // Send Photo

    //Message sentMessage = await botClient.SendPhotoAsync(
    //   chatId: chatId,
    //   photo: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg"),
    //   caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
    //   parseMode: ParseMode.Html,
    //   cancellationToken: cancellationToken);


    // From stream

    //await using Stream stream = System.IO.File.OpenRead("C:/Users/user/Desktop/IMG_0327.webp");
    //Message sentMessage = await botClient.SendStickerAsync(
    //   chatId: chatId,
    //   sticker: InputFile.FromStream(stream),
    //   cancellationToken: cancellationToken);


    // Send Audio

    //Message sendMessage = await botClient.SendAudioAsync(
    //    chatId: chatId,
    //    audio: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3"),
    //    cancellationToken: cancellationToken);


    // Send voice

    //await using Stream stream = System.IO.File.OpenRead("C:/Users/user/Desktop/audio_2023-06-20_19-40-51.ogg");
    //Message sentMessage = await botClient.SendVoiceAsync(
    //   chatId: chatId,
    //   voice: InputFile.FromStream(stream),
    //   cancellationToken: cancellationToken);


    // Send video

    //Message sentMessage = await botClient.SendVideoAsync(
    //   chatId: chatId,
    //   video: InputFile.FromUri("https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/video-countdown.mp4"),
    //   supportsStreaming: true,
    //   cancellationToken: cancellationToken);

    //await using Stream stream = System.IO.File.OpenRead("C:/Users/user/Desktop/video_2023-06-20_16-47-24. axfratxon🥰.mp4");
    //Message sentMessage = await botClient.SendVideoAsync(
    //   chatId: chatId,
    //   video: InputFile.FromStream(stream),
    //   cancellationToken: cancellationToken);


    // Send album
    //Message[] messages = await botClient.SendMediaGroupAsync(
    //chatId: chatId,
    //media: new IAlbumInputMedia[]
    //{
    //    new InputMediaPhoto(
    //        InputFile.FromUri("https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg")),
    //    new InputMediaPhoto(
    //        InputFile.FromUri("https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg")),
    //},
    //cancellationToken: cancellationToken);



    // Set poll

    //Message sendMessage = await botClient.SendPollAsync(
    //    chatId: chatId,
    //    question: "Sizam Shavkat Mirziyoyevga ovoz berasizmi?",
    //    options: new[]
    //    {
    //        "Ha",
    //        "Ha albatta",
    //        "Shubhasiz",
    //        "Boshqa ilojim ham yo'qku"
    //    },
    //    cancellationToken: cancellationToken);


    Message sendMessage = await botClient.SendLocationAsync(
        chatId: chatId,
        latitude: 40.949424,
        longitude: 68.681901,
        cancellationToken: cancellationToken);
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var errorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(errorMessage);
    return Task.CompletedTask;
}