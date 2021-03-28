using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace AlhambraScoringAndroid.Tools
{
    public static class XmlOperations
    {
        public static IEnumerable<XmlNode> GetChildNodes(this XmlNode node, string tagName)
        {
            return node.ChildNodes.Cast<XmlNode>().Where(n => n.Name == tagName);
        }

        public static XmlNode SingleChildNode(this XmlNode node, string tagName)
        {
            return node.ChildNodes.Cast<XmlNode>().Single(n => n.Name == tagName);
        }
        public static XmlNode SingleOrDefaultChildNode(this XmlNode node, string tagName)
        {
            return node.ChildNodes.Cast<XmlNode>().SingleOrDefault(n => n.Name == tagName);
        }

        public static void AddTextChild(XmlDocument document, XmlElement element, string name, string text)
        {
            XmlElement playerScoreDetails1Element = document.CreateElement(String.Empty, name, String.Empty);
            playerScoreDetails1Element.AppendChild(document.CreateTextNode(text));
            element.AppendChild(playerScoreDetails1Element);
        }
    }
}