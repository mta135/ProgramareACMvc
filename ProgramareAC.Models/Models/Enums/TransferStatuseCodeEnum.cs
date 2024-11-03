using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramareAC.Models.Models.Enums
{
    public class TransferStatuseCodeEnum
    {
        public static int MsignSucces = 0;
        
        public static int OracleTransferError = 1;
        
        public static int MsignError = 2;
        
        public static int MpassAuthenticationError = 3;
    }
}
