<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaEmpresasAfiliadas.aspx.vb" Inherits="aspx_consultaEmpresasAfiliadas" %>

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
            <legend>EMPRESAS AFILIADAS</legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <center>
            <table class="resulbbva" style="width:90%;">
                <tr>
                    <td style="width:5%;">&nbsp;&nbsp;Estatus:</td>
                    <%----BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA VISUALIZAR CORRECTAMENTE LA OPCION SELECCIONAR--%>
                    <td style="width:27%;"><asp:DropDownList ID="cmbEstatus" runat="server" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList></td>
<%--                    <td style="background-color:White;">Id Empresa:</td>
                    <td><asp:TextBox ID="txtIdEmp" runat="server" CssClass="txt2" MaxLength="7" Width="80px" Onkeypress="return validarNro('N',event);"></asp:TextBox></td>--%>
                    <td style="width:1%; text-align:right;">&nbsp;&nbsp;Razón Social:&nbsp;</td>
                    <td style="width:27%;"><asp:TextBox ID="txtRazSoc" runat="server" CssClass="txt3BBVA" MaxLength="100" style="text-transform:uppercase" Onkeypress="return ValCarac(event,11)"></asp:TextBox></td>
                </tr>
            </table>
            </center>
                <center>
                <table class="resulbbva">
                    <tr>
                        <td style="padding-top:5px;">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" width="60px" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                        <td style="padding-top:5px;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" width="60px" CssClass="buttonBBVA2"></asp:Button><br />
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
                                    <asp:TemplateField HeaderText="Id" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2id" ItemStyle-Width="42">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="empId" CommandArgument='<%# Eval("id_empresa") %>'><%#Eval("id_empresa")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="razon_social" HeaderText="Razón Social" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="nombre_corto" HeaderText="Nom. Corto" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="inicio_vigencia" HeaderText="Inicio Vigencia" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="fin_vigencia" HeaderText="Fin Vigencia" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div class="tooltip">
                                                <asp:ImageButton ID="ImageButton1" runat="server" Width="16" Height="16" CommandName="empAge" ImageUrl="../img/house_go.png" AlternateText="Relaciona Plazas" CommandArgument='<%# Eval("id_empresa") %>' />
                                                <span class="tooltiptext">Asigna Agencia</span>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                </>
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

