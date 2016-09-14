<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Site_Cart" MasterPageFile="~/Site/WebApp.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="content">
        <div class="contentHeader">
        סל קניות
            </div>
        <table class="tg" style="table-layout: fixed; min-width: 700px">

            <tr>
                <th class="tg-i3ll">שם מוצר</th>
                <th class="tg-8ua6">שם יצרן</th>
                <th class="tg-8ua6">כמות</th>
                <th class="tg-8ua6">פעולה</th>
            </tr>
            

            <asp:Repeater ID="Repeater1" ItemType="CartLine"
                SelectMethod="GetCartLines" runat="server" EnableViewState="false">

                <ItemTemplate>
                    <tr>
                        <td class="tg-4jb6"><%# Item.Product.ProductName %></td>
                        <td class="tg-4jb6"><%# Item.Product.ManufactureName%></td>
                        <td class="tg-4jb6"><%# Item.Quantity %></td>
                        <td class="tg-4jb6">
                            <button type="submit" class="btn" name="remove"
                                value="<%#Item.Product.ProductId %>">
                                להסיר</button>
                        </td>
                    </tr>
                </ItemTemplate>

            </asp:Repeater>
                

            <tr>
                <td class="tg-yw4l" colspan="2">
                    <asp:Button CssClass="btn" ID="BTNCalculate" runat="server" Text="לחשב" OnClick="CalculateClick" Visible="True" Enabled="True" EnableViewState="False" />
                    <asp:RadioButton ID="RadioBTNAllChains" GroupName="Calculate" runat="server" AutoPostBack="True" EnableViewState="False" ToolTip="Not all Products can be calculated !" />לפי כל רשתות
                    <asp:RadioButton ID="RadioBTNAllProducts" GroupName="Calculate" runat="server" AutoPostBack="True" EnableViewState="False" ToolTip="Not all Chains can be calculated !" />לפי כל סל קניות
                </td>
                <td class="tg-lqy6" colspan="2">
                    <asp:Button CssClass="btn" ID="BTNRemoveAll" runat="server" Text="להסיר הכל" OnClick="RemoveAllClick" />
                </td>
            </tr>
            
        </table>
    </div>
</asp:Content>
