<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaProductos.aspx.vb" Inherits="aspx_consultaProductos" %>
<%--BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.--%>
<%--BUG-PC-02:  09/11/2016: AMR: Error en manejaProductos--%>
<%--BUG-PC-18:  25/11/2016: AMR: CUTARPR01-El campo dice Tipo, se solicitó Tipo de Producto.--%>
<%--BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA MOSTRAR CORRECTAMENTE LA OPCION SELECCIONAR--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--RQ-MN2-6:RHERNANDEZ: 07/09/17: SE AGREGA ID BROKER PARA LA BUSQUEDA MAS AGIL DE PRODUCTOS--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
    <legend>PRODUCTOS</legend>
        <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
		        <tr>
                <%--BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA MOSTRAR CORRECTAMENTE LA OPCION SELECCIONAR--%>
                    <td style="text-align:right">Tipo de Producto:</td> <%--BUG-PC-02--%>
                    <td style="width:20%;"><asp:DropDownList ID="cmbTipoProd" runat="server" CssClass="selectBBVA"  AutoPostBack="true"></asp:DropDownList></td> <%--BUG-PC-02--%>
                    <td style="text-align:right">Marca:</td>
                    <td style="width:20%;"><asp:DropDownList ID="cmbMarca" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                    <td style="text-align:right">Submarca: </td>
                    <td style="width:20%;"><asp:DropDownList ID="cmbSubmarca" runat="server" CssClass ="selectBBVA"  AutoPostBack="true"></asp:DropDownList></td>                    
                    <td style="text-align:right">Clasificacion: </td>
                    <td style="width:20%;"><asp:DropDownList ID="cmbclasif" runat="server" CssClass="selectBBVA"  AutoPostBack="true"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="text-align:right">Broker: </td>
                    <td><asp:DropDownList ID="cmbBroker" runat="server" CssClass="selectBBVA" AutoPostBack ="true"></asp:DropDownList></td>
                    <td style="text-align:right">Estatus:</td>
                    <td><asp:DropDownList ID="cmbEstatus" runat="server" CssClass="selectBBVA"  AutoPostBack="true"></asp:DropDownList></td>
                    <td style="text-align:right">Descripción:</td>
                    <td><asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" Width="190px" MaxLength="100" Onkeypress="return ValCarac(event,12)"></asp:TextBox></td> <%--BUG-PC-02--%>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
            	</tr>
	    </table>
        </center>
        <center>
            <table class="resulbbva">
                <tr>
                    <td style="padding-top:5px;"> 
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="buttonSecBBVA2"></asp:Button><br />
                    </td>
                     <td style="padding-top:5px;"> <%--BUG-PC-02--%>                        
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar"  CssClass="buttonBBVA2"></asp:Button><br />
                    </td> 
                </tr>
            </table>
        </center>
        </fieldset>
        <br />
        <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
            <table style="width:90%;">
                    <tr>
                        <td>
                            <%--BBVA-P-412--%>
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                      AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px"
                                      EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                            <Columns>                                
                                <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="prodId" CommandArgument='<%# Eval("id_producto") %>'><%# Eval("id_producto")%></asp:LinkButton>
                                    </ItemTemplate>                                            
                                </asp:TemplateField>
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="300px" ControlStyle-Width="300px"/>
                                <asp:BoundField DataField="ANIO_MODELO" HeaderText="Modelo" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="tipo_producto" HeaderText="Tipo Producto" ItemStyle-HorizontalAlign="Center"/> <%--BUG-PC-02--%>
                                <asp:BoundField DataField="marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="SUBMARCA" HeaderText="Submarca" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="CLASIFICACION" HeaderText="Clasificación" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="TEXT_BROKER" HeaderText="Broker" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="precio" HeaderText="Precio" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <asp:BoundField DataField="PRECIO_SEMINUEVO" HeaderText="Precio SemiNuevo" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                                <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center"/>
<%--                                <asp:TemplateField HeaderText="Opc" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Width="16" Height="16" CommandName="prdAseg" ImageUrl="../img/building.png" AlternateText="Registra Aseguradoras" />
                                        <asp:ImageButton ID="ImageButton2" runat="server" Width="16" Height="16" CommandName="prdAge" ImageUrl="../img/house_go.png" AlternateText="Relaciona Plazas" />
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

