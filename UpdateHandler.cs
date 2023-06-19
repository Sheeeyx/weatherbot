using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class UpdateHandler : IUpdateHandler
{
    private readonly ILogger<UpdateHandler> logger;

    public UpdateHandler(ILogger<UpdateHandler> logger){
        this.logger = logger;
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation("Recieve update {updateType}", update.Type);

        var handlerTask = update.Type switch
        {
            
            UpdateType.Message => HandleMessageUpdateAsync(botClient, update.Message, cancellationToken),
            UpdateType.EditedMessage => HandleEditMessageUpdateAsync(botClient, update.EditedMessage, cancellationToken),
                _=> HandleUnknownUpdateAsync(botClient,update,cancellationToken)
        };

        try
        {
            await handlerTask;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error while handling {updateType} update", update.Type);
            throw;
        }
    }

    public Task HandleUnknownUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received unknowed type {updateType}", update.Type);
        return Task.CompletedTask;
    }
}