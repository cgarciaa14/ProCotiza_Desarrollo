<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaPerfil.aspx.vb" Inherits="aspx_manejaPerfil" %>

<%--BBV-P-412:AVH:14/07/2016 RQB: SE CREA CATALOGO PERFIL--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>PERFIL</legend>
        <fieldset style="padding-left:15px;">
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:8%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblIDPerfil"></asp:Label>
                    </td>
                    <td style="width:10%; text-align:right;">* Perfil:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtPerfil" CssClass="txt3BBVA" MaxLength="200" style="text-transform:uppercase" onkeyup="javascript:this.value=this.value.toUpperCase();" Onkeypress="return ValCarac(event,12);"></asp:TextBox>
                    </td>
   
                    <td style="width:9%; text-align:right;">* Estatus:</td>
                    <td style="width:27%;">
                        <asp:DropDownList runat="server" ID="cmbEstatus" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList>
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

