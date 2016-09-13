using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class StoreMapper
{
    public List<Store> StoreList { get; private set; }
    private DAL _dataAccessLayer;


    public StoreMapper()
    {
        _dataAccessLayer = new DAL();
        StoreList = new List<Store>();
    }


    public bool AddStore(long chainId, string storeId, string storeName, string reportDate)
    {

        string sql = $"INSERT INTO stores (chain_id, store_id, store_name, report_date) VALUES ('{chainId}', '{storeId}', N'{storeName}', '{reportDate}')";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public bool RemoveStoreById(string storeId)
    {
        string sql = $"DELETE FROM stores WHERE store_id = '{storeId}'";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public bool RemoveStoreByName(string storeName)
    {
        string sql = $"DELETE FROM stores WHERE store_name = N'{storeName}'";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public string GetStoreNameById(string id)
    {
        string sql = $"SELECT store_name FROM stores WHERE store_id = '{id}'";
        return (string)_dataAccessLayer.ExecuteScalar(sql);
    }


    public string GetChainIdById(string id)
    {
        string sql = $"SELECT chain_id FROM stores WHERE store_id = '{id}'";
        return (string)_dataAccessLayer.ExecuteScalar(sql);
    }


    public bool UpdateStore(long chainId, string storeId, string storeName, string reportDate)
    {
        string sql = $"UPDATE stores SET store_name = N'{storeName}', report_date = '{reportDate}' WHERE chain_id = '{chainId}' AND store_id = '{storeId}'";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public void FillStoreList()
    {
        //TODO: add clear to all mappers?
        this.StoreList.Clear();
        string sql = "SELECT * FROM stores";
        using (DataTable dataTable = _dataAccessLayer.GetDataSet(sql).Tables[0])
        {
            foreach (DataRow row in dataTable.Rows)
            {
                Store store = new Store(long.Parse(row["chain_id"].ToString()), long.Parse(row["store_id"].ToString()), row["store_name"].ToString(), row["report_date"].ToString());
                this.StoreList.Add(store);
            }
        }
    }
}