<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaAccesorios.aspx.vb" Inherits="aspx_consultaAccesorios" %>

<%--BBV-P-412:AVH:09/08/2016 RQ20.2 SE AGREGA MENSAJE DE ERROR EN EL GV--%>
<%--BBVA-P-412: 08/08/2016: AMR: RQ09 – Carga Masiva de Plan de Financiamiento--%>
<%--BUGPC03 CO02-CO19: 10/11/2016: GVARGAS: Correccion bugs--%>
<%--BBVA-P-412: JRHM 14/11/2016: BUG-PC-08 – Inserccion de valor seleccionar a combobox--%>
<%--BBVA-P-412:BUG-PC-19 JRHM 24/11/2016 Se cambiaron eventos keypress y keyup de textbox--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 19/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: relative; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;">
            <legend>ACCESORIOS</legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <center>
                <table class="resulbbva" style="width:90%;">
                    <tr>
                        <td style="text-align:right">Tipos de Productos:</td>
                        <td style="width:27%;">
                            <asp:DropDownList ID="cmbTipoProd" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                        <%--JRHM 14/11/16 Se activo autopostback para actializar datos de busqueda al sleccionar valor de combo.--%>
                        <td style="text-align:right">&nbsp;&nbsp;Marca:</td>
                        <td style="width:27%;">
                            <asp:DropDownList ID="cmbMarca" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                        <%--JRHM 14/11/16 Se activo autopostback para actializar datos de busqueda al sleccionar valor de combo.--%>
                        <td style="text-align:right">&nbsp;&nbsp;Estatus:</td>
                        <td style="width:27%;">
                            <asp:DropDownList ID="cmbEstatus" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                        <%--JRHM 14/11/16 Se activo autopostback para actializar datos de busqueda al sleccionar valor de combo.--%>
                        <td style="text-align:right">Descripción:</td>
                        <td style="width:27%;">
                            <asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                    </tr>
                </table>
                </center>
                <center>
                <table class="resulbbva">
                    <tr>
                        <td style="padding-top:5px;">
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar"  CssClass="buttonSecBBVA2"></asp:Button><br />
                        </td>
                        <td style="padding-top:5px;">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar"  CssClass="buttonBBVA2Pro"></asp:Button><br />
                        </td>
                    </tr> 
                </table>
                </center>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
                <table style="width: 90%;">
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
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="accesorioId" CommandArgument='<%# Eval("id_accesorio") %>'><%#Eval("id_accesorio")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Accesorio" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="tipo_producto" HeaderText="Tipo de Producto" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <table class="resulbbva">
                    <tr id="trBotones">
                        <td colspan="2" align="Center" style="height: 40px; background-color: White;">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="60px" CssClass="buttonBBVA"></asp:Button>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

