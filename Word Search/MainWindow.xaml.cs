﻿using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
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
using WordSearch.Models;

namespace Word_Search
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataViewModels data = new DataViewModels();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = data;
        }

        private void SelectWords_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.FileName = "Запрещенные слова 1";
                dialog.DefaultExt = ".txt";
                dialog.Filter = "Text documents (.txt)|*.txt";
                dialog.InitialDirectory = @"D:\VASILII\Запрещенные слова";
                if (dialog.ShowDialog() == true)
                {
                    string filename = dialog.FileName;
                    var word = new Word { WordSearch = data.TextCrud.DeleteSigns(System.IO.File.ReadAllText(filename)) };
                    data.SelectedWord = word;
                }
                data.SetWordsCRUD.AddFromString(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        private void SelecDirectory_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
             CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                SearchDirectory.Text = dialog.FileName;
                string fileName = "*.txt";
                data.Files.Clear();
                foreach (string findedFile in System.IO.Directory.EnumerateFiles(SearchDirectory.Text, fileName,
                    SearchOption.AllDirectories))
                {
                    FileInfo FI;
                    try
                    {
                        FI = new FileInfo(findedFile);
                        data.Files.Add(new WordSearch.Models.File { NameFile = FI.Name, PathFile = FI.FullName });
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            if (data.Files.Count == 0) MessageBox.Show(" файлы не найдены");

        }
    }
}
