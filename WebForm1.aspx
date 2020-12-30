<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TestWebCrawler.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" >
                <Columns>
                    <asp:ImageField DataImageUrlField="ItemPicture" HeaderText="圖片">
                        <ControlStyle Height="100px" Width="100px" />
                    </asp:ImageField>
                    <asp:BoundField DataField="ItemName" HeaderText="商品名稱" />
                    <asp:BoundField DataField="ItemFacts" HeaderText="商品介紹" />
                    <asp:BoundField DataField="ItemPrice" HeaderText="商品價格" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
