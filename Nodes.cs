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
}