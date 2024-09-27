using ProgramareAC.Models;
using ProgramareAC.Models.Models.Appointment;
using ProgramareAC.Models.Models.Enums;
using ProgramareAC.Models.Repositories.Abstract;
using ProgramareAC.Services.MPASS;
using ProgramareAC.Services.MSign.Model;
using ProgramareAC.Services.MSign.MSignCommunication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramareAC.Web.Controllers
{
    [Authorize]
    public class PriorAppointmentController : Controller
    {
        readonly ServiceReference.WSO2_package_017ACPortTypeClient client;

        private readonly IMSignCommunicationService _mSignCommunicationService;

        private readonly IAppointmentRepository _appointmentRepository;

        public PriorAppointmentController(IAppointmentRepository appointmentRepository)
        {
            client = new ServiceReference.WSO2_package_017ACPortTypeClient("SOAP11Endpoint");

            _mSignCommunicationService = new MSignCommunicationService();

            _appointmentRepository = appointmentRepository;
        }
        
            
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Appointment()
        {
            AppointmentModel appointmentModel = new AppointmentModel();

            appointmentModel.AcCnas = SetAcCnasItems();

            AppointmentModel parsedAppointmentModel = ParseSamlUserData.ParseTo(new AppointmentModel()); //ParseToAppointmentModel();

            appointmentModel.IDNP = parsedAppointmentModel.IDNP;
            appointmentModel.FirstName = parsedAppointmentModel.FirstName;
            appointmentModel.LastName = parsedAppointmentModel.LastName;

            return View(appointmentModel);
        }

        #region Msing

        //********************** MSING *********************

        private string RequestBaseUrl()
        {
            HttpRequestBase request = HttpContext.Request;
            string url = $"{request.Url.Scheme}://{request.Url.Authority}{request.ApplicationPath.TrimEnd('/')}";

            return url;
        }

        [HttpPost]
        public ActionResult Appointment(AppointmentModel form1)
        {
            GetAppointmentServiceName(form1);

            ViewBag.Phone = form1.Phone; // aka MSISDN;

            ViewBag.RelayState = Guid.NewGuid().ToString();
            ViewBag.ReturnUrl = string.Format("{0}/{2}/{1}", RequestBaseUrl(), "MSignDocumentResponse", "PriorAppointment");
            ViewBag.URLBrowser = ConfigurationManager.AppSettings["MSign_UrlBrowser"];

            string msingRequestId = _mSignCommunicationService.SendMSignDocumentRequest(form1);

            ViewBag.RequestID = msingRequestId;

            return View("MSignRedirect");

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult MSignDocumentResponse(string requestID, string relayState)
        {
            string view;

            int mSingAcceptedResult = _mSignCommunicationService.MSignRequestCheckAndAccepted(requestID);

            ResponseResultPack responseResult = new ResponseResultPack();

            if (mSingAcceptedResult == (int)SignStatusEnum.Success)
            {
                Tuple<decimal?, string, string> submitAppointmentResult = SubmitAppointment(requestID);
                decimal? oracleTransferStatusCode = submitAppointmentResult.Item1;

                if (oracleTransferStatusCode == 0)
                {

                    string pRequestId = submitAppointmentResult.Item2;

                    responseResult.OracleTransferStatusText = submitAppointmentResult.Item3;
                    responseResult.MsRequestId = requestID;

                    _appointmentRepository.SetAppointmentTransferStatus(requestID, pRequestId, oracleTransferStatusCode);

                    view = "Result";
                }

                else
                {
                    responseResult.OracleTransferStatusText = submitAppointmentResult.Item3;
                    responseResult.MsingInfo = "Pentru a putea beneficia de programare, este nevoie de semnarea datelor...";

                    _appointmentRepository.SetAppointmentTransferStatus(requestID, null, oracleTransferStatusCode);

                    view = "Error";
                }

                return View(view, responseResult);
            }

            view = "Error";

            responseResult.IsOracleResponse = false;
            responseResult.MsingInfo = "MSIGN. A Aparut o problema la semnarea datelor. Este nevoie de mai semnat odata.";

            return View(view, responseResult);
        }

        [HttpGet]
        public JsonResult VerifyMSignSignature(string requestId)
        {
            //            string requestId = "bf02e8ff8d9b4c6f99d5b1e00073a837";
            SignValidationResult result = _mSignCommunicationService.VerifyMSignSignature(requestId);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        //***********************************************

        #endregion

        [HttpGet]
        public JsonResult GetServiceType(string rn)
        {
            decimal _rn = Convert.ToDecimal(rn);
  
            ServiceReference.Row4[] mSrv = client.get_Serv(_rn);

            List<SelectListItem> srv = ConvertToSelectedItems(mSrv);
            return Json(srv, JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> ConvertToSelectedItems(ServiceReference.Row4[] mSrv)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (ServiceReference.Row4 _srv in mSrv)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Text = _srv.SERV_NAME_RO,
                    Value = _srv.SERV_ID
                });
            }

            return selectListItems;
        }

        public JsonResult GetTimes(string rn, string serv)
        {
            string[] words = rn.Split('|');
            string[] words1 = serv.Split('|');
            string rn1 = words[0];
            int serv1 = int.Parse(words1[0]);

            ServiceReference.Row[] arr1 = client.get_FreeTime(rn1, serv1);

            return Json(arr1, JsonRequestBehavior.AllowGet);
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

        public Tuple<decimal?, string, string> SubmitAppointment(string msRequestId) // va trebui de folosit aceasta metoda pe viitor...
        {
            AppointmentModel form1 = _appointmentRepository.GetAppointmentByMsignRequestId(msRequestId);

            ViewBag.idnp = form1.IDNP;
            ViewBag.LastName = form1.LastName;
            ViewBag.FirstName = form1.FirstName;

            string[] words2 = form1.Times.Split('|');
            ViewBag.Times1 = words2[0];
            ViewBag.Times2 = words2[1];

            ServiceReference.Row3[] arr1 = client.get_RN();
            var RN = int.Parse(form1.Select1);

            ServiceReference.Row3 el = arr1.First(x => x.RN == RN);
            var Adr = el.ADRES;
            ViewBag.Select_2 = Adr;

            string[] words1 = form1.Service.Split('|');
            ViewBag.Service1 = words1[0];
            ViewBag.Service2 = words1[1];

            string[] dd = form1.Date.Split('-');
            string dat = dd[2] + "-" + dd[1] + "-" + dd[0];
            ViewBag.BirthDate = dat;

            ServiceReference.Row2[] rez = client.set_Time(form1.IDNP, form1.LastName, form1.FirstName, dat, int.Parse(words2[0]), int.Parse(words1[0]), RN, form1.Email, form1.Phone, form1.AudienceSubject);

            if (rez[0].Error == 0)
                return new Tuple<decimal?, string, string>(rez[0].Error, rez[0].pCERERE_ID, "Error : No result.");

            return new Tuple<decimal?, string, string>(null, null, rez[0].Rezult_Text);
        }

        private void GetAppointmentServiceName(AppointmentModel form1)
        {

            ServiceReference.Row3[] arr1 = client.get_RN();
            var RN = int.Parse(form1.Select1);

            ServiceReference.Row3 el = arr1.First(x => x.RN == RN);
            var Adr = el.ADRES;
            //form1.Address = Adr;

            ViewBag.Select_2 = Adr;
            form1.ServiceName = el.NAMER;
        }

        //public ActionResult SubmitAppointment(AppointmentModel form1)
        //{
        //    ProgramareAC.ServiceReference2.WSO2_package_017ACPortTypeClient client = GetClient();

        //    ViewBag.idnp = form1.IDNP;
        //    ViewBag.LastName = form1.LastName;
        //    ViewBag.FirstName = form1.FirstName;

        //    string[] words2 = form1.Times.Split('|');
        //    ViewBag.Times1 = words2[0];
        //    ViewBag.Times2 = words2[1];

        //    ProgramareAC.ServiceReference2.Row3[] arr1 = client.get_RN();
        //    var RN = int.Parse(form1.Select1);

        //    ProgramareAC.ServiceReference2.Row3 el = arr1.First(x => x.RN == RN);
        //    var Adr = el.ADRES;
        //    ViewBag.Select_2 = Adr;

        //    string[] words1 = form1.Service.Split('|');
        //    ViewBag.Service1 = words1[0];
        //    ViewBag.Service2 = words1[1];

        //    string[] dd = form1.Date.Split('-');
        //    string dat = dd[2] + "-" + dd[1] + "-" + dd[0];
        //    ViewBag.BirthDate = dat;





        //    ProgramareAC.ServiceReference2.Row2[] rez = client.set_Time(form1.IDNP, form1.LastName, form1.FirstName, dat, int.Parse(words2[0]), int.Parse(words1[0]), RN, form1.Email, form1.Phone, form1.AudienceSubject);
        //    if (rez[0].Error == 0) {
        //        ViewBag.responce = "Error : No result.";
        //        return View("Result");
        //    }
        //    else {
        //        ViewBag.responce = rez[0].Rezult_Text;
        //        return View("Error");
        //    }

        //}


        //private List<SelectListItem> GetServiceTypeOnPost(ServiceReference.Row4[] mSrv)
        //{
        //    List<SelectListItem> selectListItems = new List<SelectListItem>();
        //    foreach (ServiceReference.Row4 _srv in mSrv) {
        //        selectListItems.Add(new SelectListItem() {
        //            Text = _srv.SERV_NAME_RO,
        //            Value = _srv.SERV_ID + '|' + _srv.SERV_NAME_RO
        //        });
        //    }

        //    return selectListItems;
        //}

        [HttpGet]
        public ActionResult Index() // for testing
        {
            string msRequestId = "adb6aec97cdb4f1ba587b1e300f82016";

            ResponseResultPack model = new ResponseResultPack();
            model.MsRequestId = msRequestId;

            var value = _appointmentRepository.GetSignPackResult(msRequestId);

            AppointmentModel form1 = _appointmentRepository.GetAppointmentByMsignRequestId(msRequestId);

            return View("Result", model);
        }
    }
}