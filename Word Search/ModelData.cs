using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Word_Search
{
    class ModelData : INotifyPropertyChanged
    {
        private string searchWords;
        private string pathFile;
        private string newPathFile;

        public string SearchWords
        {
            get { return searchWords; }
            set
            {
                searchWords = value;
                OnPropertyChanged("SearchWords");
            }
        }
        public string PathFile
        {
            get { return pathFile; }
            set
            {
                pathFile = value;
                OnPropertyChanged("PathFile");
            }
        }
        public string NewPathFile
        {
            get { return newPathFile; }
            set
            {
                newPathFile = value;
                OnPropertyChanged("NewPathFile");
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

