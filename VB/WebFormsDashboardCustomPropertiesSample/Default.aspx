<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Default.aspx.cs" Inherits="WebFormsDashboardCustomPropertiesSample.Default" %>
<%@ Register Assembly="DevExpress.Dashboard.v20.1.Web.WebForms, Version=20.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Content/Extensions/ChartAxisMaxValueExtension.js"></script>
    <script src="Content/Extensions/ChartConstantLinesExtension.js"></script>
    <script src="Content/Extensions/ChartLineOptionsExtension.js"></script>
    <script src="Content/Extensions/ChartScaleBreaksExtension.js"></script>
    <script src="Content/Extensions/ItemDescriptionExtension.js"></script>
    <script src="Content/Extensions/DashboardDescriptionExtension.js"></script>

    <script type="text/javascript">
        function onBeforeRender(sender) {
            var control = sender.GetDashboardControl();
            control.registerExtension(new DevExpress.Dashboard.DashboardPanelExtension(control));

            control.registerExtension(new ChartScaleBreaksExtension(control))
            control.registerExtension(new ChartLineOptionsExtension(control))
            control.registerExtension(new ChartAxisMaxValueExtension(control))
            control.registerExtension(new ChartConstantLinesExtension(control))
            control.registerExtension(new ItemDescriptionExtension(control))
            control.registerExtension(new DashboardDescriptionExtension(control))
        }
    </script>

    <dx:ASPxDashboard ID="ASPxDashboard1" runat="server" Width="100%" Height="100%" UseNeutralFilterMode="true" ondataloading="DataLoading">
        <ClientSideEvents BeforeRender="onBeforeRender" />
    </dx:ASPxDashboard>
</asp:Content>