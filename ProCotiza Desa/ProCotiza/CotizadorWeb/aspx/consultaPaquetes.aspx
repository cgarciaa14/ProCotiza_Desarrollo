<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaPaquetes.aspx.vb" Inherits="aspx_consultaPaquetes" %>

<%--BBVA-P-412: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08--%>
<%--BBVA-P-412: 08/08/2016: AMR: RQ09 – Carga Masiva de Plan de Financiamiento--%>
<%--BUG-PC-02:  09/11/2016: AMR: Error en manejaProductos--%>
<%--BUG-PC-42 JRHM 30/01/17 Se agrega funcionalidad de boton limpiar--%>
<%--BUG-PC-40:PVARGAS:27/01/2017:SE AGREGA LA FUNCION PARA VALIDAR SOLO NUMEROS--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 16/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: relative; margin-left: 20px; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;">
            <legend>PAQUETES FINANCIEROS</legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <center>
            <table class="resulbbva" style="width:90%;">
		        <tr>
                    <td style="text-align:right">Estatus: </td>
                    <td style="width:27%;">
                        <asp:DropDownList runat="server" ID="ddlestatus" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList>

                    </td>
                    <td style="text-align:right">Moneda:</td>
                    <td style="width:27%";><asp:DropDownList runat="server" ID="ddlmoneda" AutoPostBack="true" CssClass="selectBBVA" Enabled="false"></asp:DropDownList></td> <%--BBVA-P-412--%>
                    <td style="text-align:right">Nombre: </td>
                    <td style="width:27%";><asp:TextBox runat="server" ID="txtnombre" CssClass="txt3BBVA"  Width="190px" style="text-transform:uppercase"></asp:TextBox></td>
                    <tr>
                    <td style="text-align:right">ID Plan:</td>
                    <%--BUG-PC-40:PVARGAS:27/01/2017:SE AGREGA LA FUNCION PARA VALIDAR SOLO NUMEROS--%>
                    <td style="width:27%;"  ><asp:TextBox runat="server" ID="txtidpaq" CssClass="txt3BBVA"  Width="190px" onkeypress="return ValCarac(event,7);"></asp:TextBox></td>
                    <td style="text-align:right">Alianza: </td>
                    <td style="width:27%";><asp:DropDownList ID="ddlAlianza" runat="server" CssClass="selectBBVA"></asp:DropDownList></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td></tr>
            	</tr>
                </table>
                </center>
                <center>
                <table class="resulbbva">
                <tr>
					<%--BBVA-P-412--%>
                    <td style="padding-top:5px;">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="buttonSecBBVA2"></asp:Button><br />
                    <td style="padding-top:5px;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="buttonBBVA2"></asp:Button><br /></td>
                </tr>
	    </table>
        </center>
            </fieldset>

            <br />
            <%--<fieldset style="width:96.5%; padding-top:10px; border: .5px solid #D8D8D8;">--%>
            <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px"
                                EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="paqId" CommandArgument='<%# Eval("id_paquete") %>'><%#Eval("id_paquete")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" ControlStyle-Width="200px" />
                                    <%--BBVA-P-412--%>
                                    <%--<asp:BoundField DataField="MONEDA" HeaderText="Moneda" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" ControlStyle-Width="10px"/>--%>
                                    <asp:BoundField DataField="INI_VIG" HeaderText="Inicio Vigencia" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="FIN_VIG" HeaderText="Fin Vigencia" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="ESTATUS_PAQ" HeaderText="Vigencia" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="ESTATUS_DESC" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%--BBVA-P-412--%>
                                            <div class="tooltip">
                                                <asp:ImageButton ID="ImageButton1" runat="server" Width="16" Height="16" CommandName="copyPaq" ImageUrl="../img/page_copy.png" AlternateText="Copiar Plan" CommandArgument='<%# Eval("id_paquete") %>' />
                                                <span class="tooltiptext">Copia Paquete</span>
                                            </div>
                                            <div class="tooltip">
                                                <asp:ImageButton ID="ImageButton2" runat="server" Width="16" Height="16" CommandName="paqAge" ImageUrl="../img/house_go.png" AlternateText="Relaciona Agencias" CommandArgument='<%# Eval("id_paquete") %>' />
                                                <span class="tooltiptext">Relaciona Agencias</span>
                                            </div>
                                            <div class="tooltip">
                                                <asp:ImageButton ID="ImageButton3" runat="server" Width="16" Height="16" CommandName="paqProd" ImageUrl="../img/cart_go.png" AlternateText="Relaciona Productos" CommandArgument='<%# Eval("id_paquete") %>' />
                                                <span class="tooltiptext">Relaciona Productos</span>
                                            </div>
                                            <div class="tooltip" id="divrelPaqAseg" runat="server">
                                                <%--BUG-PC-02--%>
                                                <asp:ImageButton ID="ImageButton4" runat="server" Width="16" Height="16" CommandName="paqAseg" ImageUrl="../img/building.png" AlternateText="Relaciona Paquetes Seguro" CommandArgument='<%# Eval("id_paquete") %>' />
                                                <span class="tooltiptext">Relaciona Paquetes Aseguradoras</span>
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
                <center>
            <table style="width: 100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" background-color: White;">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="60px" CssClass="buttonBBVA2"></asp:Button>
                    </td>
                    <%--                    <td>
                        <asp:Button ID="btncargaplan" runat="server" Text="Carga Masiva" width="90px" CssClass="button"></asp:Button>
                    </td>--%>
                </tr>
            </table>
                </center>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

