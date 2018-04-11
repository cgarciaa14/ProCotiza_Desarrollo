<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaEstados.aspx.vb" Inherits="aspx_consultaEstados" %>

<%--BBV-P-412  RQ-E  gvargas   12/10/2016 Actualizacion de referencias. --%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
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

    <%--    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=grvConsulta.ClientID%>').gridviewScroll({
                width: 870,
                height: 250,
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "../img/arrowvt.png",
                varrowbottomimg: "../img/arrowvb.png",
                harrowleftimg: "../img/arrowhl.png",
                harrowrightimg: "../img/arrowhr.png"
            });
        }

    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: relative; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;">
            <legend>ESTADOS</legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <center>
                <table class="resulbbva">
                    <tr>
                        <td style="width:7%;">&nbsp;&nbsp;Estatus:</td>
                        <td style="width:27%;">
                            <asp:DropDownList ID="cmbEstatus" runat="server" CssClass="selectBBVA"></asp:DropDownList></td>
                        <td style="width:1%;">Nombre:</td>
                        <td style="width:27%;">
                            <asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return validarNro('C1',event);"></asp:TextBox></td>
                    </tr>
                </table>
                </center>
                <center>
                    <table style="width:100%;">
                        <tr>
                            <td align="center">
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" Width="60px" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                            <td align="center">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="60px" CssClass="buttonBBVA2"></asp:Button><br />
                        </td>
                        </tr>
                    </table>
                </center>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
                <table style="width: 100%;">
                    <tr>
                        <td style="background-color: White;">
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="10"
                                Width="100%" BorderWidth="0px">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="42">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="estadoId" CommandArgument='<%# Eval("id_estado") %>'><%#Eval("id_estado")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nombre" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="clave" HeaderText="Clave" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" />
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
</asp:Content>

