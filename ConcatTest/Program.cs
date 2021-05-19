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
            string[] QVarinant = new string[6];
            string Line="e";
            using (StreamReader SR = new StreamReader(MainPath, Encoding.Default))
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


                using (StreamWriter SW = new StreamWriter(MainPath, true, Encoding.Default))
                {
                    using (StreamReader SR = new StreamReader(SecondPath, Encoding.Default))
                    {

                        while (Line != null)
                        {

                            for (int i = 0; i < 6; i++)
                            {
                                Line = SR.ReadLine();
                                if (!Line.Contains("<variant>") && !Line.Contains("<question>"))
                                {
                                    i--;
                                    continue;
                                }
                                QVarinant[i] = Line;
                                
                            }
                            if (!Questions.Contains(QVarinant[0]))
                            {
                                Console.WriteLine("Новый вопрос\n"+QVarinant[0]+"\n");
                                foreach (string variant in QVarinant)
                                {
                                    //Console.WriteLine(QVarinant[0]);
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
                Console.WriteLine("Завершено!");
            }
            
            Console.WriteLine("Новых вопросов - " + count);
            Console.WriteLine("Схожих вопросов - " + exist);
            Console.ReadKey();
        }
    }
}
