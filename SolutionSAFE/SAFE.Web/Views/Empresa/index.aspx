<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="SAFE.Web.Views.Empresa.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/jquery.dataTables.min" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>

    <section class="row">
        <div class="col-md-10 offset-1">
            <h1>EMPRESAS</h1>
            <table id="ListaEmpresas" class="table table-bordered">
                <thead style="text-align: center">
                    <tr>
                        <th>ID</th>
                        <th>NOMBRE</th>
                        <th>TELEFONO</th>
                        <th>DIRECCION</th>
                        <th colspan="2">OPCIONES</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Label ID="lTabla" runat="server" Text="Label"></asp:Label>
                </tbody>
            </table>
        </div>
    </section>

    <script src='<%=ResolveClientUrl("~/Scripts/Empresa/Listar.js") %>' type="text/javascript"></script>
</body>
</html>
