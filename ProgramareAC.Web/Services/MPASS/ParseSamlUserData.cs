using System;
using System.Web;
using ProgramareAC.Models;
using static ProgramareAC.Services.MPASS.SamlHelper;

namespace ProgramareAC.Services.MPASS
{
    public static class ParseSamlUserData
    {
        public static T ParseTo<T>(T input)
        {
            HttpSessionStateBase session = new HttpSessionStateWrapper(HttpContext.Current.Session);

            SAMLUserData samlUserData = session.GetSessionUser();

            if (samlUserData != null)
            {
                if (typeof(T) == typeof(AppointmentModel))
                {
                    return (T)(object)ParseToAppointmentModel(samlUserData);
                }
            }

            return input;
        }


        private static AppointmentModel ParseToAppointmentModel(SAMLUserData samlUserData)
        {
            AppointmentModel appointmentModel = new AppointmentModel();

            appointmentModel.IDNP = samlUserData.IDNP;
            appointmentModel.FirstName = samlUserData.FirstName;
            appointmentModel.LastName = samlUserData.LastName;

            if (samlUserData.Birthday != null)
                appointmentModel.Date = samlUserData.Birthday?.ToString("dd-MM-yyyy"); //Convert.ToString(samlUserData.Birthday);

            return appointmentModel;
        }
    }
}