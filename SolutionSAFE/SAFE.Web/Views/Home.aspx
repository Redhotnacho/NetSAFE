<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SAFE.Web.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <asp:Label ID="lBienvenido" runat="server" Text="Label">Home</asp:Label><br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Views/Empresa/index.aspx">Empresas index</asp:HyperLink><br />
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Views/Usuario/Index.aspx">Usuarios index</asp:HyperLink><br />
    
    <form id="form1" runat="server">
        <br />
        <div>
            <asp:Button ID="Bsalir" runat="server" OnClick="Bsalir_Click" Text="Cerrar sesión" Width="93px" />

        </div>
    </form>
</body>
</html>
