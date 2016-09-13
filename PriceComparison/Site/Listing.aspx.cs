using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site_Listing : System.Web.UI.Page
{
    XMLParser parser = new XMLParser();
    ProductMapper prMapper = new ProductMapper();


    protected void Page_Load(object sender, EventArgs e)
    {
        prMapper.FillProductList();

        if (!IsPostBack)
        {
            parser.GetAllChainsDirectory();
            parser.GetAllStoresDirectory();
            parser.GetAllFullPricesXML();
            parser.FillProductsFromXML();
            parser.FillStoresProductsFromXML();
            this.BindGrid(string.Empty);
        }
    }


    private void BindGrid(string value)
    {
        List<Product> resultList;
        if (string.IsNullOrEmpty(value))
        {
            resultList = prMapper.ProductList;
        }
        else
        {
            resultList = prMapper.ProductList.Where(x => (x.ProductName.Contains(value)) ||
            (x.ManufactureName.Contains(value)) ||
            (x.ProductId.ToString().Equals(value))
            ).ToList();
        }

        resultList = resultList.OrderBy(g => g.ManufactureName).ToList();
        this.GridView.DataSource = resultList;
        this.GridView.DataBind();
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView.PageIndex = e.NewPageIndex;
        this.BindGrid(this.TXBSearch.Text);
    }


    protected void Search(object sender, EventArgs e)
    {
        this.BindGrid(this.TXBSearch.Text);
    }


    protected void CencelSearch(object sender, EventArgs e)
    {
        this.TXBSearch.Text = "";
        this.BindGrid(string.Empty);
    }


    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = "קוד מוצר";
            e.Row.Cells[2].Text = "שם מוצר";
            e.Row.Cells[3].Text = "שם יצרן";
            e.Row.Cells[4].Text = "כמות ליחידה";
            e.Row.Cells[5].Text = "כמות";
            e.Row.Cells[6].Text = "מדוד יחידה";
        }

        HideUnnecessaryColumns(e);
        HighlightSearchRequest(e);
    }


    private void HideUnnecessaryColumns(GridViewRowEventArgs e)
    {
        int NumberOfColumns = 7;
        if (e.Row.Cells.Count > NumberOfColumns)
        {
            for (int i = NumberOfColumns; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
        }
    }


    private void HighlightSearchRequest(GridViewRowEventArgs e)
    {
        string searchResult = RemoveInvalidChars();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 1; i < 4; i++)
            {
                e.Row.Cells[i].Text = Regex.Replace(e.Row.Cells[i].Text, searchResult.Trim(), delegate (Match match)
                {
                    return $"<span style = 'background-color:#D9EDF7'>{match.Value}</span>";
                }, RegexOptions.IgnoreCase);
            }
        }
    }


    private string RemoveInvalidChars()
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in TXBSearch.Text)
        {
            if (!(c == '*' || c == '?' || c == '+'))
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = this.GridView.SelectedRow;

        string productId = RemoveSpanRegex(row.Cells[1].Text);
        string productName = RemoveSpanRegex(row.Cells[2].Text);
        string manufactureName = RemoveSpanRegex(row.Cells[3].Text);
        string unitQuantity = RemoveSpanRegex(row.Cells[4].Text);
        string quantity = RemoveSpanRegex(row.Cells[5].Text);
        string unitMeasure = RemoveSpanRegex(row.Cells[6].Text);

        SessionManager.GetCart(Session).AddItem(
            new Product(
                long.Parse(productId),
                productName,
                manufactureName,
                unitQuantity,
                int.Parse(quantity),
                unitMeasure,
                0));
    }


    private string RemoveSpanRegex(string value)
    {
        return Regex.Replace(value, @"<[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    }
}