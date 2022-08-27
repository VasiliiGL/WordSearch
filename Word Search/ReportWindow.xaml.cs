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
        
            //double[] values = { 26, 20, 23, 7, 16 };
            double[] values = GetValues(data);
            //double[] positions = { 0, 1, 2, 3, 4 };
            double[] positions = GetPositions(data);
            //string[] labels = { "PHP", "JS", "C++", "GO", "VB" };
            string[] labels = GetWordsForLabels(data);
            ChartRatingWords.Plot.AddBar(values, positions);
            ChartRatingWords.Plot.XTicks(positions, labels);
            ChartRatingWords.Plot.SetAxisLimits(yMin: 0);

            ChartRatingWords.Plot.SaveFig("bar_labels.png");
            ChartRatingWords.Refresh();
        }

        private void ListFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var text = ((FileSearch)ListFiles.SelectedItem).Text;
            MessageBox.Show(text);
        }

        private string[] GetWordsForLabels(DataViewModels data)
        {
            var size = data.ListWords.Count;
            string[] labels = new string[size];
            for (var i=0; i<size; i++)
            {
                labels[i] = data.ListWords[i].WordSearch;
            }
            return labels;
        }
        private double[] GetValues(DataViewModels data)
        {
            var size = data.ListWords.Count;
            double[] values = new double[size];
            for (var i = 1; i <= size; i++)
            {
                values[i-1] = i*10;
            }
            return values;
        }
        private double[] GetPositions(DataViewModels data)
        {
            var size = data.ListWords.Count;
            double[] positions = new double[size];
            for (var i = 0; i < size; i++)
            {
                positions[i] = i;
            }
            return positions;
        }

    }
}
