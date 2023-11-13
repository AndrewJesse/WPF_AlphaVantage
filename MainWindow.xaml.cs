using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace WPF_AlphaVantage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AlphaVantage? alphaVantage;

        public MainWindow()
        {
            InitializeComponent();

            // Read and deserialize the API key from the JSON file
            string keyPath = "C:\\Projects\\WPF_AlphaVantage\\api_key.json";
            string json = File.ReadAllText(keyPath);
            var jsonObject = JsonSerializer.Deserialize<GetAPI>(json);

            // Initialize your AlphaVantage with the API key from the file
            if (jsonObject?.api_key != null)
            {
                alphaVantage = new AlphaVantage(jsonObject.api_key);
            }
            ConfigureChart();
        }

        private void ConfigureChart()
        {
            // Set up the series collection and assign it to the chart
            CandlestickChart.Series = new SeriesCollection
        {
            // Add a placeholder series - we'll populate it with data later
            new CandleSeries
            {
                Title = "Stock Data",
                Values = new ChartValues<OhlcPoint>()
            }
        };

            // You can add more configuration here, like setting up axes, labels, etc.
        }

        private async void LoadData_Click(object sender, RoutedEventArgs e)
        {
            var symbol = "SPY"; // Replace with the desired stock symbol
            if (alphaVantage != null)
            {
                var data = await alphaVantage.GetDailyPricesAsync(symbol);
                UpdateChart(data);
            }
        }

        private void UpdateChart(List<(string date, decimal open, decimal high, decimal low, decimal close)> stockData)
        {
            // Clear existing data
            if (CandlestickChart.Series.Count > 0)
            {
                CandlestickChart.Series[0].Values.Clear();
            }
            else
            {
                return;
            }

            // Reverse the order of the data so that the newest date is on the right
            stockData.Reverse();

            // Convert the data to OhlcPoints and add them to the chart
            foreach (var (date, open, high, low, close) in stockData)
            {
                CandlestickChart.Series[0].Values.Add(new OhlcPoint((double)open, (double)high, (double)low, (double)close));
            }
        }
    }
}