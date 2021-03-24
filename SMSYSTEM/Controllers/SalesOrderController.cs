﻿using Newtonsoft.Json;
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
    public class SalesOrderController : BaseController
    {
        // GET: SalesOrder
        SalesOrderVM_Property objSalesOrderVM_Property;
        LP_SalesOrder_Master_Property objSalesOrderProperty;
        LP_SalesOrder_BLL objSalesOrderBll;
        // GET:SalesOrder
        public ActionResult ViewSalesOrder()
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
        public JsonResult GetAllSalesOrder()
        {
            try
            {
                objSalesOrderVM_Property = new SalesOrderVM_Property();
                objSalesOrderBll = new LP_SalesOrder_BLL(objSalesOrderProperty);
                var Data = JsonConvert.SerializeObject(objSalesOrderBll.SelectAll());
                return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult AddNewSalesOrder(int? id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                objSalesOrderVM_Property = new SalesOrderVM_Property();
                Customers_Property vendor = new Customers_Property();
                Product_Property product = new Product_Property();
                Customers_BLL objcustomerbll = new Customers_BLL();
                Product_BLL objProductbll = new Product_BLL();
                LP_Quotation_BLL objQuotationbll = new LP_Quotation_BLL();
                WareHouse_BLL objWareHouseBLL = new WareHouse_BLL();
                objSalesOrderVM_Property.QSList = Helper.ConvertDataTable<LP_Quotation_Master_Property>(objQuotationbll.SelectQS());
                objSalesOrderVM_Property.CustomerLST = Helper.ConvertDataTable<Customers_Property>(objcustomerbll.ViewAllCustomers());
                objSalesOrderVM_Property.ProductList = Helper.ConvertDataTable<Product_Property>(objProductbll.ViewAll());
                objSalesOrderVM_Property.BankList = Helper.ConvertDataTable<Company_Bank_Property>(GetAllCompanyBanks());
                objSalesOrderVM_Property.wareHouseList = Helper.ConvertDataTable<WareHouse_Property>(objWareHouseBLL.SelectAll());

                objSalesOrderVM_Property.salesorderDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                Taxes_Property obj = new Taxes_Property();
                Taxes_BLL objtaxBLL = new Taxes_BLL(obj);
                ViewBag.TaxList = Helper.ConvertDataTable<Taxes_Property>(objtaxBLL.GetTaxesForCheckBox());
                //objSalesOrderVM_Property.poNumber = "Po-001";
                if (id > 0)
                {
                    LP_SalesOrder_Detail_Property objmSalesOrderdetail;
                    objSalesOrderProperty = new LP_SalesOrder_Master_Property();
                    objSalesOrderProperty.idx = Convert.ToInt16(id);

                    objSalesOrderBll = new LP_SalesOrder_BLL(objSalesOrderProperty);
                    DataTable dt = objSalesOrderBll.SelectOne();
                    objSalesOrderVM_Property.idx = Convert.ToInt16(dt.Rows[0]["salesorderIdx"].ToString());
                    objSalesOrderVM_Property.customerIdx = Convert.ToInt32(dt.Rows[0]["customerIdx"].ToString());
                    objSalesOrderVM_Property.soNumber = dt.Rows[0]["soNumber"].ToString();
                    objSalesOrderVM_Property.description = dt.Rows[0]["description"].ToString();
                    objSalesOrderVM_Property.qsIdx = Convert.ToInt16(dt.Rows[0]["qsIdx"].ToString());
                    objSalesOrderVM_Property.totalAmount = Convert.ToDecimal(dt.Rows[0]["totalAmount"].ToString());
                    string pdate = (dt.Rows[0]["salesorderdate"].ToString()).ToString();
                    string ndate = DateTime.Parse(pdate).ToString("yyyy-MM-dd");
                    objSalesOrderVM_Property.salesorderDate = Convert.ToDateTime(ndate);// DateTime.Parse(dt.Rows[0]["mrnDate"].ToString()).ToString("yyyy-MM-dd");
                    //DateTime.Parse(dt.Rows[0]["mrnDate"].ToString()).ToString("yyyy-MM-dd");
                    //foreach(DataRow dr in dt.Rows)
                    //{
                    //    objmrndetail

                    //}
                    ViewBag.DetailData = Helper.ConvertDataTable<SalesOrderVM_Property>(dt);
                    //update
                    return View("AddNewSalesOrder", objSalesOrderVM_Property);
                }
                else
                {
                    //objSalesOrderProperty = new LP_SalesOrder_Master_Property();
                    objSalesOrderVM_Property.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    objSalesOrderBll = new LP_SalesOrder_BLL();
                    LP_GenerateTransNumber_Property objtrans = new LP_GenerateTransNumber_Property();
                    objtrans.TableName = "SalesOrder";
                    objtrans.Identityfieldname = "idx";
                    objtrans.userid = Session["UID"].ToString();

                    objSalesOrderVM_Property.soNumber = objSalesOrderBll.GenerateSO(objtrans);
                    //objSalesOrderProperty.poNumber = "";
                    //objSalesOrderBll = new LP_SalesOrder_BLL(objSalesOrderProperty);
                    //objSalesOrderVM_Property.poNumber = objSalesOrderBll.GeneratePO();// "PO-001";
                    // string po = objSalesOrderBll.GeneratePO();

                    return View("AddNewSalesOrder", objSalesOrderVM_Property);

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpPost]
        public JsonResult AddUpdate(SalesOrderVM_Property objSalesOrder)

        {
            try
            {
                var BankList = Helper.ConvertDataTable<Company_Bank_Property>(GetCompanyBankById(objSalesOrder.bankIdx));
                var CustomerData = Helper.ConvertDataTable<Customers_Property>(GetCustomerByID(objSalesOrder.customerIdx));

                bool flag = false;
                int qsIdx;
                int.TryParse(objSalesOrder.qsIdx.ToString(), out qsIdx);
                objSalesOrderProperty = new LP_SalesOrder_Master_Property();
                objSalesOrderProperty.soNumber = objSalesOrder.soNumber;
                objSalesOrderProperty.customerIdx = objSalesOrder.customerIdx;
                //objSalesOrderProperty.SalesOrderTypeIdx = objSalesOrder.SalesOrderTypeIdx;
                objSalesOrderProperty.salesorderDate = objSalesOrder.salesorderDate;
                objSalesOrderProperty.description = objSalesOrder.description;
                objSalesOrderProperty.totalAmount = objSalesOrder.totalAmount;
                objSalesOrderProperty.netAmount = objSalesOrder.netAmount;
                objSalesOrderProperty.paidAmount = objSalesOrder.paidAmount;
                objSalesOrderProperty.balanceAmount = objSalesOrder.balanceAmount;
                objSalesOrderProperty.reference = objSalesOrder.reference;
                objSalesOrderProperty.taxAount = objSalesOrder.taxAount;
                objSalesOrderProperty.shippingCost = objSalesOrder.shippingCost;
                objSalesOrderProperty.discount = objSalesOrder.discount;
                objSalesOrderProperty.paymentModeIdx = objSalesOrder.paymentModeIdx;
                objSalesOrderProperty.bankIdx = objSalesOrder.bankIdx;
                objSalesOrderProperty.accorChequeNumber = objSalesOrder.accorChequeNumber;
                objSalesOrderProperty.wareHouseIdx = objSalesOrder.wareHouseIdx;
                objSalesOrderProperty.salespersonIdx = objSalesOrder.salespersonIdx;
                objSalesOrderProperty.qsIdx = qsIdx;
                if (objSalesOrderProperty.bankIdx > 0)
                {
                    objSalesOrderProperty.BankCOAIDX = BankList[0].coaidx;
                }
                
                objSalesOrderProperty.CustomerCoaidx = CustomerData[0].coaIdx;
                //  objSalesOrderProperty.paidDate = ;// objSalesOrder.paidDate;

                objSalesOrderProperty.DetailData = Helper.ToDataTable<SalesOrdersDetails_Property>(objSalesOrder.SalesOrderDetailLST);
                objSalesOrderProperty.SalesTaxData = Helper.ToDataTable<LP_salesTaxes_Property>(objSalesOrder.salesTaxesLST);
                if (objSalesOrder.idx > 0)
                {
                    objSalesOrderProperty.idx = objSalesOrder.idx;
                    objSalesOrderProperty.creationDate = DateTime.Now;
                    objSalesOrderProperty.visible = 1;
                    objSalesOrderProperty.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    objSalesOrderProperty.visible = 1;
                    objSalesOrderProperty.status = "0";
                    objSalesOrderProperty.TableName = "SalesOrderDetails";
                    objSalesOrderBll = new LP_SalesOrder_BLL(objSalesOrderProperty);
                    flag = objSalesOrderBll.Insert();
                    //update
                }
                else
                {

                    //add
                    objSalesOrderProperty.creationDate = DateTime.Now;
                    objSalesOrderProperty.visible = 1;
                    objSalesOrderProperty.createdByUserIdx = Convert.ToInt16(Session["UID"].ToString());
                    objSalesOrderProperty.visible = 1;
                    objSalesOrderProperty.status = "0";
                    objSalesOrderProperty.TableName = "SalesOrderDetails";
                    objSalesOrderBll = new LP_SalesOrder_BLL(objSalesOrderProperty);
                    flag = objSalesOrderBll.Insert();
                    var inventory = objSalesOrderBll.ProcessSalesInvoice();

                }


                return Json(new { data = "", success = true, msg = true == true ? "Successfull" : "Failed", statuscode = true == true ? 200 : 401 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SelectSOById(int id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                try
                {
                    objSalesOrderProperty = new LP_SalesOrder_Master_Property();
                    objSalesOrderProperty.idx = id;

                    objSalesOrderBll = new LP_SalesOrder_BLL(objSalesOrderProperty);
                    var Data = JsonConvert.SerializeObject(objSalesOrderBll.SelectOne());
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
        public JsonResult SelectQSById(int id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                try
                {
                    var objSalesOrderProperty = new LP_Quotation_Master_Property();
                    objSalesOrderProperty.idx = id;

                    var objSalesOrderBll = new LP_Quotation_BLL(objSalesOrderProperty);
                    var Data = JsonConvert.SerializeObject(objSalesOrderBll.SelectOne());
                    var Data2 = JsonConvert.SerializeObject(objSalesOrderBll.SelectTax());
                    return Json(new { data = Data, taxdat = Data2, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

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
        public JsonResult SelectProductStockAndUpById(int id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                try
                {

                    var Data = JsonConvert.SerializeObject(GetAllStockAndPrice(id));
                    // var Data2 = JsonConvert.SerializeObject(objSalesOrderBll.SelectTax());
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

        #region SalesReturn

        [HttpGet]
        public JsonResult CheckInverntoryforProductStock(int? id)
        {

            var Data = JsonConvert.SerializeObject(CheckInverntoryforProductStock(id));

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);






        }


        public ActionResult SalesReturn()
        {
            LP_SalesReturnVM_Property obj = new LP_SalesReturnVM_Property();
            LP_SalesOrder_BLL objSalesBLL = new LP_SalesOrder_BLL();
            obj.CustomerLSt = Helper.ConvertDataTable<Customers_Property>(GetAllCustomers());
            obj.SalesLST = Helper.ConvertDataTable<LP_SalesOrder_Master_Property>(objSalesBLL.SelectAll());
            return View(obj);
        }


        [HttpPost]
        public JsonResult SearchSales(LP_SalesReturnVM_Property objsale)
        {


            int salesIdx = int.Parse(objsale.SaleInvoiceNumber.ToString());

            var Data = JsonConvert.SerializeObject(GetAllSalesDetailsByIdx(salesIdx));

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReturnSales(int? id)
        {
            if (Session["LOGGEDIN"] != null)
            {
                objSalesOrderVM_Property = new SalesOrderVM_Property();
                Customers_Property vendor = new Customers_Property();
                Product_Property product = new Product_Property();
                Customers_BLL objcustomerbll = new Customers_BLL();
                Product_BLL objProductbll = new Product_BLL();
                LP_Quotation_BLL objQuotationbll = new LP_Quotation_BLL();
                WareHouse_BLL objWareHouseBLL = new WareHouse_BLL();
                objSalesOrderVM_Property.idx = Convert.ToInt32(id);
                objSalesOrderVM_Property.QSList = Helper.ConvertDataTable<LP_Quotation_Master_Property>(objQuotationbll.SelectQS());
                objSalesOrderVM_Property.CustomerLST = Helper.ConvertDataTable<Customers_Property>(objcustomerbll.ViewAllCustomers());
                objSalesOrderVM_Property.ProductList = Helper.ConvertDataTable<Product_Property>(objProductbll.ViewAll());
                objSalesOrderVM_Property.BankList = Helper.ConvertDataTable<Company_Bank_Property>(GetAllCompanyBanks());
                objSalesOrderVM_Property.wareHouseList = Helper.ConvertDataTable<WareHouse_Property>(objWareHouseBLL.SelectAll());

                objSalesOrderVM_Property.salesorderDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                Taxes_Property obj = new Taxes_Property();
                Taxes_BLL objtaxBLL = new Taxes_BLL(obj);
                ViewBag.TaxList = Helper.ConvertDataTable<Taxes_Property>(objtaxBLL.GetTaxesForCheckBox());
                //objPInvoiceVM = new LP_PI_ViewModel();
                //objPIProperty = new LP_P_Invoice_Property();
                //objPIProperty.idx = Convert.ToInt32(id);
                //objPIBLL = new LP_PInvoice_BLL(objPIProperty);
                //objPInvoiceVM.TaxesList = Helper.ConvertDataTable<Taxes_Property>(GetAllTaxes());
                //objPInvoiceVM.ProductList = Helper.ConvertDataTable<Product_Property>(ViewAllProducts());
                //objPInvoiceVM.BankList = Helper.ConvertDataTable<Company_Bank_Property>(GetAllCompanyBanks());
                DataSet DS = objSalesOrderBll.SelectSIWithDetailData(objSalesOrderVM_Property.idx);
                //DataSet DS = objPIBLL.SelectPIWithDetailData(objPIProperty.idx);
                if (DS.Tables.Count > 0)
                {
                    ViewBag.isReturn = 1;
                    objSalesOrderVM_Property.SalesOrderDetailLST = Helper.ConvertDataTable<SalesOrdersDetails_Property>(DS.Tables[0]);
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        objSalesOrderVM_Property.salesTaxesLST = Helper.ConvertDataTable<LP_salesTaxes_Property>(DS.Tables[1]);
                    }

                }

                return View("AddNewSalesOrder", objSalesOrderVM_Property);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        #endregion
    }
}