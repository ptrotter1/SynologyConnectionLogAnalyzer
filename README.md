# SynologyConnectionLogAnalyzer
Takes in the .csv format connection log from Synology DSM and analyzes it for abusive IP addresses.

## File Format
The .csv file must be a connection log from Synology DSM in the following format:

```text
Connection
Level,Log,Time,User,Event
Warning,Connection,2024/05/16 13:37:52,zdq,User [zdq] from [65.78.53.203] failed to sign in to [DSM] via [password] due to authorization failure.
Warning,Connection,2024/05/16 13:26:13,zd017,User [zd017] from [76.172.37.132] failed to sign in to [DSM] via [password] due to authorization failure.
Warning,Connection,2024/05/16 13:07:19,zco,User [zco] from [68.20.13.33] failed to sign in to [DSM] via [password] due to authorization failure.
Warning,Connection,2024/05/16 13:00:52,Z1804045,User [Z1804045] from [73.51.241.62] failed to sign in to [DSM] via [password] due to authorization failure.
```

## Usage
```text
  -f, --file          Path to the CSV file.

  --days-back         (Default: 30) Number of days back to consider.

  --show-ip-counts    (Default: false) Show IP counts.

  --at-least          (Default: 50) Only show IP addresses where count is at
                      least.

  --help              Display this help screen.

  --version           Display version information.
```

### Example
This will write to a file all IPs that have failed to log in at least 100 times over the last 30 days.
```bash
.\SynologyConnectionLogAnalyzer.exe -f .\connectlog_2024-5-16-14_13_9.csv --days-back 30 --at-least 100 > abusive-ips.txt
```

## Publish
Here's how to publish the (somewhat large) single executable to a file. Trimming causes issues at the moment...
```bash
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true
```
