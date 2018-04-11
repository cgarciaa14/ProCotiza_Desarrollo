<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<%-- BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.--%>
<%-- BUG-PC-55 GVARGAS 21/04/17 Cambios LogIn TC--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">@import url(./css/procotiza.css);</style>
    <script type="text/javascript" src="js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="js/Funciones.js"></script>
    <script type="text/javascript" src="js/swfobject.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="divlogin">
        <table class="resul2">
            <tr>
                <td>Usuario: </td>
                <td><asp:TextBox runat="server" ID="txtUsu" CssClass="txt2" MaxLength="20" style="text-transform:uppercase" Onkeypress="return ValCarac(event, 20);"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Contraseña: </td>
                <td><asp:TextBox runat="server" ID="txtPwd" CssClass="txt2" MaxLength="20" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td>TC: </td>
                <td><asp:TextBox runat="server" ID="txtTC" CssClass="txt2" MaxLength="20" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>USER: </td>
                <td><asp:TextBox runat="server" ID="txtUSER" CssClass="txt2" MaxLength="20" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>PASS: </td>
                <td><asp:TextBox runat="server" ID="txtIV" CssClass="txt2" MaxLength="20" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>SN: </td>
                <td><asp:TextBox runat="server" ID="txtSN" CssClass="txt2" MaxLength="20" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>ALIANZA: </td>
                <td><asp:TextBox runat="server" ID="txtalianza" CssClass="txt2" MaxLength="20" ></asp:TextBox></td>
            </tr>
            <tr>
                <td style=" background-color:White;">&nbsp;</td>
                <td><asp:Button runat="server" ID="btnAceptar" Text="Ingresar" CssClass="button3"/></td>
            </tr>
        </table>    
    </div>
    <div>
        <img src="img/logo_incredi3.jpg"/>
    </div>
    <asp:Label id="lblMensaje" runat="server"></asp:Label>		
    </form>
</body>
</html>
