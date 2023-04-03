﻿// osm2taxi
// Created by: Melody Wass <mel at strangemelody dot xyz>
// SPDX-License-Identifier: MIT

// Check the arguments to make sure there's a least one way ID.
if (args.Length < 1) {
    Console.WriteLine("Please provide at least one way ID.");
    return;
}

// Create an empty array to store the way IDs.
string[] wayIds = new string[0];

// Loop through the arguments and add them to the array if they do not contain letters.
foreach (string arg in args) {
    if (!arg.Any(char.IsLetter)) {
        Array.Resize(ref wayIds, wayIds.Length + 1);
        wayIds[wayIds.Length - 1] = arg;
    }
}

// Get all the nodes from the way ID's and count them.
string[] nodes = new string[0];
foreach (string wayId in wayIds) {
    string[] wayNodes = Nodes.Get(wayId);
    foreach (string node in wayNodes) {
        Array.Resize(ref nodes, nodes.Length + 1);
        nodes[nodes.Length - 1] = node;
    }
}
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

// Loop through all nodes, get coordinates, create a new taxiway point string, and add it to the array.
foreach (string node in nodes)
{
    // Get the coordinates for the current node.
    (double latitude, double longitude) = Coordinates.Get(node);

    // Create a new taxiway point string.
    string taxiwayPoint = $"<TaxiwayPoint index=\"{msfsIndex}\" type=\"NORMAL\" orientation=\"FORWARD\" lat=\"{latitude}\" lon=\"{longitude}\" />";

    // Add the taxiway point string to the array.
    taxiwayPoints[currentNode] = taxiwayPoint;

    // Increment the current node and MSFS index.
    currentNode++;
    msfsIndex++;

    // Show the user the current progress.
    Console.WriteLine($"Created taxiway point {currentNode} of {nodeCount}.");
}

// Create a new file and write the taxiway point strings to it.
File.WriteAllLines("taxiway_points.txt", taxiwayPoints);