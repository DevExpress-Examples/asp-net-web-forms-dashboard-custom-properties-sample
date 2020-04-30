using System;
using System.IO;
using System.Web.Hosting;
using System.Xml.Linq;

namespace WebFormsDashboardCustomPropertiesSample {
    public class Global_asax : System.Web.HttpApplication {
        void Application_Start(object sender, EventArgs e) {
            System.Web.Routing.RouteTable.Routes.MapPageRoute("defaultRoute", "", "~/Default.aspx");
            DevExpress.Web.ASPxWebControl.CallbackError += new EventHandler(Application_Error);
        }

        void Application_End(object sender, EventArgs e) {
            // Code that runs on application shutdown
        }
    
        void Application_Error(object sender, EventArgs e) {
            // Code that runs when an unhandled error occurs
        }
    
        void Session_Start(object sender, EventArgs e) {
            // Code that runs when a new session is started
            foreach(string file in Directory.EnumerateFiles(HostingEnvironment.MapPath("~/App_Data/Dashboards"), "*.xml")) {
                SessionDashboardStorage.Instance.RegisterDashboard(Path.GetFileName(file), XDocument.Load(file));
            }
        }
    
        void Session_End(object sender, EventArgs e) {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}