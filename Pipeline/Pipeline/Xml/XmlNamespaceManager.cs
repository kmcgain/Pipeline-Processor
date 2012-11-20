using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace Pipeline.XmlTools
{
    class XmlNamespace
    {
        public const string AtomNamespace = "http://www.w3.org/2005/Atom";

        public static XmlNamespaceManager GetNamespaceManager()
        {
            var nsMgr = new XmlNamespaceManager(new NameTable());
            nsMgr.AddNamespace("atom", AtomNamespace);
            return nsMgr;
        }
    }
}
