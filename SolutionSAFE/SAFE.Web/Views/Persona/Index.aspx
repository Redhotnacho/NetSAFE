<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SAFE.Web.Views.Persona.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/jquery.dataTables.min" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Listar Personas</title>
</head>
<body>

    <asp:EntityDataSource ID="EDSPersona" runat="server" ConnectionString="name=SafeEntities" DefaultContainerName="SafeEntities" EnableFlattening="False" EntitySetName="SSF_PERSONA" EntityTypeFilter="SSF_PERSONA"></asp:EntityDataSource>



     <section class="row">
        <div class="col-md-10 offset-1">
            <h1>PERSONAS</h1>
            <form id="form2" runat="server">
                <asp:GridView ID="GvPersonas" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="EDSPersona" CssClass="table table-bordered" OnRowCreated="GvPersonas_RowCreated" PageSize="8" DataKeyNames="ID">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" Visible="False"></asp:BoundField>
                        <asp:TemplateField HeaderText="RUT" SortExpression="RUT">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Utilidad.FormatRutSalida(Eval("RUT").ToString()) %>' ID="Label1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NOMBRE" SortExpression="NOMBRE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# string.Format("{0} {1} {2}", Eval("NOMBRE"), Eval("AP_PATERNO"), Eval("AP_MATERNO")) %>' ID="Label2"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="AP_PATERNO" HeaderText="PATERNO" SortExpression="AP_PATERNO" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="AP_MATERNO" HeaderText="MATERNO" SortExpression="AP_MATERNO" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="CORREO" HeaderText="CORREO" SortExpression="CORREO"></asp:BoundField>
                        <asp:BoundField DataField="TELEFONO" HeaderText="TELÉFONO" SortExpression="TELEFONO"></asp:BoundField>
                        <asp:BoundField DataField="FECHA_NAC" HeaderText="NACIMIENTO" SortExpression="FECHA_NAC" DataFormatString="{0:dd/MM/yy}"></asp:BoundField>
                        <asp:BoundField DataField="FECH_CREACION" HeaderText="CREACIÓN" SortExpression="FECH_CREACION" DataFormatString="{0:dd/MM/yy}"></asp:BoundField>
                        <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" SortExpression="ESTADO" Visible="False"></asp:BoundField>
                        <asp:TemplateField HeaderText="CONTROLES">
                            <ItemTemplate>
                                <a href='<%#String.Format("{0}?editar={1}", "/Views/Persona/Editar.aspx", Eval("ID")) %>' class='btn-sm btn-primary'>EDITAR</a>
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
                    <asp:Button ID="BtnAgregar" runat="server" Text="AGREGAR" CssClass="btn-sm btn-primary" OnClick="BtnAgregar_Click" />
                </div>
            </form>
        </div>
    </section>




</body>
</html>
