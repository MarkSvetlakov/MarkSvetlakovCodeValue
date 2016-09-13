using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class StoreProductMapper
{
    public List<StoreProduct> StoreProductList { get; private set; }
    private DAL _dataAccessLayer;

    public StoreProductMapper()
    {
        _dataAccessLayer = new DAL();
        StoreProductList = new List<StoreProduct>();
    }

    public bool AddStoreProduct(string storeId, string productId, string productPrice, string priceUpdateDate)
    {
        string sql = $"INSERT INTO stores_products (store_id, product_id, product_price, price_update_date) VALUES ('{storeId}', '{productId}', '{productPrice}', '{priceUpdateDate}')";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public bool RemoveStoreProduct(string storeId, long productId)
    {
        string sql = $"DELETE FROM stores_products WHERE store_id = '{storeId}' AND product_id = '{productId}'";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public bool UpdateStoreProduct(string storeId, long productId, double productPrice, string priceUpdateDate)
    {
        string sql = $"UPDATE stores_products SET product_price = '{productPrice}', price_update_date = '{priceUpdateDate}'  WHERE store_id = '{storeId}' AND product_id = '{productId}'";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public void FillStoreProductList()
    {
        string sql = "SELECT * FROM stores_products";
        using (DataTable dataTable = _dataAccessLayer.GetDataSet(sql).Tables[0])
        {
            foreach (DataRow row in dataTable.Rows)
            {
                StoreProduct StoreProduct = new StoreProduct(row["store_id"].ToString(), long.Parse(row["product_id"].ToString()), double.Parse(row["product_price"].ToString()), row["price_update_date"].ToString());
                this.StoreProductList.Add(StoreProduct);
            }
        }
    }
}