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
    <asp:EntityDataSource ID="EDSEmpresas" runat="server" ConnectionString="name=SafeEntities" DefaultContainerName="SafeEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="SSF_EMPRESA" EntityTypeFilter="SSF_EMPRESA"></asp:EntityDataSource>
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
    <section class="row">
        <div class="col-md-10 offset-1">
            <form id='form1' runat='server' method='post'>
        <asp:GridView ID="GvEmpresas" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="EDSEmpresas" AllowPaging="True" AllowSorting="True" CssClass="table table-bordered" OnRowCreated="GvEmpresas_RowCreated" PageSize="5">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="FECH_CREACION" HeaderText="FECH_CREACION" SortExpression="FECH_CREACION" ApplyFormatInEditMode="True" ReadOnly="True" />
                <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" SortExpression="ESTADO" />
                <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE" SortExpression="NOMBRE" />
                <asp:BoundField DataField="TELEFONO" HeaderText="TELEFONO" SortExpression="TELEFONO" />
                <asp:BoundField DataField="DIRECCION" HeaderText="DIRECCION" SortExpression="DIRECCION" />
                <asp:TemplateField HeaderText="CONTROLES">
                    <ItemTemplate>
                        <a href='<%#String.Format("{0}?empresa={1}", "/Views/Empresa/Editar.aspx", Eval("ID")) %>' class='btn-sm btn-primary'>EDITAR</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BORRADO">
                    <ItemTemplate>
                        <asp:LinkButton ID="LbActivacion" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="LbActivacion_Click"
                            class='<%# String.Format("{0}", int.Parse(Eval("ESTADO").ToString()) == 1 ? "btn-sm btn-success" : "btn-sm btn-danger") %>'>
                            <%# String.Format("{0}", int.Parse(Eval("ESTADO").ToString()) == 1 ? "ACTIVADO" : "DESACTIVADO") %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>



                <br />
                <br />
                <br />
                <br />



                </form>
            </div>
        </section>
    <script src='<%=ResolveClientUrl("~/Scripts/Empresa/Listar.js") %>' type="text/javascript"></script>
</body>
</html>
