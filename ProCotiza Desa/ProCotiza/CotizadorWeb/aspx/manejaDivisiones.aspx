<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaDivisiones.aspx.vb" Inherits="aspx_manejaDivisiones" %>

<%--BBV-P-412:AVH:06/07/2016 RQ19: SE CREA CATALOGO DE DIVISIONES--%>
<%--BUG-PC-11: AVH: 23/11/2016 SE AGREGA VALIDACION A URL--%>
<%--BBVA-P-412:BUG-PC-19 JRHM 24/11/2016 Se cambiaron eventos keypress y keyup de textbox--%>
<%--BUG-PC-24 08/12/2016 MAUT Se cambia validacion de URL--%>
<%--BUG-PC-34 12/01/2017 MAUT Se corrigen validaciones para IE--%>
<%--BUG-PC-39 25/01/17 Correccion de errores multiples--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>DIVISIÓN</legend>
        <fieldset style="padding-left:15px;">
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:5%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblIDDivision"></asp:Label>
                    </td>
                    <td style="width:10%; text-align:right;">* División:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtDivision" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                    </td>
                    <td style="width:9%; text-align:right;">* Descripción:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtDes" CssClass="txt3BBVA" MaxLength="90" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">* URL:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtURL" CssClass="txt3BBVA" MaxLength="90"  Onkeypress="return ValCarac(event,15);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                        <%--BUG-PC-24 08/12/2016 MAUT Se cambia por ManejaCar R--%>
                        <%--BUG-PC-34 12/01/2017 MAUT Se cambia por ValCarac15--%>
                    </td>
                
                    <td style="text-align:right;">* Estatus:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Default:</td>
                    <td>
                        <asp:CheckBox ID="chkDefault" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
         <center>
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
         </center>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

