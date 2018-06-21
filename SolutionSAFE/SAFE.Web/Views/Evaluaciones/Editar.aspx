<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="SAFE.Web.Views.Evaluaciones.Editar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/jquery.dataTables.min" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Editar Evaluaciones</title>
</head>
<body>
    <section class="container">
        <h2 class="page-header">EDITAR EVALUACIONES</h2>
        <form id="form1" runat="server">
            <div class="form-horizontal">

                <div class="form-group">
                    <label class="col-md-2 control-label">ID</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TbId" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="LerrorID" runat="server" Text="" CssClass="alert-danger"></asp:Label>
                    </div>
                    <asp:HiddenField ID="HdnID" runat="server" />
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label">NOMBRE</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TbNombre" runat="server"></asp:TextBox>
                    </div>
                </div>

               
                <div class="form-group">
                    <label class="col-md-2 control-label">FECHA NACIMIENTO</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TbFechaNac" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                </div>


                <asp:Label ID="Lerror" runat="server" Text="" CssClass="alert-danger"></asp:Label>
                <asp:Label ID="Lexito" runat="server" Text="" CssClass="text-success"></asp:Label>

                <div class="row">
                    <div class="col-md-2">
                        <asp:HyperLink ID="HlVolver" runat="server" NavigateUrl="~/Views/Persona/Index.aspx" CssClass="btn btn-block btn-default">Volver</asp:HyperLink>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="BtnModificar" runat="server" Text="Modificar" CssClass="btn btn-block btn-primary" OnClick="BtnModificar_Click" />
                    </div>
                </div>
            </div>

        </form>
    </section>
</body>
</html>

