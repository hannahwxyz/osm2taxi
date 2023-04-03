# osm2taxi

[![.NET](https://github.com/strangemelody/osm2taxi/actions/workflows/dotnet.yml/badge.svg)](https://github.com/strangemelody/osm2taxi/actions/workflows/dotnet.yml)

Tool to convert OpenStreetMap data to taxiway points used in Microsoft Flight Simulator scenery XML files.

## Description
`osm2taxi` is a tool to convert OpenStreetMap data to taxiway points used in MSFS scenery XML files. The program can read data from a .osm file or from a list of OSM way IDs. The program will output the `<TaxiwayPoint>` objects to a text file named `taxiway_points.txt` in the same directory as the executable.

## Usage

    osm2taxi.exe [wayId] [wayId] [wayId] ...
    osm2taxi.exe [osm file]

## Example

    osm2taxi.exe 28845325 28845326
    osm2taxi.exe airport.osm

## Output
`osm2taxi` outputs the `<TaxiwayPoint>` objects to a text file named `taxiway_points.txt` in the same directory as the executable.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Acknowledgments
* [OpenStreetMap](https://www.openstreetmap.org/) - The map data used to create this tool

## Disclaimer
This tool is not affiliated with Microsoft Flight Simulator or Microsoft Corporation in any way. This tool is provided as-is with no warranty or guarantee of any kind. Use at your own risk.