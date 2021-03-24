using Newtonsoft.Json;
using SSS.BLL.Transactions;
using SSS.Property.Setups;
using SSS.Property.Transactions;
using SSS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSYSTEM.Controllers
{
    public class ActivityController : BaseController
    {
        // GET: Activity
        #region Activity
        public ActionResult Activity()
        {  // GET: Payment
            LP_Activity_Property objActivityVM;
            LP_Activity_BLL objVoucherBll;
            LP_Voucher_Property objActivityMaster;
            if (Session["LOGGEDIN"] != null)
            {
                objActivityVM = new LP_Activity_Property();
                if (objActivityVM.idx > 0)
                {

                }
                else
                {
                    LP_Activity_BLL objbll = new LP_Activity_BLL();
                    //objActivityVM.salesOrderLST = Helper.ConvertDataTable<LP_P_Invoice_Property>(objbll.SelectAll());//JsonConvert.SerializeObject(GetAllPIByDate(objsearchPI));
                    objActivityVM.salesOrderLST = Helper.ConvertDataTable<LP_SalesOrder_Master_Property>(GetAllSalesInvoice());
                    objActivityVM.vendorCatLST = Helper.ConvertDataTable<Vendor_Category_Property>(GetAllVendorsCategory());
                    //thirdTier_BLL subheadBLL = new thirdTier_BLL();
                    //var allSubhead = subheadBLL.ViewAll();
                    //objvoucherVM.date_created = DateTime.Now;

                    //objvoucherVM.vouchertypelist = Helper.ConvertDataTable<thirdTier_Property>(subheadBLL.ViewAll());
                    //objvoucherVM.voucher_amount = 0.00m;
                    //objvoucherVM.description = "";
                    //objvoucherVM.banklist = Helper.ConvertDataTable<Company_Bank_Property>(GetAllCompanyBanks());
                    //objvoucherVM.vendorlist = Helper.ConvertDataTable<Vendors_Property>(GetAllVendors());

                    //LP_GenerateTransNumber_Property objtransnumber = new LP_GenerateTransNumber_Property();
                    //objtransnumber.TableName = "Voucher";
                    //objtransnumber.Identityfieldname = "idx";
                    //objtransnumber.userid = Session["UID"].ToString();
                    //objVoucherBll = new LP_Voucher_BLL();
                    //objvoucherVM.voucher_no = objVoucherBll.GenerateTransNo(objtransnumber);

                }

                return View("Activity", objActivityVM);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpGet]
        public JsonResult SearchProductsInDetail(int Id)
        {
            try
            {
                LP_PInvoice_BLL objbll = new LP_PInvoice_BLL();
                //DataTable tblFiltered;
                if (Id != null)
                {



                    var Data = Helper.ConvertDataTable<LP_SalesOrder_Detail_Property>(GetAllSalesInvoiceDetails(Id));//JsonConvert.SerializeObject(GetAllPIByDate(objsearchPI));
                    return Json(new { data = Data, success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "Error Occured", success = false, statuscode = 500 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { data = "Session Expired", success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult SearchVendorsOnCatIdx(int Id)
        {
            try
            {

                if (Id > 0)
                {



                    var Data = Helper.ConvertDataTable<Vendors_Property>(GetVendorByVendorCat(Id));//JsonConvert.SerializeObject(GetAllPIByDate(objsearchPI));
                    return Json(new { data = Data, success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "Error Occured", success = false, statuscode = 500 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { data = "Session Expired", success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult AddUpdate(LP_Activity_Property objVoucher)
        {
            try
            {
                bool flag = false;

                LP_Activity_Property obj = new LP_Activity_Property();
                obj.idx = objVoucher.idx;
                obj.activityDate = DateTime.Now.ToString("yyyy-MM-dd");
                obj.creationDate = DateTime.Now.ToString("yyyy-MM-dd");
                obj.createdBy = Convert.ToInt16(Session["UID"].ToString());
                obj.orderIdx = objVoucher.orderIdx;
                obj.productIdx = objVoucher.productIdx;
                obj.vendorCatIdx = objVoucher.vendorCatIdx;
                obj.vendorIdx = objVoucher.vendorIdx;
                obj.size = objVoucher.size;
                obj.qty = objVoucher.qty;
                obj.activityPrice = objVoucher.activityPrice;
                obj.description = objVoucher.description;
                LP_Activity_BLL objBLL = new LP_Activity_BLL(obj);
                flag = objBLL.Insert();
                if (flag)
                {
                    return Json(new { data = "Inserted", success = flag, msg = flag == true ? "Successfull" : "Success", statuscode = flag == true ? 200 : 401 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "", success = flag, msg = flag == true ? "Failure" : "Failure", statuscode = flag == true ? 200 : 401 }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Local Finish Good
        public ActionResult FinsihProducts(int? id)
        {  // GET: Payment



            if (Session["LOGGEDIN"] != null)
            {
                LP_FinsihProduct_Property objPInvoiceVM = new LP_FinsihProduct_Property();
                LP_FinishProduct_BLL objFPBLL = new LP_FinishProduct_BLL();
               

                objPInvoiceVM.salesOrderLST = Helper.ConvertDataTable<LP_SalesOrder_Master_Property>(objFPBLL.SelectAll());
                
                objPInvoiceVM.ProductLST = Helper.ConvertDataTable<Product_Property>(ViewAllProducts());
                
                //objGRNVM_Property.Doc_No = "GRN-001";
                if (id > 0)
                {
                    //update 
                    //objPIProperty = new LP_P_Invoice_Property();
                    //objPIProperty.idx = Convert.ToInt16(id);
                    //objPIBLL = new LP_PInvoice_BLL(objPIProperty);
                    //DataSet DS = objPIBLL.SelectByID();
                    //objPInvoiceVM.InvoiceProperty = Helper.ConvertDataTable<LP_P_Invoice_Property>(DS.Tables[0]);
                    //objPInvoiceVM.InvoiceDetails = Helper.ConvertDataTable<LP_P_Invoice_Details>(DS.Tables[1]);
                    //objPInvoiceVM.PITAXLIST = Helper.ConvertDataTable<LP_PI_Taxes_Property>(DS.Tables[2]);

                    ////
                    //objPInvoiceVM.ParentDocID = objPInvoiceVM.InvoiceProperty[0].ParentDocID;
                    //objPInvoiceVM.InvoiceNo = objPInvoiceVM.InvoiceProperty[0].InvoiceNo;
                    //objPInvoiceVM.VendorID = objPInvoiceVM.InvoiceProperty[0].VendorID;
                    //objPInvoiceVM.CreatedDate = objPInvoiceVM.InvoiceProperty[0].CreatedDate;
                    //objPInvoiceVM.Reference = objPInvoiceVM.InvoiceProperty[0].Reference;
                    //objPInvoiceVM.Description = objPInvoiceVM.InvoiceProperty[0].Description;
                    //objPInvoiceVM.NetAmount = objPInvoiceVM.InvoiceProperty[0].NetAmount;
                    //objPInvoiceVM.TotalAmount = objPInvoiceVM.InvoiceProperty[0].TotalAmount;
                    //objPInvoiceVM.TaxAmount = objPInvoiceVM.InvoiceProperty[0].TaxAmount;
                    //objPInvoiceVM.BalanceAmount = objPInvoiceVM.InvoiceProperty[0].BalanceAmount;
                    //objPInvoiceVM.PaidAmount = objPInvoiceVM.InvoiceProperty[0].PaidAmount;
                    //objPInvoiceVM.PaymentType = objPInvoiceVM.InvoiceProperty[0].PaymentType;
                    //objPInvoiceVM.bankIdx = objPInvoiceVM.InvoiceProperty[0].BankId;
                    //objPInvoiceVM.accorChequeNumber = objPInvoiceVM.InvoiceProperty[0].AccountChequeNo;
                    //ViewBag.update = true;

                    return View("FinsihProducts", objPInvoiceVM);
                }
                else
                {
                    //objPInvoiceVM.InvoiceProperty = Helper.ConvertDataTable<LP_P_Invoice_Property>(new DataTable());
                    //objPInvoiceVM.InvoiceDetails = Helper.ConvertDataTable<LP_P_Invoice_Details>(new DataTable());
                    //objPInvoiceVM.PITAXLIST = Helper.ConvertDataTable<LP_PI_Taxes_Property>(new DataTable());
                    //LP_GenerateTransNumber_Property objtrans = new LP_GenerateTransNumber_Property();
                    //objtrans.TableName = "P_Invoice";
                    //objtrans.Identityfieldname = "idx";
                    //objtrans.userid = Session["UID"].ToString();
                    //objPInvoiceVM.InvoiceNo = objPIBLL.GeneratePINo(objtrans);
                    //objPInvoiceVM.CreatedDate =DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                    return View("FinsihProducts", objPInvoiceVM);

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }








            //LP_Activity_Property objActivityVM;
            //LP_Activity_BLL objVoucherBll;
            //LP_Voucher_Property objActivityMaster;
            //if (Session["LOGGEDIN"] != null)
            //{
            //    objActivityVM = new LP_Activity_Property();
            //    if (objActivityVM.idx > 0)
            //    {

            //    }
            //    else
            //    {
            //        LP_Activity_BLL objbll = new LP_Activity_BLL();

            //        objActivityVM.salesOrderLST = Helper.ConvertDataTable<LP_SalesOrder_Master_Property>(GetAllSalesInvoice());
            //        objActivityVM.vendorCatLST = Helper.ConvertDataTable<Vendor_Category_Property>(GetAllVendorsCategory());


            //    }

            //    return View("FinsihProducts", objActivityVM);
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}
        }

        public JsonResult GetAllAcitvityBYOrderId(int id )
        {

            if (Session["LOGGEDIN"] != null)
            {
                try
                {


                    LP_FinishProduct_BLL objPIBLL = new LP_FinishProduct_BLL();
                    var Data = JsonConvert.SerializeObject(objPIBLL.SelectAllActivityOnSoNumber(id));
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
        [HttpPost]
        public JsonResult AddUpdateFP(LP_FinsihProduct_Property objPI)
        {
            try
            {
                bool flag = false;

                LP_FinsihProduct_Property obj = new LP_FinsihProduct_Property();
                 obj.orderIdx = objPI.orderIdx;
                if (objPI.InventoryDetails.Count > 0)
                {
                    obj.DetailData = Helper.ToDataTable<LP_InventoryLogs_Property>(objPI.InventoryDetails);
                    LP_FinishProduct_BLL objBLL = new LP_FinishProduct_BLL(obj);
                    flag = objBLL.Insert();
                    if (flag)
                    {
                        return Json(new { data = "Inserted", success = flag, msg = flag == true ? "Successfull" : "Success", statuscode = flag == true ? 200 : 401 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { data = "", success = flag, msg = flag == true ? "Failure" : "Failure", statuscode = flag == true ? 200 : 401 }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { data = "", success = flag, msg = flag == true ? "Failure" : "Failure", statuscode = flag == true ? 200 : 401 }, JsonRequestBehavior.AllowGet);
                }
                //obj.activityDate = DateTime.Now.ToString("yyyy-MM-dd");
                //obj.creationDate = DateTime.Now.ToString("yyyy-MM-dd");
                //obj.createdBy = Convert.ToInt16(Session["UID"].ToString());
                //obj.orderIdx = objVoucher.orderIdx;
                //obj.productIdx = objVoucher.productIdx;
                //obj.vendorCatIdx = objVoucher.vendorCatIdx;
                //obj.vendorIdx = objVoucher.vendorIdx;
                //obj.size = objVoucher.size;
                //obj.qty = objVoucher.qty;
                //obj.activityPrice = objVoucher.activityPrice;
                //obj.description = objVoucher.description;
               


            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}