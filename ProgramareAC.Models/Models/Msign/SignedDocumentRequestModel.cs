using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramareAC.Models.Models.Msign
{
    public class SignedDocumentRequestModel
    {
        public string MsignRequestId { get; set; }

        public DateTime ReesponseAt { get; set; }

        public string ResponseMessage { get; set; }
        
        public byte[] Sing { get; set; }
    }
}
