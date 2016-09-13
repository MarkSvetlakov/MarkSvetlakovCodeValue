using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class ChainMapper
{
    public List<Chain> ChainList { get; private set; }
    private DAL _dataAccessLayer;

    public ChainMapper()
    {
        _dataAccessLayer = new DAL();
        ChainList = new List<Chain>();
    }

    public bool AddChain(long id, string name)
    {
        string sql = $"INSERT INTO chains (chain_id, chain_name) VALUES ('{id}', N'{name}')";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public bool RemoveChain(long id)
    {
        string sql = $"DELETE FROM chains WHERE chain_id = '{id}'";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public bool RemoveChain(string name)
    {
        string sql = $"DELETE FROM chains WHERE chain_name = N'{name}'";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public string GetChainNameById(long id)
    {
        string sql = $"SELECT chain_name FROM chains WHERE chain_id = '{id}'";
        return (string)_dataAccessLayer.ExecuteScalar(sql);
    }


    public bool UpdateChain(long id, string name)
    {
        string sql = $"UPDATE chains SET chain_name = N'{name}' WHERE chain_id = '{id}'";
        return _dataAccessLayer.ExecuteNonQuery(sql);
    }


    public void FillChainList()
    {
        string sql = "SELECT * FROM chains";
        using (DataTable dataTable = _dataAccessLayer.GetDataSet(sql).Tables[0])
        {
            foreach (DataRow row in dataTable.Rows)
            {
                Chain chain = new Chain(long.Parse(row["chain_id"].ToString()), row["chain_name"].ToString());
                this.ChainList.Add(chain);
            }
        }
    }
}