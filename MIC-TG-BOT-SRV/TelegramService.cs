using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MIC_TG_BOT_SRV
{
    public partial class TelegramService : ServiceBase
    {
        private static ITelegramBotClient botClient;

        public TelegramService()
        {
            InitializeComponent();
            if (botClient == null)
            {
                initBot();
            }
        }

        protected override void OnStart(string[] args)
        {
            if (botClient == null)
            {
                initBot();
            }
        }

        protected override async void OnStop()
        {
            if (botClient != null)
            {
                await botClient.CloseAsync();
            }
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update?.Message?.Text == null)
                return;


            var chatId = update.Message.Chat.Id;

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You said:\n" + update.Message.Text,
                cancellationToken: cancellationToken);
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            //var ErrorMessage = exception switch
            //{
            //    ApiRequestException apiRequestException
            //        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            //    _ => exception.ToString()
            //};

            return Task.CompletedTask;
        }

        private void initBot()
        {
            botClient = new TelegramBotClient("5387804735:AAEy6uV8WgmBFcTFvCYvFKNsYlKocSsb6Xk");

            using (var cts = new CancellationTokenSource())
            {
                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = Array.Empty<UpdateType>()
                };

                botClient.StartReceiving(
                    updateHandler: HandleUpdateAsync,
                    pollingErrorHandler: HandlePollingErrorAsync,
                    receiverOptions: receiverOptions,
                    cancellationToken: cts.Token
                );
            }
        }

    }
}
