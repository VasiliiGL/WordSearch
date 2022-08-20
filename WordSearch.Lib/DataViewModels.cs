using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WordSearch.Models.CRUDs;

namespace WordSearch.Models
{
    public class DataViewModels : INotifyPropertyChanged
    {
        public Directory Directory { get; set; }
        public ObservableCollection<File> ListFiles { get; set; }
        public ObservableCollection<File> ListDangerFiles { get; set; }
        public ObservableCollection<Word> ListWords { get; set; }
        public TextCRUD TextCrud { get; set; }
        public ListWordsCRUD ListWordsCrud { get; set; }
        public FileCRUD FileCrud { get; set; }

        public DataViewModels()
        {
            Directory = new Directory { DirectorySearch = "Директория не выбрана" };
            ListFiles = new ObservableCollection<File>();
            ListDangerFiles = new ObservableCollection<File>();
            ListWords = new ObservableCollection<Word>();
            TextCrud = new TextCRUD();
            ListWordsCrud = new ListWordsCRUD();
            FileCrud = new FileCRUD();
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
        private File _selectedFile;

        public File SelectedFile
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
