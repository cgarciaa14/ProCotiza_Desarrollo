<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaMarcas.aspx.vb" Inherits="aspx_manejaMarcas" %>

<%--  BBV-P-412  RQ-WSD  gvargas   25/10/2016 Actualizacion de referencias Jquery.cockie. --%>
<%--BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca--%>
<%--BUG-PC-39 25/01/17 Correccion de errores multiples--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/jquery-1.11.1.min.js" ></script>
    <script type="text/javascript" src="../ExternalScripts/jquery.cookie.js"></script>

    
<script type="text/javascript">


          
</script>  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; margin-left:20px; top:15px;">
        <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
        <legend>MARCAS</legend>
            <fieldset style="padding-left:15px;">
                <table class="resulbbva" style="width:100%;">
                    <tr>
                        <td style="width:5%;">&nbsp;&nbsp;&nbsp;Id:</td>
                        <td style="width:27%;">
                            <asp:Label runat="server" ID="lblId"></asp:Label>
                        </td>
                    
                        <td style="width:10%; text-align:right;">* Marca:</td>
                        <td style="width:27%;">
                            <asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" Width="190px" MaxLength="90" Onkeypress="return ValCarac(event,6);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox> <%--BUG-PC-09--%>
                        </td>
                    
                        <td style="width:9%; text-align:right;">* Estatus:</td>
                        <td style="width:27%;">
                            <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="trtipouso">
                        <td style="text-align:right;">* Tipo Uso:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="cmbTipoReg" CssClass="selectBBVA"></asp:DropDownList>
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
                <center>
                <table style="width:100%;">
                    <tr id="trBotones">
                        <td colspan="2" align="center"> <%--BUG-PC-09--%>
                    	    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                        <td colspan="2" align="center">
                    	    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                        </td>                
                    </tr>
                </table>
                </center>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

