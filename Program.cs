using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

const string path = @"C:\Users\sfeliz\Downloads\calendario.jpg";

ITelegramBotClient telegramBotClient;

telegramBotClient = new TelegramBotClient("5967710145:AAGuuAAkesfPXGjGtRZmkEWOac9ROy5axrI");
var me = telegramBotClient.GetMeAsync().Result;

Console.WriteLine($"User: {me.Username} mi Nombre:{me.FirstName}");
var receiverOptions = new ReceiverOptions()
{
    AllowedUpdates = new UpdateType[]
    {

     UpdateType.Message,
UpdateType.EditedMessage,

    }


};


telegramBotClient.StartReceiving(updateHandler, errorHandler, receiverOptions);
Console.ReadLine();


Task errorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
{
    throw new NotImplementedException();
}

async Task updateHandler(ITelegramBotClient bot, Update update, CancellationToken arg3)
{


    if (update.Type == UpdateType.Message)
    {
        if (update.Message.Type == MessageType.Text)
        {

            var text = update.Message.Text;
            var id = update.Message.Chat.Id;
            string? username = update.Message.Chat.Username;
            var s = update.Message.Chat.Location;
            Console.WriteLine($"{username}: {text}: {id}: {s}");


            ReplyKeyboardMarkup replyKeyboardMarkup = new[]
         {
               new []{"Calendario academico", "Pensums"},
               new []{"Actividades", "Informaciones"},
                new []{"Redes sociales", "Programacion docente"},

           };

            ReplyKeyboardMarkup pensum = new[]
   {
               new []{"Fisica", "Matematicas"},
               new []{"Sociales", "Informatica"},
                new []{"Medicina", "Derecho"},

           };

            //var imagen = Telegram.Bot.Types.InputFiles.InputFileStream(Filepath)



            switch (text)
            {
                case "Calendario academico":

                    //var inputFile = await telegramBotClient.SendPhotoAsync(id,);
                    using (var stream = System.IO.File.Open(path, FileMode.Open))
                    {
                        InputOnlineFile fts;
                        fts = stream;
                        //fts.Filename = FileUrl.Split('\\').Last();
                        await telegramBotClient.SendPhotoAsync(id, fts, "Calendario Academico");
                    }
                    break;
                case "Pensums":
                    await telegramBotClient.SendTextMessageAsync(id, "elije tu pensum ", replyMarkup: pensum);
                    break;
                case "Actividades":
                    await telegramBotClient.SendTextMessageAsync(id, "Biblioteca libre todos los miercoles." +
                        " Cena navideña 10 de diciembre ", replyMarkup: replyKeyboardMarkup);
                    break;
                case "Informaciones":
                    await telegramBotClient.SendTextMessageAsync(id, "enviando  Informaciones... ", replyMarkup: replyKeyboardMarkup);
                    break;
                case "Redes sociales":
                    await telegramBotClient.SendTextMessageAsync(id, "enviando  redes sociales... ", replyMarkup: replyKeyboardMarkup);
                    break;
                case "Programacion docente":
                    await telegramBotClient.SendTextMessageAsync(id, "cargando programacion docente... ", replyMarkup: replyKeyboardMarkup);
                    break;
                case "atras":
                    await telegramBotClient.SendTextMessageAsync(id, "Bienvenido Uasdianos al bot de la FJD3, elige una opcion ", replyMarkup: replyKeyboardMarkup);
                    break;
                default:
                    await telegramBotClient.SendTextMessageAsync(id, "Bienvenido Uasdianos al bot de la FJD3, elige una opcion ", replyMarkup: replyKeyboardMarkup);
                    break;
            }



        }
    }

}


