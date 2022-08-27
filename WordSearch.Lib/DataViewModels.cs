using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WordSearch.Models.CRUDs;
using WordSearch.Models.Lib;

namespace WordSearch.Models
{
    public class DataViewModels : INotifyPropertyChanged
    {
        public Directory Directory { get; set; }
        public ObservableCollection<FileSearch> ListFiles { get; set; }
        public ObservableCollection<FileSearch> ListDangerFiles { get; set; }
        public ObservableCollection<Word> ListWords { get; set; }
        public TextCRUD TextCrud { get; set; }
        public ListWordsCRUD ListWordsCrud { get; set; }
        public FileCRUD FileCrud { get; set; }
        public Logger Logger { get; set; }
        public JsonCRUD JsonCRUD { get; set; }
        public Initial InitialData { get; set; }


        public DataViewModels()
        {
            Directory = new Directory { DirectorySearch = "Директория не выбрана" };
            ListFiles = new ObservableCollection<FileSearch>();
            ListDangerFiles = new ObservableCollection<FileSearch>();
            ListWords = new ObservableCollection<Word>();
            TextCrud = new TextCRUD();
            ListWordsCrud = new ListWordsCRUD();
            FileCrud = new FileCRUD();
            Logger = new Logger();
            JsonCRUD = new JsonCRUD();
            InitialData = new Initial();
            FileCrud.CreatDirectory(InitialData.DirectoryForCopyFile);
            System.Threading.Tasks.Task task = JsonCRUD.SaveJsonAsync(InitialData);
        }

        private Word _selectedWord;
        public Word SelectedWord
        {
            get { return _selectedWord; }
            set
            {
                _selectedWord = value;
                OnPropertyChanged("SelectedWord");
            }
        }
        private FileSearch _selectedFile;

        public FileSearch SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                OnPropertyChanged("SelectedFile");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
