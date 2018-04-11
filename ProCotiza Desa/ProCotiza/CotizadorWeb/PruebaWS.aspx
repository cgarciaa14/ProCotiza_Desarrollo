<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="PruebaWS.aspx.vb" Inherits="PruebaWS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--'BBV-P-412:AUBALDO:29/07/2016 RQ 13 – Reporte de Alta de Perfiles y Agencias. (NUEVA BRECHA)--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">@import url(./css/procotiza.css);</style>
    <script type="text/javascript" src="js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="js/Funciones.js"></script>
    <script type="text/javascript" src="js/swfobject.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="divlogin1">
        <table class="resul2">
            <tr>
                <td>FECHA INICIAL: </td>
                <td><asp:TextBox runat="server" ID="txtFecInicio" CssClass="txt2" MaxLength="20" style="text-transform:uppercase" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>FECHA FINAL: </td>
                <td><asp:TextBox runat="server" ID="txtFecFin" CssClass="txt2" MaxLength="20" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>GENERA REPORTE: </td>
                <td><asp:TextBox runat="server" ID="txtTipoReporte" CssClass="txt2" MaxLength="20" ></asp:TextBox></td>
            </tr>
            <tr>
                <td style=" background-color:White;">&nbsp;</td>
                <td><asp:Button runat="server" ID="btnAceptar" Text="ENVIAR" CssClass="button3"/></td>
            </tr>
        </table>    
    </div>
    
    <asp:Label id="lblMensaje" runat="server"></asp:Label>		
    </form>
</body>
</html>
