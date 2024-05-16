using System.Globalization;
using CommandLine;
using CsvHelper;
using CsvHelper.Configuration;

namespace SynologyConnectionLogAnalyzer;

public class Options
{
    [Option('f', "file", Required = false, HelpText = "Path to the CSV file.")]
    public required string FilePath { get; set; }

    [Option("days-back", Default = 30, HelpText = "Number of days back to consider.")]
    public int DaysBack { get; set; }

    [Option("show-ip-counts", Default = false, HelpText = "Show IP counts.")]
    public bool ShowIpCounts { get; set; }
    
    [Option("at-least", Default = 50, HelpText = "Only show IP addresses where count is at least.")]
    public int AtLeast { get; set; }
}

static class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
        {
            // Get the CSV file path and number of days from the command-line options
            string filePath = options.FilePath;
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Invalid file path. Must point to an existing file.");
                return;
            }
            int daysBack = options.DaysBack;
            bool showIpCounts = options.ShowIpCounts;
            int atLeast = options.AtLeast;
            
            // Configure CsvHelper to handle the CSV file
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            // Read the CSV file using CsvHelper
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);

            // skip the very first line
            csv.Read();
    
            // Read the CSV records
            var records = csv.GetRecords<ConnectLogModel>()?.ToList() ?? new List<ConnectLogModel>();
    
            // Records to show
            var recordsToUse = records
                .Where(r => r.IpAddress != string.Empty)
                .Where(r => r.Level == "Warning")
                .Where(r => r.Time > DateTime.Now.AddDays(-daysBack));

            var groups = recordsToUse.GroupBy(r => r.IpAddress)
                .OrderBy(g => g.Count());

            foreach (var group in groups.Where(g => g.Count() > atLeast))
            {
                if (showIpCounts)
                {
                    Console.WriteLine($"{group.Key}: {group.Count()}");   
                }
                else
                {
                    Console.WriteLine($"{group.Key}");
                }
            }
        });
    }
}