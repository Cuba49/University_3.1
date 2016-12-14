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

        public ColorFilter(ThreadsCount threadsCount = ThreadsCount.One)
        {
            TasksCount = (int)threadsCount;
        }

        public Stopwatch Time { get; private set; }
        public int TasksCount { get; private set; }

        private object locker = new object();

        public Bitmap ApplyFilter(Bitmap target)
        {
            var outputBitmap = new Bitmap(target);

            m_BitmapParts = new Bitmap[TasksCount];
            Task[] tasks = new Task[TasksCount];

            int widthPart = outputBitmap.Width / TasksCount;

            int offset = 0;
            if (TasksCount == 1)
            {
                m_BitmapParts[0] = outputBitmap;
                tasks[0] = CreateTaskForFilterApplying(m_BitmapParts[0], widthPart);
            }
            else
            {
                for (int i = 0; i < TasksCount; i++)
                {
                    var rectangle = new Rectangle(offset, 0, widthPart, outputBitmap.Height);

                    m_BitmapParts[i] = outputBitmap.Clone(rectangle, target.PixelFormat);
                    tasks[i] = CreateTaskForFilterApplying(m_BitmapParts[i], widthPart);
                    offset += widthPart;
                }
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

        private static Task CreateTaskForFilterApplying(Bitmap target, int width)
        {
            return new Task(() =>
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < target.Height; y++)
                    {
                        Color pixel = target.GetPixel(x, y);
                        var extremum = Math.Max(pixel.R, Math.Max(pixel.G, pixel.B)) +
                                       Math.Min(pixel.R, Math.Min(pixel.G, pixel.B));

                        target.SetPixel(x, y, Color.FromArgb(
                            pixel.A,
                            extremum - pixel.R,
                            extremum - pixel.G,
                            extremum - pixel.B));
                    }
                }
            });
        }
    }
}