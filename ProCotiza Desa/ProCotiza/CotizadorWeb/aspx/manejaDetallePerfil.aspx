<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaDetallePerfil.aspx.vb" Inherits="aspx_manejaDetallePerfil" %>

<%--BBV-P-412:AVH:12/07/2016 RQ01: SE CREA VENTANA DETALLE PERFIL--%>
<%--BBV-P-412 RQ WSC: AVH: 27/10/2016 SE QUITA UPDATE PANEL--%>
<%--BUG-PC-37: AVH: 20/01/2017 Se deshabilitar boton que consume WS--%>
<%--BUG-PC-42 JRHM 30/01/17 Se agrega limite de 20 acaracteres en txt num cuenta--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <asp:ScriptManager runat="server" ID="sm1"></asp:ScriptManager>        
    <legend>DETALLE PERFIL</legend>
        <fieldset style="padding-left:15px;">
            <table class="resulbbva">
                <tr>
                    <td style="width:5%;">
                        <asp:Label runat="server" ID="lblId" Text="Id:"></asp:Label>
                    </td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblIdVendedor" Text=""></asp:Label>
                    </td>
                 
                    <td style="width:10%; text-align:right;">
                        <asp:Label runat="server" ID="lblNom" Text="Nombre" ></asp:Label>
                    </td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblNombre" Width="200px"></asp:Label>
                    </td>
                
                    <td style="width:18%; text-align:right;">
                       <asp:Label runat="server" ID="lblPagoComisiones" Text="* Pago Comisión Individual:"></asp:Label>
                    </td>
                    <td style="width:27%;">
                       <asp:CheckBox runat="server" ID="cbPagoComisiones" AutoPostBack="true" />
                    </td>
                 </tr>
            </table>
            <asp:Panel runat="server" ID="pnlNumeroCuenta" Visible="false">
                            <table class="resulbbva">                                
                                <tr>
                                    <td style="width:8%; text-align:right;">
                                        * Numero de Cuenta:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtNumeroCuenta" CssClass="txt3BBVA" MaxLength="20" Onkeypress="return ValCarac(event,7);" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width:10%; text-align:right;">
                                        <asp:Button runat="server" ID="btnValidar" Text="Validar"  CssClass="buttonBBVA2" Visible="false" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                                    </td>
                                </tr>
                            </table> 
                        </asp:Panel>
                        <%--<asp:Panel runat="server" ID="pnlNombreJefe" Visible="false">
                            <table class="resul2">
                                <tr>
                                    <td>
                                        Nombre Jefe:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtNombreJefe" CssClass="txt" Enabled="FALSE" style="text-transform:uppercase" onkeyup="javascript:this.value=this.value.toUpperCase();" Onkeypress="return ValCarac(event,6);"></asp:TextBox>
                                    </td>                                    
                                </tr>
                            </table> 
                        </asp:Panel>--%>
                        <asp:Panel runat="server" ID="pnlTitutal" Visible="false">
                            <table class="resulbbva">
                                <tr>
                                    <td style="width:8%; text-align:right;">
                                        * Titular Cuenta:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtTitular" CssClass="txt3BBVA" Enabled="false" style="text-transform:uppercase" onkeyup="javascript:this.value=this.value.toUpperCase();" Onkeypress="return ValCarac(event,6);"></asp:TextBox>
                                    </td>                                    
                                </tr>
                            </table> 
                        </asp:Panel>
        </fieldset>
        
        <br />
        <fieldset>
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="height:40px; background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button>
                    </td>
                    <td colspan="2" align="center" style="height:40px; background-color:White;">
                        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
            
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

