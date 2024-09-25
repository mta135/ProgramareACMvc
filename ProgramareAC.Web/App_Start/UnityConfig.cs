using ProgramareAC.DataAccess;
using ProgramareAC.Models.Repositories.Abstract;
using ProgramareAC.Models.Repositories.Real;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ProgramareAC.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<IMSignRepository, MSingRepository>();
            container.RegisterType<IAppointmentRepository, AppointmentRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}