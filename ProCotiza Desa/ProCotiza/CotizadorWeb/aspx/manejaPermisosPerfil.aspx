<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaPermisosPerfil.aspx.vb" Inherits="aspx_manejaPermisosPerfil" %>

<%--''BBVA-P-412: 08/08/2016: AMR: RQ09 – Se crea scroll en la pantalla--%>
<%--BBV-P-412  RQ-E  gvargas   12/10/2016 Actualizacion de referencias. --%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/gridviewScroll.min.js"></script>

    <script type="text/javascript" src="../ExternalScripts/jquery.cookie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: relative; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;">
            <legend>PERMISOS - PERFILES</legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <center>
            <table class="resulbbva" style="width:90%;">
            <tr>
                <td style="width:5%;">Perfil:</td>
                <td style="width:27%;"><asp:DropDownList runat="server" ID="cmbPerfil" CssClass="selectBBVA" Width="150px" AutoPostBack="true"></asp:DropDownList></td>
                <td style="width:10%; text-align:right;">&nbsp;&nbsp;Menú:</td>
                <td style="width:27%;"><asp:DropDownList ID="cmbMenu" runat="server" CssClass="selectBBVA" Width="150px" AutoPostBack="true"></asp:DropDownList></td>
            </tr>
        </table>
        </center>
                <center>
            <table class="resulbbva" style="width:90%;">
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
                <div style="width: 100%; height: 200px; overflow: auto;">
                    <table class="resulBBVA" style="width: 100%;">
                        <tr>
                            <td style="background-color: White;">
                                <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                    DataKeyNames="ID_OBJETO, ASIGNADO">
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" ControlStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkmenu" AutoPostBack="true" OnCheckedChanged="chkmenu_CheckedChanged" Checked='<%# IIf(Eval("ASIGNADO")=1, "true", "false") %>' CommandName="ObjetoId" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TEXTO" HeaderText="MENU" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" ControlStyle-Width="100px" />
                                        <asp:BoundField DataField="ID_OBJETO" HeaderText="OBJ ID" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:BoundField DataField="ASIGNADO" HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <table class="resulbbva" style="width: 100%;">
                    <tr id="trBotones">
                        <td colspan="2" align="center" background-color:"White;">
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

