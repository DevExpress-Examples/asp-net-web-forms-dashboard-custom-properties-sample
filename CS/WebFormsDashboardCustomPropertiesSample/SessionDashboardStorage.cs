using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using System.Web;
using System.Web.SessionState;

public class SessionDashboardStorage : DashboardStorageBase {
    const string DashboardStorageKey = "DashboardStorage";

    private static SessionDashboardStorage instance = null;

    public static SessionDashboardStorage Instance {
        get {
            if(instance == null) {
                instance = new SessionDashboardStorage();
            }
            return instance;
        }
    }

    Dictionary<string, XDocument> Storage {
        get {
            HttpSessionState session = HttpContext.Current.Session;
            if(session != null) {
                Dictionary<string, XDocument> storage = session[DashboardStorageKey] as Dictionary<string, XDocument>;
                if(storage == null) {
                    storage = new Dictionary<string, XDocument>();
                    session[DashboardStorageKey] = storage;
                    return storage;
                }
                return storage;
            }
            throw new Exception();
        }
    }

    protected SessionDashboardStorage() : base() {

    }

    protected override IEnumerable<string> GetAvailableDashboardsID() {
        return Storage.Keys;
    }
    protected override XDocument LoadDashboard(string dashboardID) {
        XDocument document = Storage[dashboardID];
        HttpContext httpContext = HttpContext.Current;
        if(dashboardID == "ProductDetails" && httpContext != null) {
            Dashboard dashboard = new Dashboard();
            dashboard.LoadFromXDocument(document);
            string applicationPath = httpContext.Request.ApplicationPath.TrimEnd('/') + "/";
            BoundImageDashboardItem primaryImage = dashboard.Items["primaryImage"] as BoundImageDashboardItem;
            if(primaryImage != null)
                primaryImage.UriPattern = applicationPath + "Content/ProductDetailsImages/{0}.jpg";
            BoundImageDashboardItem secondaryImage = dashboard.Items["secondaryImage"] as BoundImageDashboardItem;
            if(secondaryImage != null)
                secondaryImage.UriPattern = applicationPath + "Content/ProductDetailsImages/{0} Secondary.jpg";
            document = dashboard.SaveToXDocument();
        }
        return document;
    }
    protected override void SaveDashboard(string dashboardID, XDocument dashboard, bool createNew) {
        if(createNew ^ Storage.ContainsKey(dashboardID))
            Storage[dashboardID] = dashboard;
    }
    public void RegisterDashboard(string dashboardID, XDocument dashboard) {
        SaveDashboard(dashboardID, dashboard, true);
    }
}