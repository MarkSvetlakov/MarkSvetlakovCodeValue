<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllProductsResult.aspx.cs" Inherits="Site_AllProductsResult" MasterPageFile="~/Site/WebApp.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="content">
        <h2>תוצאה</h2>

        <table class="tg" style="table-layout: fixed; min-width: 600px">

            <tr>
                <th class="tg-i3ll">שם רשת</th>
                <th class="tg-8ua6">שם חנות</th>
            </tr>
            <tr>
                <td class="not_found_label" colspan="2">
                    <asp:Label ID="LBNothingFound" runat="server" Visible="False" EnableViewState="False"></asp:Label>
                </td>
            </tr>

            <asp:Repeater ID="Repeater1" runat="server" EnableViewState="false">

                <ItemTemplate>
                    <tr>
                        <td class="tg-52z3"><%# Eval("ChainName") %></td>
                        <td class="tg-52z3"><%# Eval("StoreName") %></td>
                    </tr>
                    <tr>
                        <td class="tg-0vgo">מוצרים</td>
                        <td class="tg-0vgo">מחיר</td>
                    </tr>

                    <asp:Repeater runat="server" ID="ordersRepeater" EnableViewState="false"
                        DataSource='<%# DataBinder.Eval(Container.DataItem, "ProductsFromCart") %>'>
                        <ItemTemplate>
                            <tr>
                                <td class="tg-yw4l"><%# DataBinder.Eval(Container.DataItem, "ProductName") %></td>
                                <td class="tg-yw4l"> <%# DataBinder.Eval(Container.DataItem, "ProductPrice") %> &#8362;</td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr>
                        <td class="tg-w0dk" colspan="2"> סה"כ <%# Eval("TotalPrice") %> &#8362;</td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <p class="actionButtons">
            <a href="Cart.aspx" class="btn">חזרה</a>
        </p>
    </div>
</asp:Content>
