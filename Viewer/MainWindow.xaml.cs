using Microsoft.Win32;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller.Utils;

namespace Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRoute_Click(object sender, RoutedEventArgs e)
        {
            // TODO 実装
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
                    // TODO DataGridにcsvの内容を反映する
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
}
