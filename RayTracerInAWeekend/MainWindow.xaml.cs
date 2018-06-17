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
        private WriteableBitmap Bitmap;

        public MainWindow()
        {
            InitializeComponent();
            ImageComponent.Width = Renderer.IMG_WIDTH;
            ImageComponent.Height = Renderer.IMG_HEIGHT;

            var pixelFormat = PixelFormats.Rgb24;
            Bitmap = new WriteableBitmap(
                Renderer.IMG_WIDTH,
                Renderer.IMG_HEIGHT,
                96,
                96,
                pixelFormat,
                null
                );
            bpp = pixelFormat.BitsPerPixel / 8;
            sourceRect = new Int32Rect(0, 0, Renderer.IMG_WIDTH, Renderer.IMG_HEIGHT);
            imgBuffer = new byte[Bitmap.PixelWidth * Bitmap.PixelHeight * bpp];
            stride = bpp * Renderer.IMG_WIDTH;

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
            Renderer.Render(bpp, imgBuffer);

            Bitmap.WritePixels(sourceRect, imgBuffer, stride, 0);
        }
    }
}
