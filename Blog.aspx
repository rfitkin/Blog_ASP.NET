<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="Githubapp.Blog" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" runat="server" href="site.css" type="text/css" />
    <script type="text/javascript">
            

            window.onload = function () {
                var div = document.getElementById("dvScroll");
                var div_position = document.getElementById("div_position");
                var position = parseInt('<%=Request.Form["div_position"] %>');
                if (isNaN(position)) {
                    position = 0;
                }
                div.scrollTop = position;
                div.onscroll = function () {
                    div_position.value = div.scrollTop;
                };
            };
    </script>
</head>
<body>
<form runat="server">
    <div class="container">
        <h1 style="font-family:'Helvetica Neue', Helvetica, Arial, sans-ser;">Blog</h1> 
    
        <asp:DropDownList ID="drpPosts" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="drpPosts_SelectedIndexChanged" CssClass="drpPostcss">
            <asp:ListItem>Last 7 Days</asp:ListItem>
            <asp:ListItem>Last 30 Days</asp:ListItem>
            <asp:ListItem>Last 365 Days</asp:ListItem>
            <asp:ListItem>All Time</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="AddPost" CssClass="buttonstyle buttonoption1" runat="server" Text="New Post" OnClick="AddPost_Click"/><br />


        <div id="dvScroll" class="WordWrap" style="margin-top:10px; margin-bottom:30px; display:inline-block; max-height: 300px; overflow-y:scroll;">
            <asp:GridView ID="GridView1" runat="server" SelectedRowStyle-BackColor="#2476DB" style=" width:100%; table-layout:fixed;" AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center" Font-Size="Medium" CellPadding="3" AutoGenerateSelectButton="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="#99CCFF" />
                    <Columns>
                
                        <asp:BoundField DataField="PostTitle" HeaderText="Title" SortExpression="PostTitle" ItemStyle-CssClass="WordBreak" HeaderStyle-CssClass="WordBreak" HeaderStyle-Width="45%" ItemStyle-Width="45%" />
                        <asp:BoundField DataField="DatePosted" HeaderText="Date Posted" SortExpression="DatePosted" ItemStyle-CssClass="WordBreak" HeaderStyle-CssClass="WordBreak" HeaderStyle-Width="10%" ItemStyle-Width="10%" DataFormatString = "{0:MM/dd/yyyy}"/>
                        <asp:BoundField DataField="PostContent" runat="server" HeaderText="Content" SortExpression="PostContent" ItemStyle-Wrap="false" ItemStyle-CssClass="WordBreak" HeaderStyle-CssClass="WordBreak" HeaderStyle-Width="15%" ItemStyle-Width="15%"></asp:BoundField>
                        <asp:BoundField DataField="PostAuthor" HeaderText="Author" SortExpression="PostAuthor" ItemStyle-CssClass="WordBreak" HeaderStyle-CssClass="WordBreak" HeaderStyle-Width="20%" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="PostId" HeaderText="PostId" SortExpression="PostId" ItemStyle-CssClass="WordBreak" HeaderStyle-CssClass="WordBreak" HeaderStyle-Width="5%" ItemStyle-Width="5%" />
             
                    </Columns> 
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="#3a4f63" Font-Bold="True" ForeColor="White" CssClass="header" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle HorizontalAlign="Left" VerticalAlign="Middle"></RowStyle>
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
      
            </asp:GridView>
        </div>
                 <input type="hidden" id="div_position" name="div_position" />
                 <asp:Button ID="Editbutton" CssClass="buttonstyle buttonoption1" runat="server" Text="EDIT" OnClick="Editbutton_Click"/>
                 <asp:Button ID="deletebutton" CssClass="buttonstyle buttonoption1" runat="server" Text="DELETE" OnClick="deletebutton_Click" OnClientClick="return confirm('Are you sure you want to delete?')" />
                 <div style="overflow-y:scroll; margin-top:10px; max-height:300px; border-top:solid thin; width:100%;">
                    <asp:FormView ID="frmPosts" runat="server">  
                        <ItemTemplate>  
                            <asp:Label ID="lblAuth" runat="server" Text='<%# Eval("PostAuthor") %>'></asp:Label><br />
                            <asp:Label ID="DatePosted" DataFormatString = "{0:MM/dd/yyyy}" runat="server" Text='<%# Eval("DatePosted") %>'></asp:Label><br /> 
                            <asp:Label ID="lblPost" runat="server" Text='<%# Eval("PostTitle") %>' Font-Size="Large"></asp:Label><br />  
                            <asp:Label ID="lblCont" runat="server" Text='<%# Eval("PostContent") %>' Font-Size="Medium"></asp:Label><br />  
                        </ItemTemplate>
                    </asp:FormView> 
                 </div>       
       </div>
</form>
</body>
</html>
