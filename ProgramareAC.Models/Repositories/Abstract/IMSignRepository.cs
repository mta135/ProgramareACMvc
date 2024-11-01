using System;
using System.Threading.Tasks;
using ProgramareAC.Models;

namespace ProgramareAC.DataAccess
{
    public interface IMSignRepository
    {
        void UpdateSignedDocument(string requestId, int responseStatus, string responseMessage);

        Task SetSign(string requestId, DateTime signDate, string signerFullName, string signerIdnp, byte[] sign);

        void SetMsignDocumentRequest(int appointmentId, string msignRequest, int appType);
    }
}