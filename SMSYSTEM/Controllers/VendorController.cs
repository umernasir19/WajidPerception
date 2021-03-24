﻿using Newtonsoft.Json;
using SSS.BLL.Setups;
using SSS.Property.Setups;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSYSTEM.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        Vendors_BLL objVendorsBLL;
        Vendors_Property objVendorsProperty;
        public ActionResult ViewVendors()
        {
            return View();
        }

        public JsonResult GetAllVendors()
        {
            try
            {
                objVendorsProperty = new Vendors_Property();
                //objVendorsProperty.branchIdx = 1;//user logged in session branchIdx
                objVendorsBLL = new Vendors_BLL(objVendorsProperty);
                var Data = JsonConvert.SerializeObject(objVendorsBLL.ViewAll());
                return Json(new { data = Data, success = true, statuscode = 200, count = Data.Length }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddNewVendors(int? id)
        {
            objVendorsProperty = new Vendors_Property();
            objVendorsProperty.idx = Convert.ToInt32(id);
            //objVendorsProperty.branchIdx = 1;//It will have the value of session branchIdx
            objVendorsBLL = new Vendors_BLL(objVendorsProperty);
            DataTable categories = objVendorsBLL.ddlCategory();
            List<Vendor_Category_Property> catLST = new List<Vendor_Category_Property>();
   
            DataTable VendorTypes = objVendorsBLL.ddlVendorsType();
            List<VendorType_Property> VendorTypeLST = new List<VendorType_Property>();
            
            foreach (DataRow dr in categories.Rows)
            {
                Vendor_Category_Property objVendorsCat = new Vendor_Category_Property();
                objVendorsCat.vendorCategory = dr["vendorCategory"].ToString();
                objVendorsCat.idx = Convert.ToInt32(dr["idx"].ToString());
                catLST.Add(objVendorsCat);
            }
            ViewBag.catLST = catLST;
            
            foreach (DataRow dr in VendorTypes.Rows)
            {
                VendorType_Property objVendorsCat = new VendorType_Property();
                objVendorsCat.vendorType = dr["vendorType"].ToString();
                objVendorsCat.idx = Convert.ToInt32(dr["idx"].ToString());
                VendorTypeLST.Add(objVendorsCat);
            }
            ViewBag.VendorsTypeLST = VendorTypeLST;
            
            if (id != null && id != 0)
            {
                var dt = objVendorsBLL.GetById(id);
                //objVendorsProperty.companyIdx = 1;
                objVendorsProperty.idx = int.Parse(dt.Rows[0]["idx"].ToString());
                objVendorsProperty.vendorTypeIdx = int.Parse(dt.Rows[0]["vendorTypeIdx"].ToString());
                objVendorsProperty.vendorCatIdx = int.Parse(dt.Rows[0]["vendorCatIdx"].ToString());
           
                //objVendorsProperty.unitIdx = int.Parse(dt.Rows[0]["unitIdx"].ToString());
                objVendorsProperty.contact = (dt.Rows[0]["contact"].ToString());
                objVendorsProperty.vendorCode = (dt.Rows[0]["vendorCode"].ToString());
                objVendorsProperty.vendorName = (dt.Rows[0]["vendorName"].ToString());
                objVendorsProperty.address = (dt.Rows[0]["address"].ToString());
                //objVendorsProperty.Vendors_catIdx = int.Parse(dt.Rows[0]["Vendors_catIdx"].ToString());
                //objVendorsProperty.subCategory = dt.Rows[0]["subCategory"].ToString();
                //objVendorsProperty.HS_CodeSub = dt.Rows[0]["HS_CodeSub"].ToString();
                ////objVendorsProperty.firstName = dt.Rows[0]["firstName"].ToString();
                //objVendorsProperty.lastName = dt.Rows[0]["lastName"].ToString();
                //objVendorsProperty.CNIC = (dt.Rows[0]["CNIC"].ToString());
                //objVendorsProperty.cellNumber = (dt.Rows[0]["cellNumber"].ToString());
                //objVendorsProperty.loginId = (dt.Rows[0]["loginId"].ToString());
                //objVendorsProperty.password = dt.Rows[0]["password"].ToString();
            }


            return PartialView("_AddNewVendors", objVendorsProperty);
        }

        [HttpPost]
        public JsonResult AddUpdate(Vendors_Property objVendorsCategory)
        {
            try
            {
                if (objVendorsCategory.idx > 0)
                {

                    objVendorsCategory.lastModifiedByUserIdx = 1;
                    objVendorsCategory.lastModificationDate = DateTime.Now.ToString("dd/MM/yyyy");
                    objVendorsBLL = new Vendors_BLL(objVendorsCategory);

                    bool flag = objVendorsBLL.Update();
                    return Json(new { data = "Updated", success = flag, statuscode = 200 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //objVendorsCategory.companyIdx = 1;
                    objVendorsCategory.createdByUserIdx = 1;
                    objVendorsBLL = new Vendors_BLL(objVendorsCategory);
                    //if (objVendorsCategory.isMainBranch == 1)
                    //{
                    //    var check = objVendorsCategory.MainBranch();
                    //    if (check.Rows.Count > 0)
                    //    {
                    //        return Json(new { data = "Main Branch Already Exist", success = false, statuscode = 500 }, JsonRequestBehavior.AllowGet);
                    //    }
                    //}

                    bool flag = objVendorsBLL.Insert();
                    return Json(new { data = "Inserted", success = flag, statuscode = 200 }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Delete(int? id)
        {
            try
            {
                if (id > 0)
                {

                    Vendors_Property branchProperty = new Vendors_Property();
                    branchProperty.idx = int.Parse(id.ToString());
                    objVendorsBLL = new Vendors_BLL(id);
                    Vendors_BLL branhcBll = new Vendors_BLL(branchProperty);
                    var flag1 = branhcBll.Delete(id);
                    //if (flag1.Rows.Count > 0)
                    //{
                    if (true)
                    {
                        bool flag = objVendorsBLL.Delete(id);
                        return Json(new { data = "Deleted", success = flag, statuscode = 200 }, JsonRequestBehavior.AllowGet);
                    }
                    //else
                    //{
                    //    return Json(new { data = "Mian Branch Cannot be Delete ", success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
                    //}

                    //}
                    // return Json(new { data = "Process Completed ", success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);


                }
                else
                {
                    return Json(new { data = "Error Occur", success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}