using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RayTracerInAWeekend
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly int imgW, imgH;

        private WriteableBitmap Bitmap;

        public MainWindow()
        {
            InitializeComponent();
            imgW = (int) ImageComponent.Width;
            imgH = (int) ImageComponent.Height;

            var pixelFormat = PixelFormats.Rgb24;
            Bitmap = new WriteableBitmap(
                imgW,
                imgH,
                96,
                96,
                pixelFormat,
                null
                );
            bpp = pixelFormat.BitsPerPixel / 8;
            sourceRect = new Int32Rect(0, 0, imgW, imgH);
            imgBuffer = new byte[Bitmap.PixelWidth * Bitmap.PixelHeight * bpp];
            stride = bpp * imgW;

            ImageComponent.Source = Bitmap;

            Render();
        }

        private int bpp;
        private int stride;
        private Int32Rect sourceRect;
        private byte[] imgBuffer;
        private void Render()
        {
            Array.Clear(imgBuffer, 0, imgBuffer.Length);
            Renderer.Render(imgW, imgH, bpp, imgBuffer);

            Bitmap.WritePixels(sourceRect, imgBuffer, stride, 0);
        }
    }
}
