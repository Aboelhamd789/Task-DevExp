using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
namespace ReportingWebApp.PredefinedReports
{
     public static class ReportsFactory
{
    public static string ReportsPath { get; set; }
    public static Dictionary<string, Func<XtraReport>> Reports = new Dictionary<string, Func<XtraReport>>()
    {
        ["TestReport"] = () => new TestReport(),
        ["newReport"] = () => XtraReport.FromFile(Path.Combine(ReportsPath + "/PredefinedReports/newReport.repx"))
    };

}
}
