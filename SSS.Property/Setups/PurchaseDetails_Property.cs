﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSS.Property.Setups
{
    public class PurchaseDetails_Property
    {
        private int _idx;
        public int idx
        {
            get { return _idx; }
            set { _idx = value; }
        }

        private int _purchaseIdx;
        public int purchaseIdx
        {
            get { return _purchaseIdx; }
            set { _purchaseIdx = value; }
        }

        private int _productTypeIdx;
        public int productTypeIdx
        {
            get { return _productTypeIdx; }
            set { _productTypeIdx = value; }
        }
      
        private int _itemIdx;
        public int itemIdx
        {
            get { return _itemIdx; }
            set { _itemIdx = value; }
        }

        private decimal _unitPrice;
        public decimal unitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        private decimal _qty;
        public decimal qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        private decimal _openQty;
        public decimal openQty
        {
            get { return _openQty; }
            set { _openQty = value; }
        }

        private decimal _amount;
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
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

        private DateTime _DueDate;
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }

        private decimal _openItem;
        public decimal openItem
        {
            get { return _openItem; }
            set { _openItem = value; }
        }

        private string _ItemDescription;
        public string ItemDescription
        {
            get { return _ItemDescription; }
            set { _ItemDescription = value; }
        }

        private decimal _DVRate;
        public decimal DVRate
        {
            get { return _DVRate; }
            set { _DVRate = value; }
        }

        private decimal _TDVRate;
        public decimal TDVRate
        {
            get { return _TDVRate; }
            set { _TDVRate = value; }
        }

        private decimal _ADVRate;
        public decimal ADVRate
        {
            get { return _ADVRate; }
            set { _ADVRate = value; }
        }

    }
}
