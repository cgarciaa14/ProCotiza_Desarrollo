﻿<%--BUG-PC-34 MAUT 16/01/2017 Se agrega mensaje en Empty Text--%>
<%--BUG-PC-42 JRHM 30/01/17 Se agrega funcionalidad de boton limpiar--%>
<%----BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA VISUALIZAR CORRECTAMENTE LA OPCION SELECCIONAR--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--21/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaTiposProducto.aspx.vb" Inherits="aspx_consultaTiposProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
        <legend>TIPOS DE PRODUCTOS</legend>
            <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
                    <tr>
                        <td style="width:5%;">&nbsp;&nbsp;Estatus:</td>
                        <%----BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA VISUALIZAR CORRECTAMENTE LA OPCION SELECCIONAR--%>
                        <td style="width:27%;"><asp:DropDownList ID="cmbEstatus" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                        <td style="width:1%; text-align:right;">Descripción:</td>
                        <td style="width:27%;"><asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="50" Width="194px" Onkeypress="return ValCarac(event,18);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
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
            <fieldset style="width:96.5%; padding-top:10px; border: .5px solid #D8D8D8;">
            <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                              AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px" 
                                            EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="tipProdId" CommandArgument='<%# Eval("id_tipo_producto") %>'><%#Eval("id_tipo_producto")%></asp:LinkButton>
                                            </ItemTemplate>                                            
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap"/>
                                        <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap"/>
                                    </Columns>              
                                </asp:GridView>
                            </td>
                        </tr>
                </table>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <table style="width: 100%;">
                    <tr id="trBotones">
                        <td colspan="2" align="center" style="height:40px; background-color:White;">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" width="60px" CssClass="buttonBBVA2"></asp:Button>
                        </td>                
                    </tr>
                </table>
            </fieldset>
        </fieldset>
        </div>
        <asp:Label runat="server" ID="lblMensaje"></asp:Label>

</asp:Content>

