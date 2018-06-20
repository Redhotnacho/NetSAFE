<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SAFE.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="~/adminto/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/adminto/css/core.css" rel="stylesheet" type="text/css" />
    <link href="~/adminto/css/components.css" rel="stylesheet" type="text/css" />
    <link href="~/adminto/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="~/adminto/css/pages.css" rel="stylesheet" type="text/css" />
    <link href="~/adminto/css/menu.css" rel="stylesheet" type="text/css" />
    <link href="~/adminto/css/responsive.css" rel="stylesheet" type="text/css" />

    <title></title>
</head>
<body>
        <div class="fondoPrincipal"></div>
        <div class="clearfix"></div>
        <div class="wrapper-page">
            
            <div class="m-t-40 card-box">
                <div class="col-lg-8">
                    <div class="text-center">
                        <h4 class="text-uppercase font-bold m-b-0">SAFE</h4>
                        <h4 class="text-uppercase font-bold m-b-0">Ingreso de usuario</h4>
                    </div>
                </div>
                <div class="col-lg-4" style="margin-bottom:20px;">
                    <div class="text-right">
                        <img src="../adminto/images/SAFE.png"/>
                    </div>
<<<<<<< HEAD
                </div>                
                <div class="panel-body">
                    <form id="form1" runat="server">
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="input text required">
                                    <asp:TextBox ID="loginname" runat="server" class="form-control" placeholder="Username" required="true"></asp:TextBox><br />
                                    <asp:Label ID="lloginname" runat="server" Text="" class="login-field-icon fui-user" for="loginname"></asp:Label>
                                </div>                    
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="input password required">
                                    <asp:TextBox ID="loginpass" runat="server" TextMode="Password" class="form-control" placeholder="Contraseña" required="true"></asp:TextBox><br />
                                    
                                </div>
                                <asp:Label ID="lloginpass" runat="server" Text="" for="loginpass"></asp:Label>
                            </div>                    
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <asp:Button ID="btnLogin" runat="server" Text="Acceder" class="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                            </div>
                        </div>
                    </form>
=======
                    <asp:Button ID="btnLogin" runat="server" Text="Ingresar" class="btn btn-primary btn-large btn-block" OnClick="btnLogin_Click" />
                    
                    


>>>>>>> ae9fbc96f4e51d5c207e47040e91cad1be515ee6
                </div>
            </div>  
        </div>
</body>
</html>
