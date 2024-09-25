using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace ProgramareAC.Services.MPASS
{
    public class CertificatesProvider
    {
        /// <summary>
        /// Load certificate only with public key from storage, specified by SiteName
        /// </summary>
        /// <param name="SiteName">Name of the site</param>
        /// <returns>
        /// Certificate presented in X509Certificate2 type
        /// </returns>

        public X509Certificate2 LoadCertificateBySerialNumber(string serialNumber)
        {
            var store = new X509Store(StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            var certCollection = store.Certificates;
            var signingCert = certCollection.Find(X509FindType.FindBySerialNumber, serialNumber, false);
            if (signingCert.Count == 0) return default(X509Certificate2);
            return signingCert[0];
        }

        /// <summary>
        /// Loads the certificate from file placed on App_Data (.NET system folder).
        /// A password is only required if the file contains a private key.
        // The machine key set is specified so the certificate is accessible to the IIS process.
        /// </summary>
        /// <param name="fileName">certificate file Name</param>
        /// <param name="password">A password, is only required if the file contains a private key</param>
        /// <returns>The certificate X509Certificate2</returns>
        /// <remarks>Gleb: Метод загрузки сертификата из файла больше не используется, восстановить по необходимости. </remarks>
        private X509Certificate2 LoadCertificateFromFile(string absoluteFileNameUrl, string password)
        {
            X509Certificate2 cert = default(X509Certificate2);

            if (!File.Exists(absoluteFileNameUrl)) {
                throw new ArgumentException("The certificate file " + absoluteFileNameUrl + " doesn't exist.");
            }

            try {
                cert = new X509Certificate2(absoluteFileNameUrl, password, X509KeyStorageFlags.Exportable);
            }
            catch (Exception exception) {
                throw new ArgumentException("The certificate file " + absoluteFileNameUrl + " couldn't be loaded - " + exception.Message);
            }

            return cert;
        }

        /// <summary>
        /// Load certificate only with private key from storage, using certificate password
        /// </summary>
        /// <param name="Password">The password for the certificate</param>
        /// <returns>
        /// Certificate presented in X509Certificate2 type
        /// </returns>
        public X509Certificate2 LoadPrivateCertificate()
        {
            //TODO cache sertificate
            return LoadCertificateBySerialNumber(MPASSConfiguration.spCertificateSerialNumber);
        }

    }

}