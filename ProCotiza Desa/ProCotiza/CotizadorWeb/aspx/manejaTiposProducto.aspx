<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaTiposProducto.aspx.vb" Inherits="aspx_manejaTiposProducto" %>
<%--BUG-PC-42 JRHM 30/01/17 Se agrega validacion de caracteres para nombre de tipo producto a letras sin acentos y en UID se cambio por id externo y se limito a solo numeros--%>
<%--BUG-PC-137 21/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>TIPOS DE PRODUCTOS</legend>
        <fieldset>
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:5%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblId"></asp:Label>
                    </td>
               
                    <td style="width:10%; text-align:right;">* Descripción:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" MaxLength="100" Onkeypress="return ValCarac(event,18);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                    </td>
                
                    <td style="width:8%; text-align:right;">* Estatus:</td>
                    <td style="width:27%;">
                        <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;ID Externo:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtUid" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return ValCarac(event,7);"></asp:TextBox>
                    </td>
               
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Default</td>
                    <td>
                        <asp:CheckBox ID="chkDefault" runat="server" />
                    </td>
                
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Solicitar valor de la adaptación o equipo especial</td>
                    <td>
                        <asp:CheckBox ID="chkValAdap" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Incluye RC en el segundo remolque</td>
                    <td>
                        <asp:CheckBox ID="chkIncRC" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

