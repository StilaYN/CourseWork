using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;

namespace JokesTelegramBot
{
    internal class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("6661480078:AAH9S_OteEUL3FxP_zeWnI0vet89y2-KbFk");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                // Некоторые действия
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    var message = update.Message;
                    if (message.Text.ToLower() == "/start")
                    {
                        await botClient.SendTextMessageAsync(message.Chat,
                            "Добро пожаловать, на самого веселого бота НГТУ=)");
                        var replayKeyboard = new ReplyKeyboardMarkup(
                            new List<KeyboardButton>()
                            {
                                new KeyboardButton("Вова"),
                                new KeyboardButton("Программисты"),
                                new KeyboardButton("Национальности"),
                                new KeyboardButton("Цитаты преподов"),
                                new KeyboardButton("Штирлиц"),
                                new KeyboardButton("Студенты"),
                                new KeyboardButton("Россия"),
                                new KeyboardButton("Сон"),



                            })
                        {
                            ResizeKeyboard = true,
                        };
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите тему анекдота=)",
                            replyMarkup: replayKeyboard);
                        return;
                    }

                    if (message.Text.ToLower() == "программисты")
                    {
                        await botClient.SendTextMessageAsync(message.Chat,
                            JokesGenerator.GenerateJokes(Tags.Programmer));
                        return;

                    }

                    if (message.Text.ToLower() == "национальности")
                    {
                        await botClient.SendTextMessageAsync(message.Chat,
                            JokesGenerator.GenerateJokes(Tags.Nationalities));
                        return;

                    }

                    if (message.Text.ToLower() == "цитаты преподов")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, JokesGenerator.GenerateJokes(Tags.Quotes));
                        return;
                    }

                    if (message.Text.ToLower() == "штирлиц")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, JokesGenerator.GenerateJokes(Tags.Shtir));
                        return;
                    }
                    if (message.Text.ToLower() == "студенты")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, JokesGenerator.GenerateJokes(Tags.Students));
                        return;
                    }

                    if (message.Text.ToLower() == "другое")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, JokesGenerator.GenerateJokes(Tags.Other));
                        return;
                    }

                    if (message.Text.ToLower() == "россия")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, JokesGenerator.GenerateJokes(Tags.Russia));
                        return;
                    }

                    if (message.Text.ToLower() == "вова")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, JokesGenerator.GenerateJokes(Tags.Vova));
                        return;
                    }

                    if (message.Text.ToLower() == "сон")
                    {
                        await botClient.SendTextMessageAsync(message.Chat,
                            "Надеюсь ты весело провел время, приходи к нам еще!!!");
                        return;
                    }

                    await botClient.SendTextMessageAsync(message.Chat,
                        "К сожалению, у нас нет анекдота на выбранную тему, но ты можешь выбрать из имеющихся!!!");
                }
            }
            catch (Exception ex) { }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
            FileUpdater updater = new FileUpdater(true);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}
