using ImageComparison.ComparisonAlgorithms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace ImageComparison
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void ButtonAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\ImageTest\\";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            dlg.RestoreDirectory = true;

            if(dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                try
                {
                    switch((sender as Button).Uid)
                    {
                        case "1":
                            Image1.Source = new BitmapImage(new Uri(selectedFileName));
                            break;
                        case "2":
                            Image2.Source = new BitmapImage(new Uri(selectedFileName));
                            break;
                    }
                    
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ButtonCompare_Click(object sender, RoutedEventArgs e)
        {
            if(Image1.Source==null || Image2.Source == null)
            {
                MessageBox.Show("Choose pictures");
                return;
            }
            BitmapImage bitmapImage1 = (BitmapImage)Image1.Source;
            BitmapImage bitmapImage2 = (BitmapImage)Image2.Source;

            List<bool> bList1 = EqualPixels.GetHash(bitmapImage1);
            List<bool> bList2 = EqualPixels.GetHash(bitmapImage2);

            //count equal elementss of lists
            int equalElements = bList1.Zip(bList2, (i, j) => i == j).Count(eq => eq);
            int percentage = equalElements * 100 / 256;
            MessageBox.Show("Images are "+percentage.ToString()+"% equal");

            //convert list of boolean to list of int
            var tempList = new List<int>();
            foreach (var t in bList1)
                switch (t)
                {
                    case true:
                        tempList.Add(1);
                        break;
                    case false:
                        tempList.Add(0);
                        break;
                }
            //convert list of int to string
            var strings =
            tempList.Select(i => i.ToString(CultureInfo.InvariantCulture))
                .Aggregate((s1, s2) => s1 + ", " + s2);

            MessageBox.Show(strings);
        }
    }
}
