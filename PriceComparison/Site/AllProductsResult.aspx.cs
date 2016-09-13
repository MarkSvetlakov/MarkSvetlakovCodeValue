using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site_AllProductsResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindItemsInCart((List<CalculateResultBuilder>)new CalculateResultBuilder().BuildResult(SessionManager.GetCart(Session).Lines.ToList()));
    }


    private void BindItemsInCart(List<CalculateResultBuilder> ListOfSelectedProducts)
    {
        ListOfSelectedProducts = ListOfSelectedProducts
            .Where(x => x.ProductsFromCart.Count() == SessionManager.GetCart(Session).Lines.Count()).ToList();

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
}