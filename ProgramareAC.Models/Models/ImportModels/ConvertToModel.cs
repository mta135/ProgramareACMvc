namespace ProgramareAC.Models.ImportModels
{
    public class ConvertToModel
    {

        //public Appointment ConvertTo(AppointmentModel appointmentModel)
        //{
        //    Appointment dbModel = new Appointment();

        //    dbModel.Name = appointmentModel.FirstName;

        //    dbModel.Surname = appointmentModel.LastName;

        //    dbModel.IDNP = appointmentModel.IDNP;

        //    dbModel.BirthDate = appointmentModel.Date;

        //    dbModel.Email = appointmentModel.Email;

        //    dbModel.Phone = appointmentModel.Phone;

        //    dbModel.AudienceSubject = appointmentModel.AudienceSubject;

        //    dbModel.ServiceId = appointmentModel.Select1;

        //    dbModel.ServiceName = appointmentModel.ServiceName;

        //    dbModel.ServiceTypeId = appointmentModel.Service.Split('|')[0];

        //    dbModel.ServiceTypeName = appointmentModel.Service.Split('|')[1];

        //    string[] words2 = appointmentModel.Times.Split('|');

        //    dbModel.RequestNumber = Convert.ToInt32(words2[0]);

        //    return dbModel;
        //}


        private SignContentFields ConvertToSignContentFields(AppointmentModel appointment)
        {
            SignContentFields signing = new SignContentFields();

            signing.ServiceId = appointment.Select1;

            signing.ServiceTypeId = appointment.Service.Split('|')[0];

            signing.ServiceTypeName = appointment.Service.Split('|')[1];

            signing.BirthDate = appointment.Date;

            signing.IDNP = appointment.IDNP;

            signing.Name = appointment.FirstName;

            signing.Surname = appointment.LastName;

            signing.Email = appointment.Email;

            signing.Phone = appointment.Phone;

            return signing;
        }

    }
}