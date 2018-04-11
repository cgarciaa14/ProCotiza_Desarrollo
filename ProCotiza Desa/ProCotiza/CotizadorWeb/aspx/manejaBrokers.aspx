<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaBrokers.aspx.vb" Inherits="aspx_manejaBrokers" %>

<%--BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.--%>
<%--BUG-PC-26: JRHM: 20/12/2016 Se agrega reemplazo de acentos a textbox--%>
<%--BUG-PC-27 MAUT 27/12/2016 Se remplazan acentos--%>
<%--BUG-PC-28 JRHM 30/12/16 Se cambia metodo de validacion de caracteres para no permitir simbolos --%>
<%--BUG-PC-39 27/01/17 JRHM Se limito el id externo a 9 numeros --%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>BROKERS</legend>
        <fieldset style="padding-left:15px;">
            <table class="resulbbva" style="width:100%;">
                   <tr>
                        <td style="width:7%;">&nbsp;&nbsp;&nbsp;Id:</td>
                        <td style="width:27%;">
                            <asp:Label runat="server" ID="lblId"></asp:Label>
                        </td>
                  
                        <td style="width:10%; text-align:right;">* Razón Social:</td>
                        <td style="width:27%;">
                            <%--BUG-PC-27 MAUT 27/12/2016 Se remplazan acentos--%>
                            <asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA"  MaxLength="100" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                        </td>
                  
                        <td style="width:10%; text-align:right;">* Nombre Corto:</td>
                        <td style="width:27%;">
                            <%--BUG-PC-27 MAUT 27/12/2016 Se remplazan acentos--%>
                            <asp:TextBox runat="server" ID="txtcorto" CssClass="txt3BBVA"  MaxLength="100" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">* Vía Calculo: </td>
                        <td><asp:DropDownList runat="server" ID="cmbTipoCalcSeg" CssClass="selectBBVA" ></asp:DropDownList></td>
                        <%--<tr>
                        <td>* Prima:</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtprima" CssClass="txt" MaxLength="100" Onkeypress="return validarNro('D',event);"></asp:TextBox>
                        </td>
                    </tr>--%>
                    
                        <td style="text-align:right;">&nbsp;&nbsp;&nbsp;ID Externo:</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtexterno" CssClass="txt3BBVA"  MaxLength="9" Onkeypress="return ValCarac(event, 7);"></asp:TextBox>
                        </td>
                
                        <td style="text-align:right;">* Estatus:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA" ></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Link:</td>
                        <td><asp:TextBox runat="server" ID="txtlink" CssClass="txt3BBVA" MaxLength="300" Onkeypress="return ValCarac(event, 15);"></asp:TextBox></td>
                    
                        <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Default:</td>
                        <td>
                            <asp:CheckBox ID="chkDefault" runat="server"/>
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
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

