<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaSubMarcas.aspx.vb" Inherits="aspx_manejaSubMarcas" %>

<%--BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.--%>
<%--BUG-PC-09:AMR:17/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca--%>
<%--BBVA-P-412 BUG-PC-23 29/11/16 JRHM Se modifico metodo onkeypress de txtidexterno para solo numeros.   --%>
<%--BUG-PC-39 JRHM 25/01/17 Correccion de errores multiples--%>
<%--BUG-PC-43 01/02/17 JRHM Se limita id_externo a 9 caracteres--%>
<%--BUG-PC-137 21/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>SUBMARCAS</legend>
        <fieldset>
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:7%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;"><asp:Label runat="server" ID="lblidsubmarca"></asp:Label></td>
                
                    <td style="width:10%; text-align:right;">* Marca</td>
                    <td><asp:DropDownList runat="server" ID="ddlmarca" CssClass="selectBBVA"></asp:DropDownList></td>
                
                    <td style="width:8%; text-align:right;">* Submarca</td>
                    <td><asp:TextBox runat="server" ID="txtsubmarca" CssClass="txt3BBVA" MaxLength="90" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td> <%--BUG-PC-09--%>
                </tr>
                <tr>
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;ID Externo</td>
                    <td><asp:TextBox runat="server" ID="txtidexterno" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return  ValCarac(event,7);"></asp:TextBox></td> <%--BUG-PC-09--%>
               
                    <td style="text-align:right;">* Estatus</td>
                    <td><asp:DropDownList runat="server" ID="ddlestatus" CssClass="selectBBVA"></asp:DropDownList></td>
                
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Default:</td>
                    <td><asp:CheckBox runat="server" ID="chkdefault"/></td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
            <table  style="width:100%;">
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
    <asp:Label runat="server" ID="lblScript"></asp:Label>

</asp:Content>

