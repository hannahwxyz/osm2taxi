# osm2taxi

Tool to convert OpenStreetMap data to taxiway points used in Microsoft Flight Simulator scenery XML files.

## Description
`osm2taxi` can take multiple OpenStreetMap way ID's and convert the linked nodes to a `<TaxiwayPoint>` object usable in scenery XML files for Microsoft Flight Simulator.

## Usage

    osm2taxi.exe [wayId] [wayId] [wayId] ...

## Example

    osm2taxi.exe 28845325 28845326

## Output
`osm2taxi` outputs the `<TaxiwayPoint>` objects to a text file named `taxiway_points.txt` in the same directory as the executable.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Acknowledgments
* [OpenStreetMap](https://www.openstreetmap.org/) - The map data used to create this tool

## Disclaimer
This tool is not affiliated with Microsoft Flight Simulator or Microsoft Corporation in any way. This tool is provided as-is with no warranty or guarantee of any kind. Use at your own risk.