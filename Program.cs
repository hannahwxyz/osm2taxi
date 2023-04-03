// osm2taxi
// Created by: Melody Wass <mel at strangemelody dot xyz>
// SPDX-License-Identifier: MIT

using System.Diagnostics;
using System.Xml;

// Create an empty array to store the nodes.
string[] nodes = new string[0];

// Bool containing whether the user wants to use the OSM file.
bool useOsmFile = false;

// Check if the first argument exists. If it does and it contains a .osm file, use that. 
// If the first argument contains a number, then the way IDs are being provided as multiple arguments.
if (args.Length > 0) {
    if (args[0].Contains(".osm")) {
        // Get the nodes from the OSM file.
        useOsmFile = true;
        nodes = Nodes.GetFromOsmFile(args[0]);
    } else {
        // Get the nodes from the way IDs and add them to the array.
        foreach (string wayId in args) {
            string[] wayNodes = Nodes.Get(wayId);
            foreach (string node in wayNodes) {
                Array.Resize(ref nodes, nodes.Length + 1);
                nodes[nodes.Length - 1] = node;
            }
        }
    }
}

// Get the number of nodes.
int nodeCount = nodes.Length;

// Set the current node and MSFS index.
int currentNode = 0;
int msfsIndex = 0;

// Ask the user to provide a starting index, otherwise use 0.
Console.Write("Starting index (default 1): ");
string startingIndex = Console.ReadLine();
if (startingIndex != "") {
    msfsIndex = int.Parse(startingIndex);
} else {
    msfsIndex = 1;
}

// Create an array to store the taxiway point strings.
string[] taxiwayPoints = new string[nodeCount];

// Check for duplicate nodes, remove them, and show the user the number of duplicates.
int duplicateCount = 0;
for (int i = 0; i < nodes.Length; i++) {
    for (int j = i + 1; j < nodes.Length; j++) {
        if (nodes[i] == nodes[j]) {
            nodes[j] = "";
            duplicateCount++;
        }
    }
}
Console.WriteLine($"Removed {duplicateCount} duplicate nodes.");

// Loop through all nodes, get coordinates, create a new taxiway point string, and add it to the array.
foreach (string node in nodes)
{
    try {
        // Variables for latitude and longitude
        double latitude = 0.0;
        double longitude = 0.0;

        // Check if using the OSM file.
        if (useOsmFile) {
            // Load the OSM file and parse it to an XmlDocument.
            XmlDocument osmFile = new XmlDocument();
            osmFile.Load(args[0]);

            // Get the latitude and longitude from the OSM file.
            (latitude, longitude) = Coordinates.GetFromOsmFile(osmFile, node);
        } else {
            // Get the latitude and longitude from the OSM API.
            (latitude, longitude) = Coordinates.Get(node);
        }

        // Check if latitude and longitude are 0.
        if (latitude == 0 && longitude == 0) {
            // Show the user the current progress.
            Console.WriteLine($"Skipped node {currentNode} of {nodeCount}.");

            // Skip the rest of the loop.
            continue;
        }

        // Create a new taxiway point string.
        string taxiwayPoint = $"<TaxiwayPoint index=\"{msfsIndex}\" type=\"NORMAL\" orientation=\"FORWARD\" lat=\"{latitude}\" lon=\"{longitude}\" />";

        // Add the taxiway point string to the array.
        taxiwayPoints[currentNode] = taxiwayPoint;

        // Increment the current node and MSFS index.
        currentNode++;
        msfsIndex++;

        // Show the user the current progress.
        Console.WriteLine($"Created taxiway point {currentNode} of {nodeCount}.");
    } catch (Exception) {
        // Show the user the current progress.
        Console.WriteLine($"Skipped node {currentNode} of {nodeCount}.");
    }
}

// Show the user the number of taxiway points.
Console.WriteLine($"Created {taxiwayPoints.Length} taxiway points.");

// Create a new file and write the taxiway point strings to it.
File.WriteAllLines("taxiway_points.txt", taxiwayPoints);

// Show the user the file has been created.
Console.WriteLine("Created taxiway_points.txt.");

// Ask if the user wants to open the file in notepad, otherwise exit.
Console.Write("Open in Notepad? (y/n): ");
string openInNotepad = Console.ReadLine();
if (openInNotepad == "y" || openInNotepad == "Y") {
    Process.Start("notepad.exe", "taxiway_points.txt");
}