<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaLeyendasRep.aspx.vb" Inherits="aspx_consultaLeyendasRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>

</asp:Content>

<%----BBV-P-412 RQCOT-05: AVH 08/09/2016 SE AGREGA SECCION--%>
<%----BUG-PC-40:PVARGAS:27/01/2017: SE AGREGA ACENTO AL TEXTO--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-48 JRHM 16/02/17 SE QUITO VALIDACION DE CARACATERES DE TEXTBOX  Y SE AGREGO SCROLL EN TABLA PARA EVITTAR PERDIDA DE BOTON DE AGREGAR EN PANTALLA--%>
<%--BUG-PC-137 19/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 17/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS JAGUAR--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: relative; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;"><%--ESTE DISEÑO SE OBTUVO DE CONSULTACOTIZACIONES EL CUAL EXTIENDE EL FIELDSET AL 100% DE LA PANTALLA--%>
            <legend>LEYENDAS COTIZACIÓN</legend>
            <%--<fieldset>
                <table class="resul2">diseño original--%>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <%--se agrego esto--%>
                <center>
                <table class="resulbbva" style="width:90%;">
                    <tr>
                        <%----PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA VISUALIZAR CORRECTAMENTE LA OPCION SELECCIONAR--%>
                        <td style="text-align:right">Estatus:</td>
                        <td style="width:27%;">
                            <asp:DropDownList ID="cmbEstatus" runat="server" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList></td>
                        <td style="text-align:right">
                            <%----BUG-PC-40:PVARGAS:27/01/2017: SE AGREGA ACENTO AL TEXTO--%>
                            Secci&oacuten:
                        </td>
                        <td style="width:27%;">
                            <asp:DropDownList runat="server" ID="cmbSeccion" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList>
                        </td>
                        <td style="text-align:right">Leyenda:</td>
                        <%----PVARGAS:27/01/2017: SE CAMBIA LA OPCION DE LA FUNCION JS A 6 PARA TRAER LETRAS MINUSCULAS, MAYUSCULAS Y ESPACIOS--%>
                        <td style="width:27%;">
                            <asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="50" Width="190px"></asp:TextBox></td>
                    </tr>
                </table>
                </center>
                <center>
        <table class="resulbbva">
            <tr>
                <td style="padding-top:5px;">
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="buttonSecBBVA2"></asp:Button><br />
                </td>
                <td style="padding-top:5px;">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="buttonBBVA2"></asp:Button><br />
                </td>            
            </tr>
        </table>
            </fieldset>
            </center>
                <br />
            <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
                <%--<fieldset  ">--%>
                <table style="width: 90%;">
                    <tr>
                        <td>
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px"
                                EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <%--<asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                            AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px" PagerStyle-HorizontalAlign="Center"
                                            EmptyDataText="No se encontró información con los parámetros proporcionados.">--%>
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="leyendaId" CommandArgument='<%# Eval("id_leyenda") %>'><%#Eval("id_leyenda")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="texto" HeaderText="Leyenda" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="resul2wrap" />
                                    <asp:BoundField DataField="NOM_SECCION" HeaderText="SECCION" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                    <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <table  style="width:90%;">
                    <tr id="trBotones">
                        <td colspan="2" align="Center" style="height:40px; background-color:White;">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="60px" CssClass="buttonBBVA2"></asp:Button>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

