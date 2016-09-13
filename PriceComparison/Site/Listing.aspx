<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listing.aspx.cs" Inherits="Site_Listing" MasterPageFile="~/Site/WebApp.Master"%>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <div id="content">
    <asp:Panel ID="Panel1" runat="server" Height="600px" ViewStateMode="Inherit">

        <div id="search">
        <asp:Label ID="LBSearch" runat="server" Text="חיפוש"></asp:Label>
        <asp:TextBox ID="TXBSearch" runat="server"></asp:TextBox>
        <asp:Button CssClass="btn" ID="BTNSearch" runat="server" Text="הצג" OnClick="Search" />
        <asp:Button CssClass="btn" ID="BTNClear" runat="server" Text="נקה" OnClick="CencelSearch" />
            </div>
        

            <asp:GridView ID="GridView" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="True" PageSize="25" OnRowDataBound="OnRowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:ButtonField ControlStyle-CssClass="btn" ButtonType="Button" CommandName="Select" HeaderText="פעולה" ShowHeader="True" Text="הוסף לסל" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#343434" Font-Bold="True" ForeColor="White" />
                <PagerSettings Mode="NumericFirstLast" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>

        <br />
        <br />
        </asp:Panel>
        </div>
    </asp:Content>
