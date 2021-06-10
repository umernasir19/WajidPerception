using Newtonsoft.Json;
using SSS.BLL.Setups;
using SSS.BLL.Transactions;
using SSS.Property.Setups;
using SSS.Property.Transactions;
using SSS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSYSTEM.Controllers
{
    public class DisplayOrderController : Controller
    {
        // GET: DisplayOrder
        // GET: DisplayOrder
        DisplayOrderVM objDisplayOrderVM;
        LP_DisplayOrder_Master_Property objDOProperty;
        LP_DisplayOrder_BLL objDOBll;
        public ActionResult ViewDisplayOrder()
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
        public JsonResult GetAllDisplayOrder()
        {
            try
            {
                objDisplayOrderVM = new DisplayOrderVM();
                objDOBll = new LP_DisplayOrder_BLL(objDOProperty);
                var Data = JsonConvert.SerializeObject(objDOBll.SelectAll());
                return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddNewDisplayOrder(int? id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                objDisplayOrderVM = new DisplayOrderVM();

              
                Product_Property product = new Product_Property();
              
                Product_BLL objProductbll = new Product_BLL();
                
                objDisplayOrderVM.ProductList = Helper.ConvertDataTable<Product_Property>(objProductbll.GetByTypeId(1));//only local products
                Taxes_Property obj = new Taxes_Property();
                if (id > 0)
                {
                    LP_DisplayOrder_Details_Property objDODetails;
                    objDOProperty = new LP_DisplayOrder_Master_Property();
                    objDOProperty.idx = Convert.ToInt16(id);

                    objDOBll = new LP_DisplayOrder_BLL(objDOProperty);
                    DataTable dt = objDOBll.SelectOne();
                    //objDisplayOrderVM.idx = Convert.ToInt16(dt.Rows[0]["doIdx"].ToString());
                    objDisplayOrderVM.idx = Convert.ToInt16(dt.Rows[0]["idx"].ToString());

                    objDisplayOrderVM.doNumber = dt.Rows[0]["doNumber"].ToString();
                    objDisplayOrderVM.description = dt.Rows[0]["description"].ToString();

                    // We Don't have Total Amount in view
                    //objDisplayOrderVM.totalAmount = Convert.ToDecimal(dt.Rows[0]["totalAmount"].ToString());

                    string pdate = (dt.Rows[0]["orderDate"].ToString()).ToString();
                    string ndate = DateTime.Parse(pdate).ToString("yyyy-MM-dd");
                    objDisplayOrderVM.orderDate = Convert.ToDateTime(ndate);// DateTime.Parse(dt.Rows[0]["mrnDate"].ToString()).ToString("yyyy-MM-dd");
                    //DateTime.Parse(dt.Rows[0]["mrnDate"].ToString()).ToString("yyyy-MM-dd");
                    //foreach(DataRow dr in dt.Rows)
                    //{
                    //    objmrndetail

                    //}
                    ViewBag.DetailData = Helper.ConvertDataTable<DisplayOrderVM>(dt);
                    //update
                    return View("AddNewDisplayOrder", objDisplayOrderVM);//objDisplayOrderVM
                }
                else
                {
                    objDisplayOrderVM.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    objDOBll = new LP_DisplayOrder_BLL();
                    LP_GenerateTransNumber_Property objtrans = new LP_GenerateTransNumber_Property();
                    objtrans.TableName = "displayOrder";
                    objtrans.Identityfieldname = "idx";
                    objtrans.userid = Session["UID"].ToString();
                    objDisplayOrderVM.doNumber = objDOBll.GeneratePO(objtrans);

                    return View("AddNewDisplayOrder", objDisplayOrderVM);//, objDisplayOrderVM

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public JsonResult AddUpdate(DisplayOrderVM objDisplayOrder)
        {
            try
            {
                bool flag = false;

                objDOProperty = new LP_DisplayOrder_Master_Property();
                objDOProperty.doNumber = objDisplayOrder.doNumber;
               

                objDOProperty.orderDate = objDisplayOrder.orderDate;
                objDOProperty.description = objDisplayOrder.description;
               
               
                objDOProperty.reference = objDisplayOrder.reference;
                

                //  objPurchaseProperty.paidDate = ;// objpurchase.paidDate;

                objDOProperty.DetailData = Helper.ToDataTable<LP_DisplayOrder_Details_Property>(objDisplayOrder.DisplayOrderDetailLST);
               
                if (objDisplayOrder.idx > 0)
                {
                    objDOProperty.idx = objDisplayOrder.idx;
                    objDOProperty.creationDate = DateTime.Now;
                    objDOProperty.visible = 1;
                    objDOProperty.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    objDOProperty.visible = 1;
                    objDOProperty.status = "0";
                    objDOProperty.TableName = "displayOrderDetails";
                    objDOBll = new LP_DisplayOrder_BLL(objDOProperty);
                    flag = objDOBll.Insert();
                    //update
                }
                else
                {
                    //add
                    objDOProperty.creationDate = DateTime.Now;
                    objDOProperty.visible = 1;
                    objDOProperty.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    objDOProperty.visible = 1;
                    objDOProperty.status = "0";
                    objDOProperty.TableName = "displayOrderDetails";
                    objDOBll = new LP_DisplayOrder_BLL(objDOProperty);
                    flag = objDOBll.Insert();

                }
                return Json(new { data = "", success = flag, msg = flag == true ? "Successfull" : "Failed", statuscode = flag == true ? 200 : 401 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        // Delete
        public JsonResult Delete(int? id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                try
                {
                    objDOProperty = new LP_DisplayOrder_Master_Property();
                    objDOProperty.idx = int.Parse(id.ToString());

                    LP_DisplayOrder_BLL objBll = new LP_DisplayOrder_BLL(objDOProperty);
                    var flag1 = objBll.Delete();
                    if (flag1)
                    {
                        return Json(new { data = "Deleted", success = flag1, statuscode = 200 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { data = "Error", success = flag1, statuscode = 200 }, JsonRequestBehavior.DenyGet);
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
    }
}