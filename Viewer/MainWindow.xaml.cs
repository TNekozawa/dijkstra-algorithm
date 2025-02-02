using Controller;
using Microsoft.Win32;
using Models.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<RouteEntry> Routes { get; private set; }
        public DijkstraInterface DijkstraInterface { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            Routes = [];
            DijkstraInterface = new();
            routeDataGrid.ItemsSource = Routes;
            DataContext = this;
        }

        private void AddRoute_Click(object sender, RoutedEventArgs e)
        {
            var newRoute = new RouteEntry
            {
                FromLocation = fromLocationTextBox.Text,
                ToLocation = toLocationTextBox.Text,
                TransType = transportationTypeTextBox.Text,
                TravelTime = int.TryParse(timeTextBox.Text, out int time) ? time : 0,
                Cost = int.TryParse(costTextBox.Text, out int cost) ? cost : 0
            };

            Routes.Add(newRoute);

            // Clear input fields
            fromLocationTextBox.Clear();
            toLocationTextBox.Clear();
            transportationTypeTextBox.Clear();
            timeTextBox.Clear();
            costTextBox.Clear();
        }

        private void SearchRoute_Click(object sender, RoutedEventArgs e)
        {
            string fromName;
            var fSelectedItem = fromSelectionListBox.SelectedItem;
            if (fSelectedItem == null)
            {
                // スタート地点が指定されていない
                fromSelectionListBox.SelectedIndex = 0;
                fromName = fromSelectionListBox.Items[0].ToString();
            }
            else
            {
                fromName = fSelectedItem.ToString();
            }

            string toName;
            var tSelectedItem = toSelectionListBox.SelectedItem;
            if (tSelectedItem == null)
            {
                // スタート地点が指定されていない
                toSelectionListBox.SelectedIndex = 0;
                toName = toSelectionListBox.Items[0].ToString();
            }
            else
            {
                toName = tSelectedItem.ToString();
            }

            List<string[]> routes = [];
            foreach (var route in Routes)
            {
                string[] array = new string[route.Length];
                array[0] = route.FromLocation.ToString();
                array[1] = route.ToLocation.ToString();
                array[2] = route.TransType.ToString();
                array[3] = route.TravelTime.ToString();
                array[4] = route.Cost.ToString();

                routes.Add(array);
            }

            string costTarget = searchModeSlider.Value.ToString();
            CostType costType = CostTypeHandler.GetCostType(costTarget);

            DijkstraInterface.SetCsv(routes, costType);

            DijkstraInterface.GetPath(fromName, toName);
        }

        private void ImportCsv_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
               Filter = "CSVファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*",
               Title = "経路情報CSVファイルを選択"
            };

            if (openFileDialog.ShowDialog() == true)
            {
               try
               {
                   string filePath = openFileDialog.FileName;
                   LoadCSV(filePath);
               }
               catch (Exception ex)
               {
                   string message = $"CSVインポート中にエラーが発生しました：{ex.Message}";
                   MessageBox.Show(
                       message,
                       "エラー",
                       MessageBoxButton.OK,
                       MessageBoxImage.Error
                   );
               }
            }
        }

        private void LoadCSV(string filePath)
        {
            var csv = CsvReader.GetCSV(filePath, false);
            HashSet<string> locationNames = [];
            foreach (string[] csvRow in csv)
            {
                string fromName = csvRow[0];
                string toName = csvRow[1];
                _ = locationNames.Add(fromName);
                _ = locationNames.Add(toName);
                string transType = csvRow[2];
                string routeTime = csvRow[3];
                string routeCost = csvRow[4];

                var newRoute = new RouteEntry
                {
                    FromLocation = fromName,
                    ToLocation = toName,
                    TransType = transType,
                    TravelTime = int.TryParse(routeTime, out int time) ? time : 0,
                    Cost = int.TryParse(routeCost, out int cost) ? cost : 0
                };

                Routes.Add(newRoute);
            }

            fromSelectionListBox.ItemsSource = locationNames;
            toSelectionListBox.ItemsSource = locationNames;
        }
    }

    public class RouteEntry : INotifyPropertyChanged
    {
        public readonly int Length = 5;
        private string _fromLocation;
        public string FromLocation
        {
            get => _fromLocation;
            set
            {
                _fromLocation = value;
                OnPropertyChanged(nameof(FromLocation));
            }
        }

        private string _toLocation;
        public string ToLocation
        {
            get => _toLocation;
            set
            {
                _toLocation = value;
                OnPropertyChanged(nameof(ToLocation));
            }
        }

        private string _transType;
        public string TransType
        {
            get => _transType;
            set
            {
                _transType = value;
                OnPropertyChanged(nameof(TransType));
            }
        }

        private int _travelTime;
        public int TravelTime
        {
            get => _travelTime;
            set
            {
                _travelTime = value;
                OnPropertyChanged(nameof(TravelTime));
            }
        }

        private int _cost;
        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
