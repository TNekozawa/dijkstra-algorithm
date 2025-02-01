using Controller.Utils;
using Microsoft.Win32;
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
        public MainWindow()
        {
            InitializeComponent();
            Routes = [];
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
                TravelTime = decimal.TryParse(timeTextBox.Text, out decimal time) ? time : 0,
                Cost = decimal.TryParse(costTextBox.Text, out decimal cost) ? cost : 0
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
            // TODO 実装
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
                    var csv = CsvReader.GetCSV(openFileDialog.FileName, false);
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
                            TravelTime = decimal.TryParse(routeTime, out decimal time) ? time : 0,
                            Cost = decimal.TryParse(routeCost, out decimal cost) ? cost : 0
                        };

                        Routes.Add(newRoute);
                    }

                    fromSelectionListBox.ItemsSource = locationNames;
                    toSelectionListBox.ItemsSource = locationNames;
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
    }

    public class RouteEntry : INotifyPropertyChanged
    {
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

        private decimal _travelTime;
        public decimal TravelTime
        {
            get => _travelTime;
            set
            {
                _travelTime = value;
                OnPropertyChanged(nameof(TravelTime));
            }
        }

        private decimal _cost;
        public decimal Cost
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
