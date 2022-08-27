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
        //public Initial initialData { get; set; }
        public async Task SaveJsonAsync(Initial initialData)
        {
            FileStream fs = new FileStream("InitilData.json", FileMode.OpenOrCreate);          
            await JsonSerializer.SerializeAsync<Initial>(fs, initialData);
        }
        public void SaveJson(string initialDirectoryForWords)
        {
            FileStream fs = new FileStream("InitilData.json", FileMode.OpenOrCreate);
            Initial initialData = new Initial(initialDirectoryForWords);
        }
    }
}
