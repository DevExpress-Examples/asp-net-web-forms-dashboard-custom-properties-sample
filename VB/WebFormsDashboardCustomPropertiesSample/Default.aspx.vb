Imports WebFormsDashboardCustomPropertiesSample
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.DataAccess.Excel
Imports DevExpress.DataAccess.Sql
Imports System
Imports System.Web.Hosting

Namespace WebFormsDashboardCustomPropertiesSample
	Partial Public Class [Default]
		Inherits System.Web.UI.Page

		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
			'DashboardFileStorage dashboardFileStorage = new DashboardFileStorage("~/App_Data/Dashboards");
			'ASPxDashboard1.SetDashboardStorage(dashboardFileStorage);

			ASPxDashboard1.SetDashboardStorage(SessionDashboardStorage.Instance)

			AddHandler ASPxDashboard1.CustomExport, Sub(s, args)
				ChartConstantLinesExtension.CustomExport(args)
			End Sub
			' Uncomment this string to allow end users to create new data sources based on predefined connection strings.
			'ASPxDashboard1.SetConnectionStringsProvider(new DevExpress.DataAccess.Web.ConfigFileConnectionStringsProvider());

			Dim dataSourceStorage As New DataSourceInMemoryStorage()

			AddHandler ASPxDashboard1.ConfigureItemDataCalculation, Sub(s, args)
				args.CalculateAllTotals = True
			End Sub
			' Registers an SQL data source.
			Dim sqlDataSource As New DashboardSqlDataSource("SQL Data Source", "NWindConnectionString")
			Dim query As SelectQuery = SelectQueryFluentBuilder.AddTable("SalesPerson").SelectAllColumns().Build("Sales Person")
			sqlDataSource.Queries.Add(query)
			dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml())

			' Registers an Object data source.
			Dim objDataSource As New DashboardObjectDataSource("Object Data Source")
			dataSourceStorage.RegisterDataSource("objDataSource", objDataSource.SaveToXml())

			' Registers an Excel data source.
			Dim excelDataSource As New DashboardExcelDataSource("Excel Data Source")
			excelDataSource.FileName = HostingEnvironment.MapPath("~/App_Data/Sales.xlsx")
			excelDataSource.SourceOptions = New ExcelSourceOptions(New ExcelWorksheetSettings("Sheet1"))
			dataSourceStorage.RegisterDataSource("excelDataSource", excelDataSource.SaveToXml())

			ASPxDashboard1.SetDataSourceStorage(dataSourceStorage)
		End Sub

		Protected Sub DataLoading(ByVal sender As Object, ByVal e As DataLoadingWebEventArgs)
			If e.DataSourceName = "Object Data Source" Then
				e.Data = Invoices.CreateData()
			End If
		End Sub
	End Class
End Namespace