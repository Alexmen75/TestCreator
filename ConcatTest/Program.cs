using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConcatTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0, exist = 0;
            string MainPath, SecondPath;
            Console.WriteLine("Введите путь основного файла");
            MainPath = Console.ReadLine();
            MainPath = MainPath.Replace('"', ' ');
            MainPath = MainPath.Trim();
            Console.WriteLine("\nВведите путь второго файла");
            SecondPath = Console.ReadLine();
            SecondPath = SecondPath.Replace('"', ' ');
            SecondPath = SecondPath.Trim();
            Console.Clear();
            List<string> Questions = new List<string>();
            string[] QVarinant = new string[2];
            string Line="e";
            using (StreamReader SR = new StreamReader(MainPath))
            {
                string line;
                while ((line = SR.ReadLine()) != null)
                {
                    if (line.Contains("<question>"))
                    {
                        Questions.Add(line);
                    }
                }
            }
            try
            {
                using (StreamWriter SW = new StreamWriter(MainPath, true))
                {
                    using (StreamReader SR = new StreamReader(SecondPath))
                    {
                        while (Line != null)
                        {
                            Line = SR.ReadLine();
                            if (Line.Contains("<question>"))
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    if (!Line.Contains("<variant>") && !Line.Contains("<question>"))
                                    {
                                        i--;
                                        Line = SR.ReadLine();
                                        continue;
                                    }
                                    QVarinant[i] = Line;
                                    Line = SR.ReadLine();
                                }
                            }
                            else
                            {
                                continue;
                            }
                            if (!Questions.Contains(QVarinant[0]))
                            {
                                Console.WriteLine("Новый вопрос\n"+QVarinant[0]+"\n");
                                foreach (string variant in QVarinant)
                                {
                                    SW.WriteLine(variant);
                                }
                                count++;
                                SW.WriteLine("\n");
                            }
                            else
                            {
                                exist++;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Завершено!");
            }
            Console.WriteLine("Новых вопросов - " + count);
            Console.WriteLine("Схожих вопросов - " + exist);
            Console.ReadKey();
        }
    }
}
