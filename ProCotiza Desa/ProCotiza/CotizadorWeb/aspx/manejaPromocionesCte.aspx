<%--BUG-PC-25 MAUT 15/12/2016 Se validan campos--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaPromocionesCte.aspx.vb" Inherits="aspx_manejaPromocionesCte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; margin-left:20px; top:15px;">
        <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
        <legend>PROMOCIONES</legend>
            <fieldset>
                <table class="resulbbva" style="width:100%;">
                    <tr>
                        <td style="width:10%;">&nbsp;&nbsp;&nbsp;Id:</td>
                        <td style="width:27%;"><asp:Label runat="server" ID="lblid"></asp:Label></td>
                    
                        <td style="width:10%; text-align:right">* Descripción:</td>
                        <td style="width:27%;"><asp:TextBox runat="server" ID="txtDesc" CssClass="txt3BBVA" MaxLength="100" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                        <%--BUG-PC-25 MAUT 15/12/2016 Se cambia por ValCarac 15--%>
                    
                        <td style="width:8%; text-align:right">* Estatus:</td>
                        <td style="width:27%;">
                            <asp:DropDownList runat="server" ID="ddlEstatus" CssClass="selectBBVA"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">* ID Externo:</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtidExterno" CssClass="txt3BBVA" Onkeypress="ManejaCar('N',0,this.value);"></asp:TextBox>
                            <%--BUG-PC-25 MAUT 15/12/2016 Se cambia por ManejaCar N--%>
                        </td>
                    
                        <td style="text-align:right;">* ID Cliente:</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtIdCte" CssClass="txt3BBVA" Onkeypress="ManejaCar('N',0,this.value);"></asp:TextBox>
                            <%--BUG-PC-25 MAUT 15/12/2016 Se cambia por ManejaCar N--%>
                        </td>
                    
                        <td style="text-align:right;">* Periodicidad:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlPeriodicidad" CssClass="selectBBVA"></asp:DropDownList>
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
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

