<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="relacionaUsuarioAgencias.aspx.vb" Inherits="aspx_relacionaUsuarioAgencias" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<%--  BUG-PC-04: AVH: 11/11/2016 se utiliza la funcion ValCarac--%>
<%-- BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.--%>
<%--BUG-PC-43 01/02/17 JRHM Se agrego clase wrap a tabla para casos donde nombre de agencia sea demasiado grande para tabla--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGAN VALIDACION DE CARACTERES PARA BUSQUEDA DE AGENCIA Y FUNCION WRAP PARA LOS NOMBRES DE AGENCIAS LARGAS--%>
<%--BUG-PC-110 25/09/2017 ERODRIGUEZ Se corrigio validacion para numeros y letras en agencia--%>
<%--BUG-PC-137 29/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>--%>
<asp:content id="Content1" contentplaceholderid="head" runat="Server">
        <style type="text/css">
            @import url(../css/procotiza.css);
        </style>
        <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
        <script type="text/javascript" src="../js/Funciones.js"></script>
        <style runat="server" id="LessCSS" type="text/css" />
</asp:content>
<%--</head>
<body>
    <form id="form1" runat="server">--%>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
        <div id="cuerpoConsul" style=" position:relative; margin-left:20px; top:15px;">
        <fieldset class="fieldsetBBVA" style="width:100%; padding:15px;">
        <legend>RELACIONA USUARIOS</legend>
            <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8;">
            <center>
                <table class="resulbbva">
		            <tr>
                        <td style="width:5%;">Usuario:</td>
                        <td style="width:27%;"><asp:DropDownList ID="cmbUsuario" runat="server" CssClass="selectBBVA" AutoPostBack="true" Visible="false"></asp:DropDownList>
                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="txx3BBVA" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width:1%; text-align:right;">Agencia:</td>
                        <td style="width:27%;"><asp:TextBox ID="txtage" runat="server" CssClass="txt3BBVA" MaxLength="50" Width="194px" Onkeypress="return ValCarac(event,12);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
            	    </tr>
	        </table>
            </center>
            <center>
                <table class="resulbbva">
                    <tr>
                        <td align="center">
                            <asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaAgen" Text="Buscar"/><br /></td>
                        <td align="center">
                             <asp:Button runat="server" CssClass="buttonSecBBVA2" ID="btnLimpiar" Text="Limpiar"/><br />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnNinguna" runat="server" Text="Ninguna" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                        <td align="center">
                            <asp:Button ID="btnTodas" runat="server" Text="Todas" CssClass="buttonSecBBVA2"></asp:Button><br />
                        </td> 
                    </tr>
                </table>
            </center>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 220px; overflow-x: hidden">
                <table width="100%">
                        <tr>
                            <td>
                                    <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                                  AllowPaging="true" PageSize="20" Width="100%" BorderWidth="0px" 
                                                  DataKeyNames="ID_AGENCIA, ASIG" EmptyDataText ="No se encontró información con los parámetros proporcionados.">
                                        <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                                        <Columns>
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Agencia" ItemStyle-Width="27%" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" ControlStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="UsuarioId" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" AlternateText="Relación"/>
                                                </ItemTemplate>                                            
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID_AGENCIA" HeaderText="" Visible = "false" />
                                            <asp:BoundField DataField="ASIG" HeaderText="" Visible = "false"/>
                                        </Columns>              
                                     </asp:GridView>
                            </td>
                        </tr>
                </table>
            </fieldset>
        </fieldset>
        </div>
        <asp:Label runat="server" ID="lblMensaje"></asp:Label>
        <asp:Label runat="server" ID="lblScript"></asp:Label>
    </asp:content>
<%--</form>
</body>
</html>--%>
