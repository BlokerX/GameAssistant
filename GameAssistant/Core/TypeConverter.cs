using System.Windows;
using Drawing = System.Drawing;
using Media = System.Windows.Media;

namespace GameAssistant.Core
{
    /// <summary>
    /// Class converter.
    /// </summary>
    public static class TypeConverter
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
        public static bool? ResizeModToBool(ResizeMode resizeMode)
        {
            switch (resizeMode)
            {
                //case ResizeMode.CanResize:
                case ResizeMode.CanResizeWithGrip:
                    return true;
                default:
                case ResizeMode.NoResize:
                    return false;
            }
        }

        /// <summary>
        /// Convert bool? to System.Windows.ResizeMode.
        /// </summary>
        /// <param name="resizeMode">A bool? to convert.</param>
        /// <returns>System.Windows.ResizeMode result.</returns>
        public static ResizeMode BoolToResizeMod(bool? resizeMode)
        {
            switch (resizeMode)
            {
                case true:
                    return ResizeMode.CanResizeWithGrip;
                default:
                case false:
                    return ResizeMode.NoResize;
            }
        }

        /// <summary>
        /// Convert System.Windows.Visibility to bool?.
        /// </summary>
        /// <param name="settingsBarVisibility">System.Windows.Visibility to convert.</param>
        /// <returns>The bool? result.</returns>
        public static bool? VisibilityToBool(Visibility settingsBarVisibility)
        {
            switch (settingsBarVisibility)
            {
                case Visibility.Visible:
                    return true;
                case Visibility.Hidden:
                case Visibility.Collapsed:
                default:
                    return false;
            }
        }

        /// <summary>
        /// Convert bool? to System.Windows.Visibility.
        /// </summary>
        /// <param name="settingsBarVisibilityBool">A bool? to convert.</param>
        /// <returns>System.Windows.Visibility result.</returns>
        public static Visibility BoolToVisibility(bool? settingsBarVisibilityBool)
        {
            switch (settingsBarVisibilityBool)
            {
                case true:
                    return Visibility.Visible;
                case false:
                default:
                    return Visibility.Hidden;
            }
        }

    }
}
