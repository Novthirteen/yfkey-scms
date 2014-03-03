using System;
using System.Web;
using Castle.Core.Resource;
using Castle.Facilities.QuartzIntegration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace com.Sconit.Web
{     
    /// <summary>
    /// Summary description for Global
    /// </summary>
    public class Global : HttpApplication, IContainerAccessor
    {
        private static WindsorContainer container = null;
        private static log4net.ILog log = log4net.LogManager.GetLogger("Application");

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            // init log4net
            log4net.Config.XmlConfigurator.Configure();

            container = new WindsorContainer(new XmlInterpreter(new ConfigResource()));
        }

        void Application_End(object sender, EventArgs e)
        {
            container.Dispose();

            //  在应用程序关闭时运行的代码
        }

        void Application_Error(object sender, EventArgs e)
        {
            //todo 未捕获异常处理,页面统一做出错处理
            Exception errInfo = Server.GetLastError();
            bool bSend = false;
            string msg = errInfo.Source + ":" + errInfo.Message;

            string msgBody = "URL:" + Request.RawUrl + "\n";
            msgBody += errInfo.Message + "\n" + errInfo.Message + "\n";
            msgBody += "Error StackTrack:" + errInfo.StackTrace + "\n";
            msgBody += "Time:" + DateTime.Now.ToString() + "\n";

            Response.Write(msgBody);
            //Server.ClearError();
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            //todo 验证用户是否登录或Session失效
            //重新登陆后返回原页面
        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在会话开始时运行的代码。
        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。
            //string sessionID = Session.SessionID;
            //CasClient.Utils.CommonUtils.RemoveState(sessionID);
        }

        #region IContainerAccessor implementation

        public IWindsorContainer Container
        {
            get { return container; }
        }

        #endregion IContainerAccessor implementation


    }
}
