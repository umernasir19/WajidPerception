using SSS.BLL;
using SSS.Property.Setups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSYSTEM.Controllers
{
    public class AccountController : Controller
    {
        User_Property objuserproperty;
        User_BLL objUserBll;
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(User_Property objuser)
        {
            try
            {
                
                objUserBll = new User_BLL(objuser);
                var data = objUserBll.SelectByIDPassword();
                if (data.Rows.Count > 0)
                {
                    for (int i = 0; i <data.Rows.Count;i++)
                    {
                        Session["UID"] = data.Rows[0]["idx"].ToString();
                        Session["LOGINID"] = data.Rows[0]["loginId"].ToString();
                        Session["COMPANYID"] = data.Rows[0]["companyIdx"].ToString();
                        Session["BRANCHID"] = data.Rows[0]["branchIdx"].ToString();
                        Session["LOGGEDIN"] = true;
                        Session["ISADMIN"] = data.Rows[0]["Is_Admin"].ToString(); 
                    }
                    return Json(new { data = "",msg="Login Successfull", success = true, statuscode = 200, count = data.Rows.Count }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { data = "", msg = "Login Failed", success = true, statuscode = 200, count = 0 }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);

            }
        }
        
        public ActionResult LogOff()
        {

            Session.Clear();
            Session.Abandon();
            // Redirecting to Login page after deleting Session
            return RedirectToAction("Login", "Account");
        }

    }
}