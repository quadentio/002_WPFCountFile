using System;
using System.IO;
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
using System.Windows.Forms;

namespace _002_WPFCountFilesApp
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

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.resultTB.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Event fire when button open is clicked
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">RoutedEventArgs</param>
        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            // 1. Create folder dialog
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;

            // 2. Process with folder dialog through result dialog
            DialogResult dialog = folderDialog.ShowDialog();

            if (dialog == System.Windows.Forms.DialogResult.OK)
            {
                this.pathTB.Text = folderDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        /// <summary>
        /// Event fire when count button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countBtn_Click(object sender, RoutedEventArgs e)
        {
            // validate information
            if (!validateDirectoryPath())
            {
                System.Windows.MessageBox.Show("Please choose your root folder", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!validateChosenFormat())
            {
                System.Windows.MessageBox.Show("Please choose file format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Main process
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool validateDirectoryPath()
        {
            string path = this.pathTB.Text;
            // Empty path or invalid path
            if (String.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool validateChosenFormat()
        {
            ComboBoxItem formatItem = (ComboBoxItem)formatCmb.SelectedItem;
            if (formatItem == null)
            {
                return false;
            }

            string formatValue = formatItem.Content.ToString();
            if (String.IsNullOrEmpty(formatValue))
            {
                return false;
            }
            return true;
        }
    }
}
