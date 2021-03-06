﻿<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaBrokers.aspx.vb" Inherits="aspx_consultaBrokers" %>

<%--BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.--%>
<%--BUG-PC-40:PVARGAS:27/01/2017:SE CAMBIA EL CSSCLASS DEL TEXTBOX txtnombroker.--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
    <legend>BROKERS</legend>
        <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
		        <tr>
                    <td style="width: 5%";>Estatus:</td>
                    <td style="width: 27%;"><asp:DropDownList runat="server" ID="ddlestatus" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                    <td style="width: 1%; text-align:right;">Broker:</td>
                    <%--BUG-PC-40:PVARGAS:27/01/2017:SE CAMBIA EL CSSCLASS DEL TEXTBOX txtnombroker.--%>
                    <td style="width: 27%;"><asp:TextBox runat ="server" ID="txtnombroker" CssClass="txt3BBVA" Width="190px" Onkeypress="return ValCarac(event, 11)" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                    
            	</tr>
	    </table>
        </center>
        <center>
        <table class="resulbbva">
            <tr>
                <td style="padding-top:5px;">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                <td style="padding-top:5px;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar"  CssClass="buttonBBVA2"></asp:Button><br />
                    </td> 
            </tr>
        </table>
        </center>
        </fieldset>
        <br />
        <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
            <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                          AllowPaging="true" PageSize="8" Width="100%" BorderWidth="0px" 
                                          EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="brokerId" CommandArgument='<%# Eval("id_broker") %>'><%#Eval("id_broker")%></asp:LinkButton>
                                        </ItemTemplate>                                            
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="NOM_CORTO" HeaderText="Nom. Corto" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField dataField="VIA" HeaderText="Via Cálculo" ItemStyle-HorizontalAlign="Center"/>
                                    <%--<asp:BoundField DataField="CONSTANTE_PRIMA" HeaderText="Factor" ItemStyle-HorizontalAlign="Center"/>--%>
                                    <asp:BoundField DataField="ID_EXTERNO" HeaderText="Externo" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="TEXTO" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="LINK" HeaderText="Link" ItemStyle-HorizontalAlign="Left"/>
                                    <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <div class="tooltip">
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="AsigAse" CommandArgument='<%# Eval("id_broker") %>' ImageUrl="../img/building.png" AlternateText="Asigna Aseguradora"/>
                                            <span class="tooltiptext">Asigna Aseguradora</span>
                                        </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>              
                            </asp:GridView>
                        </td>
                    </tr>
            </table>
        </fieldset>
        <br />
        <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" background-color:"White;">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" width="60px" CssClass="buttonBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

