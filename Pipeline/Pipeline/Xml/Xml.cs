using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace Pipeline.XmlTools
{
    [Serializable]
    public class Xml
    {
        private readonly XDocument document;


        public Xml(string content)
        {
            document = XDocument.Parse(content);
            document.Declaration = document.Declaration ?? new XDeclaration("1.0", Encoding.UTF8.EncodingName, null);
        }

        public Xml(XDocument xDocument)
        {
            document = xDocument;
        }

        public XElement Root
        {
            get { return document.Root; }
        }


        public override string ToString()
        {
            if (document == null)
            {
                return string.Empty;
            }

            using (StringWriter writer = new Utf8StringWriter())
            {
                document.Save(writer, SaveOptions.None);
                return writer.ToString();
            }
        }

        public static implicit operator string(Xml xml)
        {
            return xml.ToString();
        }


        public void Validate(XmlSchemaSet schemaSet, ValidationEventHandler validationEventHandler)
        {
            document.Validate(schemaSet, validationEventHandler);
        }

        public XElement XPathSelect(string expression)
        {
            return document.XPathSelectElement(expression, XmlNamespace.GetNamespaceManager());
        }

        public IEnumerable<XElement> XPathSelectElements(string expression)
        {
            return document.XPathSelectElements(expression, XmlNamespace.GetNamespaceManager());
        }

        public T XPathEvaluate<T>(string expression)
        {
            return (T)document.XPathEvaluate(expression, XmlNamespace.GetNamespaceManager());
        }

        public override bool Equals(object obj)
        {
            var objAsXml = obj as Xml;

            return objAsXml != null && ToString().Equals(objAsXml);
        }

        public override int GetHashCode()
        {
            return document.GetHashCode();
        }

        public static bool operator ==(Xml left, Xml right)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(left, right))
                return true;

            // If one is null, but not both, return false.
            if (((object) left == null) || ((object) right == null))
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Xml left, Xml right)
        {
            return !(left == right);
        }
    }

    public static class XElementExtensions
    {
        public static XElement XPathSelect(this XElement element, string expression)
        {
            return element.XPathSelectElement(expression, XmlNamespace.GetNamespaceManager());
        }

        public static T XPathEvaluate<T>(this XElement element, string expression)
        {
            return (T)element.XPathEvaluate(expression, XmlNamespace.GetNamespaceManager());
        }
        
    }
}