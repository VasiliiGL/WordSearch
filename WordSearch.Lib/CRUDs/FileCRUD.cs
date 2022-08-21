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

namespace WordSearch.Models.CRUDs
{
    public class FileCRUD
    {
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
        //public ObservableCollection<FileSearch> SearchDangerFiles(DataViewModels data, int amount)
        //{

        //    if (data.ListFiles != null)
        //    {
        //        //ListDanderFiles.Clear();
        //        string wordForSearch;
        //        string textForSearch;
        //        for (var f = 0; f < amount; f++)
        //        {
        //            for (var w = 0; w < data.ListWords.Count; w++)
        //            {
        //                wordForSearch = data.ListWords[w].WordSearch;
        //                textForSearch = TextCrud.ReadTextOfFile(data.ListFiles[f].PathFile);
        //                // textForSearch = TextCrud.DeleteSigns(textForSearch);
        //                if (TextCrud.IsSearchWords(textForSearch, data.ListWords))
        //                {
        //                    data.ListDangerFiles.Add(data.ListFiles[f]);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return data.ListDangerFiles;
        //}
        //public ObservableCollection<FileSearch> SearchDangerFiles(DataViewModels data, int amount1, int amount2)
        //{


        //    string wordForSearch;
        //    string textForSearch;
        //    for (var f = amount1; f < amount2; f++)
        //    {
        //        for (var w = 0; w < data.ListWords.Count; w++)
        //        {
        //            wordForSearch = data.ListWords[w].WordSearch;
        //            textForSearch = TextCrud.ReadTextOfFile(data.ListFiles[f].PathFile);
        //            textForSearch = TextCrud.DeleteSigns(textForSearch);
        //            if (TextCrud.IsSearchWords(textForSearch, data.ListWords))
        //            {
        //                data.ListDangerFiles.Add(data.ListFiles[f]);
        //                break;
        //            }
        //        }
        //    }

        //    return data.ListDangerFiles;
        //}


        //public void Search(DataViewModels data)
        //{
        //    var list1 = new ObservableCollection<FileSearch>();
        //    var list2 = new ObservableCollection<FileSearch>();
        //    int amount_1;
        //    int amount_2;
        //    var task = new Task[2];
        //    int size = data.ListFiles.Count;
        //    if (data.ListFiles != null)
        //    {
        //        if (data.ListFiles.Count >= 2)
        //        {
        //            amount_1 = data.ListFiles.Count / 2;
        //            amount_2 = data.ListFiles.Count - amount_1;
        //            list1 = SearchDangerFiles(data, amount_1);
        //            list2 = SearchDangerFiles(data, amount_1, amount_2);
        //            foreach (var file in list1)
        //            {
        //                data.ListDangerFiles.Add(file);
        //            }
        //            foreach (var file in list2)
        //            {
        //                data.ListDangerFiles.Add(file);
        //            }
        //            return data.ListDangerFiles;
        //        }
        //        else
        //        {
        //            list1 = SearchDangerFiles(data, data.ListFiles.Count);
        //            foreach (var file in list1)
        //            {
        //                data.ListDangerFiles.Add(file);

        //            }
        //            return data.ListDangerFiles;
        //        }
        //    }

        //    return data.ListDangerFiles;
        //}

        //public async Task SearchAsync(DataViewModels data, CancellationToken toren )
        //{
        //    await Task.Run((Action)(() =>
        //   {
        //       if (toren.IsCancellationRequested)
        //       {
        //           Console.WriteLine("прервано");
        //           toren.ThrowIfCancellationRequested();
        //       }
        //       this.SearchAsync((DataViewModels)data);
        //   }), toren);
        //}
        //public async Task<ObservableCollection<File>> SearchAsync(DataViewModels data)
        //{
        //    await Task.Run((Action)(() =>
        //    {             
        //        this.SearchAsync((DataViewModels)data);
        //    }));
        //    return data.ListDangerFiles;
        //}


        public void CopyFiles(ObservableCollection<FileSearch> listFiles, ObservableCollection<Word> setwords)
        {
            if (listFiles != null)
            {
                foreach (var file in listFiles)
                {
                    CopyFile(file.PathFile, file.NameFile);
                    CreatCopyFile(file, setwords);
                }
            }
        }
        public void CopyFile(string path, string name)
        {
            string pathDirectory = @"D:\VASILII\Контрольная работа WordSearch\NewDirectory";
            string newpathFile = pathDirectory + @"\" + name;
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                CreatDirectory();
                fileInfo.CopyTo(newpathFile, true);
            }
        }
        public void CreatDirectory()
        {
            string path = @"D:\VASILII\Контрольная работа WordSearch\NewDirectory";
            DirectoryInfo directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                directory.Create();
            }
        }
        public void CreatCopyFile(FileSearch file, ObservableCollection<Word> setwords)
        {
            string newNameFile = "New" + "_" + file.NameFile;
            string pathDirectory = @"D:\VASILII\Контрольная работа WordSearch\NewDirectory";
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public string ReplaceTextInFile(FileSearch file, ObservableCollection<Word> SetWords)
        {
            var pathFile = file.PathFile;
            string textFile = TextCrud.ReadTextOfFile(pathFile);
            string newTextFile = "";   
            foreach (var word in SetWords)
            {
                newTextFile = TextCrud.ReplaceText(textFile, word.WordSearch);
            }
            return newTextFile;
        }

    }
}
