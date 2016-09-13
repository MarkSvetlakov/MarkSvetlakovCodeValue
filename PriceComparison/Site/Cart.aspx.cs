using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site_Cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            long productId;
            if (long.TryParse(Request.Form["remove"], out productId))
            {
                CartLine line =
                    SessionManager.GetCart(Session).Lines
                    .Where(p => p.Product.ProductId == productId).FirstOrDefault();
                Product ProductToRemove = line.Product;
                if (!ProductToRemove.Equals(null))
                {
                    SessionManager.GetCart(Session).RemoveLine(ProductToRemove);
                }
            }
        }
    }


    protected void CalculateClick(object sender, EventArgs e)
    {
        if (SessionManager.GetCart(Session).Lines.Any())
        {
            if (this.RadioBTNAllChains.Checked)
            {
                Response.Redirect("AllChainsResult.aspx");
            }
            else if (this.RadioBTNAllProducts.Checked)
            {
                Response.Redirect("AllProductsResult.aspx");
            }
            else
            {
                Response.Redirect("Result.aspx");
            }
        }
    }


    protected void RemoveAllClick(object sender, EventArgs e)
    {
        SessionManager.GetCart(Session).RemoveAllLines();
    }


    public IEnumerable<CartLine> GetCartLines()
    {
        return SessionManager.GetCart(Session).Lines;
    }

    //TODO: do something
    //public decimal CartTotal
    //{
    //    get
    //    {
    //        return SessionManager.GetCart(Session).ComputeTotalValue();
    //    }
    //}
}