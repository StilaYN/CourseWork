using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace JokesTelegramBot
{
    public class FileUpdater
    {
        public static int NumberOfTags = 5;
        private bool _needUpdate;
        private const string _readPath = "C:/Programming/C#/Семестр3/JokesTelegramBot/UpdateJokesTxt/";
        private const string _writePath = "C:/Programming/C#/Семестр3/JokesTelegramBot/JokesBin/";
        public FileUpdater(bool _needUpdate = false)
        {
            if (_needUpdate)
            {
                UpdateFile();
            }
        }

        

        private void UpdateFile()
        {
            try
            {
                int i = 1;
                while (true)
                {
                    var readFileName = _readPath+i.ToString()+".txt";
                    var (text, tag) = ReadTxt(readFileName);
                    var writeFileName = _writePath + JokesGenerator.TagsToString(tag) + ".bin";
                    WriteBin(writeFileName,text);
                    File.Delete(readFileName);
                    i++;
                }
            }
            catch{return;}
        }
        private void  WriteBin(string fileName, string text)
        {
            using (FileStream fstream = new FileStream(fileName,FileMode.OpenOrCreate))
            {
                BinaryReader reader = new BinaryReader(fstream);
                BinaryWriter writer = new BinaryWriter(fstream);
                fstream.Seek(0, SeekOrigin.Begin);
                var startPos = fstream.Position;
                fstream.Seek(0, SeekOrigin.End);
                var endPos = fstream.Position;
                if (startPos == endPos)
                {
                    fstream.Seek(0, SeekOrigin.Begin);
                    writer.Write((int)0);
                }
                writer.Seek(0, SeekOrigin.Begin);
                var number=reader.ReadInt32();
                writer.Seek(0, SeekOrigin.Begin);
                writer.Write(number+1);
                writer.Seek(0, SeekOrigin.End);
                byte[] input = Encoding.UTF8.GetBytes(text);
                writer.Write(input.Length);
                writer.Write(input,0,input.Length);
                
                
            }
        }
        private (string, Tags) ReadTxt(string fileName)
        {
            Tags tags;
            string text;
            using (StreamReader reader = new StreamReader(fileName))
            {
                tags = JokesGenerator.StringToTags(reader.ReadLine());
                text = reader.ReadToEnd();
            }
            return (text , tags);
        }
    }
}