using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ProgramareAC.DataAccess;
using ProgramareAC.Models;
using ProgramareAC.Models.LogHelper;
using ProgramareAC.Models.Models.Enums;
using ProgramareAC.Models.Repositories.Abstract;
using ProgramareAC.Models.Repositories.Real;

using ProgramareAC.Services.MSign.Model;
using ProgramareAC.Web.Services.Msign.Client;

namespace ProgramareAC.Services.MSign.MSignCommunication
{
    public class MSignCommunicationService : IMSignCommunicationService
    {
        private readonly IMSignRepository _mSignRepository;

        private readonly IAppointmentRepository _appointmentRepository;

        public MSignCommunicationService()
        {
            _mSignRepository = new MSingRepository();
            _appointmentRepository = new AppointmentRepository();
        }

        public string SendMSignDocumentRequest(AppointmentModel appointmentModel)
        {
            try
            {
                string resultRequestId = null;

                Tuple<int, string> registerResult = _appointmentRepository.RegisterAppointmenRequest(appointmentModel);

                int appointmentId = registerResult.Item1;

                string signPattern = registerResult.Item2;

                byte[] propertiesForSign = SetSh1Hash(signPattern);

                string correlationId = appointmentModel.Times.Split('|')[0];

                SignRequest signRequest = new SignRequest
                {
                    ShortContentDescription = "Programarea online la audienţa la aparatul central al CNAS",
                    ContentType = ContentType.Hash,
                    SignatureReason = "Programarea online la audienţa la aparatul central al CNAS. Confirmare pentru validitatea datelor",
                };

                SignContent signContent = new SignContent
                {
                    CorrelationID = correlationId, 
                    MultipleSignatures = false,
                    Content = propertiesForSign
                };

                SignContent[] signContents = new SignContent[1];

                signContents[0] = signContent;

                signRequest.Contents = signContents;

                IMSign client = MSignClientFactory.Create();

                resultRequestId = client.PostSignRequest(signRequest);

                _mSignRepository.SetMsignDocumentRequest(appointmentId, resultRequestId, AppTypeEnum.ProgramareAc);

                return resultRequestId;
            }
            catch (Exception ex)
            {
                WriteLog.Common.Error("Method SendMSignDocumentRequest give an erorr: ", ex);

                return null;
            }
        }

        public int MSignRequestCheckAndAccepted(string requestID)
        {
            return MsignCheckRequest(requestID);
        }

        private int MsignCheckRequest(string requestID)
        {
            int msignResponseResult = -1;

            try
            {
                var client = MSignClientFactory.Create();

                SignResponse signResponse = client.GetSignResponse(requestID, "ro");

                int singResponseStatus = (int)signResponse.Status;
                msignResponseResult = singResponseStatus;

                if (singResponseStatus == (int)SignStatus.Success)
                {
                    _mSignRepository.UpdateSignedDocument(requestID, singResponseStatus, signResponse.Message);

                    SignPackSaveModel signPackSaveModel = new SignPackSaveModel(signResponse.Results);

                    List<SignPackSaveItem> items = signPackSaveModel.Items;

                    SignPackSaveItem signItem = items.First();
                    byte[] sign = signItem.XadesBytes;

                    _mSignRepository.SetSign(requestID, signItem.SignDate, signItem.SignerFullName, signItem.SignerIDNP, sign);
                }
                else
                {
                    _mSignRepository.UpdateSignedDocument(requestID, singResponseStatus, signResponse.Message);
                }
            }
            catch (Exception ex)
            {
                WriteLog.Common.Error("Method MsignCheckRequest give an error. MSIngRequestId: " + requestID + "; Exception: ", ex);
            }

            return msignResponseResult;
        }

        public SignValidationResult VerifyMSignSignature(string requestId)
        {
            SignPackResult signPack = _appointmentRepository.GetSignPackResult(requestId);
            SignValidationResult result = new SignValidationResult();

            byte[] hash = SetSh1Hash(signPack.SignPattern); 
            byte[] sign = signPack.Sing;
            string correlationId = signPack.OrarId;

            if (sign == null || hash == null)
                throw new Exception("Could not load sign for verify - parameter empty, DB empty. Task<SignValidationResult> VerifyMSignSignature(...)");

            //Получить данные прямо из подписи
            XAdES xades = new XAdES(sign);
            result.Signer = xades.SubjectName;
            result.SignDate = xades.SignTimeStamp;

            try
            {
                IMSign client = MSignClientFactory.Create();

                VerificationContent verificationContent = new VerificationContent
                {
                    CorrelationID = correlationId,
                    Content = hash,
                    Signature = sign
                };

                VerificationContent[] contentsField = new VerificationContent[1];
                contentsField[0] = verificationContent;

                VerificationRequest verificationRequest = new VerificationRequest
                {

                    Contents = contentsField,
                    SignedContentType = ContentType.Hash
                };

                VerificationResponse verificationResponse = client.VerifySignatures(verificationRequest);

                VerificationResult verificationResult = verificationResponse.Results.First();

                result.Status = verificationResult.SignaturesValid ? SignValidationStatus.Valid : SignValidationStatus.NonValid;

                result.Message = verificationResult.Message;

                if (verificationResult.SignaturesValid)
                {
                    var firstCertificate = verificationResult.Certificates.FirstOrDefault();
                    var SignedAt = firstCertificate.SignedAt;
                }
            }
            catch (Exception ex)
            {
                WriteLog.Common.Error("Method VerifyMSignSignature give an error. Exception: ", ex);

                result.Status = SignValidationStatus.ValidationError;
            }
            return result;
        }

        private static byte[] SetSh1Hash(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {

                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                return sha1.ComputeHash(inputBytes);

            }
        }

        private string ConverToSignContentFields(SignContentFields signing)
        {
            string singPattern = $"Name:{signing.Surname}/Surname:{signing.Name}/IDNP:{signing.IDNP}/BirthDate:{signing.BirthDate}/Phone:{signing.Phone}/Email:{signing.Email}/";

            return singPattern;
        }

        private SignContentFields ConvertToSignContentFields(AppointmentModel appointment)
        {
            SignContentFields signing = new SignContentFields();

            signing.ServiceId = appointment.Select1;

            signing.ServiceName = appointment.ServiceName;

            signing.ServiceTypeId = appointment.Service.Split('|')[0];

            signing.ServiceTypeName = appointment.Service.Split('|')[1];

            signing.BirthDate = appointment.Date;

            signing.IDNP = appointment.IDNP;

            signing.Name = appointment.FirstName;

            signing.Surname = appointment.LastName;

            signing.Email = appointment.Email;

            signing.Phone = appointment.Phone;

            return signing;
        }
    }
}