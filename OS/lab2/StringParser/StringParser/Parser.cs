using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StringParser
{
    class Parser
    {
        const int m = 29;
        char[] alf = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '!', '.', ' ' };
        int linesCount = System.IO.File.ReadAllLines("input.txt").Length;

        int[,] key = new int[3, 3];//matrica kodirovania

        // создаем семафор
        static Semaphore sem = new Semaphore(1, 1);

        private static int tmpNumberString;
        private static string tmpInputString;
        private static string tmpOutputString;


        int forRead = 1;// счетчик чтения
        private int forEncryption;
        private int forWriter;

        public Parser()
        {
            key[0, 0] = 1;
            key[0, 1] = 2;
            key[0, 2] = 3;

            //stroka 2
            key[1, 0] = 7;
            key[1, 1] = 5;
            key[1, 2] = 4;

            //stroka 3
            key[2, 0] = 8;
            key[2, 1] = 2;
            key[2, 2] = 7;
            for (int i = 0; i < linesCount; i++)
            {
                
                new Thread(Read).Start(i);
                new Thread(Encryption).Start();
                new Thread(Writer).Start();
                
            }
        }

        public void Read(object i)
        {
            while (forRead > 0)
            {
                sem.WaitOne();
                tmpNumberString = Convert.ToInt32(i);
                Console.WriteLine("Читание строки " + Convert.ToInt32(i));
                IEnumerable<string> strings = System.IO.File.ReadLines("input.txt").Skip(Convert.ToInt32(i)).Take(1);
                tmpInputString = (strings.ToArray())[0];
                forRead--;
                forEncryption++;
                sem.Release();
            }

        }

        public void Encryption()
        {
            while (forEncryption > 0)
            {
                sem.WaitOne();
                Console.WriteLine("Шифрование строки " + tmpNumberString);
                string text = tmpInputString;
                int x = text.Length;
                int y = 0; //количество строк, Data.n - количество столбцов
                if (x % 3 != 0)
                    y = x / 3 + 1;
                else y = x / 3;
                int xy = y * 3 - x; //сколько символов нужно добавить
                if (xy != 0)
                    for (int i = 0; i < xy; i++)
                        text += 'e';
                int[] mass = new int[text.Length];
                //преобразование о.т. в массив идентификаторов
                for (int i = 0; i < text.Length; i++)
                    for (int j = 0; j < m; j++)
                        if (text[i] == alf[j])
                        {
                            mass[i] = j;
                            break;
                        }
                //преобразование массива идентификаторов в матрицу идентификаторов
                int[,] mid = new int[3, y];
                int p = 0;
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < y; j++)
                    {
                        mid[i, j] = mass[p];
                        p++;
                    }
                //перемножение матриц
                int[,] r = new int[3, y];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        for (int k = 0; k < mid.GetLength(0); k++)
                        {
                            r[i, j] += key[i, k] * mid[k, j];
                        }
                    }
                }
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < y; j++)
                        r[i, j] = Math.Abs(r[i, j] % m);
                //преобразование в строку символов
                string rez = "";
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < y; j++)
                        for (int o = 0; o < m; o++)
                            if (r[i, j] == o)
                            {
                                rez += alf[o];
                                break;
                            }
                tmpOutputString = rez;
                forEncryption--;
                forWriter++;
                sem.Release();
            }
        }

        public void Writer()
        {
            while (forWriter > 0)
            {
                sem.WaitOne();
                Console.WriteLine("Запись строки " + tmpNumberString);
                string str = tmpOutputString + Environment.NewLine;
                File.AppendAllText("output.txt", str, Encoding.UTF8);
                forWriter--;
                forRead++;
                sem.Release();
            }

        }
    }
}
