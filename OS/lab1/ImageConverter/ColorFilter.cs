using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

using ImageFilter.Core.Interfaces;

namespace ImageFilter.Core
{
    public enum ThreadsCount : byte
    {
        One = 1, Two = 2
    }

    public sealed class ColorFilter : IImageFilter
    {
        private Bitmap[] m_BitmapParts;

        public Stopwatch Time { get; private set; }
        public int TasksCount { get; private set; }

        private object locker = new object();

        public ColorFilter(ThreadsCount threadsCount = ThreadsCount.One)
        {
            TasksCount = (int)threadsCount;
        }


        public Bitmap SetFilter(Bitmap inputImage)
        {
            var outputBitmap = new Bitmap(inputImage);

            m_BitmapParts = new Bitmap[TasksCount];
            Task[] tasks = new Task[TasksCount];

            int widthPart = outputBitmap.Width / TasksCount;

            int offset = 0;

            for (int i = 0; i < TasksCount; i++)
            {
                var rectangle = new Rectangle(offset, 0, widthPart, outputBitmap.Height);

                m_BitmapParts[i] = outputBitmap.Clone(rectangle, inputImage.PixelFormat);
                tasks[i] = TaskCreator(m_BitmapParts[i], widthPart);
                offset += widthPart;
            }


            Time = Stopwatch.StartNew();
            foreach (Task task in tasks)
            {
                task.Start();
            }

            Task.WaitAll(tasks);
            Time.Stop();

            using (Graphics graphics = Graphics.FromImage(outputBitmap))
            {
                offset = 0;
                foreach (Bitmap bitmap in m_BitmapParts)
                {
                    graphics.DrawImage(bitmap, new Point(offset, 0));
                    offset += widthPart;
                }
            }

            return outputBitmap;
        }

        private static Task TaskCreator(Bitmap bitmapPart, int width)
        {
            return new Task(() =>
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < bitmapPart.Height; y++)
                    {
                        Color pixel = bitmapPart.GetPixel(x, y);

                        
                        bitmapPart.SetPixel(x, y, Color.FromArgb(
                            pixel.A,
                            Math.Truncate(((pixel.R - 128) * 0.9) + 128),
                           (pixel.G - 128) * 0.9 + 128,
                            (pixel.B - 128) * 0.9 + 128));
                    }
                }
            });
        }
    }
}