using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ProgramareAC.Services.MPASS
{
    public class SamlHelper
    {

        #region Building
        public static string BuildAuthnRequest(string requestID, string destination, string assertionConsumerUrl, string issuer)
        {
            string authnRequestTemplate =
                @"<saml2p:AuthnRequest ID=""{0}"" Version=""2.0"" IssueInstant=""{1}"" Destination=""{2}"" AssertionConsumerServiceURL=""{3}"" xmlns:saml2p=""urn:oasis:names:tc:SAML:2.0:protocol"" xmlns:saml2=""urn:oasis:names:tc:SAML:2.0:assertion"">" +
                  @"<saml2:Issuer>{4}</saml2:Issuer>" +
                  @"<saml2p:NameIDPolicy AllowCreate=""true""/>" +
                @"</saml2p:AuthnRequest>";

            authnRequestTemplate = String.Format(authnRequestTemplate, requestID, XmlConvert.ToString(DateTime.UtcNow, XmlDateTimeSerializationMode.Utc),
                destination, assertionConsumerUrl, issuer);

            return XmlSigner.Sign(authnRequestTemplate);
        }

        public static string BuildLogoutRequest(string requestID, string destination, string issuer, string nameID, string sessionIndex)
        {
            string logoutRequestTemplate =
                @"<saml2p:LogoutRequest ID=""{0}"" Version=""2.0"" IssueInstant=""{1}"" Destination=""{2}"" xmlns:saml2p=""urn:oasis:names:tc:SAML:2.0:protocol"" xmlns:saml2=""urn:oasis:names:tc:SAML:2.0:assertion"">" +
                    @"<saml2:Issuer>{3}</saml2:Issuer>" +
                    @"<saml2:NameID>{4}</saml2:NameID>" +
                    @"<saml2p:SessionIndex>{5}</saml2p:SessionIndex>" +
                @"</saml2p:LogoutRequest>";

            logoutRequestTemplate = String.Format(logoutRequestTemplate, requestID, XmlConvert.ToString(DateTime.UtcNow, XmlDateTimeSerializationMode.Utc),
                destination, issuer, nameID, sessionIndex);

            return XmlSigner.Sign(logoutRequestTemplate);
        }

        public static string BuildLogoutResponse(string responseID, string destination, string requestID, string issuer)
        {
            string logoutResponseTemplate =
                @"<saml2p:LogoutResponse ID=""{0}"" Version=""2.0"" IssueInstant=""{1}"" Destination=""{2}"" InResponseTo=""{3}"" xmlns:saml2p=""urn:oasis:names:tc:SAML:2.0:protocol"" xmlns:saml2=""urn:oasis:names:tc:SAML:2.0:assertion"">" +
                    @"<saml2:Issuer>{4}</saml2:Issuer>" +
                    @"<saml2p:Status>" +
                        @"<saml2p:StatusCode Value=""urn:oasis:names:tc:SAML:2.0:status:Success""/>" +
                    @"</saml2p:Status>" +
                @"</saml2p:LogoutResponse>";

            logoutResponseTemplate = String.Format(logoutResponseTemplate, responseID, XmlConvert.ToString(DateTime.UtcNow, XmlDateTimeSerializationMode.Utc),
                destination, requestID, issuer);

            return XmlSigner.Sign(logoutResponseTemplate);
        }
        #endregion



        #region Parsing and Verification
        public static XmlDocument LoadAndVerifyResponse(string response, string expectedDestination, TimeSpan timeout, string expectedRequestID,
             IEnumerable<string> validStatusCodes, out XmlNamespaceManager ns, out bool isAuthenticationCancelled)
        {
            var result = new XmlDocument();
            ns = new XmlNamespaceManager(result.NameTable);
            isAuthenticationCancelled = false;
            ns.AddNamespace("saml2p", "urn:oasis:names:tc:SAML:2.0:protocol");
            ns.AddNamespace("saml2", "urn:oasis:names:tc:SAML:2.0:assertion");

            result.LoadXml(Decode(response));
            var responseElement = result.DocumentElement;
            if (responseElement == null) throw new ApplicationException("SAML Response invalid");

            // verify Signature
            if (MPASSConfiguration.SignatureValidationEnabled) {
                // verify Signature
                if (!XmlSigner.Verify(result, XmlSigner.LoadPublicCertificate())) {
                    throw new ArgumentException("SAML Response signature invalid");
                }
            }

            // verify IssueInstant
            var issueInstant = responseElement.GetAttribute("IssueInstant");
            if ((issueInstant == null) || ((DateTime.UtcNow - XmlConvert.ToDateTime(issueInstant, XmlDateTimeSerializationMode.Utc)).Duration() > timeout)) {
                throw new ApplicationException("SAML Response expired");
            }

            // verify Destination, according to [SAMLBind, 3.5.5.2]
            var responseDestination = responseElement.GetAttribute("Destination");
            if ((responseDestination == null) || !responseDestination.Equals(expectedDestination, StringComparison.CurrentCultureIgnoreCase)) {
                throw new ApplicationException("SAML Response is not for this Service");
            }

            // verify InResponseTo
            if (responseElement.GetAttribute("InResponseTo") != expectedRequestID) {
                throw new ApplicationException("SAML Response not expected");
            }

            // verify StatusCode
            var statusCodeValueAttribute = responseElement.SelectSingleNode("saml2p:Status/saml2p:StatusCode/@Value", ns);
            if (statusCodeValueAttribute == null) {
                throw new ApplicationException("SAML Response does not contain a StatusCode Value");
            }
            if (!validStatusCodes.Contains(statusCodeValueAttribute.Value, StringComparer.OrdinalIgnoreCase)) {
                var statusMessageNode = responseElement.SelectSingleNode("saml2p:Status/saml2p:StatusMessage", ns);

                //if Authentication Cancelled return flag to redirect to login page
                if (statusCodeValueAttribute.Value == "urn:oasis:names:tc:SAML:2.0:status:Responder") {
                    isAuthenticationCancelled = true;
                }
                else {
                    throw new ApplicationException(String.Format("Received failed SAML Response, status code: '{0}', status message: '{1}'", statusCodeValueAttribute.Value, statusMessageNode != null ? statusMessageNode.InnerText : null));
                }
            }

            return result;
        }

        public static XmlDocument LoadAndVerifyLoginResponse(string response, string expectedDestination, TimeSpan timeout, string expectedRequestID, string expectedAudience,
            out XmlNamespaceManager ns, out string sessionIndex, out string nameID, out Dictionary<string, IList<string>> attributes, out bool isAuthenticationCancelled)
        {
            var result = LoadAndVerifyResponse(response, expectedDestination, timeout, expectedRequestID, new[] { "urn:oasis:names:tc:SAML:2.0:status:Success" }, out ns, out isAuthenticationCancelled);

            //if Authentication Cancelled return flag to redirect to login page
            if (isAuthenticationCancelled) {
                sessionIndex = string.Empty;
                nameID = string.Empty;
                attributes = new Dictionary<string, IList<string>>();
                return result;
            }

            // get to Assertion
            var assertionNode = result.SelectSingleNode("/saml2p:Response/saml2:Assertion", ns);
            if (assertionNode == null) {
                throw new ApplicationException("SAML Response does not contain an Assertion");
            }

            // verify Audience
            var audienceNode = assertionNode.SelectSingleNode("saml2:Conditions/saml2:AudienceRestriction/saml2:Audience", ns);
            if ((audienceNode == null) || (audienceNode.InnerText != expectedAudience)) {
                throw new ApplicationException("The SAML Assertion is not for this Service");
            }

            // get SessionIndex
            var sessionIndexAttribute = assertionNode.SelectSingleNode("saml2:AuthnStatement/@SessionIndex", ns);
            if (sessionIndexAttribute == null) {
                throw new ApplicationException("The SAML Assertion AuthnStatement does not contain a SessionIndex");
            }
            sessionIndex = sessionIndexAttribute.Value;

            // get to Subject
            var subjectNode = assertionNode.SelectSingleNode("saml2:Subject", ns);
            if (subjectNode == null) {
                throw new ApplicationException("No Subject found in SAML Assertion");
            }

            // verify SubjectConfirmationData, according to [SAMLProf, 4.1.4.3]
            var subjectConfirmationDataNode = subjectNode.SelectSingleNode("saml2:SubjectConfirmation/saml2:SubjectConfirmationData", ns) as XmlElement;
            if (subjectConfirmationDataNode == null) {
                throw new ApplicationException("No Subject/SubjectConfirmation/SubjectConfirmationData found in SAML Assertion");
            }
            if (!subjectConfirmationDataNode.GetAttribute("Recipient").Equals(expectedDestination, StringComparison.CurrentCultureIgnoreCase)) {
                throw new ApplicationException("The SAML Response is not for this Service");
            }
            if (!subjectConfirmationDataNode.HasAttribute("NotOnOrAfter") || XmlConvert.ToDateTime(subjectConfirmationDataNode.GetAttribute("NotOnOrAfter"), XmlDateTimeSerializationMode.Utc) < DateTime.UtcNow) {
                throw new ApplicationException("Expired SAML Assertion");
            }

            // get NameID, which is normally an IDNP
            var nameIDNode = subjectNode.SelectSingleNode("saml2:NameID", ns);
            if (nameIDNode == null) {
                throw new ApplicationException("No Subject/NameID found in SAML Assertion");
            }
            nameID = nameIDNode.InnerText;

            // get attributes
            attributes = new Dictionary<string, IList<string>>();
            foreach (XmlElement attributeElement in assertionNode.SelectNodes("saml2:AttributeStatement/saml2:Attribute", ns)) {
                var attributeName = attributeElement.GetAttribute("Name");
                var attributeValues = attributeElement.SelectNodes("saml2:AttributeValue", ns).Cast<XmlElement>().Select(attributeValueElement => attributeValueElement.InnerXml).ToList();
                attributes.Add(attributeName, attributeValues);
            }

            return result;
        }

        public static XmlDocument LoadAndVerifyLogoutRequest(string request, string expectedDestination, TimeSpan timeout, string expectedNameID, string expectedSessionIndex,
            out string requestID)
        {
            var result = new XmlDocument();
            result.LoadXml(Decode(request));

            // verify Signature
            if (MPASSConfiguration.SignatureValidationEnabled) {
                // verify Signature
                if (!XmlSigner.Verify(result, XmlSigner.LoadPublicCertificate())) {
                    throw new ApplicationException("LogoutRequest signature invalid");
                }
            }

            var ns = new XmlNamespaceManager(result.NameTable);
            ns.AddNamespace("saml2p", "urn:oasis:names:tc:SAML:2.0:protocol");
            ns.AddNamespace("saml2", "urn:oasis:names:tc:SAML:2.0:assertion");

            // verify IssueInstant
            var issueInstantAttribute = result.SelectSingleNode("/saml2p:LogoutRequest/@IssueInstant", ns);
            if ((issueInstantAttribute == null) ||
                ((DateTime.UtcNow - XmlConvert.ToDateTime(issueInstantAttribute.Value, XmlDateTimeSerializationMode.Utc)).Duration() > timeout)) {
                throw new ApplicationException("The LogoutRequest is expired");
            }

            // verify Destination, according to [SAMLBind, 3.5.5.2]
            var requestDestination = result.SelectSingleNode("/saml2p:LogoutRequest/@Destination", ns);
            if ((requestDestination == null) || !requestDestination.Value.Equals(expectedDestination, StringComparison.CurrentCultureIgnoreCase)) {
                throw new ApplicationException("The LogoutRequest is not for this Service");
            }

            // verify NameID
            var nameIDElement = result.SelectSingleNode("/saml2p:LogoutRequest/saml2:NameID", ns);
            if ((nameIDElement == null) || ((expectedNameID != null) && !nameIDElement.InnerText.Equals(expectedNameID, StringComparison.CurrentCultureIgnoreCase))) {
                throw new ApplicationException("The LogoutRequest received is for a different user");
            }

            // verify SessionIndex
            var sessionIndexElement = result.SelectSingleNode("/saml2p:LogoutRequest/saml2p:SessionIndex", ns);
            if ((sessionIndexElement == null) || ((expectedSessionIndex != null) && !sessionIndexElement.InnerText.Equals(expectedSessionIndex, StringComparison.CurrentCultureIgnoreCase))) {
                throw new ApplicationException("The LogoutRequest is not expected in this user session");
            }

            // get LogoutRequest ID
            var logoutRequestIDAttribute = result.SelectSingleNode("/saml2p:LogoutRequest/@ID", ns);
            if (logoutRequestIDAttribute == null) {
                throw new ApplicationException("LogoutRequest does not have an ID");
            }
            requestID = logoutRequestIDAttribute.Value;

            return result;
        }

        public static XmlDocument LoadAndVerifyLogoutResponse(string response, string expectedDestination, TimeSpan timeout, string expectedRequestID,
           out XmlNamespaceManager ns, out bool isAuthenticationCancelled)
        {
            return LoadAndVerifyResponse(response, expectedDestination, timeout, expectedRequestID,
                new[] { "urn:oasis:names:tc:SAML:2.0:status:Success", "urn:oasis:names:tc:SAML:2.0:status:PartialLogout" }, out ns, out isAuthenticationCancelled);
        }
        #endregion


        public static SAMLUserData CreateUserData(string nameID, string sessionIndex, Dictionary<string, IList<string>> attributes)
        {
            SAMLUserData userData = new SAMLUserData();

            if (!string.IsNullOrEmpty(nameID)) {
                userData.SessionIndex = sessionIndex;
                userData.IDNP = nameID;

                userData.FirstName = attributes["FirstName"].First();
                userData.LastName = attributes["LastName"].First();

                //if(attributes.ContainsKey("BirthDate"))
                //    userData.Birthday = DateTime.Parse(attributes["BirthDate"].First());

                if (attributes.ContainsKey("Language"))
                    userData.Language = attributes["Language"].First();
                
                if (attributes.ContainsKey("Email")) {
                    userData.Email = attributes["Email"].First();
                }

                return userData;
            }

            throw new Exception("MPASS CreateUserData nameID is empty");

        }




        #region Encoding
        public static string Encode(string message)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
        }

        public static string Decode(string message)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(message));
        }
        #endregion

        public class SAMLUserData
        {
            public string SessionIndex { get; set; }
            public string IDNP { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime Birthday { get; set; }
            public string Email { get; set; }
            /// <summary>
            /// пол 1- муж,2-жен
            /// </summary>
            public byte Gender { get; set; }
            public string Language { get; set; }
        }
    }

}
