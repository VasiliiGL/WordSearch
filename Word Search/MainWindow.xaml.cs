using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WordSearch.Models;

namespace Word_Search
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataViewModels data = new DataViewModels();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = data;
        }

        private void SelectWords_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.FileName = "Запрещенные слова 1";
                dialog.DefaultExt = ".txt";
                dialog.Filter = "Text documents (.txt)|*.txt";
                dialog.InitialDirectory = @"D:\VASILII\Запрещенные слова";
                if (dialog.ShowDialog() == true)
                {
                    string filename = dialog.FileName;
                    var word = new Word { WordSearch = data.TextCrud.DeleteSigns(System.IO.File.ReadAllText(filename)) };
                    data.SelectedWord = word;
                }
                data.SetWordsCRUD.AddFromString(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}
