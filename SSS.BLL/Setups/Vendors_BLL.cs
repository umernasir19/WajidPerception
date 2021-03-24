using SSS.BLL.Setups;
using SSS.DAL.Setups;
using SSS.Property.Setups;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SSS.BLL.Setups
{
    public class Vendors_BLL
    {
        private Vendors_DAL objVendors_DAL;
        private Vendors_Property objVendorsProperty;

        private int? id;

        public Vendors_BLL()
        {
            

        }
        public Vendors_BLL(Vendors_Property objVendors_Property)
        {
            objVendorsProperty = objVendors_Property;

        }
        public Vendors_BLL(int? id)
        {
            this.id = id;
        }
        public DataTable ViewAll()
        {
            objVendors_DAL = new Vendors_DAL(objVendorsProperty);
            return objVendors_DAL.SelectAll();
        }
        public DataTable ddlVendorsType()
        {
            objVendors_DAL = new Vendors_DAL(objVendorsProperty);
            return objVendors_DAL.ddlVendorsType();
        }
        public DataTable ddlCategory()
        {
            objVendors_DAL = new Vendors_DAL(objVendorsProperty);
            return objVendors_DAL.ddlCategory();
        }

        public DataTable GetByCatId(int? id)
        {
            objVendors_DAL = new Vendors_DAL(objVendorsProperty);
            return objVendors_DAL.SelectByCatId(id);
        }

        public DataTable GetById(int? id)
        {
            objVendors_DAL = new Vendors_DAL(objVendorsProperty);
            return objVendors_DAL.SelectById(id);
        }
        public bool Insert()
        {
            objVendors_DAL = new Vendors_DAL(objVendorsProperty);
            return objVendors_DAL.Insert();
        }
        public bool Update()
        {
            objVendors_DAL = new Vendors_DAL(objVendorsProperty);
            return objVendors_DAL.Update();
        }
        public bool Delete(int? id)
        {
            objVendors_DAL = new Vendors_DAL(objVendorsProperty);
            return objVendors_DAL.Delete(id);
        }
    }
}
