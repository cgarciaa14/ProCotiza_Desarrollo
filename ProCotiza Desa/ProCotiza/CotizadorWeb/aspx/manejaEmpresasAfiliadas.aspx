<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaEmpresasAfiliadas.aspx.vb" Inherits="aspx_manejaEmpresasAfiliadas" %>

<%--BBV-P-412  RQ-E  gvargas   12/10/2016 Actualizacion de referencias. --%>
<%--  BUGPC16 24/11/2016: GVARGAS: Correccion Error Date Picker--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>--%>
    <style type="text/css">@import url(../css/jquery-ui.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
    <%--<script type="text/javascript" src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>--%>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/ui.datepicker-es-MX.js"></script>
    <script type="text/javascript" src="../js/datepicker-es.js"></script>
    <script type="text/javascript" src="../ExternalScripts/jquery.cookie.js"></script>
    <script type="text/javascript" src="../ExternalScripts/bootstrap.min.js"></script>
    <style type="text/css">
        .ui-datepicker 
        {
            font-size:63%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>EMPRESAS AFILIADAS</legend>
        <fieldset style="padding-left:30px;">
            <table class="resulbbva">
               <tr>
                    <td style="width:10%;">Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblId"></asp:Label>
                    </td>
                
                    <td style="width:10%; text-align:right;">* Razón Social:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtRazSoc" CssClass="txt3BBVA" MaxLength="150" style="text-transform:uppercase" Onkeypress="return ValCarac(event,11)"></asp:TextBox>
                    </td>
                
                    <td style="width:9%; text-align:right;">* Nombre Corto:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtNomCto" CssClass="txt3BBVA" MaxLength="30" style="text-transform:uppercase" Onkeypress="return ValCarac(event,11)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">* Inicio Vigencia:</td>
                    <td>
                        <asp:TextBox ID="txtFecIni" CssClass="txt3BBVA" onpaste="false" runat="server"></asp:TextBox>
                    </td>
               
                    <td style="text-align:right;">* Fin Vigencia:</td>
                    <td>
                        <asp:TextBox ID="txtFecFin" CssClass="txt3BBVA" onpaste="false" runat="server"></asp:TextBox>
                    </td>
                
                    <td style="text-align:right;">UID:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtUid" CssClass="txt3BBVA" MaxLength="20" Onkeypress="return ValCarac(event,11)"></asp:TextBox>
                    </td>
                </tr>        
                <tr>
                    <td style="text-align:right;">URL Acceso:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtURL" CssClass="txt3BBVA" MaxLength="200" Onkeypress="return validarNro('P1',event);"></asp:TextBox>
                    </td>
                
                    <td style="text-align:right;">Imagen Login:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtImgLogin" CssClass="txt3BBVA" MaxLength="200" Onkeypress="return validarNro('P1',event);"></asp:TextBox>
                    </td>
                
                    <td style="text-align:right;">Color Fondo Login:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtColorLogin" CssClass="txt3BBVA" MaxLength="200" Onkeypress="return validarNro('P1',event);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">Logo Encabezado:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtLogoEnc" CssClass="txt3BBVA" MaxLength="200" Onkeypress="return validarNro('P1',event);"></asp:TextBox>
                    </td>
                
                    <td style="text-align:right;">Logo Reporte:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtLogoRep" CssClass="txt3BBVA" MaxLength="200" Onkeypress="return validarNro('P1',event);"></asp:TextBox>
                    </td>
                
                    <td style="text-align:right;">Default:</td>
                    <td>
                        <asp:CheckBox ID="chkDefault" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">* Estatus:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA"></asp:DropDownList>
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
    <script type="text/javascript">
        $(document).ready(function () {
            pickerSettins();
        });
    </script>
</asp:Content>

