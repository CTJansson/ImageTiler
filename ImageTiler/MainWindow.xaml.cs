using Hjg.Pngcs;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageTiler
{
    public partial class MainWindow : Window // FIXA FILESTREAMING ISTÄLLET FÖR ATT HÄMTA IN IMAGE DIREKT TILL APPLIKATIONEN
    {
        private System.Drawing.Image img;
        private int imageWidth;
        private int imageHeight;
        private int cropSize;
        private string format;
        private string filename;
        private int zoomLevel;

        public MainWindow()
        {
            InitializeComponent();

            // DEFAULT VALUES
            cropSize = 256;
            format = ".png";
            zoomLevel = 1;
            // END

            this.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\C#\ImageTiler\ImageTiler\background.png")));

            imageBrowseBtn.Click += ImageBrowseBtn_Click;
            tileImageBtn.Click += TileImageBtn_Click;
        }

        private void TileImageBtn_Click(object sender, RoutedEventArgs e)
        {
            string baseFolderPath = @"c:\ImageTiler";
            Directory.CreateDirectory(baseFolderPath);

            int lengthX = 0;
            int lengthY = 0;
            int lengthZ = 0; 

            double width = imageWidth / cropSize;
            lengthX = (int)Math.Round(width);

            double height = imageHeight / cropSize;
            lengthY = (int)Math.Round(height);

            lengthZ = zoomLevel;

            for (int z = lengthZ; z > 0; z--)
            {
                string zFolderPath = Path.Combine(baseFolderPath, z.ToString());
                Directory.CreateDirectory(zFolderPath);

                int multiplier = 1;

                for (int x = 0; x < lengthX; x++)
                {
                    string xFolderPath = Path.Combine(zFolderPath, x.ToString());
                    Directory.CreateDirectory(xFolderPath);

                    for (int y = 0; y < lengthY; y++)
                    {
                        CropImageAndSave(xFolderPath, y, x, multiplier);
                    }
                }

                multiplier = multiplier * 2;
            }

            img.Dispose();
        }

        private void CropImageAndSave(string xFolderPath, int y, int x, int multiplier)
        {
            //FileStream fs = new FileStream(filename, FileMode.Open);
            //PngReader pngrdr = new PngReader(fs);

            Bitmap bmp = new Bitmap(cropSize, cropSize);

            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(img, new Rectangle(0, 0, cropSize, cropSize), new Rectangle(x * cropSize, y * cropSize, multiplier * cropSize, multiplier * cropSize), GraphicsUnit.Pixel);

            string imageSavePath = Path.Combine(xFolderPath, y.ToString() + format);

            if (format == ".png")
            {
                bmp.Save(imageSavePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            else if (format == ".jpg")
            {
                bmp.Save(imageSavePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            bmp.Dispose();
            g.Dispose();
        }

        private void ImageBrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|ALL FILES (*)|*.*";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
                filename = dlg.FileName;

            imagePathTxtBox.Text = filename;
            img = System.Drawing.Image.FromFile(filename);

            imageWidth = img.Width;
            imageHeight = img.Height;

        }

        private void ChangeCropSize(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;

            if (radioButton.Name == "radio128")
                cropSize = 128;
            else if (radioButton.Name == "radio256")
                cropSize = 256;
            else if (radioButton.Name == "radio512")
                cropSize = 512;
            else if (radioButton.Name == "radio1024")
                cropSize = 1024;
        }

        private void ChangeFormat(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;

            if (radioButton.Name == "radioJPG")
                format = ".jpg";
            else if (radioButton.Name == "radioPNG")
                format = ".png";
        }

        private void ChangeZoomLevel(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
            {
                return;
            }

            if (radioButton.Name == "zoomLevel1")
            {
                zoomLevel = 1;
            }
            else if (radioButton.Name == "zoomLevel2")
            {
                zoomLevel = 2;
            }
            else if (radioButton.Name == "zoomLevel3")
            {
                zoomLevel = 3;
            }
            else if (radioButton.Name == "zoomLevel4")
            {
                zoomLevel = 4;
            }
            else if (radioButton.Name == "zoomLevel5")
            {
                zoomLevel = 5;
            }
            else if (radioButton.Name == "zoomLevel6")
            {
                zoomLevel = 6;
            }
            else if (radioButton.Name == "zoomLevel7")
            {
                zoomLevel = 7;
            }
        }
    }
}
