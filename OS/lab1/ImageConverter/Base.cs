using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using ImageFilter.Core;
using ImageFilter.Core.Interfaces;

namespace ImageFilter
{
    public static class Base
    {
        private static readonly string FileName ="input.bmp";
        
        public static void Waiting()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--------Фильтрация изображения----------\n");
                Console.WriteLine("Поместите в папку Debug нужное изображение с именем input.bmp и нажмите любую клавишу для продолжения");
                Console.ReadKey(false);

                bool isImageExist = File.Exists(FileName);
                if (isImageExist)
                    break;

                Console.WriteLine("Изображение не найдено. Попробуйте еще раз");
            }
            ThreadsCount threadsCount;
            while (true)
            {
                Console.Clear();
                Console.Write("Введите количество необходимых потоков (1 или 2): ");

                char inputNumber = Console.ReadKey(false).KeyChar;

                bool isInputSuccess;
                switch (inputNumber)
                {
                    case '1':
                    {
                        threadsCount = ThreadsCount.One;
                        isInputSuccess = true;
                        break;
                    }
                    case '2':
                    {
                        threadsCount = ThreadsCount.Two;
                        isInputSuccess = true;
                        break;
                    }
                   
                    default:
                        Console.WriteLine("Некоректно введено число! Попробуйте еще раз");
                        Console.ReadKey();
                        continue;
                }

                if (isInputSuccess) break;
            }

            Bitmap inputImage;
            using (var stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                inputImage = new Bitmap(stream);
            }

            IImageFilter filter = new ColorFilter(threadsCount);
            Bitmap outputImage = filter.SetFilter(inputImage);

            outputImage.Save("output.bmp", ImageFormat.Bmp);
            Console.Clear();
            Console.WriteLine("На обработку затрачено {0} (мс)\n",filter.Time.ElapsedMilliseconds);
            Console.ReadKey();

        }
    }
}
