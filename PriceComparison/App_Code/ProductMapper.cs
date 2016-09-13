using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


public class ProductMapper
{

    public List<Product> ProductList { get; private set; }
    private DAL _dataAccessLayer;


    public ProductMapper()
    {
        _dataAccessLayer = new DAL();
        ProductList = new List<Product>();
    }


    public bool AddProduct(
        string id,
        string productName,
        string manufactureName,
        string unitQuantity,
        string quantity,
        string unitMeasure)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("INSERT INTO products (product_id, product_name, manufacture_name, unit_quantity, quantity, unit_measure)");
        sql.Append($"VALUES ('{id}', N'{productName}', N'{manufactureName}', N'{unitQuantity}', '{quantity}', N'{unitMeasure}')");
        return _dataAccessLayer.ExecuteNonQuery(sql.ToString());
    }


    public bool RemoveProduct(long id)
    {
        string sql = $"DELETE FROM products WHERE product_id = '{id}'";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public bool RemoveProduct(string name)
    {
        string sql = $"DELETE FROM products WHERE product_name = N'{name}'";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public string GetProductNameById(long id)
    {
        string sql = $"SELECT product_name FROM products WHERE product_id = '{id}'";
        return (string)_dataAccessLayer.ExecuteScalar(sql);
    }


    public bool UpdateProduct(
        long id,
        string productName,
        string manufactureName,
        string unitQuantity,
        int quantity,
        string unitMeasure)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("UPDATE products");
        sql.Append($" SET product_name = N'{productName}', manufacture_name = N'{manufactureName}', unit_quantity = N'{unitQuantity}', quantity = '{quantity}', unit_measure = N'{unitMeasure}'");
        sql.Append($" WHERE product_id = '{id}'");
        return _dataAccessLayer.ExecuteNonQuery(sql.ToString());
    }


    public void FillProductList()
    {
        string sql = "SELECT * FROM products";

        using (DataTable dataTable = _dataAccessLayer.GetDataSet(sql).Tables[0])
        {
            foreach (DataRow row in dataTable.Rows)
            {
                long id;
                string productName;
                string manufactureName;
                string unitQuantity;
                int quantity;
                string unitMeasure;

                long.TryParse(row["product_id"].ToString(), out id);
                productName = row["product_name"].ToString();
                manufactureName = row["manufacture_name"].ToString();
                unitQuantity = row["unit_quantity"].ToString();
                int.TryParse(row["quantity"].ToString(), out quantity);
                unitMeasure = row["unit_measure"].ToString();

                Product product = new Product(id, productName, manufactureName, unitQuantity, quantity, unitMeasure, 0);
                this.ProductList.Add(product);
            }
        }
    }
}