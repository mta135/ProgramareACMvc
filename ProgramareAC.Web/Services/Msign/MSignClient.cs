using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using ProgramareAC.Web.Services.Msign.Client;

namespace ProgramareAC.Services.MSign
{
    public class MSignClientFactory
    {
        public static IMSign Create(string serverUrl, string clientCertificateSerial)
        {
            EndpointAddress address = new EndpointAddress(serverUrl);
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Security = {
                    Mode = BasicHttpSecurityMode.Transport,
                    Transport = {
                        ClientCredentialType = HttpClientCredentialType.Certificate
                    }
                }
            };
            MSignClient client = new MSignClient(binding, address)
            {
                Endpoint = {
                    Contract = ContractDescription.GetContract(typeof(IMSign), typeof(MSignClient))
                }
            };
            if (client.ClientCredentials != null)
            {
                client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySerialNumber, clientCertificateSerial);
            }

            return client;
        }

        public static IMSign Create()
        {
            var url = ConfigurationManager.AppSettings["MSign_ServiceUrl"];
            var serial = ConfigurationManager.AppSettings["MSign_ClientCertificateSerial"];

            return Create(url, serial);
        }
    }
}