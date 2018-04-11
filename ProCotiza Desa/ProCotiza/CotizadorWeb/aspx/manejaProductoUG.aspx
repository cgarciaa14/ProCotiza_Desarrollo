<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaProductoUG.aspx.vb" Inherits="aspx_manejaProductoUG" %>

<%--BBV-P-412: AVH: RQ 14: 26/07/2016 SE AGREGA VENTANA DE PRODUCTOS UG--%>
<%--BBVA-P-412: JRHM 14/11/2016: BUG-PC-08 Cambios para recibir alfanumericos en textbox--%>
<%--BBVA-P-412:BUG-PC-19 JRHM 24/11/2016 Se habilito la escritura de caracteres en minuscula de textbox--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>PRODUCTO UG</legend>
        <fieldset style="padding-left:15px;">
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:7%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblIDProducto"></asp:Label>
                    </td>
       
                    <td style="width:10%; text-align:right;">* Producto UG:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtProducto" CssClass="txt3BBVA" Width="190px" MaxLength="200" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox> <%-- JRHM 14/11/16 SE CAMBIO EL EVENTO KEYPRESS PARA QUE ESTE CAMPO SOLO PERMITA ALFANUMERICOS --%>
                    </td>
               
                    <td style="width:9%; text-align:right;">* Descripción:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtDes" CssClass="txt3BBVA" Width="190px"  MaxLength="200" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox> <%-- JRHM 14/11/16 SE CAMBIO EL EVENTO KEYPRESS PARA QUE ESTE CAMPO SOLO PERMITA ALFANUMERICOS --%>
                    </td>
                </tr>                              
                <tr>
                    <td style="text-align:right;">* Estatus:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbEstatus" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList>
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
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="height:40px; background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                    <td colspan="2" align="center" style="height:40px; background-color:White;">
                    	<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>


