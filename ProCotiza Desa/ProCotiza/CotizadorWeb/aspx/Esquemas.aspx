<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="Esquemas.aspx.vb" Inherits="aspx_Esquemas" %>

<%--BBVA RQTARESQ-06 CGARCIA 19/04/2017 SE CREA EL OBJETO DE ESQUEMA PARA LA RELACION DEL CATÁLOGO DE ESQUEMAS --%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
    <legend>Esquemas</legend>
        <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
                <tr>
                    <td style="width:5%;">
                        &nbsp;&nbsp;Estaus
                    </td> 
                    <td style="width:27%;">
                        <asp:DropDownList ID="CmbEstatusEsquema" runat="server" AutoPostBack="true" CssClass="selectBBVA">
                        </asp:DropDownList>
                    </td>
                    <td style="width:1%; text-align:right;">
                        &nbsp;&nbsp;Esquema
                    </td>
                    <td style="width:27%;">
                        <asp:TextBox ID="txtEsquema" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,14);"
                            onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                    </td>
                </tr>
            </table>
            </center>
            <center>
                <table>
                    <tr>
                        <td style="width:10%; text-align:right;">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" Width="60px" CssClass="buttonSecBBVA2" /></td>
                        <br />
                        <td style="width:10%; text-align:right;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="60px" CssClass="buttonBBVA2" />
                    </td>
                    </tr>
                </table>
            </center>
        </fieldset>
    <br />
        <fieldset style="width:96.5%; padding-top:10px; border: .5px solid #D8D8D8;">
            <table style="width: 100%;">
                <tr>
                    <td style="background-color: White;">
                        <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                            AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px" 
                            EmptyDataText="No se encontró información con los parámetros proporcionados.">
                            <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                            <Columns>
                                <asp:TemplateField HeaderText="Id" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center">      
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="ID_ESQUEMAS" CommandArgument='<%# Eval("ID_ESQUEMAS") %>'><%#Eval("ID_ESQUEMAS")%></asp:LinkButton>                                            
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="C_ESQUEMAS" HeaderText="Esquema" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                <asp:BoundField DataField="CDESCRIPCION" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset style="width:96.5%; padding-top:10px; border: .5px solid #D8D8D8;">
            <table  style="width: 100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" background-color:"White;">
                        <asp:Button ID="btnagregar" runat="server" Text="Agregar" Width="60px" CssClass="buttonBBVA2" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

