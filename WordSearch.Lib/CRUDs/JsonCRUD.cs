using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WordSearch.Models.Lib;

namespace WordSearch.Models.CRUDs
{
    public class JsonCRUD
    {

        public static async Task SaveJsonAsync(Initial initialData)
        {
            FileStream fs = new FileStream("InitilData.json", FileMode.OpenOrCreate);          
            await JsonSerializer.SerializeAsync<Initial>(fs, initialData);
        }
        public void СhangeInitialDataInJson(string initialDirectoryForWords, string directoryForCopyFile, string directorySearchFile)
        {
            FileStream fs = new FileStream("InitilData.json", FileMode.OpenOrCreate);
            Initial initialData = new Initial(initialDirectoryForWords, directoryForCopyFile, directorySearchFile);
            Task task = SaveJsonAsync(initialData);
        }
        public static async Task<Initial> ReadJsonFileAsync()
        {
            FileStream fs = new FileStream("InitilData.json", FileMode.OpenOrCreate);
            var initialData = await JsonSerializer.DeserializeAsync<Initial>(fs);
            return initialData;
        }
        public static Initial ReadJsonFileAsync(string path)
        {
            using var fs = new FileStream(path, FileMode.Open);
            var initialData = JsonSerializer.DeserializeAsync<Initial>(fs).Result;
            return initialData;
        }
        public static Initial ReadJsonFile()
        {
            using var fs = new FileStream("InitilData.json", FileMode.OpenOrCreate);
            var initialData = JsonSerializer.DeserializeAsync<Initial>(fs).Result;
            return initialData;
        }
        public static void SaveJson(Initial initialData)
        {
            using var fs = new FileStream("InitilData.json", FileMode.OpenOrCreate);
            JsonSerializer.SerializeAsync<Initial>(fs, initialData);
        }

    }
}
