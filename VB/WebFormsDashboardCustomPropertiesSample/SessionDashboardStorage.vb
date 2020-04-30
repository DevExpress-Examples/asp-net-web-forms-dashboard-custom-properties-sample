Imports System
Imports System.Collections.Generic
Imports System.Xml.Linq
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports System.Web
Imports System.Web.SessionState

Public Class SessionDashboardStorage
	Inherits DashboardStorageBase

	Private Const DashboardStorageKey As String = "DashboardStorage"

'INSTANT VB NOTE: The field instance was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private Shared instance_Conflict As SessionDashboardStorage = Nothing

	Public Shared ReadOnly Property Instance() As SessionDashboardStorage
		Get
			If instance_Conflict Is Nothing Then
				instance_Conflict = New SessionDashboardStorage()
			End If
			Return instance_Conflict
		End Get
	End Property

	Private ReadOnly Property Storage() As Dictionary(Of String, XDocument)
		Get
			Dim session As HttpSessionState = HttpContext.Current.Session
			If session IsNot Nothing Then
'INSTANT VB NOTE: The local variable storage was renamed since Visual Basic will not allow local variables with the same name as their enclosing function or property:
				Dim storage_Conflict As Dictionary(Of String, XDocument) = TryCast(session(DashboardStorageKey), Dictionary(Of String, XDocument))
				If storage_Conflict Is Nothing Then
					storage_Conflict = New Dictionary(Of String, XDocument)()
					session(DashboardStorageKey) = storage_Conflict
					Return storage_Conflict
				End If
				Return storage_Conflict
			End If
			Throw New Exception()
		End Get
	End Property

	Protected Sub New()
		MyBase.New()

	End Sub

	Protected Overrides Function GetAvailableDashboardsID() As IEnumerable(Of String)
		Return Storage.Keys
	End Function
	Protected Overrides Function LoadDashboard(ByVal dashboardID As String) As XDocument
		Dim document As XDocument = Storage(dashboardID)
		Dim httpContext As HttpContext = System.Web.HttpContext.Current
		If dashboardID = "ProductDetails" AndAlso httpContext IsNot Nothing Then
			Dim dashboard As New Dashboard()
			dashboard.LoadFromXDocument(document)
			Dim applicationPath As String = httpContext.Request.ApplicationPath.TrimEnd("/"c) & "/"
			Dim primaryImage As BoundImageDashboardItem = TryCast(dashboard.Items("primaryImage"), BoundImageDashboardItem)
			If primaryImage IsNot Nothing Then
				primaryImage.UriPattern = applicationPath & "Content/ProductDetailsImages/{0}.jpg"
			End If
			Dim secondaryImage As BoundImageDashboardItem = TryCast(dashboard.Items("secondaryImage"), BoundImageDashboardItem)
			If secondaryImage IsNot Nothing Then
				secondaryImage.UriPattern = applicationPath & "Content/ProductDetailsImages/{0} Secondary.jpg"
			End If
			document = dashboard.SaveToXDocument()
		End If
		Return document
	End Function
	Protected Overrides Sub SaveDashboard(ByVal dashboardID As String, ByVal dashboard As XDocument, ByVal createNew As Boolean)
		If createNew Xor Storage.ContainsKey(dashboardID) Then
			Storage(dashboardID) = dashboard
		End If
	End Sub
	Public Sub RegisterDashboard(ByVal dashboardID As String, ByVal dashboard As XDocument)
		SaveDashboard(dashboardID, dashboard, True)
	End Sub
End Class