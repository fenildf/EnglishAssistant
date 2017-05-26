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
    }

    class EnglishAssistant
    {
        static List<EnglishWord> readFromFile(string path)
        {
            StreamReader wordFileStreamReader = new StreamReader(path);
            string wordFileString = wordFileStreamReader.ReadToEnd();
            string[] wordInfo = wordFileString.Split('\n');
            List<EnglishWord> EnglishWordList = new List<EnglishWord>();
            foreach (string wordInfoString in wordInfo)
            {
                string[] word = wordInfoString.Split(',');
                EnglishWordList.Add(new EnglishWord(word[0], word[2], word[1]));
            }
            return EnglishWordList;
        }


        static void Main(string[] args)
        {
            Random r = new Random();
            List<EnglishWord> Words=readFromFile("wordFile.txt");
            Algorithm.ShuffleArray(Words);
            DateTime startTime = DateTime.Now;
            foreach (var word in Words)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(string.Format("{0}. {1}",word.Property,word.ChineseText));
                while(true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    string answer = Console.ReadLine();
                    if (answer.ToLower() == word.EnglishText.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Right");
                        break;
                    }
                    else if (answer.ToLower() == "fuck")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(word.EnglishText);
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You are wrong");
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ok, you pass the test");
            Console.WriteLine("And you cost {0} seconds to pass the test",DateTime.Now-startTime);
            Console.ResetColor();
        }
    }
}
