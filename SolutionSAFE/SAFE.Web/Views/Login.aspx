<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SAFE.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Content/Login.css" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login">
            <div class="login-screen">
                <div class="app-title">
                    <h1>Login</h1>
                </div>

                <div class="login-form">
                    <p class="" id="mensaje"></p>
                    <div class="control-group">
                        <asp:TextBox ID="loginname" runat="server" class="login-field" placeholder="Username" required="true"></asp:TextBox><br />
                        <asp:Label ID="lloginname" runat="server" Text="" class="login-field-icon fui-user" for="loginname"></asp:Label>
                    </div>

                    <div class="control-group">
                        <asp:TextBox ID="loginpass" runat="server" TextMode="Password" class="login-field" placeholder="Contraseña" required="true"></asp:TextBox><br />
                        <asp:Label ID="lloginpass" runat="server" Text="" class="login-field-icon fui-lock" for="loginpass"></asp:Label>
                    </div>
                    <asp:Button ID="btnLogin" runat="server" Text="Ingresar" class="btn btn-primary btn-large btn-block" OnClick="btnLogin_Click" />
                    
                    


                </div>
            </div>
        </div>
    </form>
    <!-- Scripts -->

</body>
</html>
