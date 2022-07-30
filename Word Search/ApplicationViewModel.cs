using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Word_Search
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        private ModelData selectedData;

        public ObservableCollection<ModelData> Datas { get; set; }
        public ModelData SelectedData
        {
            get { return selectedData; }
            set
            {
                selectedData = value;
                OnPropertyChanged("SelectedData");
            }
        }

        public ApplicationViewModel()
        {
            Datas = new ObservableCollection<ModelData>
            {
                new ModelData { SearchWords="iPhone", PathFile=@"D:\VASILII\Учеба си шарп\ОнлайнКурс\Системное программирование\WordSearch",
                    NewPathFile=@"D:\VASILII\Учеба си шарп\ОнлайнКурс\Системное программирование\WordSearch" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

