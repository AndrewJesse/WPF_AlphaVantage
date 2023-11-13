# S&P 500 Stock Price Viewer

This WPF application utilizes the Alpha Vantage API to display the S&P 500 (SPY) stock market price action on a candlestick chart. It's designed to provide a clear and interactive way to visualize stock price movements over time.

## Features

- **Real-Time Data**: Fetches the latest stock prices for the S&P 500 index.
- **Candlestick Chart**: Visualizes stock prices in a detailed and easy-to-understand format.
- **Responsive Design**: Optimized for various screen sizes and devices.

## Getting Started

### Prerequisites

- .NET 7.0 or higher
- Visual Studio 2022
- Windows 10/11

### Installation

1. Clone the repository:
   git clone https://github.com/AndrewJesse/WPF_AlphaVantage

2. Grab a free API key at Alpha Vantage. 
  https://www.alphavantage.co/support/#api-key 

3. Create a api_key.json file in the root directory with the following content:
  { "api_key": "YOUR_API_KEY" }
  Replace YOUR_API_KEY with your actual Alpha Vantage API key.

### Usage
Run the application from Visual Studio. Click the "Load Data" button to fetch and display the latest S&P 500 stock prices on the candlestick chart.

### Contributing
Contributions are welcome! Please feel free to submit a pull request or open an issue.

### Acknowledgments
- Alpha Vantage API for providing stock price data.
- LiveCharts for the WPF charting library.

### License
This project is licensed under the MIT License - see the LICENSE file for details.
