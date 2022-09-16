using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WordSearch.Models.CRUDs;

namespace WordSearch.Models.Models
{
    public class Report: DataViewModels
    {
        string pathReportFile = @"D:\VASILII\Контрольная работа WordSearch\NewDirectory\REPORT.txt";
        string listWord;

        public Report(DataViewModels data)
        {
            Directory = data.Directory;
            ListDangerFiles = data.ListDangerFiles;
            ListWords = data.ListWords;
            SaveFileReport();
        }
        public string GetPath()
        {
            return pathReportFile;
        }
        public string GetListWordToString()
        {
            foreach (var word in ListWords)
            {
                listWord = listWord + " " + string.Join(" ", word.WordSearch);
            }
            return listWord;
        }


        public void SaveFileReport()
        {
            FileCRUD.CrearFileReport(pathReportFile);
            FileCRUD.SaveTextInFile($"Cлова для поиска: {GetListWordToString()}",pathReportFile);
            foreach (var file in ListDangerFiles )
            {
                SaveFileReport(file);
            }
            
        }
        public void SaveFileReport(FileSearch file)
        {
            string nameFile = file.NameFile;

            FileCRUD.SaveTextInFile($"Имя файла: {nameFile}", pathReportFile);
            FileCRUD.SaveTextInFile($"Опасные слова: {file.ListWordsReport}", pathReportFile);

        }
    }
}
