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

        /// <summary>
        /// Convert System.Windows.ResizeMode to bool?.
        /// </summary>
        /// <param name="resizeMode">System.Windows.ResizeMode to convert.</param>
        /// <returns>The bool? tesult.</returns>
        public static bool? ResizeModToBool(System.Windows.ResizeMode resizeMode)
        {
            switch (resizeMode)
            {
                //case ResizeMode.CanResize:
                case System.Windows.ResizeMode.CanResizeWithGrip:
                    return true;
                default:
                case System.Windows.ResizeMode.NoResize:
                    return false;
            }
        }

        /// <summary>
        /// Convert bool? to System.Windows.ResizeMode.
        /// </summary>
        /// <param name="resizeMode">A bool? to convert.</param>
        /// <returns>System.Windows.ResizeMode result.</returns>
        public static System.Windows.ResizeMode BoolToResizeMod(bool? resizeMode)
        {
            switch (resizeMode)
            {
                case true:
                    return System.Windows.ResizeMode.CanResizeWithGrip;
                default:
                case false:
                    return System.Windows.ResizeMode.NoResize;
            }
        }

    }
}
