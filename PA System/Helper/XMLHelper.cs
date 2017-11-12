using PA_System.Helper;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PA_System.Infrastructure
{
    public class XMLHelper
    {
        //This method converts a list of any type into xml
        public static string Serialize<T>(T value)
        {
            if (value == null) return null;

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Encoding = new UnicodeEncoding(false, false);

            settings.Indent = false;

            settings.OmitXmlDeclaration = true;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, value);
                }

                return textWriter.ToString().RemoveXmlNameSpace();
            }
        }
    }
}