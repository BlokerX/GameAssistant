using Media = System.Windows.Media;
using Drawing = System.Drawing;

namespace GameAssistant.Core
{
    /// <summary>
    /// Class converter.
    /// </summary>
    internal static class TypeConverter
    {
        /// <summary>
        /// Convert System.Windows.Media.Color to System.Drawing.Color.
        /// </summary>
        /// <param name="mediaColor">System.Windows.Media.Color to convert.</param>
        /// <returns>System.Drawing.Color result.</returns>
        public static Drawing.Color ConvertColorMediaToDrawing(Media.Color mediaColor)
        {
            return Drawing.Color.FromArgb(mediaColor.A, mediaColor.R, mediaColor.G, mediaColor.B);
        }

        /// <summary>
        /// Convert System.Drawing.Color to System.Windows.Media.Color.
        /// </summary>
        /// <param name="drawingColor">System.Drawing.Color to convert.</param>
        /// <returns>System.Windows.Media.Color result.</returns>
        public static Media.Color ConvertColorDrawingToMedia(Drawing.Color drawingColor)
        {
            return Media.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
        }
    }
}
