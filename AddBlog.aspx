<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBlog.aspx.cs" Inherits="Githubapp.AddBlog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" href="site.css" type="text/css" />
    <title></title>
</head>
<body>
    <form runat="server">
        <div class="container">
        
        <label class="shorttxt">Author: </label>  
            <asp:TextBox ID="txtAuthor" CssClass="longtxt" MaxLength="60" style="margin-top:10px; margin-bottom:10px;" required="" runat="server" TextMode="SingleLine" AutoCompleteType="Disabled" AutoPostBack="True"></asp:TextBox><br />
        <label class="shorttxt">Title: </label>  
            <asp:TextBox ID="txtTitle" CssClass="longtxt" MaxLength="60" style="margin-top:10px; margin-bottom:10px;" required="" runat="server" TextMode="SingleLine" AutoCompleteType="Disabled" AutoPostBack="True"></asp:TextBox><br />  
         <label class="shorttxt" style="margin-top:5px;">Content:</label>       
            <textarea runat="server"  id="mytextarea" class="longtxt" style="height:400px;"></textarea><br />
        <label class="shorttxt" style="margin-top:5px;">Expiration Date:</label>
            <input runat="server" id="exdate" class="logintxt" name="exdate" required="" type="date" />
        <div style="margin-top:5px;">
            <asp:Button ID="addbtn" CssClass="buttonstyle buttonoption1" runat="server" Text="Add Post" OnClick="addbtn_Click" />  
            <asp:Button ID="Cancelbutton" CssClass="buttonstyle buttonoption1" runat="server" Text="Cancel" OnClientClick="javascript:window.location.href='Blog.aspx'; return false;" />
        </div>
            
    </div>
    </form>
</body>
</html>
