using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace ProgramareAC.Models.LogHelper
{
    public class WriteLog
    {
        public static ILog Common { get; private set; }


        static WriteLog()
        {

        }

        public static void InitLoggers()
        {
            #region log4net Static
            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());

            var fileInfo = new FileInfo(@"log4net.config");

            XmlConfigurator.Configure(repository, fileInfo);
            #endregion 

            Common = LogManager.GetLogger("CommonAppender");
        }
    }
}

