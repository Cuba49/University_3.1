using System.Diagnostics;
using System.Drawing;

namespace ImageFilter.Core.Interfaces
{
    /// <summary>
    /// Defines a members to work with images
    /// </summary>
    public interface IImageFilter
    {
        /// <summary>
        /// Gets time, that was ellapsed on converting
        /// </summary>
        Stopwatch Time { get; }

        /// <summary>
        /// Gets or sets threads count for converting
        /// </summary>
        int TasksCount { get; }

        /// <summary>
        /// Creates new Bitmap image and applies
        /// filter to it
        /// </summary>
        /// <param name="target">Image source</param>
        /// <returns>New System.Drawing.Bitmap 
        /// image with applied filter</returns>
        Bitmap ApplyFilter(Bitmap target);

    }
}