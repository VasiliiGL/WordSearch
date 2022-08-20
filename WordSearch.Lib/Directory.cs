using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WordSearch.Models
{
    public class Directory : INotifyPropertyChanged
    {
        private string _directorySearch;
        public string DirectorySearch
        {
            get => _directorySearch;
            set
            {
                if (_directorySearch != value)
                {
                    _directorySearch = value;
                    OnPropertyChanged(nameof(DirectorySearch));
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
