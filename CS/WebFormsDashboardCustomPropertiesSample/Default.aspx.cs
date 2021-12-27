using WebFormsDashboardCustomPropertiesSample;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Excel;
using DevExpress.DataAccess.Sql;
using System;
using System.Web.Hosting;

namespace WebFormsDashboardCustomPropertiesSample {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //DashboardFileStorage dashboardFileStorage = new DashboardFileStorage("~/App_Data/Dashboards");
            //ASPxDashboard1.SetDashboardStorage(dashboardFileStorage);

            ASPxDashboard1.SetDashboardStorage(SessionDashboardStorage.Instance);

            ASPxDashboard1.CustomExport += (s, args) => {
                ChartConstantLinesExtension.CustomExport(args);
            };
            // Uncomment this string to allow end users to create new data sources based on predefined connection strings.
            //ASPxDashboard1.SetConnectionStringsProvider(new DevExpress.DataAccess.Web.ConfigFileConnectionStringsProvider());

            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();

            ASPxDashboard1.ConfigureItemDataCalculation += (s, args) => {
                args.CalculateAllTotals = true;
            };
            // Registers an SQL data source.
            DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "NWindConnectionString");
            SelectQuery query = SelectQueryFluentBuilder
                .AddTable("SalesPerson")
                .SelectAllColumns()
                .Build("Sales Person");
            sqlDataSource.Queries.Add(query);
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());

            ASPxDashboard1.SetDataSourceStorage(dataSourceStorage);
        }
    }
}