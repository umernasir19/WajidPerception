﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSS.Property.Setups;
using SSS.Property.Transactions.ViewModels;
using SSS.Property.Transactions;
using SSS.Utility;
using SSS.BLL.Transactions;
using Newtonsoft.Json;
using System.Data;
using SSS.Property.Setups.Accounts;

namespace SMSYSTEM.Controllers
{
    public class PaymentController : BaseController
    {
        // GET: Payment
        LP_Voucher_ViewModel objvoucherVM;
        LP_Voucher_BLL objVoucherBll;
        LP_Voucher_Property objvouchermaster;
        public ActionResult Vouchers()
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

        public JsonResult GetAllPaymentVoucher()
        {
            try
            {
                objvouchermaster = new LP_Voucher_Property();
                objVoucherBll = new LP_Voucher_BLL(objvouchermaster);

                var Data = JsonConvert.SerializeObject(objVoucherBll.SelectAllPaymentVoucher());
                return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        #region Add Voucher
        public ActionResult AddVoucher(int? id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                objvoucherVM = new LP_Voucher_ViewModel();
                if (objvoucherVM.idx > 0)
                {

                }
                else
                {
                    objvoucherVM.date_created = DateTime.Now;
                    objvoucherVM.vouchertypelist = Helper.ConvertDataTable<LP_Transaction_Type_Property>(GetAllTransactionType());
                    objvoucherVM.voucher_amount = 0.00m;
                    objvoucherVM.description = "";
                    //objvoucherVM.BankList = Helper.ConvertDataTable<Company_Bank_Property>(GetAllCompanyBanks());
                    objvoucherVM.vendorlist = Helper.ConvertDataTable<Vendors_Property>(GetAllVendors());
                    objvoucherVM.explist = Helper.ConvertDataTable<fourthTier_Property>(GetChildAccountsByheadIdx(4)); //All Expenses
                    objvoucherVM.BankList = Helper.ConvertDataTable<Company_Bank_Property>(GetAllCompanyBanks()); //Added
                    //objvoucherVM.CustomerBankList = Helper.ConvertDataTable<LP_CustomerBanks_Property>(GetAllCompanyBanks()); //Added
                    LP_GenerateTransNumber_Property objtransnumber = new LP_GenerateTransNumber_Property();
                    objtransnumber.TableName= "Voucher";
                    objtransnumber.Identityfieldname = "idx";
                    objtransnumber.userid = Session["UID"].ToString();
                    objtransnumber.tranTypeIdx ="6";
                    objVoucherBll = new LP_Voucher_BLL();
                    objvoucherVM.voucher_no= objVoucherBll.GenerateTransNo(objtransnumber);

                }

                return View("_AddVoucher", objvoucherVM);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public JsonResult AddUpdate(LP_Voucher_ViewModel objVoucher)
        {
            try
            {
                bool flag = false;
                objvouchermaster = new LP_Voucher_Property();
                objvouchermaster.idx = objVoucher.idx;
                objvouchermaster.voucher_no = objVoucher.voucher_no;
                objvouchermaster.vendor_id = objVoucher.vendor_id;
                objvouchermaster.voucher_type = objVoucher.voucher_type;
                objvouchermaster.date_created =objVoucher.date_created.ToString("yyyy-MM-dd");
                if (objVoucher.description != null)
                {
                    objvouchermaster.description = objVoucher.description;
                }
                else
                {
                    objvouchermaster.description = "";
                }
                // objMRNProperty.description = objMRN.description.Length>0? objMRN.description : "";

                //  objMRNProperty.paidDate = ;// objMRN.paidDate;

               
                if (objVoucher.idx > 0)
                {
                    ////objMRNProperty.creationDate = DateTime.Now;
                    ////objMRNProperty.visible = 1;
                    ////// objMRNProperty.status = "0";
                    ////objMRNProperty.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    //objvouchermaster.creationDate = DateTime.Now;
                    //objvouchermaster.lastModificationDate = DateTime.Now;
                    //objvouchermaster.lastModifiedByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    ////  objMRNVM_Property.createdByUserIdx = DateTime.Now; ;
                    //objvouchermaster.TableName = "MRNDetails";
                    //objMRNBll = new LP_MRN_BLL(objvouchermaster);
                    //flag = objMRNBll.Insert();
                    //update
                }
                else
                {
                    //add
                   
                    objvouchermaster.status = 0;
                    objvouchermaster.accorChequeNumber = objVoucher.accorChequeNumber;
                    objvouchermaster.bankIdx = objVoucher.bankIdx;
                    objvouchermaster.paymentModeIdx = objVoucher.paymentModeIdx;
                    objvouchermaster.voucher_amount = decimal.Parse(objVoucher.voucher_amount.ToString());
                    objvouchermaster.DetailData = Helper.ToDataTable<AccountGJ>(objVoucher.AccountGJLST);
                    objvouchermaster.u_id = Convert.ToInt16(Session["UID"].ToString());

                    objvouchermaster.TableName = "VoucherDetails";
                    objVoucherBll = new LP_Voucher_BLL(objvouchermaster);
                    flag = objVoucherBll.InsertPayment();

                }
                return Json(new { data = "", success = flag, msg = flag == true ? "Successfull" : "Failed", statuscode = flag == true ? 200 : 401 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SearchPurchaseInvoice(LP_Voucher_ViewModel objsearchPI)
        {
            if (Session["LOGGEDIN"] != null)
            {
                try
                {
                    //DataTable Data = GetAllPIByDate(objsearchPI);
                    var Data = Helper.ConvertDataTable<LP_P_Invoice_Property>(GetAllPIByDate(objsearchPI));//JsonConvert.SerializeObject(GetAllPIByDate(objsearchPI));
                    return Json(new { data = Data, success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);

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


        public JsonResult getCashBalance()
        {
            try
            {
                objvoucherVM = new LP_Voucher_ViewModel();
                LP_Voucher_BLL objBLL= new LP_Voucher_BLL();
                var Data = JsonConvert.SerializeObject(objBLL.getcashBalance());
                return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getCustomerPaymentBalance()
        {
            try
            {
                objvoucherVM = new LP_Voucher_ViewModel();
                LP_Voucher_BLL objBLL = new LP_Voucher_BLL();
                var Data = JsonConvert.SerializeObject(objBLL.getcustomerPaymentBalance());
                return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getVendorBalance(int? id)
        {
            try
            {  if (id != null)
                {
                    objvoucherVM = new LP_Voucher_ViewModel();
                    LP_Voucher_BLL objBLL = new LP_Voucher_BLL();
                    var Data = JsonConvert.SerializeObject(objBLL.getvendorBalance(int.Parse(id.ToString())));
                    return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "ID null", success = false, statuscode = 200 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


    }
}