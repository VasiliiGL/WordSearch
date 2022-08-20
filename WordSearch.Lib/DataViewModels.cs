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
        public ObservableCollection<File> Files { get; set; }
        public ObservableCollection<Word> SetWords { get; set; }
        public TextCRUD TextCrud { get; set; }
        public SetWordsCRUD SetWordsCRUD { get; set; }

        public DataViewModels()
        {
            Directory = new Directory { DirectorySearch = "Директория не выбрана" };
            Files = new ObservableCollection<File>();            
            SetWords = new ObservableCollection<Word>();
            TextCrud = new TextCRUD();
            SetWordsCRUD = new SetWordsCRUD();
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
