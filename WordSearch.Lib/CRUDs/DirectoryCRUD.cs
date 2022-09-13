using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WordSearch.Models.CRUDs
{
    public class DirectoryCRUD
    {
        public async Task SearchFilesInDirectoryAsync(DataViewModels data, string path, string fileName, IProgress<int> progress)
        {
            data.ListFiles.Clear();
            var amountFiles = Directory.GetFiles(path).Length;
            var amountDir = Directory.GetDirectories(path);
            foreach (var dir in amountDir)
            {
                amountFiles += Directory.GetFiles(dir).Length;
            }
            await Task.Run(() =>
            {
                for (int i = 0; i <= amountFiles; i++)
                {
                    progress.Report(i);
                    if (amountFiles < 100) Thread.Sleep(100);
                }
                progress.Report(100);
            });
            foreach (string findedFile in System.IO.Directory.EnumerateFiles(path, fileName, SearchOption.AllDirectories))
            {
                //progress.Report(data.ListFiles.Count);
                FileInfo FI;
                    try
                    {
                        FI = new FileInfo(findedFile);
                        if (FI.DirectoryName != data.InitialData.DirectoryForCopyFile)
                        {                         
                            data.ListFiles.Add(new FileSearch
                            {
                                NameFile = FI.Name,
                                PathFile = FI.FullName,
                                SizeFile = FI.Length,
                                //Text = data.TextCrud.ReadTextOfFile(FI.FullName)
                            });
                           
                        
                        }
                    //progress.Report(100);
                }
                    catch
                    {
                        continue;
                    }
             }
                
           
        }

        
    }
}
