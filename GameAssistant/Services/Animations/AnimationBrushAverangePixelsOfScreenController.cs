using System.Windows.Media;

namespace GameAssistant.Services.Animations
{
    /// <summary>
    /// Averange pixels of screen animation controller.
    /// </summary>
    internal class AnimationBrushAverangePixelsOfScreenController : AnimationControllerBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="animationInterval">Refresh time.</param>
        public AnimationBrushAverangePixelsOfScreenController(double animationInterval = 90) : base(animationInterval) { }

        /// <summary>
        /// Animate as RGB style.
        /// </summary>
        protected override void Animate()
        {
            var tmpColor = default(Color);
            if (animationTimer.Enabled)
                System.Windows.Application.Current?.Dispatcher.Invoke(() => tmpColor = ((SolidColorBrush)(brush.Variable)).Color);

            var screen = new System.Drawing.Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var gfx = System.Drawing.Graphics.FromImage(screen);
            gfx.CopyFromScreen(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Location, new System.Drawing.Point(0, 0), System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size);

            var tmp = GetAverageColor(screen);

            tmpColor = Color.FromRgb(tmp.R, tmp.G, tmp.B);

            System.Windows.Application.Current?.Dispatcher.Invoke(() => brush.Variable = new SolidColorBrush(tmpColor));
        }

        private unsafe System.Drawing.Color GetAverageColor(System.Drawing.Bitmap image, int sampleStep = 1)
        {
            var data = image.LockBits(
                new System.Drawing.Rectangle(System.Drawing.Point.Empty, image.Size),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            var row = (int*)data.Scan0.ToPointer();
            var (sumR, sumG, sumB) = (0L, 0L, 0L);
            var stride = data.Stride / sizeof(int) * sampleStep;

            for (var y = 0; y < data.Height; y += sampleStep)
            {
                for (var x = 0; x < data.Width; x += sampleStep)
                {
                    var argb = row[x];
                    sumR += (argb & 0x00FF0000) >> 16;
                    sumG += (argb & 0x0000FF00) >> 8;
                    sumB += argb & 0x000000FF;
                }
                row += stride;
            }

            image.UnlockBits(data);

            var numSamples = data.Width / sampleStep * data.Height / sampleStep;
            var avgR = sumR / numSamples;
            var avgG = sumG / numSamples;
            var avgB = sumB / numSamples;
            return System.Drawing.Color.FromArgb((int)avgR, (int)avgG, (int)avgB);
        }

        //private Color GetAverangeScreenColor()
        //{
        //    int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        //    int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        //    System.Drawing.Color[,] pixels = new System.Drawing.Color[screenWidth, screenHeight];

        //    for (int i = 1; i <= screenHeight; i++)
        //    {
        //        for (int j = 1; j <= screenWidth; j++)
        //        {
        //            pixels[i, j] = GetPixelColor(i, j);
        //        }
        //    }

        //    ulong[] averangeScreenColor = new ulong[3];
        //    for (int i = 1; i <= screenHeight; i++)
        //    {
        //        ulong[] averangeScreenColorWidth = new ulong[3];

        //        for (int j = 1; j <= screenWidth; j++)
        //        {
        //            averangeScreenColorWidth[0] += pixels[i, j].R;
        //            averangeScreenColorWidth[1] += pixels[i, j].G;
        //            averangeScreenColorWidth[2] += pixels[i, j].B;
        //        }

        //        averangeScreenColorWidth[0] = averangeScreenColorWidth[0] / (ulong)screenWidth;
        //        averangeScreenColorWidth[1] = averangeScreenColorWidth[1] / (ulong)screenWidth;
        //        averangeScreenColorWidth[2] = averangeScreenColorWidth[2] / (ulong)screenWidth;

        //        averangeScreenColor[0] += averangeScreenColorWidth[0];
        //        averangeScreenColor[1] += averangeScreenColorWidth[1];
        //        averangeScreenColor[2] += averangeScreenColorWidth[2];
        //    }

        //    averangeScreenColor[0] = averangeScreenColor[0] / (ulong)screenHeight;
        //    averangeScreenColor[1] = averangeScreenColor[1] / (ulong)screenHeight;
        //    averangeScreenColor[2] = averangeScreenColor[2] / (ulong)screenHeight;

        //    return Color.FromRgb((byte)averangeScreenColor[0], (byte)averangeScreenColor[1], (byte)averangeScreenColor[2]);
        //}

        //#region Helpers

        //[DllImport("user32.dll")]
        //static extern IntPtr GetDC(IntPtr hwnd);
        //[DllImport("user32.dll")]
        //static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        //[DllImport("gdi32.dll")]
        //static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        //public System.Drawing.Color GetPixelColor(int x, int y)
        //{
        //    IntPtr hdc = GetDC(IntPtr.Zero);
        //    uint pixel = GetPixel(hdc, x, y);
        //    ReleaseDC(IntPtr.Zero, hdc);
        //    return System.Drawing.Color.FromArgb((int)pixel);
        //}

        //#endregion

    }
}
