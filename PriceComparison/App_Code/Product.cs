using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public struct Product
{
    public long ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string ManufactureName { get; private set; }
    public string UnitQuantity { get; private set; }
    public int Quantity { get; set; }
    public string UnitMeasure { get; private set; }
    public double ProductPrice { get; set; }

    public Product(
        long id, 
        string productName, 
        string manufactureName, 
        string unitQuantity, 
        int quantity, 
        string unitMeasure,
        double price)
    {
        this.ProductId = id;
        this.ProductName = productName;
        this.ManufactureName = manufactureName;
        this.UnitQuantity = unitQuantity;
        this.Quantity = quantity;
        this.UnitMeasure = unitMeasure;
        this.ProductPrice = price;
    }
}