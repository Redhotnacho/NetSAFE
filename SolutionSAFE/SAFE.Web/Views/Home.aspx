<%@ Page Language="C#" MasterPageFile="~/menu.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SAFE.Web.Home" %>


<<<<<<< HEAD
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-10 col-md-offset-1">
        <div class="panel panel-color panel-primary">
            <div class="panel-heading" >
                <h3 class="panel-title" style="text-align: center; font-size: 25px;">DashBoard</h3>
            </div>
            <div class="panel-body">
                <asp:Label ID="lBienvenido" runat="server" Text="Label">Home</asp:Label><br />
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Views/Empresa/index.aspx">Empresas index</asp:HyperLink><br />
                <form id="form1" runat="server">
                    <br />
                    <div>
                        <asp:Button ID="Bsalir" runat="server" OnClick="Bsalir_Click" Text="Cerrar sesión" Width="93px" />
=======
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
>>>>>>> ae9fbc96f4e51d5c207e47040e91cad1be515ee6

                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
