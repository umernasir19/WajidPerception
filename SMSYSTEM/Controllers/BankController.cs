using Newtonsoft.Json;
using SSS.BLL.Setups;
using SSS.Property.Setups;
using SSS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSYSTEM.Controllers
{
    public class BankController : Controller
    {
        // GET: Bank
        Bank_BLL objBanlBLL;
        Bank_Property objbankproperty;
        public ActionResult ViewBanks()
        {
            if(Session["LOGGEDIN"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }

        public JsonResult GetAllBanks()
        {

            if (Session["LOGGEDIN"] != null)
            {
                try
                {


                    objBanlBLL = new Bank_BLL();
                    var Data = JsonConvert.SerializeObject(objBanlBLL.ViewAll());
                    return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { data = "Session Expired", success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        

        public ActionResult AddNewBank(int? id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                objbankproperty = new Bank_Property();
                try
                {
                    if (id > 0)
                    {
                        objbankproperty.idx =Convert.ToInt32(id);
                        objBanlBLL = new Bank_BLL(objbankproperty);
                        objBanlBLL.SelectOne();
                    }
                    else
                    {
                       
                    }
                }
                catch(Exception ex)
                {

                }
                return PartialView("_AddNewBank", objbankproperty);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public JsonResult AddUpdate(Bank_Property objbank)
        {

            if (Session["LOGGEDIN"] != null)
            {
                try
                {
                    objbankproperty = new Bank_Property();
                    objbankproperty= objbank;
                    objbankproperty.visible = 1;
                    objbankproperty.createdByUserIdx =Convert.ToInt32(Session["UID"].ToString());
                    objbankproperty.creationDate = DateTime.Now;

                    objBanlBLL = new Bank_BLL(objbankproperty);
                    //check existing name
                    if (objBanlBLL.CheckBank() > 0)
                    {
                        return Json(new { data = objbankproperty, success = true, msg="Bank Name Already Exist",statuscode = 401, count =0 }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        bool flag;
                        if (objbankproperty.idx > 0)
                        {
                            //update
                           flag= objBanlBLL.Update();
                            return Json(new { data = objbankproperty, success = flag, msg = "Success", statuscode = 200, count = 0 }, JsonRequestBehavior.AllowGet);


                        }
                        else
                        {
                            //add
                            flag= objBanlBLL.Insert();
                            return Json(new { data = objbankproperty, success = flag, msg = "Success", statuscode = 200, count = 0 }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    

                }
                catch (Exception ex)
                {
                    return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { data = "Session Expired", success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }

        }


        #region Comapny Bank
        CompanyBank_BLL objcompanybankbll;
        CompanyBank_Property objcompanybankproperty;


        public ActionResult ViewCompanyBanks()
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

        public JsonResult GetAllCompanyBanks()
        {

            if (Session["LOGGEDIN"] != null)
            {
                try
                {


                    objcompanybankbll = new CompanyBank_BLL();
                    var Data = JsonConvert.SerializeObject(objcompanybankbll.ViewAll());
                    return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { data = "Session Expired", success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult AddCompanyNewBank(int? id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                objcompanybankproperty = new CompanyBank_Property();
                objBanlBLL = new Bank_BLL();
                
                objcompanybankproperty.BankList =Helper.ConvertDataTable<Bank_Property>(objBanlBLL.ViewAll());
                try
                {
                    if (id > 0)
                    {
                        objcompanybankproperty.idx = Convert.ToInt32(id);
                        objcompanybankbll = new CompanyBank_BLL(objcompanybankproperty);
                        objcompanybankbll.SelectOne();
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }
                return PartialView("_AddCompanyNewBank", objcompanybankproperty);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public JsonResult AddComapnyUpdate(CompanyBank_Property objcomapnybank)
        {

            if (Session["LOGGEDIN"] != null)
            {
                try
                {
                    objcompanybankproperty = new CompanyBank_Property();
                    objcompanybankproperty = objcomapnybank;
                    objcompanybankproperty.visible = 1;
                    objcompanybankproperty.createdByUserIdx = Convert.ToInt32(Session["UID"].ToString());
                    objcompanybankproperty.creationDate = DateTime.Now;

                    objcompanybankbll = new CompanyBank_BLL(objcompanybankproperty);
                    ////check existing name
                    if (0 > 0)
                    {
                        return Json(new { data = objbankproperty, success = true, msg = "Bank Name Already Exist", statuscode = 401, count = 0 }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        bool flag;
                        if (objcomapnybank.idx > 0)
                        {
                            //update
                            flag = objcompanybankbll.Update();
                            return Json(new { data = objcomapnybank, success = flag, msg = "Success", statuscode = 200, count = 0 }, JsonRequestBehavior.AllowGet);


                        }
                        else
                        {
                            //add
                            flag = objcompanybankbll.Insert();
                            return Json(new { data = objcomapnybank, success = flag, msg = "Success", statuscode = 200, count = 0 }, JsonRequestBehavior.AllowGet);

                        }
                    }


                }
                catch (Exception ex)
                {
                    return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { data = "Session Expired", success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
    }
}