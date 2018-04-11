<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaEstados.aspx.vb" Inherits="aspx_manejaEstados" %>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>ESTADOS</legend>
        <fieldset style="padding-left:15px;">
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:8%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblId"></asp:Label>
                    </td>
                
                    <td style="width:10%; text-align:right">* Nombre:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" MaxLength="100" Onkeypress="return validarNro('C1',event);"></asp:TextBox>
                    </td>
                
                    <td style="width:10%; text-align:right">* Clave:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtCveEdo" CssClass="txt3BBVA" MaxLength="20" Onkeypress="return validarNro('A1',event);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right">* Estatus:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                
                    <td style="text-align:right">&nbsp;&nbsp;&nbsp;UID:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtUid" CssClass="txt3BBVA" MaxLength="20" Onkeypress="return validarNro('A',event);"></asp:TextBox>
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

