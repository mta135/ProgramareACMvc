using System.Configuration;

namespace ProgramareAC.Services.MPASS
{
    public static class MPASSConfiguration
    {
        /// <summary>
        /// Gleb: Initialize with App config
        /// </summary>
        public static void InitializeSettingsMVC5()
        {
            spCertificateSerialNumber = ConfigurationManager.AppSettings["MPASS_SP_CertificateSerialNumber"];
            idpCertificateSerialNumber = ConfigurationManager.AppSettings["MPASS_IDP_CertificateSerialNumber"];
            spCertificateFileName = ConfigurationManager.AppSettings["MPASS_SP_CertificatePath"];
            PfxPassword = ConfigurationManager.AppSettings["MPASS_SP_CertificatePassword"];
            idpCertificateFileName = ConfigurationManager.AppSettings["MPASS_IDP_CertificatePath"];
            SamlLoginDestination = ConfigurationManager.AppSettings["SamlLoginDestination"];
            SamlLogoutDestination = ConfigurationManager.AppSettings["SamlLogoutDestination"];
            Issuer = ConfigurationManager.AppSettings["MPASS_Issuer"];
            SignatureValidationEnabled = bool.Parse(ConfigurationManager.AppSettings["MPASS_SignatureValidationEnabled"]);
            SamlMessageTimeout = ConfigurationManager.AppSettings["SamlMessageTimeout"];
        }

        /// <summary>
        /// Gleb: Initialize with .NET core
        /// Should look like this 
        /// </summary>
        /// <param name="config"></param>
        /// <remarks>
        /// "MPASS": {
        ///        "SP_CertificateSerialNumber": "46d399f4",
        ///       "IDP_CertificateSerialNumber": "46d399d0",
        ///        "Issuer": "http://test.raportare.md",
        ///        "SamlLoginDestination": "https://testmpass.gov.md/LoginChoice.aspx",
        ///        "SamlLogoutDestination": "https://testmpass.gov.md/logout/saml",
        ///        "SamlMessageTimeout": "00:10:00",
        ///        "SignatureValidationEnabled": true
        ///      }
        /// </remarks>
        //public static void InitializeSettingsCore(IConfiguration config)
        //{
        //    spCertificateSerialNumber = config.GetValue<string>("MPASS:SP_CertificateSerialNumber");
        //    idpCertificateSerialNumber = config.GetValue<string>("MPASS:IDP_CertificateSerialNumber");
        //    spCertificateFileName = config.GetValue<string>("MPASS:SP_CertificatePath");
        //    PfxPassword = config.GetValue<string>("MPASS:SP_CertificatePassword");
        //    idpCertificateFileName = config.GetValue<string>("MPASS:IDP_CertificatePath");
        //    SamlLoginDestination = config.GetValue<string>("MPASS:SamlLoginDestination");
        //    SamlLogoutDestination = config.GetValue<string>("MPASS:SamlLogoutDestination");
        //    SamlMessageTimeout = config.GetValue<string>("MPASS:SamlMessageTimeout");
        //    Issuer = config.GetValue<string>("MPASS:Issuer");
        //    SignatureValidationEnabled = config.GetValue<bool>("MPASS:SignatureValidationEnabled");
        //}

        // The application key to the identity provider's certificate.
        public const string IdPX509Certificate = "idpX509Certificate";

        // The application key to the service provider's certificate.
        public const string SPX509Certificate = "spX509Certificate";

        // The service provider's certificate/private key file 
        public static string spCertificateSerialNumber { get; set; }

        // The identity provider's certificate file name - must be in the application directory.
        public static string idpCertificateSerialNumber { get; set; }

        // The service provider's certificate/private key file 
        public static string spCertificateFileName { get; set; }

        // The service provider's certificate/private key file password.
        public static string PfxPassword { get; set; }

        // The identity provider's certificate file name - must be in the application directory.
        public static string idpCertificateFileName { get; set; }

        /// <summary>
        /// Gets the assertion URL.
        /// </summary>
        public static string SamlLoginDestination { get; set; }

        /// <summary>
        /// Gets the Single logout URL.
        /// </summary>
        public static string SamlLogoutDestination { get; set; }

        public static string SamlMessageTimeout { get; set; }

        /// <summary>
        /// Gets the MPASS_Issuer.
        /// </summary>
        public static string Issuer { get; set; }

        public static bool SignatureValidationEnabled { get; set; }
    }
}