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
        }

        private  void SelectWords_Click(object sender, RoutedEventArgs e)
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

        private void SelectDirectory_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker1 = new BackgroundWorker();
            worker1.WorkerReportsProgress = true;
            worker1.DoWork += worker_DoWork;
            worker1.ProgressChanged += worker_ProgressChanged;
            worker1.RunWorkerAsync();

            var dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = data.InitialData.DirectorySearchFile;
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                SearchDirectory.Text = dialog.FileName;
                string fileName = "*.txt";
                data.ListFiles.Clear();
                try
                {
                    foreach (string findedFile in System.IO.Directory.EnumerateFiles(SearchDirectory.Text, fileName,
                   SearchOption.AllDirectories))
                    {
                        FileInfo FI;
                        try
                        {
                            FI = new FileInfo(findedFile);
                            if (FI.DirectoryName != data.InitialData.DirectoryForCopyFile)
                                data.ListFiles.Add(new FileSearch { NameFile = FI.Name, PathFile = FI.FullName, SizeFile = FI.Length });
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    Task task = data.Logger.SaveLogAsync($"{DateTime.Now} Выбрана директория для поиска: {SearchDirectory.Text}");
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

        private void SearchWordsUnsafe_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            
            //worker.RunWorkerAsync();

            data.FileCrud.SearchDangerFiles(data.ListFiles, data.ListWords, data.ListDangerFiles, worker);
            data.FileCrud.CopyFiles(data);
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

        //public async Task SearhFilesAsync()
        //{
        //    FileSearch file = new FileSearch();
        //    //await Task.Run(() => data.FileCrud.Search(data));
        //    for (var i=0;i<data.ListFiles.Count;i++)
        //    {
        //        await Task.Run(() =>
        //        {
        //            if (data.TextCrud.IsSearchWords(data.TextCrud.ReadTextOfFile(data.ListFiles[i].PathFile), data.ListWords))
        //            {
        //                file = data.ListFiles[i];
        //                data.ListDangerFiles.Add(file);
        //            }
                    
        //        });             
        //    }
        //}

        //private void Window_ContentRendered(object sender, EventArgs e)
        //{
        //    BackgroundWorker worker = new BackgroundWorker();
        //    worker.WorkerReportsProgress = true;
        //    worker.DoWork += worker_DoWork;
        //    worker.ProgressChanged += worker_ProgressChanged;

        //    worker.RunWorkerAsync();

        //}

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var size = data.ListFiles.Count;
            //if (data.ListFiles.Count == 0) size = 100;
            for (int i = 0; i <=size; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(10);
            }
        }

    }
}
