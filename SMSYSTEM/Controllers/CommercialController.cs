using Newtonsoft.Json;
using SSS.BLL.Setups;
using SSS.BLL.Transactions;
using SSS.Property.Setups;
using SSS.Property.Transactions;
using SSS.Property.Transactions.ViewModels;
using SSS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSYSTEM.Controllers
{
    public class CommercialController : BaseController
    {
        LP_PerformaInvoice_ViewModel objPurchaseVM_Property;
        LP_Performa_Invoice_Property objPurchaseProperty;
        LP_Consigment_ViewModel objConsigmentVm;

        LP_Consigment_Property _objconsigment;

        LP_CI_ViewModel objCIPOVm;
        LP_CI_PurchaseOrder_Property _objCIMaster;
         LP_CI_PurchaseDetails_Property _objCIDetail;
        LP_PI_BLL objpurchaseBll;


        #region Performa Invoice Work
        // GET: Commercial
        public ActionResult ViewCI()
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
        public JsonResult GetAllPurchase()
        {
            try
            {
                objPurchaseVM_Property = new LP_PerformaInvoice_ViewModel();
                objpurchaseBll = new LP_PI_BLL();
                var Data = JsonConvert.SerializeObject(objpurchaseBll.SelectAll());
                return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult AddNewCI(int? id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                objPurchaseVM_Property = new LP_PerformaInvoice_ViewModel();
                Vendors_Property vendor = new Vendors_Property();
                Product_Property product = new Product_Property();
                Vendors_BLL objvendorbll = new Vendors_BLL();
                Product_BLL objProductbll = new Product_BLL();
                LP_MRN_BLL objMRNbll = new LP_MRN_BLL();
               
                objPurchaseVM_Property.VendorLST = Helper.ConvertDataTable<Vendors_Property>(objvendorbll.ViewAll());
                objPurchaseVM_Property.DepartmentList = Helper.ConvertDataTable<Departments_property>(GetAllDepartments());
                objPurchaseVM_Property.PurchaseType_List = Helper.ConvertDataTable<LP_Purchase_Type>(GetAllPurchaseType());
                objPurchaseVM_Property.ProductList = Helper.ConvertDataTable<Product_Property>(objProductbll.ViewAll());
                objPurchaseVM_Property.purchaseDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                //objPurchaseVM_Property.poNumber = "Po-001";
                if (id > 0)
                {
                    LP_Purchase_Detail_Property objmpurchasedetail;
                    objPurchaseProperty = new LP_Performa_Invoice_Property();
                    objPurchaseProperty.idx = Convert.ToInt16(id);

                    objpurchaseBll = new LP_PI_BLL();
                    DataTable dt = objpurchaseBll.SelectOne();
                    objPurchaseVM_Property.idx = Convert.ToInt16(dt.Rows[0]["purchaseIdx"].ToString());
                    objPurchaseVM_Property.vendorIdx = Convert.ToInt16(dt.Rows[0]["vendorIdx"].ToString());
                    objPurchaseVM_Property.poNumber = dt.Rows[0]["poNumber"].ToString();
                    objPurchaseVM_Property.description = dt.Rows[0]["description"].ToString();
                   
                    objPurchaseVM_Property.DepartmentID = Convert.ToInt16(dt.Rows[0]["DepartmentID"].ToString());
                    objPurchaseVM_Property.totalAmount = Convert.ToDecimal(dt.Rows[0]["totalAmount"].ToString());
                    string pdate = (dt.Rows[0]["purchaseDate"].ToString()).ToString();
                    string ndate = DateTime.Parse(pdate).ToString("yyyy-MM-dd");
                    objPurchaseVM_Property.purchaseDate = Convert.ToDateTime(ndate);// DateTime.Parse(dt.Rows[0]["mrnDate"].ToString()).ToString("yyyy-MM-dd");
                    //DateTime.Parse(dt.Rows[0]["mrnDate"].ToString()).ToString("yyyy-MM-dd");
                    //foreach(DataRow dr in dt.Rows)
                    //{
                    //    objmrndetail

                    //}
                    ViewBag.DetailData = Helper.ConvertDataTable<PurchaseVM_Property>(dt);
                    //update
                    return View("AddNewCI", objPurchaseVM_Property);
                }
                else
                {
                    //objPurchaseProperty = new LP_Purchase_Master_Property();
                    objPurchaseVM_Property.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    objpurchaseBll = new LP_PI_BLL();
                    LP_GenerateTransNumber_Property objtrans = new LP_GenerateTransNumber_Property();
                    objtrans.TableName = "CommercialInvoice";
                    objtrans.Identityfieldname = "idx";
                    objtrans.userid = Session["UID"].ToString();
                    objPurchaseVM_Property.poNumber = objpurchaseBll.GeneratePINo(objtrans);
                    //objPurchaseProperty.poNumber = "";
                    //objpurchaseBll = new LP_Purchase_BLL(objPurchaseProperty);
                    //objPurchaseVM_Property.poNumber = objpurchaseBll.GeneratePO();// "PO-001";
                    // string po = objpurchaseBll.GeneratePO();

                    return View("AddNewCI", objPurchaseVM_Property);

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpPost]
        public JsonResult AddUpdate(LP_PerformaInvoice_ViewModel objpurchase)
        {
            try
            {
                bool flag = false;
                
                
                objPurchaseProperty = new LP_Performa_Invoice_Property();
                objPurchaseProperty.poNumber = objpurchase.poNumber;
                objPurchaseProperty.vendorIdx = objpurchase.vendorIdx;
                objPurchaseProperty.purchaseTypeIdx = objpurchase.purchaseTypeIdx;
                objPurchaseProperty.purchaseDate = objpurchase.purchaseDate;
                objPurchaseProperty.description = objpurchase.description;
                objPurchaseProperty.totalAmount = objpurchase.totalAmount;
                objPurchaseProperty.netAmount = objpurchase.netAmount;
                objPurchaseProperty.paidAmount = objpurchase.paidAmount;
                objPurchaseProperty.balanceAmount = objpurchase.balanceAmount;
                objPurchaseProperty.DocumentNumber = objpurchase.DocumentNumber;
                objPurchaseProperty.ContainerNo = objpurchase.ContainerNo;
                objPurchaseProperty.ExchangeRate = objpurchase.ExchangeRate;
                objPurchaseProperty.DepartmentID = objpurchase.DepartmentID;
                //  objPurchaseProperty.paidDate = ;// objpurchase.paidDate;

                objPurchaseProperty.DetailData = Helper.ToDataTable<LP_Performa_Invoice_Details_Property>(objpurchase.CommercialDetailList);
                if (objpurchase.idx > 0)
                {
                    objPurchaseProperty.idx = objpurchase.idx;
                    objPurchaseProperty.creationDate = DateTime.Now;
                    objPurchaseProperty.visible = 1;
                    objPurchaseProperty.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    objPurchaseProperty.visible = 1;
                    objPurchaseProperty.status = "0";
                    objPurchaseProperty.TableName = "CommercialDetails";
                    objpurchaseBll = new LP_PI_BLL();
                    flag = objpurchaseBll.Insert();
                    //update
                }
                else
                {
                    //add
                    objPurchaseProperty.creationDate = DateTime.Now;
                    objPurchaseProperty.visible = 1;
                    objPurchaseProperty.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    objPurchaseProperty.visible = 1;
                    objPurchaseProperty.status = "0";
                    objPurchaseProperty.TableName = "CommercialDetails";
                    objpurchaseBll = new LP_PI_BLL(objPurchaseProperty);
                    flag = objpurchaseBll.Insert();

                }
                return Json(new { data = "", success = flag, msg = flag == true ? "Successfull" : "Failed", statuscode = flag == true ? 200 : 401 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region Commercial Invoice Work

        public ActionResult ViewCommercialInvoice()
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
        public JsonResult GetAllCI()
        {
            try
            {
                objPurchaseVM_Property = new LP_PerformaInvoice_ViewModel();
                objpurchaseBll = new LP_PI_BLL();
                var Data = JsonConvert.SerializeObject(objpurchaseBll.SelectAllCIPO());
                return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPIById(int id)
        {
            objpurchaseBll = new LP_PI_BLL();
           var Data=JsonConvert.SerializeObject(objpurchaseBll.SelectPIBYID(id));
            return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddNewCIPO(int? id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                objCIPOVm = new LP_CI_ViewModel();
                Vendors_Property vendor = new Vendors_Property();
                Product_Property product = new Product_Property();
                Vendors_BLL objvendorbll = new Vendors_BLL();
                Product_BLL objProductbll = new Product_BLL();

                objpurchaseBll = new LP_PI_BLL();
                // objPurchaseVM_Property.VendorLST = Helper.ConvertDataTable<Vendors_Property>(objvendorbll.ViewAll());
                // objPurchaseVM_Property.DepartmentList = Helper.ConvertDataTable<Departments_property>(GetAllDepartments());
                // objPurchaseVM_Property.PurchaseType_List = Helper.ConvertDataTable<LP_Purchase_Type>(GetAllPurchaseType());
                objCIPOVm.ProductList = Helper.ConvertDataTable<Product_Property>(objProductbll.ViewAll());
                objCIPOVm.PerformaLISt = Helper.ConvertDataTable<LP_Performa_Invoice_Property>(objpurchaseBll.SelectAll());

                objCIPOVm.purchaseDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                //objPurchaseVM_Property.poNumber = "Po-001";
                if (id > 0)
                {
                    //LP_Purchase_Detail_Property objmpurchasedetail;
                    //objPurchaseProperty = new LP_Performa_Invoice_Property();
                    //objPurchaseProperty.idx = Convert.ToInt16(id);

                    //objpurchaseBll = new LP_PI_BLL();
                    //DataTable dt = objpurchaseBll.SelectOne();
                    //objPurchaseVM_Property.idx = Convert.ToInt16(dt.Rows[0]["purchaseIdx"].ToString());
                    //objPurchaseVM_Property.vendorIdx = Convert.ToInt16(dt.Rows[0]["vendorIdx"].ToString());
                    //objPurchaseVM_Property.poNumber = dt.Rows[0]["poNumber"].ToString();
                    //objPurchaseVM_Property.description = dt.Rows[0]["description"].ToString();

                    //objPurchaseVM_Property.DepartmentID = Convert.ToInt16(dt.Rows[0]["DepartmentID"].ToString());
                    //objPurchaseVM_Property.totalAmount = Convert.ToDecimal(dt.Rows[0]["totalAmount"].ToString());
                    //string pdate = (dt.Rows[0]["purchaseDate"].ToString()).ToString();
                    //string ndate = DateTime.Parse(pdate).ToString("yyyy-MM-dd");
                    //objPurchaseVM_Property.purchaseDate = Convert.ToDateTime(ndate);// DateTime.Parse(dt.Rows[0]["mrnDate"].ToString()).ToString("yyyy-MM-dd");
                    ////DateTime.Parse(dt.Rows[0]["mrnDate"].ToString()).ToString("yyyy-MM-dd");
                    ////foreach(DataRow dr in dt.Rows)
                    ////{
                    ////    objmrndetail

                    ////}
                    //ViewBag.DetailData = Helper.ConvertDataTable<PurchaseVM_Property>(dt);
                    //update
                    return View("AddNewCIPO", objCIPOVm);
                }
                else
                {
                    //objPurchaseProperty = new LP_Purchase_Master_Property();
                    objCIPOVm.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    objpurchaseBll = new LP_PI_BLL();
                    LP_GenerateTransNumber_Property objtrans = new LP_GenerateTransNumber_Property();
                    objtrans.TableName = "CI_PurchaseOrder";
                    objtrans.Identityfieldname = "idx";
                    objtrans.userid = Session["UID"].ToString();
                    objCIPOVm.poNumber = objpurchaseBll.GenerateCINo(objtrans);
                    objCIPOVm.CDPercntage = 20;
                    objCIPOVm.RDPercentage = 20;
                    objCIPOVm.ACDPercentage = 20;
                    objCIPOVm.STPercentage = 20;
                    objCIPOVm.ITPercentage = 20;
                    objCIPOVm.TDTax = 0.00m;
                    objCIPOVm.CleaningPrice = 0.00m;
                    objCIPOVm.TotalPrice = 0.00m;
                    objCIPOVm.ASTPercentage = 20;
                    objCIPOVm.CleaningPrice = 20;
                    //objPurchaseProperty.poNumber = "";
                    //objpurchaseBll = new LP_Purchase_BLL(objPurchaseProperty);
                    //objPurchaseVM_Property.poNumber = objpurchaseBll.GeneratePO();// "PO-001";
                    // string po = objpurchaseBll.GeneratePO();

                    return View("AddNewCIPO", objCIPOVm);

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpPost]
        public JsonResult AddUpdateCI(LP_CI_ViewModel objpurchase)
        {
            try
            {
                bool flag = false;


                _objCIMaster = new LP_CI_PurchaseOrder_Property();
                _objCIMaster.poNumber = objpurchase.poNumber;
                _objCIMaster.PINO = objpurchase.PINO;
               //_objCIMaster.purchaseDate = objpurchase.purchaseDate;
                _objCIMaster.purchaseDate = objpurchase.purchaseDate;
                _objCIMaster.description = objpurchase.description;
                _objCIMaster.totalAmount = objpurchase.totalAmount;
                _objCIMaster.netAmount = objpurchase.netAmount;
                _objCIMaster.paidAmount = objpurchase.paidAmount;
                _objCIMaster.balanceAmount = objpurchase.balanceAmount;
               // _objCIMaster.DocumentNumber = objpurchase.DocumentNumber;
               // _objCIMaster.ContainerNo = objpurchase.ContainerNo;
               // _objCIMaster.ExchangeRate = objpurchase.ExchangeRate;
               // _objCIMaster.DepartmentID = objpurchase.DepartmentID;
                //  _objCIMaster.paidDate = ;// objpurchase.paidDate;

                _objCIMaster.DetailData = Helper.ToDataTable<LP_CI_PurchaseDetails_Property>(objpurchase.CILIST);
                if (objpurchase.idx > 0)
                {
                    _objCIMaster.idx = objpurchase.idx;
                    _objCIMaster.creationDate = DateTime.Now;
                    _objCIMaster.visible = 1;
                    _objCIMaster.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    _objCIMaster.visible = 1;
                    _objCIMaster.status = "0";
                    _objCIMaster.TableName = "CommercialDetails";
                    objpurchaseBll = new LP_PI_BLL();
                    flag = objpurchaseBll.Insert();
                    //update
                }
                else
                {
                    //add
                    _objCIMaster.creationDate = DateTime.Now;
                    _objCIMaster.visible = 1;
                    _objCIMaster.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    _objCIMaster.visible = 1;
                    _objCIMaster.status = "0";
                    _objCIMaster.TableName = "CommercialDetails";
                    objpurchaseBll = new LP_PI_BLL(_objCIMaster);
                    flag = objpurchaseBll.InsertCIPO();

                }
                return Json(new { data = "", success = flag, msg = flag == true ? "Successfull" : "Failed", statuscode = flag == true ? 200 : 401 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }



        #endregion

        #region Shipment calculate

        public ActionResult ViewConsigment()
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

        public JsonResult GetAllConsigment()
        {
            try
            {
                objPurchaseVM_Property = new LP_PerformaInvoice_ViewModel();
                objpurchaseBll = new LP_PI_BLL();
                var Data = JsonConvert.SerializeObject(objpurchaseBll.SelectAllCIPO());
                return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCIById(int id)
        {
            objpurchaseBll = new LP_PI_BLL();
            var Data = JsonConvert.SerializeObject(objpurchaseBll.SelectCIBYID(id));
            return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddNewConsigment(int? id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                objConsigmentVm = new LP_Consigment_ViewModel();
                Vendors_Property vendor = new Vendors_Property();
                Product_Property product = new Product_Property();
                Vendors_BLL objvendorbll = new Vendors_BLL();
                Product_BLL objProductbll = new Product_BLL();

                objpurchaseBll = new LP_PI_BLL();
                // objPurchaseVM_Property.VendorLST = Helper.ConvertDataTable<Vendors_Property>(objvendorbll.ViewAll());
                // objPurchaseVM_Property.DepartmentList = Helper.ConvertDataTable<Departments_property>(GetAllDepartments());
                // objPurchaseVM_Property.PurchaseType_List = Helper.ConvertDataTable<LP_Purchase_Type>(GetAllPurchaseType());
                objConsigmentVm.ProductList = Helper.ConvertDataTable<Product_Property>(objProductbll.ViewAll());
                objConsigmentVm.CIPO = Helper.ConvertDataTable<LP_CI_PurchaseOrder_Property>(objpurchaseBll.SelectAllCIPO());

                objConsigmentVm.Invoicedate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                //objPurchaseVM_Property.poNumber = "Po-001";
                if (id > 0)
                {
                    //LP_Purchase_Detail_Property objmpurchasedetail;
                    //objPurchaseProperty = new LP_Performa_Invoice_Property();
                    //objPurchaseProperty.idx = Convert.ToInt16(id);

                    //objpurchaseBll = new LP_PI_BLL();
                    //DataTable dt = objpurchaseBll.SelectOne();
                    //objPurchaseVM_Property.idx = Convert.ToInt16(dt.Rows[0]["purchaseIdx"].ToString());
                    //objPurchaseVM_Property.vendorIdx = Convert.ToInt16(dt.Rows[0]["vendorIdx"].ToString());
                    //objPurchaseVM_Property.poNumber = dt.Rows[0]["poNumber"].ToString();
                    //objPurchaseVM_Property.description = dt.Rows[0]["description"].ToString();

                    //objPurchaseVM_Property.DepartmentID = Convert.ToInt16(dt.Rows[0]["DepartmentID"].ToString());
                    //objPurchaseVM_Property.totalAmount = Convert.ToDecimal(dt.Rows[0]["totalAmount"].ToString());
                    //string pdate = (dt.Rows[0]["purchaseDate"].ToString()).ToString();
                    //string ndate = DateTime.Parse(pdate).ToString("yyyy-MM-dd");
                    //objPurchaseVM_Property.purchaseDate = Convert.ToDateTime(ndate);// DateTime.Parse(dt.Rows[0]["mrnDate"].ToString()).ToString("yyyy-MM-dd");
                    ////DateTime.Parse(dt.Rows[0]["mrnDate"].ToString()).ToString("yyyy-MM-dd");
                    ////foreach(DataRow dr in dt.Rows)
                    ////{
                    ////    objmrndetail

                    ////}
                    //ViewBag.DetailData = Helper.ConvertDataTable<PurchaseVM_Property>(dt);
                    //update
                    return View("AddNewConsigment", objConsigmentVm);
                }
                else
                {
                    //objPurchaseProperty = new LP_Purchase_Master_Property();
                    objConsigmentVm.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    objpurchaseBll = new LP_PI_BLL();
                    LP_GenerateTransNumber_Property objtrans = new LP_GenerateTransNumber_Property();
                    objtrans.TableName = "consigment";
                    objtrans.Identityfieldname = "idx";
                    objtrans.userid = Session["UID"].ToString();
                    objConsigmentVm.InvoiceNo = objpurchaseBll.GenerateCINo(objtrans);
                    //objCIPOVm.CDPercntage = 20;
                    //objCIPOVm.RDPercentage = 20;
                    //objCIPOVm.ACDPercentage = 20;
                    //objCIPOVm.STPercentage = 20;
                    //objCIPOVm.ITPercentage = 20;
                    //objCIPOVm.TDTax = 0.00m;
                    //objCIPOVm.CleaningPrice = 0.00m;
                    //objCIPOVm.TotalPrice = 0.00m;
                    //objCIPOVm.ASTPercentage = 20;
                    //objCIPOVm.CleaningPrice = 20;
                    //objPurchaseProperty.poNumber = "";
                    //objpurchaseBll = new LP_Purchase_BLL(objPurchaseProperty);
                    //objPurchaseVM_Property.poNumber = objpurchaseBll.GeneratePO();// "PO-001";
                    // string po = objpurchaseBll.GeneratePO();

                    return View("AddNewConsigment", objConsigmentVm);

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }


        [HttpPost]
        public JsonResult AddUpdateConsigment(LP_Consigment_ViewModel objpurchase)
        {
            try
            {
                bool flag = false;


                _objconsigment = new LP_Consigment_Property();
                _objconsigment.InvoiceNo = objpurchase.InvoiceNo;
                _objconsigment.ParentDocumentId = objpurchase.ParentDocumentId;
                //_objCIMaster.purchaseDate = objpurchase.purchaseDate;
                _objconsigment.Invoicedate = objpurchase.Invoicedate;
                _objconsigment.Reference = objpurchase.Reference;
                //_objconsigment. = objpurchase.totalAmount;
                //_objconsigment.netAmount = objpurchase.netAmount;
                //_objconsigment.paidAmount = objpurchase.paidAmount;
                //_objconsigment.balanceAmount = objpurchase.balanceAmount;
                // _objCIMaster.DocumentNumber = objpurchase.DocumentNumber;
                // _objCIMaster.ContainerNo = objpurchase.ContainerNo;
                // _objCIMaster.ExchangeRate = objpurchase.ExchangeRate;
                // _objCIMaster.DepartmentID = objpurchase.DepartmentID;
                //  _objCIMaster.paidDate = ;// objpurchase.paidDate;
                _objconsigment.CIPODetails = Helper.ToDataTable<LP_CI_PurchaseDetails_Property>(objpurchase.CIDetailsPO);
                _objconsigment.DetailData = Helper.ToDataTable<LP_ConsigmentDetails>(objpurchase.ConsigmentDetails);
                if (objpurchase.idx > 0)
                {
                    _objCIMaster.idx = objpurchase.idx;
                    _objCIMaster.creationDate = DateTime.Now;
                    _objCIMaster.visible = 1;
                    _objCIMaster.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    _objCIMaster.visible = 1;
                    _objCIMaster.status = "0";
                    _objCIMaster.TableName = "CommercialDetails";
                    objpurchaseBll = new LP_PI_BLL();
                    flag = objpurchaseBll.Insert();
                    //update
                }
                else
                {
                    //add
                    _objconsigment.creationDate = DateTime.Now;
                    //_objconsigment.visible = 1;
                    _objconsigment.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    //_objconsigment.visible = 1;
                    _objconsigment.status = 0;
                    _objconsigment.TableName = "consigmentdetails";
                    objpurchaseBll = new LP_PI_BLL(_objconsigment);
                    flag = objpurchaseBll.InsertConsigment();

                }
                return Json(new { data = "", success = flag, msg = flag == true ? "Successfull" : "Failed", statuscode = flag == true ? 200 : 401 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

    }
}