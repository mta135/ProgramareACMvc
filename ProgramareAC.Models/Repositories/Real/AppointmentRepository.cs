using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ProgramareAC.DataBaseConnection;
using ProgramareAC.Models.Repositories.Abstract;
using ProgramareAC.Services.MSign.Model;

namespace ProgramareAC.Models.Repositories.Real
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DataBaseAccesConfig _db;

        public AppointmentRepository()
        {
            _db = new DataBaseAccesConfig();
        }


        private SqlCommand CommnadRegisterAppointment(AppointmentModel appointment)
        {
            SqlCommand command = new SqlCommand("[dbo].[RegisterAppointment]", _db.Connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = appointment.LastName;
            command.Parameters.Add("@Surname", SqlDbType.NVarChar).Value = appointment.FirstName;
            command.Parameters.Add("@IDNP", SqlDbType.NVarChar).Value = appointment.IDNP;
            command.Parameters.Add("@BirthDate", SqlDbType.NVarChar).Value = appointment.Date;

            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = appointment.Email;
            command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = appointment.Phone;
            command.Parameters.Add("@AudienceSubject", SqlDbType.NVarChar).Value = appointment.AudienceSubject;

            command.Parameters.Add("@ServiceId", SqlDbType.NVarChar).Value = appointment.Select1;
            command.Parameters.Add("@ServiceName", SqlDbType.NVarChar).Value = appointment.ServiceName;

            command.Parameters.Add("@ServiceTypeId", SqlDbType.NVarChar).Value = appointment.Service.Split('|')[0];
            command.Parameters.Add("@ServiceTypeName", SqlDbType.NVarChar).Value = appointment.Service.Split('|')[1];

            command.Parameters.Add("@OrarId", SqlDbType.NVarChar).Value = appointment.Times.Split('|')[0];
            command.Parameters.Add("@RegisterDate", SqlDbType.NVarChar).Value = appointment.Times.Split('|')[1];
            command.Parameters.Add("@Hash", SqlDbType.VarBinary).Value = appointment.Hash;

            command.Parameters.Add("@SignPattern", SqlDbType.NVarChar).Value = appointment.SingPattern;
            command.Parameters.Add("@MsingRequestId", SqlDbType.NVarChar).Value = appointment.MsignRequestId;

            return command;
        }

        public async Task RegisterAppointmenRequest(AppointmentModel appointment)
        {
            try
            {

                SqlCommand command = CommnadRegisterAppointment(appointment);
                await command.ExecuteNonQueryAsync();
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


        private SqlCommand CommandGetAppointmentByMsignRequestId(string msignRequestId)
        {
            SqlCommand command = new SqlCommand("GetAppointmentByMsignRequestId", _db.Connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MsingRequestId", SqlDbType.NVarChar).Value = msignRequestId;

            return command;
        }

        public AppointmentModel GetAppointmentByMsignRequestId(string requestId)
        {
            try
            {

                SqlCommand command = CommandGetAppointmentByMsignRequestId(requestId);
                command.ExecuteNonQuery();

                SqlDataReader reader = command.ExecuteReader();
                AppointmentModel appointment = new AppointmentModel();

                if (reader.Read())
                {

                    appointment.FirstName = (string)reader["Surname"];

                    appointment.LastName = (string)reader["Name"];

                    appointment.IDNP = (string)reader["IDNP"];

                    appointment.Date = (string)reader["BirthDate"];

                    appointment.Email = (string)reader["Email"];

                    appointment.Phone = (string)reader["Phone"];
                    appointment.AudienceSubject = (string)reader["AudienceSubject"];

                    appointment.Select1 = (string)reader["ServiceId"];

                    appointment.ServiceName = (string)reader["ServiceName"];

                    string times = (string)reader["OrarId"] + "|" + (string)reader["RegisterDate"];
                    appointment.Times = times;

                    appointment.Hash = (byte[])reader["Hash"];

                    string service = (string)reader["ServiceTypeId"] + "|" + (string)reader["ServiceTypeName"];
                    appointment.Service = service;

                    appointment.PCerereId = (string)reader["PCerereId"];

                }

                return appointment;
            }
            catch (SqlException ex)
            {

                string excception = ex.ToString();
                return new AppointmentModel();
            }
            finally
            {
                _db.Dispose();
            }
        }


        private SqlCommand CommandSignSetResult(string msignRequestId)
        {
            SqlCommand command = new SqlCommand("[dbo].[GetAppointmentByMsignRequestId]", _db.Connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MsingRequestId", SqlDbType.NVarChar).Value = msignRequestId;

            return command;
        }

        public SignPackResult GetSignPackResult(string requestId)
        {
            try
            {

                SqlCommand command = CommandSignSetResult(requestId);
                command.ExecuteNonQuery();

                SqlDataReader reader = command.ExecuteReader();
                SignPackResult signResult = new SignPackResult();

                if (reader.Read())
                {

                    signResult.Hash = (byte[])reader["Hash"];

                    signResult.SignDate = (DateTime)reader["SignDate"];

                    signResult.SignerFullName = (string)reader["SignerFullName"];

                    signResult.SingerIdnp = (string)reader["SingerIdnp"];

                    signResult.Sing = (byte[])reader["Sign"];

                    signResult.PCerereId = (string)reader["PCerereId"];
                }

                return signResult;
            }
            catch (SqlException ex)
            {

                //WriteLog.Common.Error("Method GetSignPackResult give an error: ", ex);
                return new SignPackResult();
            }
            finally
            {
                _db.Dispose();
            }
        }


        private SqlCommand CommnadSetFinalAppointment(string msRequestId, string pCerereId, decimal? OracleTransferStatusCode)
        {
            SqlCommand command = new SqlCommand("[dbo].[SetAppointmentTransferStatus]", _db.Connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@MsingRequestId", SqlDbType.NVarChar).Value = msRequestId;

            if (!string.IsNullOrEmpty(pCerereId))
                command.Parameters.Add("@PCerereId", SqlDbType.NVarChar).Value = pCerereId;

            if (OracleTransferStatusCode != null)
                command.Parameters.Add("@OracleTransferStatus", SqlDbType.NVarChar).Value = OracleTransferStatusCode;

            return command;
        }

        public async Task SetAppointmentTransferStatus(string msRequestId, string pRequestId, decimal? submitResponseCode)
        {
            try
            {

                SqlCommand command = CommnadSetFinalAppointment(msRequestId, pRequestId, submitResponseCode);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {

                string exception = ex.ToString();
                throw;
            }
            finally
            {

                _db.Dispose();
            }
        }

    }
}
