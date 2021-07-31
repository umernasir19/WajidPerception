﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SSS.Property.Transactions
{
    public class LP_Quotation_Master_Property
    {
        public int salescheck { get; set; }
        public decimal salePrice { get; set; }
        private int _idx;
        public int idx
        {
            get { return _idx; }
            set { _idx = value; }
        }
        public int MRNIdx { get; set; }
        private string _qsNumber;
        public string qsNumber
        {
            get { return _qsNumber; }
            set { _qsNumber = value; }
        }

        private int _customerIdx;
        public int customerIdx
        {
            get { return _customerIdx; }
            set { _customerIdx = value; }
        }

        private int _purchaseTypeIdx;
        public int purchaseTypeIdx
        {
            get { return _purchaseTypeIdx; }
            set { _purchaseTypeIdx = value; }
        }

        private DateTime _quotationDate;
        public DateTime quotationDate
        {
            get { return _quotationDate; }
            set { _quotationDate = value; }
        }

        private string _description;
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        private decimal _totalAmount;
        public decimal totalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        private decimal _netAmount;
        public decimal netAmount
        {
            get { return _netAmount; }
            set { _netAmount = value; }
        }

        private decimal _paidAmount;
        public decimal paidAmount
        {
            get { return _paidAmount; }
            set { _paidAmount = value; }
        }

        private decimal _balanceAmount;
        public decimal balanceAmount
        {
            get { return _balanceAmount; }
            set { _balanceAmount = value; }
        }

        private int _paymentModeIdx;
        public int paymentModeIdx
        {
            get { return _paymentModeIdx; }
            set { _paymentModeIdx = value; }
        }

        private int _bankIdx;
        public int bankIdx
        {
            get { return _bankIdx; }
            set { _bankIdx = value; }
        }

        private string _accorChequeNumber;
        public string accorChequeNumber
        {
            get { return _accorChequeNumber; }
            set { _accorChequeNumber = value; }
        }

        private DateTime _paidDate;
        public DateTime paidDate
        {
            get { return _paidDate; }
            set { _paidDate = value; }
        }

        private DateTime _creationDate;
        public DateTime creationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        private int _createdByUserIdx;
        public int createdByUserIdx
        {
            get { return _createdByUserIdx; }
            set { _createdByUserIdx = value; }
        }

        //private DateTime _lastModificationDate;
        //public DateTime lastModificationDate
        //{
        //    get { return _lastModificationDate; }
        //    set { _lastModificationDate = value; }
        //}
        private string _lastModificationDate;
        public string lastModificationDate
        {
            get { return _lastModificationDate; }
            set { _lastModificationDate = value; }
        }

        private int _lastModifiedByUserIdx;
        public int lastModifiedByUserIdx
        {
            get { return _lastModifiedByUserIdx; }
            set { _lastModifiedByUserIdx = value; }
        }

        private int _visible;
        public int visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        private int _paymentStatus;
        public int paymentStatus
        {
            get { return _paymentStatus; }
            set { _paymentStatus = value; }
        }

        private string _status;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        private int _isPaid;
        public int isPaid
        {
            get { return _isPaid; }
            set { _isPaid = value; }
        }

        private decimal _discount;
        public decimal discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        private decimal _tax;
        public decimal tax
        {
            get { return _tax; }
            set { _tax = value; }
        }

        private decimal _taxAount;
        public decimal taxAount
        {
            get { return _taxAount; }
            set { _taxAount = value; }
        }       
      
        public string subject { get; set; }

        private DateTime _purchaseduedate;
        public DateTime purchaseduedate
        {
            get { return _purchaseduedate; }
            set { _purchaseduedate = value; }
        }
        private DataTable _detail_data;
        public DataTable DetailData
        {
            get
            {
                return _detail_data;
            }
            set
            {
                _detail_data = value;
            }
        }
        private string _tableName;
        public String TableName
        {
            get
            {
                return _tableName;
            }
            set
            {
                _tableName = value;
            }
        }
        private DataTable _sales_tax_data;
        public DataTable SalesTaxData
        {
            get
            {
                return _sales_tax_data;
            }
            set
            {
                _sales_tax_data = value;
            }
        }
       
        private string _reference;
        public string reference
        {
            get
            {
                return _reference;
            }
            set
            {
                _reference = value;
            }
        }

        public string ItemDescription { get; set; }
        public decimal shippingCost { get; set; } //Added By Arsalan
    }
}
