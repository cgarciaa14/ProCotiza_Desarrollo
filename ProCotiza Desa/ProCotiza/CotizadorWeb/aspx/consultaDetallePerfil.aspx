<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaDetallePerfil.aspx.vb" Inherits="aspx_consultaDetallePerfil" %>

<%--BBV-P-412:AVH:12/07/2016 RQ01: SE CREA VENTANA DETALLE PERFIL--%>
<%--BUG-PC-42 JRHM 30/01/17 SE MODIFICO CARACTERES PERMITIDOS EN NOMBRE Y USERNAME DE USUARIO--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
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
            <legend>DETALLE PERFIL VENDEDOR</legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <center>
            <table class="resulbbva" style="width:90%;">
            <tr>
                <td style="text-align:right;">Perfil:</td>
                <%----BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA VISUALIZAR CORRECTAMENTE LA OPCION SELECCIONAR--%>
                <td style="width:27%;"><asp:DropDownList ID="ddlperfil" runat="server" CssClass="selectBBVA" AutoPostBack="true" Enabled="FALSE"></asp:DropDownList></td>
                <td style="text-align:right;">Estatus:</td>
                <td style="width:27%;"><asp:DropDownList ID="cmbEstatus" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                <td style="text-align:right;">Username:</td>
                <td style="width:27%;"><asp:TextBox ID="txtusername" runat="server" MaxLength="12" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,20);"></asp:TextBox></td>
                <td style="text-align:right;">Nombre:</td>
                <td style="width:27%;"><asp:TextBox ID="txtnombre" runat="server" MaxLength="12" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,18);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td> 
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
            <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
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
                                    <asp:TemplateField HeaderText="Id" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="usuId" CommandArgument='<%# Eval("id_usuario") %>'><%# Eval("id_usuario") %></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="username" HeaderText="Username" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="perfil" HeaderText="Perfil" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="empresa" HeaderText="Empresa" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Opc." ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <div class="tooltip">
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="resetPwd" CommandArgument='<%# Eval("id_usuario") %>' ImageUrl="../img/key_go.png" AlternateText="Reinicia Contraseña" />
                                                <%--ToolTip="Reinicia Contraseña"--%>
                                                <span class="tooltiptext">Reinicia Contraseña</span>
                                            </div>
                                            <div class="tooltip">
                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="AsigAge" CommandArgument='<%# Eval("id_usuario") %>' ImageUrl="../img/house_go.png" AlternateText="Asigna Agencia" />
                                                <span class="tooltiptext">Asigna Agencia</span>
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
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="60px" CssClass="buttonBBVA2" Visible="false"></asp:Button>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

