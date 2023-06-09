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

using System.IO;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GetRandomFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = "";
        List<string> files = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void choiseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.files = new List<string>();
            string t = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                List<string> files = new List<string>();
                List<string> folders = new List<string>();
                folders.Add(dialog.FileName);
                while (folders.Count != 0)
                {
                    List<string> folderst = new List<string>();
                    foreach (string folder in folders)
                    {
                        List<string> paths = new List<string>();
                        paths.AddRange(Directory.GetFiles(folder));
                        paths.AddRange(Directory.GetDirectories(folder));
                        if (searchInFolders.IsChecked != null && (bool)searchInFolders.IsChecked) {
                            foreach (string path in paths)
                            {
                                if (File.Exists(path))
                                {
                                    this.files.Add(path);
                                }
                                if (Directory.Exists(path))
                                {
                                    folderst.Add(path);
                                }
                            }
                        }
                        else
                        {
                            foreach (string path in paths)
                            {
                                if (File.Exists(path))
                                {
                                    this.files.Add(path);
                                }
                            }
                        }
                    }
                    folders = folderst;
                }
                DataInfo.Content = $"Files found: {this.files.Count}.";
            }
            else
            {
                DataInfo.Content = $"Files found: err.";
            }
        }

        private void openBtn_Click(object sender, RoutedEventArgs e)
        {

            if (path != "")
            {
                System.Diagnostics.Process.Start("explorer.exe", "/select, \"" + path + "\"");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (files.Count != 0)
            {
                var r = new Random();
                path = files[r.Next(0, files.Count - 1)];
                RandomFileInfo.Content = path;
            }
        }
    }
}
