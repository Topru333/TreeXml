using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TreeParts
{
    [Serializable]
    public class Node : IXmlSerializable
    {
        private int Number;
        private Node Left = null;
        private Node Right = null;
        public Node()
        {
            Number = 0;
        }
        public Node(int number)
        {
            Number = number;
        }
        public void ReplaceLeft(Node l)
        {
            Left = l;
        }
        public void ReplaceRight(Node r)
        {
            Right = r;
        }
        public int GetValue()
        { return Number; }
        public Node GetRight()
        { return Right; }
        public Node GetLeft()
        { return Left; }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Number");
            writer.WriteValue(Number);
            writer.WriteEndElement();
            if (Left != null)
            {
                writer.WriteStartElement("Left");
                Left.WriteXml(writer);
                writer.WriteEndElement();
            }
            if (Right != null)
            {
                writer.WriteStartElement("Right");
                Right.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("Node");
            ReadSubNode(reader, this);
            reader.ReadEndElement();
        }
        public void ReadSubNode(XmlReader reader,Node n)
        {
            reader.ReadStartElement("Number");
            n.Number = reader.ReadContentAsInt();
            reader.ReadEndElement();
            if (reader.IsStartElement())
            {
                reader.ReadStartElement("Left");
                n.Left = new Node();
                ReadSubNode(reader, n.Left);
                reader.ReadEndElement();
                ///
                reader.ReadStartElement("Right");
                n.Right = new Node();
                ReadSubNode(reader, n.Right);
                reader.ReadEndElement();
            }
        }
        public XmlSchema GetSchema()
        {return null;}
    }
}
