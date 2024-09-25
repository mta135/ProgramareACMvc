using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace ProgramareAC.Services.MSign.Model
{
    public class XAdES
    {
        public X509Certificate Certificate { get; private set; }

        public bool StartsWith(byte[] thisArray, byte[] otherArray)
        {
            // Handle invalid/unexpected input
            // (nulls, thisArray.Length < otherArray.Length, etc.)

            if (thisArray == null) return false;
            if (otherArray == null) return true;
            if (otherArray.Length > thisArray.Length) return false;

            for (int i = 0; i < otherArray.Length; ++i) {
                if (thisArray[i] != otherArray[i]) {
                    return false;
                }
            }

            return true;
        }

        public XAdES(byte[] xadesBytes)
        {
            if (xadesBytes == null) return;
            try {
                byte[] utf8Preamble = Encoding.UTF8.GetPreamble();

                string xmlString;
                xmlString = StartsWith(xadesBytes, utf8Preamble) ?
                    Encoding.UTF8.GetString(xadesBytes, utf8Preamble.Length, xadesBytes.Length - utf8Preamble.Length) :
                    Encoding.UTF8.GetString(xadesBytes);

                //var xmlString = Encoding.UTF8.GetString(xadesBytes);

                ////Remove BOM
                //string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
                //if (xmlString.StartsWith(_byteOrderMarkUtf8))
                //{
                //    xmlString = xmlString.Remove(0, _byteOrderMarkUtf8.Length);
                //}

                var xml = XDocument.Parse(xmlString, LoadOptions.None);

                XNamespace nsds = @"http://www.w3.org/2000/09/xmldsig#";
                //XNamespace nsxades = @"http://uri.etsi.org/01903/v1.3.2#";

                var certBytes = Convert.FromBase64String(xml.Root.Descendants(nsds + "X509Certificate").First().Value);
                Certificate = new X509Certificate(certBytes);

                //Вне зависимости от пространства имен ( связано с подписями налоговой )
                SignTimeStamp = DateTime.Parse(
                    (from p in xml.Descendants()
                     where p.Name.LocalName == "SigningTime"
                       && p.Name.NamespaceName.Contains("http://uri.etsi.org/")
                       && p.Parent.Name.LocalName.Contains("SignedSignatureProperties")
                     select p).First().Value);

                //SignTimeStamp = DateTime.Parse(xml.Root.Descendants("SigningTime").First().Value);
                SubjectIDNP = GetSubjectPropertyValue(Certificate.Subject, "serialNumber");
                SubjectName = GetSubjectPropertyValue(Certificate.Subject, "CN");
                Position = GetSubjectPropertyValue(Certificate.Subject, "T");
            }
            catch (Exception exc) {
                SubjectName = "ERROR: " + exc.Message;
            }
        }

        [Obsolete("This method is deprecated, Use .ctor")]
        public static string GetXAdESInfo(byte[] xadesBytes)
        {
            var xml = XDocument.Parse(Encoding.UTF8.GetString(xadesBytes), LoadOptions.None);

            XNamespace nsds = @"http://www.w3.org/2000/09/xmldsig#";
            XNamespace nsxades = @"http://uri.etsi.org/01903/v1.3.2#";

            var certBytes = Convert.FromBase64String(xml.Root.Descendants(nsds + "X509Certificate").First().Value);
            var certificate = new X509Certificate(certBytes);

            return string.Format("<b>Время подписи</b>: {0} <br /> <b>Данные получателя</b>: {1}  <br /> <b>Серийный номер</b>: {2}",
                DateTime.Parse(xml.Root.Descendants(nsxades + "SigningTime").First().Value),
                certificate.Subject,
                certificate.GetSerialNumberString());
        }

        private static string GetSubjectPropertyValue(string subject, string property)
        {
            var index = subject.ToLower().IndexOf(property.ToLower() + "=");
            if (index == -1) return string.Empty;

            var str = subject.Substring(index + property.Length + 1);
            var endIndex = str.IndexOf(",");
            if (endIndex == -1) endIndex = str.Length;
            return str.Substring(0, endIndex);
        }

        /// <summary>
        /// IDNP держателя карты (подписи)
        /// </summary>

        public string SubjectIDNP { get; set; }

        /// <summary>
        /// ФИО держателя карты (подписи)
        /// </summary>

        public string SubjectName { get; set; }

        /// <summary>
        /// Должность
        /// </summary>

        public string Position { get; set; }

        /// <summary>
        /// Время подписи
        /// </summary>

        public DateTime SignTimeStamp { get; set; }
    }
}
