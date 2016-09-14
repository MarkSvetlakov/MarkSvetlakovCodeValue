using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Cart
{
    private List<CartLine> lineCollection = new List<CartLine>();


    public void AddItem(Product product)
    {
        CartLine line = lineCollection
            .Where(p => p.Product.ProductId == product.ProductId)
            .FirstOrDefault();
        if (line == null)
        {
            lineCollection.Add(new CartLine
            {
                Product = product
            });
        }
        else
        {
            lineCollection.Where(x => x.Product.ProductId == product.ProductId).FirstOrDefault().Quantity +=1;
        }
    }


    public void RemoveLine(Product product)
    {
        lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
    }


    public void RemoveAllLines()
    {
        lineCollection.Clear();
    }


    public decimal ComputeTotalValue()
    {
        return lineCollection.Sum(e => e.Product.Quantity * e.Quantity);
    }


    public void Clear()
    {
        lineCollection.Clear();
    }


    public IEnumerable<CartLine> Lines
    {
        get { return lineCollection; }
    }
}