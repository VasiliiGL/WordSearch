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

        
        public async Task SearchDangerFilesAsync(DataViewModels data, IProgress<int> progress)
        {

            if (data.ListFiles != null)
            {
                //worker.RunWorkerAsync();
                data.ListDangerFiles.Clear();
                string wordForSearch;
                string textForSearch;
                await Task.Run(() =>
                {
                    for (int i = 0; i <= data.ListFiles.Count; i++)
                    {
                        progress.Report(i);
                        Thread.Sleep(100);
                    }
                    progress.Report(100);
                });

                for (var f = 0; f < data.ListFiles.Count; f++)
                {
                    //progress.Report(f);
                   
                    for (var w = 0; w < data.ListWords.Count; w++)
                    {
                        wordForSearch = data.ListWords[w].WordSearch;
                        textForSearch = TextCrud.ReadTextOfFile(data.ListFiles[f].PathFile);
                        // textForSearch = TextCrud.DeleteSigns(textForSearch);
                        if (TextCrud.IsSearchWords(textForSearch, data.ListWords))
                        {
                            data.ListFiles[f].Text = data.TextCrud.ReadTextOfFile(data.ListFiles[f].PathFile);
                            data.ListDangerFiles.Add(data.ListFiles[f]);
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
                        //SaveDataReport(file, data);
                    }
                },
                async () =>
                {
                    if (data.ListDangerFiles.Count == 1) await CreatCopyFilesAsyng(data.ListDangerFiles, data.ListWords, data.InitialData.DirectoryForCopyFile);
                    else
                    {
                        var listDangerFiles1 = new ObservableCollection<FileSearch>();
                        var listDangerFiles2 = new ObservableCollection<FileSearch>();
                        for (var i = 0; i < data.ListDangerFiles.Count / 2; i++)
                        {
                            listDangerFiles1.Add(data.ListDangerFiles[i]);
                        }
                        for (var i = data.ListDangerFiles.Count / 2; i < data.ListDangerFiles.Count; i++)
                        {
                            listDangerFiles2.Add(data.ListDangerFiles[i]);
                        }

                        await CreatCopyFilesAsyng(listDangerFiles1, data.ListWords, data.InitialData.DirectoryForCopyFile);
                        await CreatCopyFilesAsyng(listDangerFiles2, data.ListWords, data.InitialData.DirectoryForCopyFile);
                    }
                
                },
                async () =>
                {
                    foreach (var file in data.ListDangerFiles)
                    {

                        await SaveDataReportAsyng(file, data);
                        Notify?.Invoke(this, new LoggerEventArgs($"{DateTime.Now} Запуск асинхронного подчета слов в файле {file.NameFile}"));
                    }
                }
                );
            }
        }

        public async Task SaveDataReportAsyng(FileSearch file, DataViewModels data)
        {
            await Task.Run(() => data.TextCrud.SearchDangerWord(file, data.ListWords));
        }
        public void SaveDataReport(FileSearch file, DataViewModels data)
        {
             data.TextCrud.SearchDangerWord(file, data.ListWords);
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

        

       
        public async Task CreatCopyFilesAsyng(ObservableCollection<FileSearch> listDangerFiles, ObservableCollection<Word> setwords, string _pathDirectory)
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
                        Notify?.Invoke(this, new LoggerEventArgs($"{DateTime.Now} Запуск асинхронного создания копия файла с заменой слов: {newpathFileCopy}"));
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


    }
}
