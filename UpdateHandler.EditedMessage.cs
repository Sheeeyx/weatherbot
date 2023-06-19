using Telegram.Bot;
using Telegram.Bot.Types;

public partial class UpdateHandler 
{
    public Task HandleEditMessageUpdateAsync(ITelegramBotClient botClient, Message editedMessage, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Received edited message {userId}. New Text: {newText}", editedMessage.Chat.FirstName, editedMessage.Text);

        return Task.CompletedTask;
    }
}