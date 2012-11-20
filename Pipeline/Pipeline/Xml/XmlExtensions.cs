using System.Xml.Linq;

namespace Pipeline.XmlTools
{
    public static class AtomName
    {
        public static XName Get(this string name)
        {
            return XName.Get(name, XmlNamespace.AtomNamespace);
        }
    }
}