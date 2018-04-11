<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaAseguradoras.aspx.vb" Inherits="aspx_consultaAseguradoras" %>

<%--BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.--%>
<%--BUG-PC-40:PVARGAS:27/01/2017:SE CAMBIA CSSCLASS DEL TEXTBOX txtrazonsocial.--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-46 JRHM 09/02/17 Se modifica validacion de caracteres de nombre aseguradora--%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
    <legend>ASEGURADORAS</legend>
        <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
		    <tr>
                <td style="width:5%;">Estatus</td>
                <td style="width:27%;"><asp:DropDownList runat="server" ID="ddlestatus" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                <td style="width:10%; text-align:right;">Broker</td>
                <td style="width:27%;"><asp:DropDownList runat="server" ID="ddlbroker" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                <td style="width:8%; text-align:right;">Nombre:</td>
                <%--BUG-PC-40:PVARGAS:27/01/2017:SE CAMBIA CSSCLASS DEL TEXTBOX txtrazonsocial.--%>
                <td style="width:27%;"><asp:TextBox runat ="server" ID="txtrazonsocial" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,11);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
            </tr>
	    </table>
        </center>
        <center>
        <table>
            <tr>
                <td style="width:10%; text-align:right;">
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" width="60px" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                    <td style="width:10%; text-align:right;">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" width="60px" CssClass="buttonBBVA2"></asp:Button><br />
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
                                          AllowPaging="true" PageSize="8" Width="100%" BorderWidth="0px" 
                                          EmptyDataText="No se encontró información con los parámetros proporcionados.">
                            <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                            <Columns>
                                <asp:TemplateField HeaderText="Id" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="AseguradoraId" CommandArgument='<%# Eval("id_aseguradora") %>'><%#Eval("id_aseguradora")%></asp:LinkButton>
                                    </ItemTemplate>                                            
                                </asp:TemplateField>
                                <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="NOM_CORTO" HeaderText="Nom. Corto" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="ID_EXTERNO" HeaderText="Externo" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="TEXTO" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center"/>
                                <%--<asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15px" ControlStyle-Width="15px">
                                    <ItemTemplate>
                                        <div class="tooltip">
                                            <asp:ImageButton ID="ImageButton1" runat="server" Width="16" Height="16" CommandName="IDAseg" CommandArgument='<%# Eval("id_aseguradora") %>' ImageUrl="../img/building.png" AlternateText="Relaciona Brokers" />
                                            <span class="tooltiptext">Asigna Broker</span>
                                        </div>
                                    </ItemTemplate>                                            
                                </asp:TemplateField>--%>
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

