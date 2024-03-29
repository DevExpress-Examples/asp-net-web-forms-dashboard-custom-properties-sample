Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Web.Hosting
Imports System.Xml.Linq

Namespace WebFormsDashboardCustomPropertiesSample
	Public Class Global_asax
		Inherits System.Web.HttpApplication
		Private Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
			System.Web.Routing.RouteTable.Routes.MapPageRoute("defaultRoute", "", "~/Default.aspx")
			AddHandler DevExpress.Web.ASPxWebControl.CallbackError, AddressOf Application_Error
		End Sub

		Private Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
			' Code that runs on application shutdown
		End Sub

		Private Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
			' Code that runs when an unhandled error occurs
		End Sub

		Private Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
			' Code that runs when a new session is started
			For Each file As String In Directory.EnumerateFiles(HostingEnvironment.MapPath("~/App_Data/Dashboards"), "*.xml")
				SessionDashboardStorage.Instance.RegisterDashboard(Path.GetFileName(file), XDocument.Load(file))
			Next file
		End Sub

		Private Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
			' Code that runs when a session ends. 
			' Note: The Session_End event is raised only when the sessionstate mode
			' is set to InProc in the Web.config file. If session mode is set to StateServer 
			' or SQLServer, the event is not raised.
		End Sub
	End Class
End Namespace