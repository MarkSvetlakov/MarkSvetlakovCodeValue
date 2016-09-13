using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_CartTotal : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        cartQuantity.InnerText = SessionManager.GetCart(Session).Lines.Count().ToString();
        cartLink.HRef = "~/Site/Cart.aspx";
    }
}