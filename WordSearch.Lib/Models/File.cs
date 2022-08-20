using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WordSearch.Models
{
    public class File : INotifyPropertyChanged
    {
        private string _nameFile;
        private string _pathFile;
        private string _text;
        private long _sizeFile;
        

        public Dictionary<Word, int> ListDangerWords { get; set; }
        public string NameFile
        {
            get => _nameFile;
            set
            {
                if (_nameFile != value)
                {
                    _nameFile = value;
                    OnPropertyChanged(nameof(NameFile));
                }
            }
        }
        public string PathFile
        {
            get => _pathFile;
            set
            {
                if (_pathFile != value)
                {
                    _pathFile = value;
                    OnPropertyChanged(nameof(PathFile));
                }
            }
        }

        public long SizeFile
        {
            get => _sizeFile;
            set
            {
                if (_sizeFile != value)
                {
                    _sizeFile = value;
                    OnPropertyChanged(nameof(SizeFile));
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
