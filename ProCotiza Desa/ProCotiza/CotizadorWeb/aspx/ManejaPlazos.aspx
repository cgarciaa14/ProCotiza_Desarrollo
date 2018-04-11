<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="ManejaPlazos.aspx.vb" Inherits="aspx_ManejaPlazos" %>
<%--BBV-P-412:BUG-PC-10 JRHM: 18/11/2016 Se bloquea el uso de simbolos a textbox valor plazo--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>PLAZOS</legend>
        <fieldset>
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:8%;">Id:</td>
                    <td style="width:27%;"><asp:Label runat="server" ID="lblidplazo"></asp:Label></td>
               
                    <td style="width:10%; text-align:right;" >* Periodicidad: </td>
                    <td style="width:27%;"><asp:DropDownList runat="server" ID="ddlperiodicidad" CssClass="selectBBVA"  AutoPostBack="true"></asp:DropDownList></td>
                
                    <td style="width:9%; text-align:right;">* Valor: </td>
                    <td style="width:27%;"><asp:TextBox runat="server" ID="txtvalor" CssClass="txt3BBVA" MaxLength="2" Onkeypress="return ValCarac(event,7);"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:right;">* Descripción: </td>
                    <td><asp:TextBox runat="server" ID="txtdesc" CssClass="txt3BBVA" Enabled="false"></asp:TextBox></td>
                
                    <td style="text-align:right;">* Estatus: </td>
                    <td><asp:DropDownList runat="server" ID="ddlestatus" CssClass="selectBBVA"></asp:DropDownList></td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" background-color:"White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                    <td colspan="2" align="center" background-color:"White;">
                    	<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

