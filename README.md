# SynologyConnectionLogAnalyzer
Takes in the .csv format connection log from Synology DSM and analyzes it for abusive IP addresses.

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

## Publish
```bash
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true
```
