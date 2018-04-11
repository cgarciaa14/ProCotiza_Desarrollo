<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaTiposOperacion.aspx.vb" Inherits="aspx_consultaTiposOperacion" %>

<%----BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA VISUALIZAR CORRECTAMENTE LA OPCION SELECCIONAR--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: relative; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;">
            <legend>TIPOS DE OPERACIÓN</legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <center>
            <table class="resulbbva" style="width:90%;">
                    <tr>
                        <td style="width:5%;">&nbsp;&nbsp;Estatus:</td>
                        <%----BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA VISUALIZAR CORRECTAMENTE LA OPCION SELECCIONAR--%>
                        <td style="width:27%;"><asp:DropDownList ID="cmbEstatus" runat="server" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList></td>  
                        <td style="width:1%; text-align:right;">Descripción:</td>
                        <td style="width:27%;"><asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return validarNro('C1',event);"></asp:TextBox></td>
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
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <table style="width: 100%;">
                    <tr>
                        <td style="background-color: White;">
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px"
                                EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="tipOpId" CommandArgument='<%# Eval("id_tipo_operacion") %>'><%#Eval("id_tipo_operacion")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="clave" HeaderText="Clave" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="nombre" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div class="tooltip">
                                                <asp:ImageButton ID="ImageButton1" runat="server" Width="16" Height="16" CommandName="toGiro" ImageUrl="../img/user_suit.png" AlternateText="Relaciona Giros" CommandArgument='<%# Eval("id_tipo_operacion") %>' />
                                                <span class="tooltiptext">Asigna Giro</span>
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
                <table style="width: 100%;">
                    <tr id="trBotones">
                        <td colspan="2" align="center" style="height: 40px; background-color: White;">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="60px" CssClass="buttonBBVA2"></asp:Button>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

