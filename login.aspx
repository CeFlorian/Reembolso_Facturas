<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="webFacturas.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fulanos S.A. - login</title>
    <link href="Contents/Login/Login.css" rel="stylesheet" />
    <link href="Contents/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <%--contenedor--%>
        <div class="wrapper fadeInDown">
            <div id="formContent">
                <!-- Tabs Titles -->
                <div style="margin-bottom: 100px;">

                </div>
                <!-- Icon -->
                <div class="fadeIn first">
                    <img src="Contents/images/logo.png" id="icon" alt="User Icon" />
                </div>
                <div class="fadeIn first">
                    <h3>Sistema de reintegro de facturas</h3>
                </div>
                <!-- Login Form -->
                <asp:TextBox ID="txtUsuario" CssClass="fadeIn second" placeholder="Su código de empleado" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtPass" TextMode="Password" CssClass="fadeIn second" placeholder="Su contraseña" runat="server"></asp:TextBox>
                <br/>
                <asp:Label ID="lbMensaje" runat="server" Text="Esta es una prueba" Visible="false" ForeColor="Red"></asp:Label>
                <br />
                <asp:Button ID="Button1" CssClass="fadeIn fourth" runat="server" Text="Entrar" OnClick="Button1_Click" />
                
                <!-- Remind Passowrd -->
                <div id="formFooter">
                    <a class="underlineHover" href="#">¿Olvidó su contraseña?</a>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
