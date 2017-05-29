using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Zzkluck;

namespace EnglishAssistant
{
    class EnglishWord
    {
        public EnglishWord(string englishText, string chineseText, string property)
        {
            this.EnglishText = englishText;
            this.ChineseText = chineseText;
            this.Property = property;
        }
        public string EnglishText { get; set; }
        public string ChineseText { get; set; }
        public string Property { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}. {2}", EnglishText, Property, ChineseText);
        }
        public string ToFileFormatString()
        {
            return string.Format("{0},{1},{2}", EnglishText, Property, ChineseText);
        }
    }

    class EnglishAssistant
    {
        static string MainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EnglishAssistant";
        static string NotesDirectory = MainDirectory + "\\Notes";
        static string WordFilePath = MainDirectory + "\\wordFile.txt";

        static List<EnglishWord> Words;
        static void Main(string[] args)
        {
            CheckDirectoryExist();
            Words = readFromFile(WordFilePath);
            List<EnglishWord> mistakedWords = DoPractice();
            SaveMistakes(mistakedWords);
            Console.ReadLine();
        }

        static void CheckDirectoryExist()
        {
            if (Directory.Exists(MainDirectory) == false)
            {
                Directory.CreateDirectory(MainDirectory);
                Directory.CreateDirectory(NotesDirectory);
            }
        }
        static List<EnglishWord> readFromFile(string path)
        {
            StreamReader wordFileStreamReader = new StreamReader(path);
            string wordFileString = wordFileStreamReader.ReadToEnd();
            wordFileStreamReader.Close();
            string[] wordInfo = wordFileString.Split('\n');
            List<EnglishWord> EnglishWordList = new List<EnglishWord>();
            foreach (string wordInfoString in wordInfo)
            {
                string[] word = wordInfoString.Split(',');
                if (word.Length >= 3)
                {
                    EnglishWordList.Add(new EnglishWord(word[0], word[2], word[1]));
                }
            }
            return EnglishWordList;
        }
        static List<EnglishWord> DoPractice()
        {
            int practicedWord = 0;
            List<EnglishWord> mistakedWords = new List<EnglishWord>();

            Algorithm.ShuffleArray(Words);
            DateTime startTime = DateTime.Now;

            foreach (var word in Words)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(string.Format("{0}. {1}", word.Property, word.ChineseText));
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    string answer = Console.ReadLine();
                    if (answer.ToLower() == word.EnglishText.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Right");
                        practicedWord += 1;
                        break;
                    }
                    else if (answer.ToLower() == "fuck")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(word.EnglishText);
                        FuckTheWord(word);
                        mistakedWords.Add(word);
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong");
                    }
                }

            }
            DateTime endTime = DateTime.Now;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ok, you pass the test");
            Console.WriteLine("And you cost {0} to pass the {1} tests", endTime - startTime, practicedWord);
            Console.WriteLine("During the test, you fucked {0} words", mistakedWords.Count);
            Console.ResetColor();
            return mistakedWords;
        }
        static void SaveMistakes(List<EnglishWord> mistakes)
        {
            string NoteFileName = NotesDirectory + "\\" + DateTime.Now.Year + DateTime.Now.Month
                + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + "_Practice";
            StreamWriter NoteFileWriter = new StreamWriter(File.Create(NoteFileName));

            StringBuilder NoteString = new StringBuilder();
            foreach (EnglishWord word in mistakes)
            {
                NoteString.Append(word.ToFileFormatString()).Append("\n");
            }

            Console.WriteLine("WritingNotes......");
            NoteFileWriter.Write(NoteString);
            NoteFileWriter.Close();
            Console.WriteLine("Success!");
        }
        static void FuckTheWord(EnglishWord fuckingWord)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Let's repeat the fucking word 3 times");
            int fuckedTime = 0;
            while (fuckedTime < 3)
            {
                Console.ForegroundColor = ConsoleColor.White;
                string input = Console.ReadLine();
                if (input.ToLower() == fuckingWord.EnglishText.ToLower())
                {
                    fuckedTime += 1;
                }
                else if (input == "zzkluck")
                {
                    break;
                }
            }
        }
    }
}
