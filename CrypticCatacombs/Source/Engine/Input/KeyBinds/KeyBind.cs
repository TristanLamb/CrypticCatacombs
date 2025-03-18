using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CrypticCatacombs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.AxHost;
using System.Text.Json;

namespace CrypticCatacombs
{
    public class KeyBind
    {
        public string name, key;
        public KeyBind(string NAME, string KEY)
        {
            name = NAME;
            key = KEY;
        }

        public string Key
        {
			set
			{
				key = value;
			}
		}

        public virtual XElement ReturnXML()
        {
			XElement xml = new XElement("Key",
			               new XAttribute("n", name),
			               new XElement("value", key));
			return xml;
		}
    }
}
