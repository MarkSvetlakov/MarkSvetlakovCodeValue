using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public struct StoreProduct
{
    public string StoreId { get;  set; }
    public long ProductId { get;  set; }
    public double ProductPrice { get;  set; }
    public string PriceUpdateDate { get;  set; }

    public StoreProduct(string storeId, long productId, double productPrice, string priceUpdateDate)
    {
        this.StoreId = storeId;
        this.ProductId = productId;
        this.ProductPrice = productPrice;
        this.PriceUpdateDate = priceUpdateDate;
    }
}