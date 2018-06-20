<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agregar.aspx.cs" Inherits="SAFE.Web.Views.Usuario.Agregar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/jquery.dataTables.min" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Agregar Usuario</title>
</head>
<body>
    <asp:EntityDataSource ID="EDSPerfil" runat="server" ConnectionString="name=SafeEntities" DefaultContainerName="SafeEntities" EnableFlattening="False" EntitySetName="SSF_PERFIL" EntityTypeFilter="SSF_PERFIL" Select="it.[ID], it.[PERFIL]"></asp:EntityDataSource>
    <asp:EntityDataSource ID="EDSEmpresa" runat="server" ConnectionString="name=SafeEntities" DefaultContainerName="SafeEntities" EnableFlattening="False" EntitySetName="SSF_EMPRESA" EntityTypeFilter="SSF_EMPRESA" Select="it.[ID], it.[NOMBRE]"></asp:EntityDataSource>


    <section class="container">
        <h2 class="page-header">AGREGAR USUARIO</h2>
        <form id="form1" runat="server">

            <div class="form-horizontal">

                <div class="form-group">
                    <label class="col-md-2 control-label">PERSONA</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TbBuscarPersona" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="BtnBuscarPersona" runat="server" Text="Buscar Persona" CssClass="btn btn-block btn-primary" OnClick="BtnBuscarPersona_Click" />
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="DdlPersona" runat="server" CssClass="dropdown" Enabled="False" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="0">&lt;Seleccione Persona&gt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="LerrorIDPers" runat="server" Text="" CssClass="alert-danger"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">PERFIL</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="DdlPerfil" runat="server" CssClass="dropdown" DataSourceID="EDSPerfil" DataTextField="PERFIL" DataValueField="ID">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">EMPRESA</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="DdlEmpresa" runat="server" CssClass="dropdown" DataSourceID="EDSEmpresa" DataTextField="NOMBRE" DataValueField="ID">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label">USERNAME</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TbUsername" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">CONTRASEÑA</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TbContrasena" runat="server" TextMode="Password"></asp:TextBox><br />
                        <label class="col-md-2 control-label">Repetir Contraseña</label><br />
                        <asp:TextBox ID="TbContrasena2" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <asp:Label ID="Lerror" runat="server" Text="" CssClass="alert-danger"></asp:Label>
                <asp:Label ID="Lexito" runat="server" Text="" CssClass="text-success"></asp:Label>

                <div class="row">
                    <div class="col-md-2">
                        <asp:HyperLink ID="HlVolver" runat="server" NavigateUrl="~/Views/Usuario/Index.aspx" CssClass="btn btn-block btn-default">Volver</asp:HyperLink>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" CssClass="btn btn-block btn-primary" OnClick="BtnAgregar_Click"/>
                    </div>
                </div>
            </div>

        </form>
    </section>
</body>
</html>
