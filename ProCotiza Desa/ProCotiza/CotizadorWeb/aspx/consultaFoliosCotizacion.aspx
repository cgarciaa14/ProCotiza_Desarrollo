<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaFoliosCotizacion.aspx.vb" Inherits="aspx_consultaFoliosCotizacion" %>

<%--BBV-P-412 RQCOT-04:AVH: 14/09/2016 SE AGREGAN FILTROS DE BUSQUEDA --%>
<%--'BBV-P-412 RQCOT-04:AVH: 20/09/2016 RECOTIZACION--%>
<%--BBV-P-412 RQ06:AMR: 11-10-2016 Administración de Planes de Financiamiento: Promociones--%>
<%--BBV-P-412  RQ-E  gvargas   12/10/2016 Actualizacion de referencias. --%>
<%--BBV-P-412  RQ D	 gvargas   03/11/2016 Mod Field Set desface segun tamaño de pantalla --%>
<%--  BBV-P-412  RQ F    gvargas   07/11/2016 Cambios CSS dinamico. --%>
<%--  BUGPC16 24/11/2016: GVARGAS: Correccion Error Date Picker--%>
<%--BUG-PC-25 MAUT 15/12/2016 Validaciones--%>
<%--BUG-PC-26: JRHM: 20/12/2016 Se modifica cantidad de cotizaciones que aparecen por pagina de grid--%>
<%--BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.--%>
<%--BUG-PC-33: JRHM: 10/01/2017 Se modifico rango de fechas para no seleccionar fechas superior a la actual--%>
<%--BUG-PC-46  JRHM 10/02/17 Se modifico validacion de campo de nombre del cliente --%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!--<link rel="stylesheet" href="//code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css"/>-->
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/gridviewScroll.min.js"></script> 
    <script type="text/javascript" src="../js/Funciones.js"></script>
     <script type="text/javascript" src="../js/datepicker-es.js"></script>

    <script type="text/javascript" src="../ExternalScripts/jquery.cookie.js"></script>
    <script type="text/javascript" src="../ExternalScripts/bootstrap.min.js"></script>

    <%--BUG-PC-25 MAUT 16/12/2016 Se agrega funcion para validar fecha--%>
    <script type="text/javascript">
        
        $(document).ready(function () {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            var settingsDate = {
                dateFormat: "dd/mm/yy",
                showAnim: "slide",
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                selectOtherMonths: true,
                autoSize: true,
                yearRange: "-99:+0",
                maxDate: "+0m +0d" 
            };

            
            $('#ContentPlaceHolder1_txtFecIni').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');
            $('#ContentPlaceHolder1_txtFecFin').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');
        });
        
    </script>

    <style type="text/css">
        .ui-datepicker 
        {
            font-size:63%;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
    <legend>CONSULTA COTIZACIONES</legend>
        <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
		        <tr>
                    <td style="width:5%;">Folio:</td>
                    <td style="width:27%;"><asp:TextBox ID="txtFolio" runat="server" CssClass="txt3BBVA" onkeypress="return ValCarac(event,7);" Width="190px"></asp:TextBox></td>
                    <%--BUG-PC-25 MAUT 16/12/2016 Se cambia etiqueta--%>
                    <td style="width:10%; text-align:right;">Fecha del:</td>
                    <td style="width:27%;">
                        <asp:TextBox ID="txtFecIni" CssClass="txt3BBVA" onpaste="false" runat="server" Width="190px"></asp:TextBox>
                        <%--BUG-PC-25 MAUT 16/12/2016 Se valida Fecha--%>
                    </td>
                    <td style="width:8%; text-align:right;">Al:</td>
                    <td style="width:27%;">
                        <asp:TextBox ID="txtFecFin" CssClass="txt3BBVA" onpaste="false" runat="server" Width="190px"
   ></asp:TextBox>
                            <%--BUG-PC-25 MAUT 16/12/2016 Se valida Fecha--%>
                    </td> 
            	</tr>
                <tr>
                    <td style="text-align:right">Agencia:</td>
                    <td><asp:DropDownList runat="server" ID="cmbAgencia" CssClass="selectBBVA" OnSelectedIndexChanged="cmbAgencia_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                    <td style="text-align:right;">
                        <asp:Label runat="server" ID="lblAlianza" Text="Alianza:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbAlianza" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                    <td  style="text-align:right;">
                        <asp:Label runat="server" ID="lblGrupo" Text="Grupo:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbGrupo" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">
                        <asp:Label runat="server" ID="lblDivision" Text="Division:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbDivision" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                    <td style="text-align:right;">
                        <asp:Label runat="server" ID="lblEstado" Text="Estado:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbEstado" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                    <td style="text-align:right;">
                        <asp:Label runat="server" ID="lblNombreCli" Text="Nombre Cliente:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNombreCli" CssClass="txt3BBVA" Width="190px"  Onkeypress="return ValCarac(event,18)" onkeyup="ReemplazaAcentos(event, this.id, this.value);" ></asp:TextBox>
                    </td>
                </tr>
	    </table>
        </center>
        <center>
        <table class="resulbbva">
            <tr >
                <td style="padding-top:5px;">
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="buttonSecBBVA2"></asp:Button><br />
                </td>
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
                                            AllowPaging="true" PageSize="8" Width="100%" BorderWidth="0px" 
                                            EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                                <Columns>            
                                    <asp:BoundField DataField="id_cotizacion" HeaderText="Folio" ItemStyle-Width="5%" ControlStyle-Width="5%" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre del cliente" />
                                    <asp:BoundField DataField="Agencia" HeaderText="Agencia" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="Categoria" HeaderText="Categoria" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Producto" HeaderText="Producto" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Center"/>              
                                    <asp:BoundField DataField="Plazo" HeaderText="Plazo" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="pago_periodo_total" HeaderText="Pago" DataFormatString="{0:C}" HtmlEncode="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="fecha_cot" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="tipo_cot" HeaderText="Tipo Cot." ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField DataField="vigencia_cot" HeaderText="Vigencia" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:TemplateField HeaderText="Edit." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgEdit" runat="server" CssClass="resul" CommandName="EditId" CommandArgument='<%# Eval("id_cotizacion") %>' ImageUrl="../img/script_edit.png" AlternateText="Edita cotización" />
                                        </ItemTemplate>                                            
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ver" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="folioId" CommandArgument='<%# Eval("id_cotizacion") %>' ImageUrl="../img/printer.png" AlternateText="Cargar cotización"/>
                                        </ItemTemplate>                                            
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sol." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnSol" runat="server" CssClass="resul" CommandName="SolId" CommandArgument='<%# Eval("id_cotizacion") %>' ImageUrl="../img/script_edit.png" AlternateText="Carga Solicitud"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>              
                            </asp:GridView>
 
                            <asp:Label runat="server" ID="lblIdAse" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblIdProm" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblIdVend" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="Label1"></asp:Label>
                            
                        </td>
                    </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <script type="text/javascript">
        $(document).ready(function () {
            pickerSettins3();
        });
    </script>
</asp:Content>

