using System.Diagnostics;
using System.Drawing;

namespace ImageFilter.Core.Interfaces
{
    public interface IImageFilter
    {
        Stopwatch Time { get; }
        int TasksCount { get; }
        Bitmap SetFilter(Bitmap target);
    }
}