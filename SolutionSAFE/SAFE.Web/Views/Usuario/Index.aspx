<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SAFE.Web.Views.Usuario.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/jquery.dataTables.min" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Listar Usuarios</title>
</head>
<body>
    <asp:EntityDataSource ID="EDSUsuario" runat="server" ConnectionString="name=SafeEntities" DefaultContainerName="SafeEntities" EnableFlattening="False" EntitySetName="SSF_USUARIO" EntityTypeFilter="SSF_USUARIO"></asp:EntityDataSource>

    <section class="row">
        <div class="col-md-10 offset-1">
            <h1>USUARIOS</h1>
            <form id="form1" runat="server">
                <asp:GridView ID="GvUsuarios" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="EDSUsuario" CssClass="table table-bordered" OnRowCreated="GvUsuarios_RowCreated" PageSize="6">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" Visible="False" />
                        <asp:BoundField DataField="FECH_CREACION" HeaderText="FECH_CREACION" SortExpression="FECH_CREACION" ReadOnly="true" DataFormatString="{0:dd/MM/yy}" />
                        <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" SortExpression="ESTADO" Visible="False" />
                        <asp:BoundField DataField="USERNAME" HeaderText="USERNAME" SortExpression="USERNAME" />
                        <asp:BoundField DataField="CONTRASENA" HeaderText="CONTRASENA" SortExpression="CONTRASENA" />
                        <asp:TemplateField HeaderText="PERSONA" SortExpression="ID_PERSONA">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# String.Format("Rut: {0} - Nombre: {1} {2}",  Find(Eval("ID").ToString()).SSF_PERSONA.RUT, Find(Eval("ID").ToString()).SSF_PERSONA.NOMBRE, Find(Eval("ID").ToString()).SSF_PERSONA.AP_PATERNO) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PERFIL" SortExpression="ID_PERFIL">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# String.Format("{0}",  Find(Eval("ID").ToString()).SSF_PERFIL.PERFIL) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EMPRESA" SortExpression="ID_EMPRESA">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# String.Format("{0}",  Find(Eval("ID").ToString()).SSF_EMPRESA.NOMBRE) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CONTROLES">
                            <ItemTemplate>
                                <a href='<%#String.Format("{0}?editar={1}", "/Views/Usuario/Editar.aspx", Eval("ID")) %>' class='btn-sm btn-primary'>EDITAR</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BORRADO">
                            <ItemTemplate>
                                <asp:LinkButton ID="LbActivacion" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="LbActivacion_Click"
                                    class='<%# String.Format("{0}", int.Parse(Eval("ESTADO").ToString()) == 1 ? "btn-sm btn-success" : "btn-sm btn-danger") %>'>
                            <%# String.Format("{0}", int.Parse(Eval("ESTADO").ToString()) == 1 ? "ACTIVO" : "DESACTIVADO") %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="col-md-2">
                    <asp:Button ID="BtnAgregar" runat="server" Text="AGREGAR" CssClass="btn-sm btn-primary" OnClick="BtnAgregar_Click"/>
                </div>
            </form>
        </div>
    </section>
</body>
</html>
