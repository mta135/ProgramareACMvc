using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProgramareAC.Models
{
    public class AppointmentModel
    {
        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        public System.DateTime DateCreated { get; set; }

        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        public string Date { get; set; }

        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        public string Times { get; set; }

        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = @"IDNP-ul trebuie să fie din 13 cifre.")]
        public string IDNP { get; set; }

        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        public string Select1 { get; set; }

        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        public string Service { get; set; }
   
        public string Email { get; set; }
    
        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        [StringLength(250, ErrorMessage = @"Subiectul audientiei trebuie să fie din 250 de caractere.")]
        public string AudienceSubject { get; set; }

        public string ServiceName { get; set; }

        public byte[] Hash { get; set; }

        public IEnumerable<SelectListItem> AcCnas { get; set; }
        public IEnumerable<SelectListItem> TipulServiciului { get; set; }



        // Fields for MSING
        public   string SingPattern { get; set; }
        public string MsignRequestId { get; set; }

    }
}