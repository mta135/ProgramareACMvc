using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramareAC.Models.Models.Appointment
{
    public class ResponseResultPack
    {
        public string MsingInfo { get; set; }
        public string MsRequestId { get; set; }

        public string OracleTransferStatusText { get; set; }

        public bool IsOracleResponse { get; set; } = true;
    }
}
