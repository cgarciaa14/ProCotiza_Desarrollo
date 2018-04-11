<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaAseguradoras.aspx.vb" Inherits="aspx_manejaAseguradoras" %>
<%--BUG-PC-46 JRHM 09/02/17 Se modifica validacion de caracteres de nombre aseguradora--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>ASEGURADORAS</legend>
        <fieldset>
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:9%;">Id:</td>
                    <td style="width:27%;"><asp:Label runat="server" ID="lblidAseg"></asp:Label></td>
                
                    <td style="width:10%; text-align:right;">* Razón Social:</td>
                    <td style="width:27%;"><asp:TextBox runat="server" ID="txtrazonsocial" CssClass="txt3BBVA" Width="190px" Onkeypress="return ValCarac(event,11);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                
                    <td style="width:27%; text-align:right;">* Nombre Corto:</td>
                    <td style="width:27%;"><asp:TextBox runat="server" ID="txtnomcorto" CssClass="txt3BBVA" Width="190px" Onkeypress="return ValCarac(event,11);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;ID Externo:</td>
                    <td><asp:TextBox runat="server" ID="txtidext" CssClass="txt3BBVA" Width="190px" Onkeypress="return ValCarac(event,7);"></asp:TextBox></td>
                
                    <td style="text-align:right;">* Estatus: </td>
                    <td><asp:DropDownList runat="server" ID="ddlestatus" CssClass="selectBBVA"></asp:DropDownList></td>
                
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
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

