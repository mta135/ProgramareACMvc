using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramareAC.Services.MSign.Model;

namespace ProgramareAC.Models.Repositories.Abstract
{
    public interface IAppointmentRepository
    {
        Task RegisterAppointmenRequest(AppointmentModel appointment);

        AppointmentModel GetAppointmentByMsignRequestId(string requestId);

        SignPackResult GetSignPackResult(string requestId);

        Task SetAppointmentTransferStatus(string msRequestId, string pRequestId, decimal? submitResponseCode);
    }
}
