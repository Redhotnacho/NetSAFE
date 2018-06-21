<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agregar.aspx.cs" Inherits="SAFE.Web.Views.Evaluaciones.Agregar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/jquery.dataTables.min" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Agregar Evaluaciones</title>
</head>
<body>
    <section class="container">
        <h2 class="page-header">AGREGAR EVALUACIONES</h2>
        <form id="form1" runat="server">

            <div class="form-horizontal">

                <div class="form-group">
                    <label class="col-md-2 control-label">ID</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TbId" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="LerrorId" runat="server" Text="" CssClass="alert-danger"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">NOMBRE</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TbNombre" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">TIPO EVALUACIÓN</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TbTevaluacion" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">FECHA CREACIÓN</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TbFechaCrea" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <asp:Label ID="Lerror" runat="server" Text="" CssClass="alert-danger"></asp:Label>
                <asp:Label ID="Lexito" runat="server" Text="" CssClass="text-success"></asp:Label>

                <div class="row">
                    <div class="col-md-2">
                        <asp:HyperLink ID="HlVolver" runat="server" NavigateUrl="~/Views/Evaluaciones/Index.aspx" CssClass="btn btn-block btn-default">Volver</asp:HyperLink>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" CssClass="btn btn-block btn-primary" OnClick="BtnAgregar_Click" />
                    </div>
                </div>
            </div>

        </form>
    </section>
</body>
</html>
