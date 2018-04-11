<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaAlianzas.aspx.vb" Inherits="aspx_manejaAlianzas" %>

<%--BBV-P-412:AVH:28/06/2016 RQ16: SE CREA CATALOGO ALIANZAS--%>
<%--BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.--%>
<%--BBV-P-412_RQCOT-05:AVH:31/08/2016 SE AGREGA RUTA DE IMAGEN PARA EL REPORTE--%>
<%--BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.--%>
<%--BUG-PC-17: AVH 25/11/2016 SE AGREGA VALIDACION AL CAMPO URL--%>
<%--BUG-PC-26: JRHM: 20/12/2016 Se agrega reemplazo de acentos a textbox--%>
<%--BBV-P-412:BUG-PC-35 18/01/2017 JRHM Se cambia cambian campos obligatorios a campos normales--%>
<%--BUG-PC-38 MAUT 23/01/2017 Se quita campo de imagen reporte--%>
<%--BUG-PC-39 25/01/17 Correccion de errores multiples--%>
<%--BBV-P-412 RQADM-04 ERV 20/04/2017 Se agrego funcionalidad para VALIDA_PROSPECTOR--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <style type="text/css">@import url(../css/procotiza.css); 
        #txtNomImagen
        {
            width: 302px;
        }
    </style>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>ALIANZAS</legend>
        <fieldset style="padding-left:15px;">
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:7%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblIDAlianza"></asp:Label>
                    </td>
                
                    <td style="width:10%; text-align:right;">* Alianza:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtAlianza" CssClass="txt3BBVA" Width="180px" MaxLength="90" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                    </td>
               
                     <td style="width:8%; text-align:right;">Descripción:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtDes" CssClass="txt3BBVA" Width="180px" MaxLength="90" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">URL:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtURL" CssClass="txt3BBVA" Width="180px" MaxLength="90"  Onkeypress="return ValCarac(event,15);"></asp:TextBox>
                    </td>
                
                    <td style="text-align:right;">* Estatus:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbEstatus" AutoPostBack="true" CssClass="selectBBVA" Width="190px"></asp:DropDownList>
                    </td>
                
                    <td style="text-align:right;">*Broker: </td>
                    <td><asp:DropDownList runat ="server" ID="ddlbroker" CssClass="selectBBVA" Width="190px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Valida Prospector:</td>
                    <td>
                        <asp:CheckBox ID="ChkProspector" runat="server" />
                    </td>
                
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Default:</td>
                    <td>
                        <asp:CheckBox ID="chkDefault" runat="server" />
                    </td>
                </tr>
                <%--<tr>                    
					<td>*Imagen Reporte</td>
					<td>
					<input id="txtNomImagen" runat="server" 
					onkeypress="javascript:RegresaTexto(this);" type="file" size="28" name="txtNomImagen"/>
					</td>                    															
				</tr>
                            <tr>
                    <td>&nbsp;</td>
                                <td>
                        <asp:Image runat="server" ID="Image1" Width="200px" Height="150px"/>
                                </td>
                </tr>--%>
                <%--BUG-PC-38 MAUT 23/01/2017 Se comenta porque se pide que se quite este campo--%>
                        </table>

<%--            <table>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Image runat="server" ID="Image1" Width="100px" Height="100px"/>
                    </td>
                </tr>
            </table>--%>

        </fieldset>
        <br />
        <fieldset>
            <center>
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                    <td colspan="2" align="center" style="background-color:White;">
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

