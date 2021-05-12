using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory+"/Test";
            string Question;
            string Answer;
            string variant;
            List<string> ExistQuestion = new List<string>();
            List<string> ExistVariant = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }
            FileInfo test = new FileInfo(path+"/test.txt");
            if(!test.Exists)
            {
                test.Create();
            }
           using (StreamReader SR = new StreamReader(path + "/test.txt", Encoding.Default))
            {
                string line;
                while ((line = SR.ReadLine()) != null)
                {
                    if (line.Contains("<question>"))
                    {
                        ExistQuestion.Add(line);
                    }
                }
            }
           
            using (StreamWriter SW = new StreamWriter(path + "/test.txt", true, Encoding.Default))
            {
                while (true)
                {
                    Console.WriteLine("Вопрос: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Question = Console.ReadLine();
                    if (ExistQuestion.Contains("<question>" + Question))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Такой вопрос уже есть\n нажмите кнопку для продолжения");
                        Console.ReadKey();
                        Console.ReadKey();
                        Console.Clear();
                        Console.ResetColor();
                        continue;
                    }
                    if (Question == "0")
                    {
                        break;
                    }
                    SW.WriteLine("<question>" + Question);
                    ExistQuestion.Add("<question>" + Question);
                    Console.ReadKey();
                    Console.ResetColor();
                    Console.WriteLine("Ответ: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Answer = Console.ReadLine();
                    ExistVariant.Add(Answer);
                    Console.ResetColor();
                    SW.WriteLine("<variant>" + Answer);
                    int i = 0;
                    while (i<4)
                    {
                        Console.WriteLine("Вариани{0}:", i + 2);
                        variant = Console.ReadLine();
                        if (ExistVariant.Contains(variant))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Такой вариант уже существует");
                            Console.ReadKey();
                            Console.ResetColor();
                            continue;
                        }
                        ExistVariant.Add(variant);
                        SW.WriteLine("<variant>" + variant);
                        i++;
                    }
                    SW.WriteLine("\n");
                    SW.Flush();
                    Console.Clear();
                    ExistVariant.Clear();
                }
            }
        }
    }
}
