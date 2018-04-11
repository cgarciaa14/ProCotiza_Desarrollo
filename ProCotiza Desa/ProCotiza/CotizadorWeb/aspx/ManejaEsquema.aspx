<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="ManejaEsquema.aspx.vb" Inherits="aspx_ManejaEsquema" %>

<%--BBVA RQTARESQ-06 CGARCIA 19/04/2017 SE CREA EL OBJETO DE MANEJA ESQUEMA PARA LA RELACION DEL CATÁLOGO DE ESQUEMAS --%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>ESQUEMA</legend>
        <fieldset style="padding-left:15px;">
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:7%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblIDProducto"></asp:Label>
                    </td>
                    <td style="width:10%; text-align:right;">* Esquema:</td>
                    <td style="width:27%;" >
                        <asp:TextBox runat="server" ID="txtEsquema" CssClass="txt3BBVA" Width="190px" MaxLength="200"  Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox> <%-- JRHM 14/11/16 SE CAMBIO EL EVENTO KEYPRESS PARA QUE ESTE CAMPO SOLO PERMITA ALFANUMERICOS --%>
                    </td>
                    <td style="width:9%; text-align:right;">* Descripción:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtDes" CssClass="txt3BBVA" MaxLength="200" Width="190px"  Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox> <%-- JRHM 14/11/16 SE CAMBIO EL EVENTO KEYPRESS PARA QUE ESTE CAMPO SOLO PERMITA ALFANUMERICOS --%>
                    </td>
                </tr>                              
                <tr>
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
            <table  style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" background-color:"White">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                    <td colspan="2" align="center" background-color:"White">
                    	<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

