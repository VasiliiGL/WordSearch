using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using WordSearch.Models.Lib;

namespace WordSearch.Models.CRUDs
{
    public class FileCRUD
    {
        public delegate void AccountHandler(FileCRUD sender, LoggerEventArgs e);
        public event AccountHandler? Notify;

        public TextCRUD TextCrud { get; set; }
        public FileCRUD()
        {
            TextCrud = new TextCRUD();
        }

        
        public void SearchDangerFiles(ObservableCollection<FileSearch> listFiles, ObservableCollection<Word> listWords, ObservableCollection<FileSearch> ListDanderFiles, BackgroundWorker worker)
        {
            if (listFiles != null)
            {
                worker.RunWorkerAsync();
                ListDanderFiles.Clear();
                string wordForSearch;
                string textForSearch;
               
                for (var f = 0; f < listFiles.Count; f++)
                {
                    for (var w = 0; w < listWords.Count; w++)
                    {
                        wordForSearch = listWords[w].WordSearch;
                        textForSearch = TextCrud.ReadTextOfFile(listFiles[f].PathFile);
                        // textForSearch = TextCrud.DeleteSigns(textForSearch);
                        if (TextCrud.IsSearchWords(textForSearch, listWords))
                        {
                            ListDanderFiles.Add(listFiles[f]);
                            break;
                        }
                    }
                }
            }
        }


        public void CopyFiles(DataViewModels data)
        {
            if (data.ListDangerFiles != null)
            {

                Parallel.Invoke(() =>
                {
                    foreach (var file in data.ListDangerFiles)
                    {
                        CopyFile(file.PathFile, file.NameFile, data.InitialData.DirectoryForCopyFile);
                    }
                },
                async () =>
                {
                   if(data.ListDangerFiles.Count==1) await CreatCopyFiles(data.ListDangerFiles, data.ListWords, data.InitialData.DirectoryForCopyFile);
                   else
                    {
                        var listDangerFiles1 = new ObservableCollection<FileSearch>();
                        var listDangerFiles2 = new ObservableCollection<FileSearch>();
                        for (var i =0; i< data.ListDangerFiles.Count/2;i++)
                        {                          
                            listDangerFiles1.Add(data.ListDangerFiles[i]);
                        }
                        for (var i = data.ListDangerFiles.Count / 2; i < data.ListDangerFiles.Count; i++)
                        {
                            listDangerFiles2.Add(data.ListDangerFiles[i]);
                        }

                        await CreatCopyFiles(listDangerFiles1, data.ListWords, data.InitialData.DirectoryForCopyFile);
                        await CreatCopyFiles(listDangerFiles2, data.ListWords, data.InitialData.DirectoryForCopyFile);
                    }
                });
            }
        }

        public void CopyFile(string path, string name, string _pathDirectory)
        {
            string pathDirectory = _pathDirectory;
            string newpathFile = pathDirectory + @"\" + name;
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                CreatDirectory(_pathDirectory);
                fileInfo.CopyTo(newpathFile, true);
                Notify?.Invoke(this, new LoggerEventArgs($"{DateTime.Now} Сохранена копия копия файла: {name}"));
            }
        }
        public void CreatDirectory(string _pathDirectory)
        {
            string path = _pathDirectory;
            DirectoryInfo directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                directory.Create();
            }
        }
        //public void CreatCopyFile(FileSearch file, ObservableCollection<Word> setwords)
        //{
        //    string newNameFile = "New" + "_" + file.NameFile;
        //    string pathDirectory = @"D:\VASILII\Контрольная работа WordSearch\NewDirectory";
        //    string newpathFileCopy = pathDirectory + @"\" + newNameFile;
        //    string newText;
        //    StreamWriter streamWriter;
        //    try
        //    {
        //        FileInfo fileInfo = new FileInfo(newpathFileCopy);
        //        streamWriter = fileInfo.CreateText();
        //        newText = ReplaceTextInFile(file, setwords);
        //        streamWriter.WriteLine(newText);
        //        streamWriter.Close();
        //        Notify?.Invoke(this, new LoggerEventArgs($"{DateTime.Now} Создана копия файла с заменой слов: {newpathFileCopy}"));
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        async Task CreatCopyFiles(ObservableCollection<FileSearch> listDangerFiles, ObservableCollection<Word> setwords, string _pathDirectory)
        {
            await Task.Run(() =>
            {
                foreach (var file in listDangerFiles)
                {
                    string newNameFile = "New" + "_" + file.NameFile;
                    string pathDirectory = _pathDirectory;
                    string newpathFileCopy = pathDirectory + @"\" + newNameFile;
                    string newText;
                    StreamWriter streamWriter;
                    try
                    {
                        FileInfo fileInfo = new FileInfo(newpathFileCopy);
                        streamWriter = fileInfo.CreateText();
                        newText = ReplaceTextInFile(file, setwords);
                        streamWriter.WriteLine(newText);
                        streamWriter.Close();
                        Notify?.Invoke(this, new LoggerEventArgs($"{DateTime.Now} Создана копия файла с заменой слов: {newpathFileCopy}"));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                };
            });           
        }


        public string ReplaceTextInFile(FileSearch file, ObservableCollection<Word> SetWords)
        {
            var pathFile = file.PathFile;
            string textFile = TextCrud.ReadTextOfFile(pathFile);
            string newTextFile = "";   
            foreach (var word in SetWords)
            {
                newTextFile = TextCrud.ReplaceText(textFile, word.WordSearch);
                Notify?.Invoke(this, new LoggerEventArgs($"{DateTime.Now} Выполнена замена слов в файле: {pathFile}"));
            }
            return newTextFile;
        }

        //async Task<string> ReplaceTextInFileAsync(FileSearch file, ObservableCollection<Word> SetWords)
        //{
        //    var pathFile = file.PathFile;
        //    string textFile = TextCrud.ReadTextOfFile(pathFile);
        //    string newTextFile = "";
        //    await Task.Run(() =>
        //    {
        //        foreach (var word in SetWords)
        //        {
        //            newTextFile = TextCrud.ReplaceText(textFile, word.WordSearch);
        //        }
        //    });
        //    return newTextFile;
        //}

    }
}
