using System;
using System.IO;
using System.Text;

namespace JokesTelegramBot
{
    public static class JokesGenerator
    {
        public static string TagsToString(Tags tags)
        {
            switch (tags)
            {
                case Tags.Nationalities:
                    return "Nationalities";
                case Tags.Programmer:
                    return "Programmer";
                case Tags.Quotes:
                    return "Quotes";
                case Tags.Students:
                    return "Students";
                case Tags.Russia:
                    return "Russia";
                case Tags.Vova:
                    return "Vova";
                case Tags.Shtir:
                    return "Shtir";
                default:
                    return "Other";

            }
        }

        public static Tags StringToTags(string str)
        {
            switch (str)
            {
                case "Nationalities":
                    return Tags.Nationalities;
                case "Programmer":
                    return Tags.Programmer;
                case "Quotes":
                    return Tags.Quotes;
                case "Students":
                    return Tags.Students;
                case "Russia":
                    return Tags.Russia;
                case "Shtir":
                    return Tags.Shtir;
                case "Vova":
                    return Tags.Vova;
                default:
                    return Tags.Other;
            }
        }

        public static string GenerateJokes(Tags tags)
        {
            var filePath = "C:/Programming/C#/Семестр3/JokesTelegramBot/JokesBin/";
            var fileName = filePath + TagsToString(tags) + ".bin";
            Random random = new Random();
            try
            {
                using (FileStream fstream = new FileStream(fileName, FileMode.Open))
                {
                    BinaryReader binareReader = new BinaryReader(fstream);
                    var numberOfJokes = binareReader.ReadInt32();
                    var n = random.Next(0, numberOfJokes);
                    for (var i = 0; i < n; i++)
                    {
                        var sizeOfSkip = binareReader.ReadInt32();
                        fstream.Seek(sizeOfSkip, SeekOrigin.Current);
                    }
                    var size = binareReader.ReadInt32();
                    byte[] buffer = new byte[size];
                    fstream.Read(buffer, 0, size);
                    return Encoding.UTF8.GetString(buffer);
                }
            }
            catch
            {
                return "Нет анекдота на вашу тему";}
            return "Нет анекдота на вашу тему";
        }
    }
}