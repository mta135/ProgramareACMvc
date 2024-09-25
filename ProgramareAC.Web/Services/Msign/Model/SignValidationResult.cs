using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramareAC.Services.MSign.Model
{
    public class SignValidationResult
    {
        public SignValidationStatus Status { get; set; }

        public string Message { get; set; }

        public string Signer { get; set; }

        public DateTime? SignDate { get; set; }

    }

    public enum SignValidationStatus
    {
        Valid = 1,
        NonValid = 2,
        ValidationError = 3
    }
}
