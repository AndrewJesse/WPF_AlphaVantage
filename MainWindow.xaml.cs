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

namespace WPF_AlphaVantage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AlphaVantage alphaVantage;

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
            else
            {
            }
        }

        private async void LoadData_Click(object sender, RoutedEventArgs e)
        {
            var data = await alphaVantage.GetDailyPricesAsync("SPY");

            // TODO: Add logic to process and display this data in a chart
        }
    }
}