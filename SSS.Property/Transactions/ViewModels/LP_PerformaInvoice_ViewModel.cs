using SSS.Property.Setups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SSS.Property.Transactions.ViewModels
{
    public class LP_PerformaInvoice_ViewModel
    {
        public int idx { get; set; }
        public string poNumber { get; set; }
        public int vendorIdx { get; set; }
        public int purchaseTypeIdx { get; set; }
        [DataType(DataType.Date)]
        public DateTime purchaseDate { get; set; }
        public string description { get; set; }
        public decimal totalAmount { get; set; }
        public decimal netAmount { get; set; }
        public decimal paidAmount { get; set; }
        public decimal balanceAmount { get; set; }
        public int paymentModeIdx { get; set; }
        public int bankIdx { get; set; }
        public string accorChequeNumber { get; set; }
        public DateTime paidDate { get; set; }
        public DateTime creationDate { get; set; }
        public int createdByUserIdx { get; set; }
        public DateTime lastModificationDate { get; set; }
        public int lastModifiedByUserIdx { get; set; }
        public int visible { get; set; }
        public int paymentStatus { get; set; }
        public string status { get; set; }
        public int isPaid { get; set; }
        public decimal discount { get; set; }
        public decimal tax { get; set; }
        public decimal taxAount { get; set; }
        public DateTime purchaseduedate { get; set; }
        public bool IsGRNLocked { get; set; }
        public int DepartmentID { get; set; }
        public string ContainerNo { get; set; }
        public string DocumentNumber { get; set; }

        public decimal unitPrice { get; set; }
        public decimal qty { get; set; }
        public decimal amount { get; set; }
        public decimal DVAmount { get; set; }
        public decimal ADVAmount { get; set; }
        public decimal TDVAmount { get; set; }
        public string ItemDescription { get; set; }
        public decimal ExchangeRate { get; set; }
        
        public string HSCode { get; set; }
        public List<Vendors_Property> VendorLST { get; set; }
        public List<LP_Purchase_Type> PurchaseType_List { get; set; }
        public List<Product_Property> ProductList { get; set; }

        public List<Departments_property> DepartmentList { get; set; }
        public List<PaymentMode_Property> Paymentmodelist { get; set; }//important to be Added
        public List<Company_Bank_Property> BankList { get; set; }
        public List<LP_Performa_Invoice_Details_Property> CommercialDetailList { get; set; }
    }
}
