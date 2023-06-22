using Telegram.Bot.Types;
using Microsoft.AspNetCore.Mvc;
using TelegramBot.WebHook.Xizmatlar;

namespace TelegramBot.WebHook.Controllers;

public class WebHookController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] Update update,
        [FromServices] UpdateHandler handler,
        CancellationToken cancellationToken)
    {
        await handler.HandleUpdates(update, cancellationToken);
        return Ok();
    }
}
