using SSS.Property.Setups.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSYSTEM.Controllers
{
    public class ReportingController : BaseController
    {
        // GET: Reports
        public ActionResult ViewReports()
        {
            if (Session["LOGGEDIN"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public JsonResult AddUpdate(LP_Report_Property objreport)
        {
            try
            {
                if (objreport.ReportID == 1)
                {
                    objreport.ReportName = "Purchase_Report";
                }

                // Added By Ahsan
                if (objreport.ReportID == 2)
                {
                    objreport.ReportName = "ImportedExpense_Report";
                }

                return SelectReportData(objreport);
            }
            catch (Exception ex)
            {
                using (StreamWriter tr = new StreamWriter(Server.MapPath("/Reports/Error.txt"), true))
                {
                    tr.WriteLine("Exception at level Reporting Controller ADDUpdate Start" + DateTime.Now);
                    tr.WriteLine("Exception at level Reporting Controller ADDUpdate " + ex.InnerException + DateTime.Now);
                  
                    tr.WriteLine("Exception at level Reporting Controller ADDUpdate " + ex.Message + DateTime.Now);
                    tr.WriteLine("Exception at level Reporting Controller ADDUpdate End" + DateTime.Now);
                    return Json(new { data = "/Reports/MRNReport.Pdf", success = false, msg = "Failed", statuscode = 400 }, JsonRequestBehavior.AllowGet);

                }
            }
        }
    }
}