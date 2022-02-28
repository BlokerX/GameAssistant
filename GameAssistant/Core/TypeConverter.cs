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

        /// <summary>
        /// Convert System.Windows.Media.FontFamily to System.Drawing.FontFamily.
        /// </summary>
        /// <param name="mediaFontFamily">System.Windows.Media.FontFamily to convert.</param>
        /// <returns>System.Drawing.FontFamily result.</returns>
        public static Drawing.FontFamily ConvertFontFamilyMediaToDrawing(Media.FontFamily mediaFontFamily)
        {
            return new Drawing.FontFamily(mediaFontFamily.Source);
        }

        /// <summary>
        /// Convert System.Drawing.FontFamily to System.Windows.Media.FontFamily.
        /// </summary>
        /// <param name="drawingFontFamily">System.Drawing.FontFamily to convert.</param>
        /// <returns>System.Windows.Media.FontFamily result.</returns>
        public static Media.FontFamily ConvertFontFamilyDrawingToMedia(Drawing.FontFamily drawingFontFamily)
        {
            return new Media.FontFamily(drawingFontFamily.Name);
        }

    }
}
