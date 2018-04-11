<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="ConsultaPromocionesCte.aspx.vb" Inherits="aspx_ConsultaPromocionesCte" %>
<%--BUG-PC-25 MAUT 15/12/2016 Correcciones pantalla--%>
<%----BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA VISUALIZAR CORRECTAMENTE LA OPCION SELECCIONAR--%>
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
        <legend>PROMOCIONES</legend>
            <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
		            <tr>
                        <td style="width:5%;">&nbsp;&nbsp;Estatus:</td>
                        <%----BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA VISUALIZAR CORRECTAMENTE LA OPCION SELECCIONAR--%>
                        <td style="width:27%"><asp:DropDownList ID="ddlEstatus" runat="server" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList></td>
                        <td style="width:1%; text-align:right">Promoción:</td>
                        <td style="width:27%"><asp:TextBox ID="txtPromo" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
            	    </tr>
	        </table>
            </center>
            <center>
                <table class="resulbbva" >
                    <tr>
                        <td style="padding-top:5px;">
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar"  CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                        <td style="padding-top:5px;">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar"  CssClass="buttonBBVA2"></asp:Button><br />
                        </td> 
                    </tr>
                </table>
            </center>
            </fieldset>
            <br />
            <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
                <table style="width:100%;">
                        <tr>
                            <td style="background-color:White;">
                                    <asp:GridView ID="grvResult" runat="server" AutoGenerateColumns="false" 
                                              AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px"  
                                              EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                        <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" ControlStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="promoid" CommandArgument='<%# Eval("ID_PROMOCION") %>'><%#Eval("ID_PROMOCION")%></asp:LinkButton>
                                            </ItemTemplate>                                            
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Promoción" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ID_EXTERNO" HeaderText="ID Externo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ID_CLIENTE" HeaderText="ID Cliente" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PERIODICIDAD" HeaderText="Periodicidad" ItemStyle-HorizontalAlign="Center" />
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
                        <td colspan="2" align="center" style="height:40px; background-color:White;">
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

