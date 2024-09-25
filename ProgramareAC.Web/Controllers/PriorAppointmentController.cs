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
        ServiceReference.WSO2_package_017ACPortTypeClient client;

        public PriorAppointmentController()
        {
            client = new ServiceReference.WSO2_package_017ACPortTypeClient("SOAP11Endpoint");
        }
        
            
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Appointment()
        {

         
            var value = client.get_RN();


            AppointmentModel appointmentModel = new AppointmentModel();
            return View(appointmentModel);
        }
    }
}