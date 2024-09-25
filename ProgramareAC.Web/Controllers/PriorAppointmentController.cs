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
        readonly ServiceReference.WSO2_package_017ACPortTypeClient client;

        public PriorAppointmentController()
        {
            client = new ServiceReference.WSO2_package_017ACPortTypeClient("SOAP11Endpoint");
        }
        
            
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Appointment()
        {
            AppointmentModel appointmentModel = new AppointmentModel();

            appointmentModel.AcCnas = SetAcCnasItems();
            appointmentModel.TipulServiciului = new List<SelectListItem>();

            return View(appointmentModel);
        }

        private List<SelectListItem> SetAcCnasItems()
        {
            ServiceReference.Row3[] arrRN =  client.get_RN();

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (ServiceReference.Row3 _arrRN in arrRN)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = _arrRN.RN.ToString(),
                    Text = _arrRN.NAMER
                });
            }

            return selectListItems;
        }
    }
}