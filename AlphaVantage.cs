using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

public class AlphaVantage
{
    private readonly string _apiKey;

    public AlphaVantage(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<List<(string date, decimal open, decimal high, decimal low, decimal close)>> GetDailyPricesAsync(string symbol)
    {
        string queryUrl = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}";

        using (HttpClient client = new HttpClient())
        {
            string? jsonString = await client.GetStringAsync(queryUrl);

            var stockData = JsonSerializer.Deserialize<StockData>(jsonString);

            List<(string date, decimal open, decimal high, decimal low, decimal close)> dailyPrices = new List<(string date, decimal open, decimal high, decimal low, decimal close)>();

            if (stockData != null && stockData.TimeSeriesDaily != null)
            {
                foreach (var entry in stockData.TimeSeriesDaily)
                {
                    if (entry.Value != null)
                    {
                        decimal open = entry.Value.Open != null ? decimal.Parse(entry.Value.Open) : 0;
                        decimal high = entry.Value.High != null ? decimal.Parse(entry.Value.High) : 0;
                        decimal low = entry.Value.Low != null ? decimal.Parse(entry.Value.Low) : 0;
                        decimal close = entry.Value.Close != null ? decimal.Parse(entry.Value.Close) : 0;

                        dailyPrices.Add((entry.Key, open, high, low, close));
                    }
                }
            }

            return dailyPrices;
        }
    }
}

public class GetAPI
{
    public string? api_key { get; set; }
}

public class StockData
{
    [JsonPropertyName("Meta Data")]
    public MetaData? MetaData { get; set; }

    [JsonPropertyName("Time Series (Daily)")]
    public Dictionary<string, DailyQuote>? TimeSeriesDaily { get; set; }
}

public class MetaData
{
    [JsonPropertyName("1. Information")]
    public string? Information { get; set; }

    [JsonPropertyName("2. Symbol")]
    public string? Symbol { get; set; }

    [JsonPropertyName("3. Last Refreshed")]
    public string? LastRefreshed { get; set; }

    [JsonPropertyName("4. Output Size")]
    public string? OutputSize { get; set; }

    [JsonPropertyName("5. Time Zone")]
    public string? TimeZone { get; set; }
}

public class DailyQuote
{
    [JsonPropertyName("1. open")]
    public string? Open { get; set; }

    [JsonPropertyName("2. high")]
    public string? High { get; set; }

    [JsonPropertyName("3. low")]
    public string? Low { get; set; }

    [JsonPropertyName("4. close")]
    public string? Close { get; set; }

    [JsonPropertyName("5. volume")]
    public string? Volume { get; set; }
}