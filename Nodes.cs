// osm2taxi
// Created by: Melody Wass <mel at strangemelody dot xyz>
// SPDX-License-Identifier: MIT

using System.Xml;

public static class Nodes {
    public static string[] Get(string wayId) {
        // Create an empty array to store the nodes.
        string[] nodes = new string[0];

        // Download the way XML from the OSM API.
        XmlDocument wayXml = new XmlDocument();
        wayXml.Load("https://www.openstreetmap.org/api/0.6/way/" + wayId);

        // Get the nodes from the way XML.
        XmlNodeList wayNodes = wayXml.GetElementsByTagName("nd");

        // Loop through the nodes and add them to the array.
        foreach (XmlNode node in wayNodes) {
            // Get the node ID.
            string nodeId = node.Attributes["ref"].Value;

            // Add the node ID to the array.
            Array.Resize(ref nodes, nodes.Length + 1);
            nodes[nodes.Length - 1] = nodeId;
        }

        // Return the array of nodes.
        return nodes;
    }

    public static string[] GetFromOsmFile(string file) {
        // Create an empty array to store the nodes.
        string[] nodes = new string[0];
        
        // Create an empty array to store the ways that match.
        XmlNode[] matchingWays = Array.Empty<XmlNode>();

        // Print the file path.
        Console.WriteLine("Using OSM file: " + file);

        // Load the OSM file.
        XmlDocument osmXml = new XmlDocument();
        osmXml.Load(file);

        // Get all <way> elements.
        XmlNodeList ways = osmXml.GetElementsByTagName("way");

        // Loop through all <way> elements.
        foreach (XmlNode way in ways) {
            // Get all child elements in the <way> element.
            XmlNodeList wayChildren = way.ChildNodes;

            // Loop through all child elements.
            foreach (XmlNode wayChild in wayChildren) {
                // Check if the child element is a <tag> element.
                if (wayChild.Name == "tag") {
                    // Get the key and value of the <tag> element.
                    string key = wayChild.Attributes["k"].Value;
                    string value = wayChild.Attributes["v"].Value;

                    // Check if the key is "aeroway" and the value is "taxiway".
                    if (key == "aeroway" && value == "taxiway") {
                        // Add the <way> element to the array.
                        Array.Resize(ref matchingWays, matchingWays.Length + 1);
                        matchingWays[matchingWays.Length - 1] = way;
                    }
                }
            }
        }

        // Print the number of matching ways.
        Console.WriteLine("Found " + matchingWays.Length + " matching ways.");

        // Loop through all matching ways and add the nodes to the array.
        foreach (XmlNode way in matchingWays) {
            // Get all child elements in the <way> element.
            XmlNodeList wayChildren = way.ChildNodes;

            // Loop through all child elements.
            foreach (XmlNode wayChild in wayChildren) {
                // Check if the child element is a <nd> element.
                if (wayChild.Name == "nd") {
                    // Get the node ID.
                    string nodeId = wayChild.Attributes["ref"].Value;

                    // Add the node ID to the array.
                    Array.Resize(ref nodes, nodes.Length + 1);
                    nodes[nodes.Length - 1] = nodeId;
                }
            }
        }
    
        // Return the array of nodes.
        return nodes;
    }
}