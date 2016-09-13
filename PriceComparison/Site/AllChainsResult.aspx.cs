using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site_Result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindItemsInCart((List<CalculateResultBuilder>)new CalculateResultBuilder().BuildResult((List<CartLine>)GetProductsInAllChains()));
    }


    private void BindItemsInCart(List<CalculateResultBuilder> ListOfSelectedProducts)
    {
        ListOfSelectedProducts = ListOfSelectedProducts
            .Where(x => x.ProductsFromCart.Count() == GetProductsInAllChains().Count()).ToList();

        if (!IsResultListEmpty(ListOfSelectedProducts))
        {
            this.Repeater1.DataSource = ListOfSelectedProducts.OrderBy(x => x.TotalPrice);
            this.Repeater1.DataBind();
        }
    }


    private bool IsResultListEmpty(List<CalculateResultBuilder> ListOfSelectedProducts)
    {
        if (!ListOfSelectedProducts.Any())
        {
            this.LBNothingFound.Visible = true;
            return true;
        }
        else
        {
            this.LBNothingFound.Visible = false;
            return false;
        }
    }


    public IEnumerable<CartLine> GetProductsInAllChains()
    {
        StoreProductMapper storeProductsMapper = new StoreProductMapper();
        storeProductsMapper.FillStoreProductList();
        StoreMapper storeMapper = new StoreMapper();
        storeMapper.FillStoreList();
        ChainMapper chainMapper = new ChainMapper();
        chainMapper.FillChainList();
        List<CartLine> productsFromCart = SessionManager.GetCart(Session).Lines.ToList();

        foreach (var product in productsFromCart)
        {
            var list = storeProductsMapper.StoreProductList
                        .Where(p => p.ProductId == product.Product.ProductId);

            var list2 = storeMapper.StoreList
                .Where(p => list.Any(a => a.StoreId == p.StoreId.ToString().PadLeft(3, '0')));

            var list3 = chainMapper.ChainList
                .Where(p => list2.Any(a => a.ChainId == p.ChainId));

            if (list3.Count() != chainMapper.ChainList.Count)
            {
                productsFromCart = productsFromCart
                    .Where(x => x.Product.ProductId != product.Product.ProductId).ToList();
            }
        }


        return productsFromCart;
    }
}