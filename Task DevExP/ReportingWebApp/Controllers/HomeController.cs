using System.Collections.Generic;
using DevExpress.DataAccess.Sql;
using Microsoft.AspNetCore.Mvc;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.ReportDesigner;
using DevExpress.AspNetCore.Reporting.QueryBuilder;
using DevExpress.AspNetCore.Reporting.ReportDesigner;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.XtraReports.Web.WebDocumentViewer;


namespace ReportingWebApp.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Error() {
            Models.ErrorModel model = new Models.ErrorModel();
            return View(model);
        }

        public IActionResult Designer(
            [FromServices] IReportDesignerClientSideModelGenerator clientSideModelGenerator,
            [FromQuery] string reportName) {
            Models.ReportDesignerCustomModel model = new Models.ReportDesignerCustomModel();
            model.ReportDesignerModel = CreateDefaultReportDesignerModel(clientSideModelGenerator, reportName, null);
            return View(model);
        }

         public static Dictionary<string, object> GetAvailableDataSources() {
            var dataSources = new Dictionary<string, object>();
            // Create a SQL data source with the specified connection string.
            SqlDataSource ds = new SqlDataSource("NWindConnectionString");
            // Create a SQL query to access the Products data table.
            SelectQuery query = SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products");
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            dataSources.Add("Northwind", ds);
            return dataSources;
        }

        public static ReportDesignerModel CreateDefaultReportDesignerModel(IReportDesignerClientSideModelGenerator clientSideModelGenerator, string reportName, XtraReport report) {
            reportName = string.IsNullOrEmpty(reportName) ? "TestReport" : reportName;
            var dataSources = GetAvailableDataSources();
            if(report != null) {
                return clientSideModelGenerator.GetModel(report, dataSources, ReportDesignerController.DefaultUri, WebDocumentViewerController.DefaultUri, QueryBuilderController.DefaultUri);
            }
            return clientSideModelGenerator.GetModel(reportName, dataSources, ReportDesignerController.DefaultUri, WebDocumentViewerController.DefaultUri, QueryBuilderController.DefaultUri);
        }
        public IActionResult Viewer(
            [FromServices] IWebDocumentViewerClientSideModelGenerator clientSideModelGenerator,
            [FromQuery] string reportName) {

            var reportToOpen = string.IsNullOrEmpty(reportName) ? "newReport" : reportName;
            var model = new Models.ViewerModel {
                ViewerModelToBind = clientSideModelGenerator.GetModel(reportToOpen, WebDocumentViewerController.DefaultUri)
            };
            return View(model);
        }
    }
}
