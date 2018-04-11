<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="copiaPaquete.aspx.vb" Inherits="aspx_copiaPaquete" %>
<%--BUG-PC-37: AVH: 20/01/2017 Se deshabilitar boton que consume WS--%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 16/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div style=" position:relative; margin-left:20px; top:15px;">
<fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>COPIA PAQUETE</legend>
        <fieldset>
        <table class="resulbbva">
                <tr>
                    <td style="width:8%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;"><asp:Label runat="server" ID="lblId"></asp:Label></td>
                
                    <td style="width:10%; text-align:right;">&nbsp;&nbsp;&nbsp;Nombre:</td>
                    <td style="width:27%;"><asp:Label runat="server" ID="lblNom"></asp:Label></td>

                    <td style="width:8%; text-align:right;">*Nombre Copia:</td>
                    <td style="width:27%;"><asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" onkeyup="ReemplazaAcentos(event, this.id, this.value);" Onkeypress="return ValCarac(event,12)"></asp:TextBox></td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="height:40px; background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"></asp:Button></td>
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

