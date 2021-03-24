using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSS.Property.Setups;
using SSS.Property.Transactions;
using SSS.DAL.Transactions;
using System.Data;

namespace SSS.BLL.Transactions
{
    public class LP_Voucher_BLL
    {
        LP_Voucher_Property objvoucherproperty;
        LP_Voucher_DAL objDAL;

        public LP_Voucher_BLL()
        {

        }

        public LP_Voucher_BLL(LP_Voucher_Property _objvoucherproperty)
        {
            objvoucherproperty = _objvoucherproperty;
        }

        public bool Insert()
        {
            objDAL = new LP_Voucher_DAL(objvoucherproperty);
            return objDAL.Insert();
        }

        public DataTable SelectAll()
        {
            objDAL = new LP_Voucher_DAL();
            return objDAL.SelectAll();
        }
        public DataTable SelectOne()
        {
            objDAL = new LP_Voucher_DAL(objvoucherproperty);
            return objDAL.SelectOne();
        }


        public string GenerateTransNo(LP_GenerateTransNumber_Property objtransno)
        {
            string TransactionNumber = "";
            objDAL = new LP_Voucher_DAL();
            DataTable dt = objDAL.GenerateVoucherNo(objtransno);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TransactionNumber = dr["TransNumber"].ToString();
                    TransactionNumber = "VR-00" + TransactionNumber + "-" + objtransno.userid;


                }
                return TransactionNumber;
            }
            else
            {

                TransactionNumber = "VR-001-" + objtransno.userid;

                return TransactionNumber;
            }
            //return _objMRNDAL.GenerateMRNNo(objtransno);
        }
    }
}
