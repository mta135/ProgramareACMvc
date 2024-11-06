using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramareAC.Models.Models.Appointment
{
    public class CheckStatusModel
    {
        public string RequestStatus { get; set; }


        [Required(ErrorMessage = @"Acest cîmp este obligator, pentru verificarea cererii")]
        [RegularExpression("([0-9]+)", ErrorMessage = "se accepta doar cifre")]
        public int? pCerereId { get; set; }

    }
}
