using Newtonsoft.Json;
using SSS.BLL.Setups;
using SSS.BLL.Transactions;
using SSS.Property.Setups;
using SSS.Property.Transactions;
using SSS.Property.Transactions.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSYSTEM.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        LP_Purchase_Type_BLL objpurchasetypebll;
        Product_BLL objProduct;
        public DataTable GetAllPurchaseType()
        {
            objpurchasetypebll = new LP_Purchase_Type_BLL();
            return objpurchasetypebll.SelectAll();
        }
        public DataTable ViewAllProducts()
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    Product_Property objProductProperty = new Product_Property();
                    //objProductProperty.branchIdx = 1;//user logged in session branchIdx
                    Product_BLL objProductBLL = new Product_BLL(objProductProperty);
                    return objProductBLL.ViewAll();

                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }

        }
        public DataTable GetAllTaxes()
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    Taxes_BLL objTaxes = new Taxes_BLL();
                   
                    return objTaxes.ViewAll();
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }

        public DataTable GetAllCompanyBanks()
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    CompanyBank_BLL objcompanybll = new CompanyBank_BLL();
                    
                    return objcompanybll.ViewAll();
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetCompanyBankById(int id)
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    CompanyBank_Property objcompany = new CompanyBank_Property();
                    objcompany.idx = id;
                    CompanyBank_BLL objcompanybll = new CompanyBank_BLL(objcompany);

                    return objcompanybll.SelectOne();
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetAllTransactionType()
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    LP_Transaction_Type_BLL objtransactiontypebll = new LP_Transaction_Type_BLL();

                    return objtransactiontypebll.SelectAll();
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetAllPIByDate(LP_Voucher_ViewModel objvchr)
        {

            if (Session["LOGGEDIN"] != null)
            {
                try
                {


                    LP_PInvoice_BLL objPIBLL = new LP_PInvoice_BLL();
                   
                    return objPIBLL.SelectPIByDate(objvchr);
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }

        }
        public DataTable GetAllSalesInvoice()
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    LP_SalesOrder_Master_Property objProperty = new LP_SalesOrder_Master_Property();
                    LP_SalesOrder_BLL objvendorcatbll = new LP_SalesOrder_BLL();

                    return objvendorcatbll.SelectAll();
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetAllSalesInvoiceDetails(int id)
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    LP_SalesOrder_Master_Property objProperty = new LP_SalesOrder_Master_Property();
                    LP_SalesOrder_BLL objvendorcatbll = new LP_SalesOrder_BLL();

                    return objvendorcatbll.SelectAllSalesInvoiceDetails(id);
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetAllSIByDate(LP_Voucher_ViewModel objvchr)
        {

            if (Session["LOGGEDIN"] != null)
            {
                try
                {


                    LP_SalesOrder_BLL objPIBLL = new LP_SalesOrder_BLL();

                    return objPIBLL.SelectSIByDate(objvchr);
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }

        }
        public DataTable GetAllVendorsCategory()
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    Vendor_Category_Property objProperty = new Vendor_Category_Property();
                    Vendor_Category_BLL objvendorcatbll = new Vendor_Category_BLL(objProperty);

                    return objvendorcatbll.ViewAll();
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetAllVendors()
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    Vendors_BLL objvendorbll = new Vendors_BLL();

                    return objvendorbll.ViewAll();
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetVendorById(int id)
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    Vendors_BLL objvendorbll = new Vendors_BLL();

                    return objvendorbll.GetById(id);
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetAllCustomers()
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    Customers_BLL objvendorbll = new Customers_BLL();

                    return objvendorbll.ViewAllCustomers();
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetVendorByVendorCat(int id)
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    Vendors_BLL objvendorbll = new Vendors_BLL();

                    return objvendorbll.GetByCatId(id);
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetCustomerByID(int id)
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    Customers_Property ObjCustomer_Property = new Customers_Property();
                    ObjCustomer_Property.idx = id;
                    Customers_BLL objvendorbll = new Customers_BLL(ObjCustomer_Property);

                    return objvendorbll.GetCustomerById();
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetAllDepartments()
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    Departments_BLL objDepartment = new Departments_BLL();

                    return objDepartment.ViewAll();
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable GetAllStockAndPrice(int productid)
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    LP_SalesOrder_BLL objSalesOrder = new LP_SalesOrder_BLL();

                    return objSalesOrder.GetStockandPrice(productid);
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }
        }

        //public DataTable GetAllHSCodes()
        //{
        //    objProduct = new Product_BLL();
        //    return objProduct.ViewAll();
        //}
        public DataTable ViewAllProduct()
        {
            if (Session["LOGGEDIN"] != null)
            {

                try
                {
                    Product_Property objProductProperty = new Product_Property();
                    //objProductProperty.branchIdx = 1;//user logged in session branchIdx
                    Product_BLL objProductBLL = new Product_BLL(objProductProperty);
                    return objProductBLL.ViewAll();


                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }

        }
        public DataTable ViewAllBranches()
        {
            if (Session["LOGGEDIN"] != null)
            {
                try
                {
                    Branch_Property objBranchProperty = new Branch_Property();
                    Branch_BLL objBranch = new Branch_BLL(objBranchProperty);
                    return objBranch.ViewAll();

                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }

        }

        [HttpPost]
        public DataTable GetAllSalesDetailsByIdx(int salesInvoiceIdx)
        {

            if (Session["LOGGEDIN"] != null)
            {
                try
                {


                    LP_SalesOrder_BLL objPIBLL = new LP_SalesOrder_BLL();

                    return objPIBLL.SelectSalesForSalesReturn(salesInvoiceIdx);
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }

        }


        public DataTable CheckProductInInventory(int salesInvoiceIdx)
        {

            if (Session["LOGGEDIN"] != null)
            {
                try
                {


                    LP_SalesOrder_BLL objPIBLL = new LP_SalesOrder_BLL();

                    return objPIBLL.CheckProductInInventory(salesInvoiceIdx);
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
            else
            {
                return new DataTable();
            }

        }
    }
}