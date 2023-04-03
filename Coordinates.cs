// osm2taxi
// Created by: Melody Wass <mel at strangemelody dot xyz>
// SPDX-License-Identifier: MIT

using System.Xml;

public static class Coordinates {
    public static (double, double) Get(string nodeId) {
        // Download the node XML from the OSM API.
        XmlDocument nodeXml = new XmlDocument();
        nodeXml.Load("https://www.openstreetmap.org/api/0.6/node/" + nodeId);

        // Get the node's latitude and longitude.
        XmlNode node = nodeXml.GetElementsByTagName("node")[0];
        double latitude = double.Parse(node.Attributes["lat"].Value);
        double longitude = double.Parse(node.Attributes["lon"].Value);

        // Return the coordinates.
        return (latitude, longitude);
    }

    public static (double, double) GetFromOsmFile(XmlDocument osmFile, string id) {
        // Get all <node> elements.
        XmlNodeList nodes = osmFile.GetElementsByTagName("node");

        // Loop through all <node> elements.
        foreach (XmlNode node in nodes) {
            // Check if the node ID matches the ID provided.
            if (node.Attributes["id"].Value == id) {
                // Get the node's latitude and longitude.
                double latitude = double.Parse(node.Attributes["lat"].Value);
                double longitude = double.Parse(node.Attributes["lon"].Value);

                // Return the coordinates.
                return (latitude, longitude);
            }
        }

        // Return 0, 0 if the node ID is not found.
        return (0, 0);
    }
}