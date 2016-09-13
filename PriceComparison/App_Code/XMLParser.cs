using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

public class XMLParser
{
    private const string _xmlPath = "~/App_Data/XML/";
    private const string _pricesPath = "Price/";
    private const string _fullPricesPath = "PriceFull/";
    private List<string> _chainsPath = new List<string>();
    private List<string> _storesPath = new List<string>();
    private List<string> _xmlNames = new List<string>();
    ProductMapper productMapper;
    ChainMapper chainMapper;
    StoreMapper storeMapper;
    StoreProductMapper storeProductMapper;
    

    public XMLParser()
    {
        productMapper = new ProductMapper();
        chainMapper = new ChainMapper();
        storeMapper = new StoreMapper();
        storeProductMapper = new StoreProductMapper();
        storeMapper.FillStoreList();
        productMapper.FillProductList();
        chainMapper.FillChainList();
        storeProductMapper.FillStoreProductList();
    }


    public void FillProductsFromXML()
    {
        string fullPath;
        foreach (var chainPath in _chainsPath)
        {
            foreach (var storePath in _storesPath)
            {
                foreach (var xmlName in _xmlNames)
                {
                    fullPath = HttpContext.Current.Server.MapPath($"{_xmlPath}{chainPath}{storePath}{_fullPricesPath}{xmlName}");

                    if (File.Exists(fullPath))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            ds.ReadXml(fullPath);
                            foreach (DataRow row in ds.Tables["product"].Rows)
                            {
                                productMapper.AddProduct(
                                    row["ItemCode"].ToString(),
                                    row["ItemName"].ToString(),
                                    row["ManufactureName"].ToString(),
                                    row["UnitQty"].ToString(),
                                    row["Quantity"].ToString(),
                                    row["UnitMeasure"].ToString());
                            }
                        }
                    }
                }
            }
        }
    }


    public void FillStoresProductsFromXML()
    {
        string storeId;
        string fullPath;
        foreach (var chainPath in _chainsPath)
        {
            foreach (var storePath in _storesPath)
            {
                foreach (var xmlName in _xmlNames)
                {
                    fullPath = HttpContext.Current.Server.MapPath($"{_xmlPath}{chainPath}{storePath}{_fullPricesPath}{xmlName}");

                    if (File.Exists(fullPath))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            ds.ReadXml(fullPath);

                            foreach (DataRow storeIdrow in ds.Tables["Prices"].Rows)
                            {
                                storeId = storeIdrow["StoreID"].ToString();

                                foreach (DataRow row in ds.Tables["product"].Rows)
                                {
                                    storeProductMapper.AddStoreProduct(
                                        storeId,
                                        row["ItemCode"].ToString(),
                                        row["ItemPrice"].ToString(),
                                        row["PriceUpdateDate"].ToString());
                                }
                            }
                            File.Delete(fullPath);
                        }
                    }
                }
            }
        }
    }


    public void GetAllChainsDirectory()
    {
        bool isValidChainId;
        long chainId;
        string chainPath;

        DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath(_xmlPath));
        DirectoryInfo[] directories =
            di.GetDirectories("*", SearchOption.TopDirectoryOnly);

        foreach (var item in directories)
        {
            chainPath = item.Name + "/";
            isValidChainId = long.TryParse(item.Name.Substring(item.Name.Length - 13), out chainId);

            if (isValidChainId)
            {
                foreach (var chain in chainMapper.ChainList)
                {
                    if (chain.ChainId == chainId)
                    {
                        _chainsPath.Add(chainPath);
                        break;
                    }
                }
            }
        }
    }


    public void GetAllStoresDirectory()
    {
        bool isValidStoreId;
        long StoreId;
        string storePath;

        foreach (var chainPath in _chainsPath)
        {
            DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath($"{_xmlPath}{chainPath}"));
            DirectoryInfo[] directories =
                di.GetDirectories("*", SearchOption.TopDirectoryOnly);

            foreach (var item in directories)
            {
                storePath = item.Name + "/";
                isValidStoreId = long.TryParse(item.Name.Substring(item.Name.Length - 3), out StoreId);

                if (isValidStoreId)
                {
                    foreach (var store in storeMapper.StoreList)
                    {
                        if (store.StoreId == StoreId)
                        {
                            _storesPath.Add(storePath);
                        }
                    }
                }
            }
        }
    }


    public void GetAllFullPricesXML()
    {
        string fullDirectory;
        foreach (var chainPath in _chainsPath)
        {
            foreach (var storePath in _storesPath)
            {
                fullDirectory = HttpContext.Current.Server.MapPath($"{_xmlPath}{chainPath}{storePath}{_fullPricesPath}");
                if (Directory.Exists(fullDirectory))
                {
                    DirectoryInfo di = new DirectoryInfo(fullDirectory);
                    FileInfo[] files =
                    di.GetFiles("*", SearchOption.TopDirectoryOnly);
                    foreach (var item in files)
                    {
                        _xmlNames.Add(item.Name);
                    }
                }
            }
        }
    }
}