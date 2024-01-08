using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.Model
{
    public static class Images
    {
        public readonly static ImageSource cell = LoadImage("Terrain/Placeholder.png");
        public readonly static ImageSource castle = LoadImage("GridContents/Castle.png");
        public readonly static ImageSource desertCity = LoadImage("GridContents/DesertCity.png");
        public readonly static ImageSource castle2 = LoadImage("GridContents/Castle2.png");
        public readonly static ImageSource village = LoadImage("GridContents/Village.png");
        public readonly static ImageSource carriage = LoadImage("GridContents/Carriage.png");
        public readonly static List<ImageSource> desert = LoadNumbered("Terrain/FancySand1.png");
        public readonly static List<ImageSource> grasslands = LoadNumbered("Terrain/FancyGrass1.png");
        public readonly static List<ImageSource> water = LoadNumbered("Terrain/FancyWater1.png");

        public readonly static ImageSource player = LoadImage("Player/PlayerDown0.png");



        public static ImageSource RandomDesert { get { return desert[Random.Shared.Next(0, desert.Count)]; } }
        public static ImageSource RandomGrasslands { get { return grasslands[Random.Shared.Next(0, grasslands.Count)]; } }
        public static ImageSource RandomWater { get { return water[Random.Shared.Next(0, water.Count)]; } }

        private static ImageSource LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"pack://application:,,,/Assets/Images/{fileName}", UriKind.RelativeOrAbsolute));
        }

        private static ImageSource LoadFullPath(string fullPath)
        {
            return new BitmapImage(new Uri($"{fullPath}", UriKind.Absolute));
        }

        public static List<ImageSource> LoadNumbered(string fileName, int count = 0)
        {
            List<ImageSource> imgList = new List<ImageSource>();
            if (count == 0) 
            {
                count = int.MaxValue;
            }
            int pointPos = fileName.LastIndexOf('.');
            if (pointPos == -1) { throw new Exception($"{fileName} does not contain \".\""); };
            int numPos = pointPos - 1;
            if (numPos < 0 || !char.IsNumber(fileName[numPos])) { throw new Exception($"{fileName} does not contain number before \".\""); }
            while (numPos > 0 && char.IsNumber(fileName[numPos])) { numPos--; }
            string postfix = fileName.Substring(pointPos);
            string prefix = fileName.Substring(0, numPos+1);
            int imgNum = int.Parse(fileName.Substring(numPos + 1, pointPos - numPos - 1));
            while (count-- > 0) 
            {
                var bitmap = new BitmapImage();
                try
                {
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri($"pack://application:,,,/Assets/Images/{prefix + imgNum.ToString() + postfix}", UriKind.RelativeOrAbsolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    imgNum++;
                    imgList.Add(bitmap);
                }
                catch
                {
                    break;
                }
            }
            return imgList;
        }

        public static ImageSource Merge(BitmapImage img1, BitmapImage img2)
        {
            // Loads the images to tile (no need to specify PngBitmapDecoder, the correct decoder is automatically selected)
            BitmapFrame frame1 = BitmapFrame.Create(img1);
            BitmapFrame frame2 = BitmapFrame.Create(img2);

            // Draws the images into a DrawingVisual component
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen()) 
            {
                drawingContext.DrawImage(frame1, new System.Windows.Rect(0,0,img1.PixelWidth, img1.PixelHeight));
                drawingContext.DrawImage(frame2, new System.Windows.Rect(0, 0, img2.PixelWidth, img2.PixelHeight));
            }

            // Converts the Visual (DrawingVisual) into a BitmapSource
            RenderTargetBitmap bmp = new RenderTargetBitmap(img1.PixelWidth, img1.PixelHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            return bmp;
        }

        public static ImageSource MultiMerge(List<BitmapImage> imgs)
        {
            int width = imgs[0].PixelWidth;
            int height = imgs[0].PixelHeight;
            List<BitmapFrame> frames = new List<BitmapFrame>();
            for (int i = 0; i < imgs.Count; i++) 
            {
                if (width != imgs[i].PixelWidth || height != imgs[i].PixelHeight)
                    throw new Exception("Unmatched Images size");
                frames.Add(BitmapFrame.Create(imgs[i]));
            }
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
               for (int i = 0; i < frames.Count; i++)
                {
                    drawingContext.DrawImage(frames[i], new System.Windows.Rect(0, 0, width, height));
                }
            }
            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            bmp.Freeze();

            return bmp;
        }
    }
}
