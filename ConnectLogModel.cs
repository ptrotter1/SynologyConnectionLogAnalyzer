using System.Text.RegularExpressions;

namespace SynologyConnectionLogAnalyzer;

public class ConnectLogModel
{
    public string Level { get; set; } = string.Empty;
    public string Log { get; set; } = string.Empty;
    public DateTime Time { get; set; }
    public string User { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
    public string IpAddress => Regex.Match(Event, @"\[(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\]").Groups[1].Value;
}