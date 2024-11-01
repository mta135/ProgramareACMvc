using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ProgramareAC.DataBaseConnection;
using ProgramareAC.Models;
using ProgramareAC.Models.LogHelper;
using ProgramareAC.Models.Models.Msign;

namespace ProgramareAC.DataAccess
{
    public class MSingRepository : IMSignRepository
    {
        DataBaseAccesConfig _db;

        public MSingRepository()
        {
            _db = new DataBaseAccesConfig();
        }

        private SqlCommand CommnadUpdateMsignRequest(string requestId, int responseStatus, string responseMessage)
        {
            SqlCommand command = new SqlCommand("[dbo].[UpdateSignedDocument]", _db.Connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@MsingRequestId", SqlDbType.NVarChar).Value = requestId;

            command.Parameters.Add("@ResposnseAt", SqlDbType.DateTime2).Value = DateTime.Now;
            command.Parameters.Add("@ResponseStatus", SqlDbType.Int).Value = responseStatus;

            if (!string.IsNullOrEmpty(responseMessage))
                command.Parameters.Add("@ResponseMessage", SqlDbType.NVarChar).Value = responseMessage;

            return command;
        }

        public void UpdateSignedDocument(string requestId, int responseStatus, string responseMessage)
        {
            try
            {

                SqlCommand command = CommnadUpdateMsignRequest(requestId, responseStatus, responseMessage);
                command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {

                string exception = ex.ToString();
            }
            finally
            {

                _db.Dispose();
            }
        }


        private SqlCommand CommnadSetSign(string requestId, DateTime signDate, string signerFullName, string signerIdnp, byte[] sign)
        {
            SqlCommand command = new SqlCommand("[dbo].[SetSign]", _db.Connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@MsingRequestId", SqlDbType.NVarChar).Value = requestId;

            command.Parameters.Add("@SignDate", SqlDbType.DateTime2).Value = signDate;
            command.Parameters.Add("@SignerFullName", SqlDbType.NVarChar).Value = signerFullName;
            command.Parameters.Add("@SingerIdnp", SqlDbType.NVarChar).Value = signerIdnp;

            command.Parameters.Add("@Sign", SqlDbType.VarBinary).Value = sign;

            return command;
        }

        public async Task SetSign(string requestId, DateTime signDate, string signerFullName, string signerIdnp, byte[] sign)
        {
            try
            {
                SqlCommand command = CommnadSetSign(requestId, signDate, signerFullName, signerIdnp, sign);

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                WriteLog.Common.Error("SetSign method give an error: ", ex);
            }
            finally
            {
                _db.Dispose();
            }
        }

        
        private SqlCommand CommandSetMsignDocumentRequest(int appointmentId, string msignRequest, int appType)
        {
            SqlCommand command = new SqlCommand("[dbo].[SetMsignDocumentRequest]", _db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@AppointmentId", SqlDbType.Int).Value = appointmentId;

            command.Parameters.Add("@MsingRequestId", SqlDbType.NVarChar).Value = msignRequest;
            command.Parameters.Add("@ProgramareAC", SqlDbType.TinyInt).Value = appType;

            return command;
        }

        public void SetMsignDocumentRequest(int appointmentId, string msignRequest, int appType)
        {
            try
            {
                SqlCommand command = CommandSetMsignDocumentRequest(appointmentId, msignRequest, appType);
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                WriteLog.Common.Error("SetMsignDocumentRequest method give an error: ", ex);
            }
            finally
            {
                _db.Dispose();
            }

        }
    }
}