using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_ChainList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected string CreateHomeLinkHtml()
    {
        return "<a href='Listing.aspx'>דף ראשי</a>";
    }
}