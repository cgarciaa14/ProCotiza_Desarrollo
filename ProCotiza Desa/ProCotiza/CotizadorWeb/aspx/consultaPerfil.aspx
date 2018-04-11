﻿<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaPerfil.aspx.vb" Inherits="aspx_consultaPerfil" %>

<%--BBV-P-412:AVH:14/07/2016 RQB: SE CREA CATALOGO PERFIL--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
    <legend>PERFIL</legend>
        <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
		        <tr>
                    <td style="width:5%;">&nbsp;&nbsp;Estatus:</td>
                    <td style="width:27%;"><asp:DropDownList ID="cmbEstatus" runat="server" AutoPostBack="true" CssClass="selectBBVA" ></asp:DropDownList></td>
                    <td style="width:1%; text-align:right;">Perfil:</td>
                    <td style="width:27%;"><asp:TextBox ID="txtPerfil" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return validarNro('A1',event);"></asp:TextBox></td>
            	</tr>
	    </table>
        </center>
        <center>
            <table class="resulbbva">
                <tr>
                    <td style="padding-top:5px;">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                    <td style="padding-top:5px;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="buttonBBVA2"></asp:Button><br />
                    </td> 
                </tr>
            </table>
        </center>
        </fieldset>
        <br />
        <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
            <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                          AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px" 
                                         EmptyDataText ="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="perfilId" CommandArgument='<%# Eval("ID_PERFIL") %>'><%#Eval("ID_PERFIL")%></asp:LinkButton>
                                        </ItemTemplate>                                            
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Perfil" HeaderText="Perfil" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="Estatus_DESC" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center"/>                                

                                </Columns>              
                            </asp:GridView>
                        </td>
                    </tr>
            </table>
        </fieldset>
        <br />
        <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" background-color:"White;">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="buttonBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

