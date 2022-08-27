using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WordSearch.Models;

namespace Word_Search
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow(DataViewModels data)
        {
            InitializeComponent();
            DataContext = data;
        }

        private void ListFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var text = ((FileSearch)ListFiles.SelectedItem).Text;
            MessageBox.Show(text);
        }
    }
}
