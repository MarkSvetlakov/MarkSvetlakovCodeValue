using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public struct CalculateResultBuilder
{
    public string ChainName { get; private set; }
    public string StoreName { get; private set; }
    public List<Product> ProductsFromCart { get; private set; }
    public double TotalPrice { get; private set; }
    
    
    public CalculateResultBuilder(string chainName, string storeName, List<Product> productsFromCart, double totalPrice)
    {
        this.ChainName = chainName;
        this.StoreName = storeName;
        this.ProductsFromCart = productsFromCart;
        this.TotalPrice = totalPrice;
    }


    public IEnumerable<CalculateResultBuilder> BuildResult(List<CartLine> listOfCartItems)
    {
        StoreProductMapper storeProductsMapper = new StoreProductMapper();
        StoreMapper storeMapper = new StoreMapper();
        ChainMapper chainMapper = new ChainMapper();
        storeProductsMapper.FillStoreProductList();
        storeMapper.FillStoreList();
        chainMapper.FillChainList();
        

        List<CalculateResultBuilder> CalculatedList = new List<CalculateResultBuilder>();
        List<Product> products = new List<Product>();
        List<Chain> chains = new List<Chain>();
        List<Store> stores = new List<Store>();
        List<StoreProduct> storeProducts = new List<StoreProduct>();


        foreach (var item in listOfCartItems)
        {
            products.Add(item.Product);
        }


        foreach (var product in products)
        {
            storeProducts.AddRange(storeProductsMapper.StoreProductList
                .Where(p => p.ProductId == product.ProductId).ToList());
            stores.AddRange(storeMapper.StoreList
                .Where(p => storeProducts.Any(a => a.StoreId == p.StoreId.ToString().PadLeft(3, '0'))).ToList());
            chains.AddRange(chainMapper.ChainList
                .Where(p => stores.Any(a => a.ChainId == p.ChainId)).ToList());
        }


        chains = chains.GroupBy(x => x.ChainId)
            .Select(g => g.First()).ToList();

        stores = stores.GroupBy(x => x.StoreId)
            .Select(g => g.First()).ToList();



        foreach (var chain in chains)
        {
            foreach (var store in stores)
            {
                List<Product> newProductList = new List<Product>();
                if (chain.ChainId == store.ChainId)
                {
                    foreach (var storeProduct in storeProducts)
                    {
                        foreach (var item in products)
                        {
                            if (storeProduct.StoreId == store.StoreId.ToString().PadLeft(3, '0'))
                            {
                                if (item.ProductId == storeProduct.ProductId)
                                {
                                    Product product = item;
                                    product.ProductPrice = storeProduct.ProductPrice;
                                    newProductList.Add(product);
                                }
                            }
                        }
                    }
                    CalculatedList.Add(new CalculateResultBuilder(
                        chain.ChainName, 
                        store.StoreName, 
                        newProductList
                        .OrderBy(x => x.ProductPrice).ToList(),
                        newProductList
                        .Sum(x => x.ProductPrice)));
                }
            }
        }
        return CalculatedList;
    }
}