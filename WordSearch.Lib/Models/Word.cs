using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WordSearch.Models
{
    public class Word : INotifyPropertyChanged
    {
        private string _wordSearch;
        public string WordSearch
        {
            get => _wordSearch;
            set
            {
                if (_wordSearch != value)
                {
                    _wordSearch = value;
                    OnPropertyChanged(nameof(WordSearch));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
}
