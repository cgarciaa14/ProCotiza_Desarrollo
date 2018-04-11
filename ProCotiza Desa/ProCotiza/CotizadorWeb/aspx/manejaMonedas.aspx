<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaMonedas.aspx.vb" Inherits="aspx_manejaMonedas" %>

<%--BUG-PC-24 MAUT 08/12/2016 Se cambian validaciones--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>MONEDAS</legend>
        <fieldset style="padding-left:15px;">
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:8%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblId"></asp:Label>
                    </td>
                
                    <td style="width:10%; text-align:right;">* Nombre:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" MaxLength="100" Onkeypress="return ValCarac(event,6);"></asp:TextBox>
                        <%--BUG-PC-24 MAUT 08/12/2016 Se cambia funcion a ValCarac 6--%>
                    </td>
               
                    <td style="width:8%; text-align:right;">&nbsp;&nbsp;&nbsp;Moneda Base:</td>
                    <td style="width:27%;">
                        <asp:CheckBox ID="chkBase" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">* Valor Cambio:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtValCamb" CssClass="txt3BBVA" MaxLength="100" Onkeypress="return checkDecimals(event,this.value,2);"></asp:TextBox>
                        <%--BUG-PC-24 MAUT 08/12/2016 Se cambia funcion a checkDecimals--%>
                    </td>
                
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;ID Externo:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtUid" CssClass="txt3BBVA" MaxLength="20" Onkeypress="return ValCarac(event,7);"></asp:TextBox>
                        <%--BUG-PC-24 MAUT 08/12/2016 Se cambia funcion a ValCarac 7--%>
                    </td>
                
                    <td style="text-align:right;">* Estatus:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
            <table  style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button>
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

