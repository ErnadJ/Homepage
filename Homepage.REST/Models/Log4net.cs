//---------------------------------------------------------------------------------------
// <copyright file="Log4net.cs" company="PP2000">
//     Copyright (c) 2017 All Rights reserved
// </copyright>
//---------------------------------------------------------------------------------------
namespace Homepage.REST
{
    using log4net;
    using System;

    public class Log4net
    {
        public static ILog Logger;

        public static void InitializeLogger()
        {
            GlobalContext.Properties["LogName"] = string.Format("{0:yyyyMMdd}.log", DateTime.Now);

            Logger = LogManager.GetLogger("PP2000");
        }
    }
}
