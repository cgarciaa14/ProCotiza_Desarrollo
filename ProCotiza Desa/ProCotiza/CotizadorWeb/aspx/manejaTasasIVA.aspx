<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaTasasIVA.aspx.vb" Inherits="aspx_manejaTasasIVA" %>
<%--JRHM 10/11/16 BUG-PC-05 Cambio de regla de campos para aceptar alfanumericos.--%>
<%--JRHM 23/11/16 BBVA-P-412:BUG-PC-15 Se agrego evento para reemplazar letras con acento, evento de alfanumericos y se cambio limite de numeros enteros para campo valor--%>
<%--BBV-P-412:BUG-PC-23 JRHM: 30/11/2016 Se modifico campo nombre para que solo permitira texto, numeros y un simbolo % al final del texto.--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
     <script type="text/javascript">
         function validapor() {
             var nombretasa = $("input[id$=txtNom]").val();
             if (!nombretasa == '') {
                 expr = /^[a-zA-Z0-9\ ]+%?$/;
                 if (!expr.test(nombretasa)) {
                     alert("El formato del normbre de la tasa es incorrecto solo puede llevar un '%' al final o ninguno.");
                     $("input[id$=txtNom]").val('');
                 }
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>TASAS IVA</legend>
        <fieldset>
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:5%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblId"></asp:Label>
                    </td>
                
                    <td style="width:10%; text-align:right;">* Nombre:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" MaxLength="100" Onkeypress="return ValCarac(event,19);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" onblur="validapor();"></asp:TextBox> <%--JRHM 10/11/16 Se cambio el parametro a 'A1' para que el campo ahora acepte caracteres alfanumericos.--%>
                    </td>
                
                    <td style="width:9%; text-align:right">* Tasa:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtTasa" CssClass="txt3BBVA" MaxLength="6" Onkeypress="return checkDecimals(event,this.value,1);"></asp:TextBox> 
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right">* Estatus:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
               
                    <td style="text-align:right">Default:</td>
                    <td>
                        <asp:CheckBox ID="chkDefault" runat="server" />
                    </td>
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

