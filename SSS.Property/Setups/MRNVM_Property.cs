﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SSS.Property.Setups
{
    public class MRNVM_Property
    {
        public int idx { get; set; }
        public string mrNumber { get; set; }
        //[Required(ErrorMessage = "Please Select Vendor")]

        //public int vendorIdx { get; set; }
        //public int MRNIdx { get; set; }
        //public int POIdx { get; set; }
        //public int purchaseTypeIdx { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please Select Date")]
        public string mrnDate { get; set; }
        public string description { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        //public decimal totalAmount { get; set; }
        //public decimal netAmount { get; set; }
        //public string paidAmount { get; set; }
        //public string balanceAmount { get; set; }
        ////[Required(ErrorMessage = "Please Select Payment")]
        //public int paymentModeIdx { get; set; }
        //public int bankIdx { get; set; }
        //public string accorChequeNumber { get; set; }

        //[DataType(DataType.Date)]
        //public string paidDate { get; set; }
        public DateTime creationDate { get; set; }
        public int createdByUserIdx { get; set; }
        public string lastModificationDate { get; set; }
        public int lastModifiedByUserIdx { get; set; }
        public int visible { get; set; }
        public int paymentStatus { get; set; }
        public string status { get; set; }
        //public int isPaid { get; set; }
        //public decimal discount { get; set; }
        //public decimal tax { get; set; }
        //public decimal taxAount { get; set; }

        public int itemIdx { get; set; }
        //public decimal unitPrice { get; set; }
        public decimal qty { get; set; }
        //public decimal amount { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime DueDate { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime purchaseduedate { get; set; }

        //public List<Vendors_Property> VendorLST { get; set; }
        public string itemName { get; set; }
        public List<Product_Property> ProductList { get; set; }

        //public List<PaymentMode_Property> Paymentmodelist { get; set; }//important to be Added
        //public List<Company_Bank_Property> BankList { get; set; }
        public List<mrnDetails_Property> MrnDetailsLST { get; set; }
    }
}
