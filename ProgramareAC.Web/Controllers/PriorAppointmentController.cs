using ProgramareAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramareAC.Web.Controllers
{
    public class PriorAppointmentController : Controller
    {
        
        
        
        
        
        
        
        
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Appointment()
        {
            AppointmentModel appointmentModel = new AppointmentModel();
            return View(appointmentModel);
        }
    }
}