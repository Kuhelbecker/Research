using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using TeleReposter.Models.Instagram;

namespace TeleReposter
{
    public class Bot
    {
        public string Name { get; }

        public string Token { get; } = @"<bot_access_token>";

        public TelegramBotClient Client { get; }

        public Bot()
        {
            this.Client = new TelegramBotClient(this.Token);
        }

        public async void LogToChannel(ChatId chatId, Exception exception)
        {
            var text = "*Exception*" + Environment.NewLine +
                       "target site: " + exception.TargetSite + Environment.NewLine +
                       "message:" + Environment.NewLine +
                       exception.Message;

            await this.Client.SendTextMessageAsync(chatId, text);
        }

        public async void LogToChannel(ChatId chatId, string text)
        {
            await this.Client.SendTextMessageAsync(chatId, text);
        }

        public async void SendTextMessageToChannel(ChatId chatId, string text)
        {
            await this.Client.SendTextMessageAsync(chatId, text);
        }

        public async void SendMediaToChannel(ChatId chatId, FileToSend file, InstagramMediaTypes type)
        {
            file.Filename = string.Empty;

            try
            {
                var telegramBotClient = new TelegramBotClient(this.Token);

                switch (type)
                {
                    case InstagramMediaTypes.Image:
                        await telegramBotClient.SendPhotoAsync(chatId, file);
                        break;
                    case InstagramMediaTypes.Video:
                        await telegramBotClient.SendVideoAsync(chatId, file);
                        break;
                    default:
                        await telegramBotClient.SendPhotoAsync(chatId, file);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.LogToChannel(-1001107875397, ex);
            }
        }
    }
}
