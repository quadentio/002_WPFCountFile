using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;

namespace _002_WPFCountFilesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] cmbItems = new string[] { "*.frm", "*.txt" };
        /// <summary>
        /// Main Method
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init display value or display property
        /// </summary>
        private void Init()
        {
            /* result textblock */
            this.resultTB.Visibility = Visibility.Hidden;

            /* format combobox */
            Array.ForEach(this.cmbItems, item => {
                this.formatCmb.Items.Add(item);
            });
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

            // 2. Process with result dialog
            DialogResult dialog = folderDialog.ShowDialog();

            if (dialog == System.Windows.Forms.DialogResult.OK)
            {
                this.pathTB.Text = folderDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Event fired after window is loaded
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        /// <summary>
        /// Event fire when count button is clicked
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
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
            /// Main process
            /*
             * (solution 1)
             * 1. Get all files and directory from pathTB
             * 2. If file: check if file is *.frm (end with .frm)
             * 3. If directory: recursive
             */
            /*
             * (solution 2)
             * 1. Get all *.frm files and directory from pathTB
             * 2. If file: count
             * 3. If directory: recursive
             */
            string targetFormat = formatCmb.SelectedItem.ToString();
            try
            {
                this.resultTB.Text = (Directory.GetFiles(this.pathTB.Text, targetFormat, SearchOption.AllDirectories)).Length.ToString();
            }
            catch (UnauthorizedAccessException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Show result
            this.resultTB.Foreground = new SolidColorBrush(Colors.Red);
            this.resultTB.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Validate pathTB
        /// </summary>
        /// <returns>True: valid path, False: invalid path</returns>
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
        /// Validate selected item from formatCmb
        /// </summary>
        /// <returns>True: item is selected, False: item is not selected</returns>
        private bool validateChosenFormat()
        {
            string formatItem = formatCmb.SelectedItem.ToString();
            if (String.IsNullOrEmpty(formatItem))
            {
                return false;
            }

            return true;
        }
    }
}
