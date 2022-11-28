using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

namespace Hash2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filename = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;

                lblFileName.Content = filename;
            }
        }

        private async void ButtonComputeHash_Click(object sender, RoutedEventArgs e)
        {
            HashAlgorithm h = MD5.Create();
            
            lblAlgo.Content = ((ComboBoxItem)cboHashAlgo.SelectedItem).Content.ToString();

            switch (((ComboBoxItem)cboHashAlgo.SelectedItem).Content.ToString())
            {
                case "MD5":
                    h = MD5.Create();
                    break;
                case "SHA1":
                    h = SHA1.Create();
                    break;
                case "SHA384":
                    h = SHA384.Create();
                    break;
                case "SHA256":
                    h = SHA256.Create();
                    break;
                case "SHA512":
                    h = SHA512.Create();
                    break;
                //case "RIPEMD160":   // Does not work on net6. 
                //    h = RIPEMD160Managed.Create(); 
                //    break;
                default:
                    break;
                
                        
            }
            if (!File.Exists(filename))
                return;
            using (FileStream f = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var t = Task.Run(() => h.ComputeHash(f));
                
                byte[] result = await t;


                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("X2"));
                    if (i % 4 == 3)
                    {
                        sb.Append(" ");
                    }
                }
                txtHashResult.Text = sb.ToString();
            }
        }

        private void cboHashAlgo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lblAlgo.Content = ((ComboBoxItem)cboHashAlgo.SelectedItem).Content.ToString();
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtHashResult.Text.ToString());
        }
    }
}
