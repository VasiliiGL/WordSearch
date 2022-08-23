using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WordSearch.Models.CRUDs;

namespace WordSearch.Models.Lib
{
    public class Logger
    {
        public FileCRUD FileCrudLog { get; set; }
        static string pathDirectory = @"D:\VASILII\Контрольная работа WordSearch\NewDirectory";
        static string pathFileLog = pathDirectory + @"\" + "logger.txt";
        public Logger()
        {
            FileCrudLog = new FileCRUD(); 
        }
        public async Task SaveLogAsync (string message)
        {
            FileCrudLog.CreatDirectory();
            
            using (StreamWriter writerLog = new StreamWriter(pathFileLog, true))
            {
                await writerLog.WriteLineAsync(message);
            }
        }
        public async Task ClearLogAsync()
        {
            using (StreamWriter writer = new StreamWriter(pathFileLog, false))
            {
                await writer.WriteLineAsync($"Дата создания файла:{DateTime.Now}");
            }
        }
    }
}
