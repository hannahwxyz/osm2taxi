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
}