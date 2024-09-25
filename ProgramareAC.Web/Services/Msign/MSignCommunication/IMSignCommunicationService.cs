using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgramareAC.Models;
using ProgramareAC.Services.MSign.Model;

namespace ProgramareAC.Services.MSign.MSignCommunication
{
    public interface IMSignCommunicationService
    {
        string SendMSignDocumentRequest(AppointmentModel appointmentModel);

        int MSignRequestCheckAndAccepted(string requestID);

        SignValidationResult VerifyMSignSignature(string requestId);

    }
}