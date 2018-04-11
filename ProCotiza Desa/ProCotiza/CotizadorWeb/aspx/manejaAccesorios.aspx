<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaAccesorios.aspx.vb" Inherits="aspx_manejaAccesorios" %>

<%--BBVA-P-412: 08/08/2016: AMR: RQ09 – Carga Masiva de Plan de Financiamiento--%>
<%--BUGPC03 CO02-CO19: 10/11/2016: GVARGAS: Correccion bugs--%>
<%--BBVA-P-412:BUG-PC-19 JRHM 24/11/2016 Se cambiaron eventos keypress y keyup de textbox--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>ACCESORIOS</legend>
        <fieldset style="padding-left:15px;">
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:10%;">Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblId" ></asp:Label>
                    </td>
                
                    <td style="width:10%; text-align:right;">* Tipo de Producto:</td>
                    <td style="width:27%;">
                        <asp:DropDownList runat="server" ID="cmbTipoProd" CssClass="selectBBVA" Width="190px"></asp:DropDownList>
                    </td>
                
                    <td style="width:27%; text-align:right;">* Marca:</td>
                    <td style="width:27%;">
                        <asp:DropDownList runat="server" ID="cmbMarca" CssClass="selectBBVA" Width="190px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">* Descripción:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" Width="180px" MaxLength="100" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                    </td>
                
                    <td style="text-align:right;">* Precio:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPrecio" CssClass="txt3BBVA" Width="180px" MaxLength="100" Onkeypress="return checkDecimals(event, this.value, 11)" Text="0"></asp:TextBox>
                    </td>
                
                    <td style="text-align:right;">* Estatus:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA" Width="190px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">Afecta Cálculo Seguro:</td>
                    <td>
                        <asp:CheckBox ID="chkAfectSeg" runat="server" />
                    </td>
                
                    <td style="text-align:right;">Default:</td>
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
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2Pro"></asp:Button>
                    </td>                
                </tr>
            </table>
            </center>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

