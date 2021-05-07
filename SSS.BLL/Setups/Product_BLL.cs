﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSS.Property.Setups;
using SSS.DAL;
using SSS.DAL.Setups;
using System.Data;
using FluentValidation;

namespace SSS.BLL.Setups
{
    public class Product_BLL 
    {
        private Product_DAL objProduct_DAL;
        private Product_Property objProductProperty;

        private int? id;
        public Product_BLL()
        {

        }
        public Product_BLL(Product_Property objProduct_Property)
        {
            objProductProperty = objProduct_Property;

        }
        public Product_BLL(int? id)
        {
            this.id = id;
        }
        public DataTable ViewAll()
        {
            objProduct_DAL = new Product_DAL(objProductProperty);
            return objProduct_DAL.SelectAll();
        }
        public DataTable ddlProductType()
        {
            objProduct_DAL = new Product_DAL(objProductProperty);
            return objProduct_DAL.ddlProductType();
        }
        public DataTable ddlCategory()
        {
            objProduct_DAL = new Product_DAL(objProductProperty);
            return objProduct_DAL.ddlCategory();
        }
        public DataTable ddlSubCategory()
        {
            objProduct_DAL = new Product_DAL(objProductProperty);
            return objProduct_DAL.ddlSubCategory();
        }
        public DataTable ddlUnit()
        {
            objProduct_DAL = new Product_DAL(objProductProperty);
            return objProduct_DAL.ddlUnit();
        }
    
        public DataTable GetById(int? id)
        {
            objProduct_DAL = new Product_DAL(objProductProperty);
            return objProduct_DAL.SelectById(id);
        }
        public DataTable GetByTypeId(int id)
        {
            objProduct_DAL = new Product_DAL(objProductProperty);
            return objProduct_DAL.SelectByTypeId(id);
        }
        public bool Insert()
        {
            objProduct_DAL = new Product_DAL(objProductProperty);
            return objProduct_DAL.Insert();
        }
        public bool Update()
        {
            objProduct_DAL = new Product_DAL(objProductProperty);
            return objProduct_DAL.Update();
        }
        public bool Delete(int? id)
        {
            objProduct_DAL = new Product_DAL(objProductProperty);
            return objProduct_DAL.Delete(id);
        }
        public string GenerateProductCode(LP_GenerateTransNumber_Property objtransno)
        {
            string TransactionNumber = "";
            objProduct_DAL = new Product_DAL();
            DataTable dt = objProduct_DAL.GenerateProductCode(objtransno);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TransactionNumber = dr["TransNumber"].ToString();
                    TransactionNumber = "P-00" + TransactionNumber + "-" + objtransno.userid;


                }
                return TransactionNumber;
            }
            else
            {

                TransactionNumber = "P-001-" + objtransno.userid;

                return TransactionNumber;
            }
            //return _objMRNDAL.GenerateMRNNo(objtransno);
        }
    }
}
