<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SAFE.Web.Views.CapEmpresa.Index" %>

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
    <asp:EntityDataSource ID="EDSCapEmpresa" runat="server" ConnectionString="name=SafeEntities" DefaultContainerName="SafeEntities" EnableFlattening="False" EntitySetName="SSF_CAPACITACIONEMPRESA" EntityTypeFilter="SSF_CAPACITACIONEMPRESA"></asp:EntityDataSource>



     <section class="row">
        <div class="col-md-10 offset-1">
            <h1>Capacitaciones y Empresas</h1>
            <form id="form2" runat="server">
                <asp:GridView ID="GvCapEmpresas" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="EDSCapEmpresa" CssClass="table table-bordered" OnRowCreated="GvCapEmpresas_RowCreated" PageSize="8" DataKeyNames="ID">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" SortExpression="ESTADO" Visible="False"></asp:BoundField>
                        <asp:TemplateField HeaderText="CAPACITACIÓN" SortExpression="ID_CAPACITACION">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Find(Eval("ID").ToString()).SSF_CAPACITACION.NOMBRE %>' ID="Label1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ESTADO CAPACITACIÓN" SortExpression="ID_ESTADOCAPACITACION">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Find(Eval("ID").ToString()).SSF_ESTADOCAPAEMPRESA.ESTADOCAPAEMP %>' ID="Label3"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EMPRESA" SortExpression="ID_EMPRESA">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Find(Eval("ID").ToString()).SSF_EMPRESA.NOMBRE %>' ID="Label2"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="USUARIO" SortExpression="ID_USUARIO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Find(Eval("ID").ToString()).SSF_USUARIO.USERNAME %>' ID="Label4"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="CANTIDAD_ALUMNOS" HeaderText="N&#176; ALUMNOS" SortExpression="CANTIDAD_ALUMNOS"></asp:BoundField>
                        <asp:BoundField DataField="FECH_CREACION" HeaderText="CREACI&#211;N" SortExpression="FECH_CREACION" DataFormatString="{0:dd/MM/yy}"></asp:BoundField>
                        <asp:TemplateField HeaderText="CONTROLES">
                            <ItemTemplate>
                                <a href='<%#String.Format("{0}?editar={1}", "/Views/CapEmpresa/Editar.aspx", Eval("ID")) %>' class='btn-sm btn-primary'>EDITAR</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BORRADO">
                            <ItemTemplate>
                                <asp:LinkButton ID="LbActivacion" runat="server" CommandArgument='<%# Eval("ID").ToString() %>' OnClick="LbActivacion_Click"
                                    class='<%# String.Format("{0}", int.Parse(Eval("ESTADO").ToString()) == 1 ? "btn-sm btn-success" : "btn-sm btn-danger") %>'>
                            <%# String.Format("{0}", int.Parse(Eval("ESTADO").ToString()) == 1 ? "ACTIVO" : "DESACTIVADO") %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <div class="col-md-2">
                    <asp:Button ID="BtnAgregar" runat="server" Text="AGREGAR" CssClass="btn-sm btn-primary" OnClick="BtnAgregar_Click" />
                </div>
            </form>
        </div>
    </section>

</body>
</html>
