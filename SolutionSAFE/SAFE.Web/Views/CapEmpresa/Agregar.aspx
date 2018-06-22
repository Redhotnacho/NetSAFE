<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agregar.aspx.cs" Inherits="SAFE.Web.Views.CapEmpresa.Agregar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/jquery.dataTables.min" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Agregar Capacitación para Empresa</title>
</head>
<body>
    <asp:EntityDataSource ID="EDSCapacitacion" runat="server" ConnectionString="name=SafeEntities" DefaultContainerName="SafeEntities" EnableFlattening="False" EntitySetName="SSF_CAPACITACION" EntityTypeFilter="SSF_CAPACITACION" Select="it.[ID], it.[NOMBRE]"></asp:EntityDataSource>
    <asp:EntityDataSource ID="EDSEmpresa" runat="server" ConnectionString="name=SafeEntities" DefaultContainerName="SafeEntities" EnableFlattening="False" EntitySetName="SSF_EMPRESA" EntityTypeFilter="SSF_EMPRESA" Select="it.[ID], it.[NOMBRE]"></asp:EntityDataSource>
    <asp:EntityDataSource ID="EDSEstadoCap" runat="server" ConnectionString="name=SafeEntities" DefaultContainerName="SafeEntities" EnableFlattening="False" EntitySetName="SSF_ESTADOCAPAEMPRESA" EntityTypeFilter="SSF_ESTADOCAPAEMPRESA" Select="it.[ID], it.[ESTADOCAPAEMP]"></asp:EntityDataSource>


    <section class="container">
        <h2 class="page-header">AGREGAR CAPACITACIÓN EN EMPRESA</h2>
        <form id="form1" runat="server">

            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-md-2 control-label">CAPACITACIÓN</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="DdlCapacitacion" runat="server" CssClass="" DataSourceID="EDSCapacitacion" DataTextField="NOMBRE" DataValueField="ID">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">ESTADO CAPACITACIÓN</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="DdlEstadoCap" runat="server" CssClass="" DataSourceID="EDSEstadoCap" DataTextField="ESTADOCAPAEMP" DataValueField="ID">
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
                    <label class="col-md-2 control-label">USUARIO</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="DdlUsuario" runat="server" CssClass="dropdown" Enabled="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">CANTIDAD ALUMNOS</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TbNroAlumnos" runat="server" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                
                <asp:Label ID="Lerror" runat="server" Text="" CssClass="alert-danger"></asp:Label>
                <asp:Label ID="Lexito" runat="server" Text="" CssClass="text-success"></asp:Label>

                <div class="row">
                    <div class="col-md-2 ">
                        <asp:HyperLink ID="HlVolver" runat="server" NavigateUrl="~/Views/CapEmpresa/Index.aspx" CssClass="btn btn-block btn-default">Volver</asp:HyperLink>
                    </div>
                    <div class="col-md-2 ">
                        <asp:Button ID="BtnGuardar" runat="server" Text="Agregar" CssClass="btn btn-block btn-primary" OnClick="BtnGuardar_Click"/>
                    </div>
                </div>
            </div>

        </form>
    </section>
</body>
</html>
