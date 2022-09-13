using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            Task taskLogClear = data.Logger.ClearLogAsync();
            data.FileCrud.Notify += data.Logger.SaveMessage;
            BackgroundWorker worker;
        }
        
        private void SelectWords_Click(object sender, RoutedEventArgs e)
        {
            data.ListDangerFiles.Clear();
            try
            {
                var dialog = new OpenFileDialog();
                dialog.FileName = "";
                dialog.DefaultExt = ".txt";
                dialog.Filter = "Text documents (.txt)|*.txt";
                dialog.InitialDirectory = data.InitialData.DirectoryForWords;
                if (dialog.ShowDialog() == true)
                {
                    string filename = dialog.FileName;
                    var word = new Word { WordSearch = data.TextCrud.DeleteSigns(System.IO.File.ReadAllText(filename)) };
                    data.SelectedWord = word;
                    Task task = data.Logger.SaveLogAsync($"{DateTime.Now} Выбраны слова для поиска: {data.SelectedWord.WordSearch}");
                }
                data.ListWordsCrud.AddFromString(data);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Task task = data.Logger.SaveLogAsync($"{DateTime.Now} Ошибка: {ex}");
            }

        }
        private void ReportProgress(int value)
        {
            pbStatus.Value = value;
    
        }
        private async void SelectDirectory_Click(object sender, RoutedEventArgs e)
        {
            var progress = new Progress<int>(ReportProgress);
            
            data.ListFiles.Clear();
            var dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = data.InitialData.DirectorySearchFile;
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            
            if (result == CommonFileDialogResult.Ok)
            {
                SearchDirectory.Text = dialog.FileName;
                string fileName = "*.txt";

                try
                {
                    Label_progress.Content = "Поиск файлов в директории...";
                    //var progress = new Progress<int>(ReportProgress);
                    await data.DirectoryCrud.SearchFilesInDirectoryAsync(data, SearchDirectory.Text, fileName, progress);
                    Task task = data.Logger.SaveLogAsync($"{DateTime.Now} Выбрана директория для поиска: {SearchDirectory.Text}");
                    Label_progress.Content = "Поиск файлов в директории завершен";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    Task task = data.Logger.SaveLogAsync($"{DateTime.Now} Ошибка: {ex}");
                }
            }
            if (data.ListFiles.Count == 0)
            {
                MessageBox.Show("Файлы не найдены");
                Task task = data.Logger.SaveLogAsync($"{DateTime.Now} Файлы не найдены");
            }
        }

        private async void SearchWordsUnsafe_Click(object sender, RoutedEventArgs e)
        {

            var progress = new Progress<int>(ReportProgress);
            Label_progress.Content = "Поиск опасных файлов ...";
            await data.FileCrud.SearchDangerFilesAsync(data, progress);
            data.FileCrud.CopyFiles(data);
            Label_progress.Content = "Поиск опасных файлов завершен";
            if (data.ListDangerFiles.Count == 0)
            {
                _ = MessageBox.Show("Файлы не найдены");
                Task task = data.Logger.SaveLogAsync($"{DateTime.Now} Файлы не найдены");
            }
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow reportWindow = new ReportWindow(data);
            reportWindow.Show();
        }

        private void PrintReport_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
