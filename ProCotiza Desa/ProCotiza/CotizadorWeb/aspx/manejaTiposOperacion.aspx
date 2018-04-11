<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaTiposOperacion.aspx.vb" Inherits="aspx_manejaTiposOperacion" %>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="position: relative; margin-left: 20px; top: 15px;">
        <fieldset class="fieldsetBBVA" style="width: 900px; padding: 15px;">
            <legend>TIPOS DE OPERACIÓN</legend>
            <fieldset>
                <div style="height: 400px; overflow: auto">
                    <table class="resulbbva" style="width: 100%;">
                        <tr>
                            <td style="width: 20%">Id:</td>
                            <td class="resul">
                                <asp:Label runat="server" ID="lblId"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>* Tipo Esquema:</td>
                            <td class="resul">
                                <asp:DropDownList runat="server" ID="cmbTipEsq" CssClass="selectBBVA"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>* Nombre:</td>
                            <td class="resul">
                                <asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" MaxLength="100" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>* Clave:</td>
                            <td class="resul">
                                <asp:TextBox runat="server" ID="txtClave" CssClass="txt3BBVA" MaxLength="50" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>* Estatus:</td>
                            <td class="resul">
                                <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA" Width="200px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Capital Incluye IVA:</td>
                            <td class="resul">
                                <asp:CheckBox runat="server" ID="chkCapIncIva" CssClass="resul" />
                            </td>
                        </tr>
                        <tr>
                            <td>Pide Opción de Compra:</td>
                            <td class="resul">
                                <asp:CheckBox runat="server" ID="chkOpcComp" CssClass="resul" />
                            </td>
                        </tr>
                        <tr>
                            <td>Pide Valor Residual:</td>
                            <td class="resul">
                                <asp:CheckBox runat="server" ID="chkValResid" CssClass="resul" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>* Leyenda Valor Residual:</td>
                            <td class="resul">
                                <asp:TextBox runat="server" ID="txtLeyValRes" CssClass="txt3BBVA" MaxLength="50" onkeypress="ManejaCar('t',1,this.value)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Pide Depósito en Garantía:</td>
                            <td class="resul">
                                <asp:CheckBox runat="server" ID="chkDepGar" CssClass="resul" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>* Leyenda Depósito en Garantía:</td>
                            <td class="resul">
                                <asp:TextBox runat="server" ID="txtLeyDepGar" CssClass="txt3BBVA" MaxLength="50" onkeypress="ManejaCar('t',1,this.value)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Cobra Gastos Extra:</td>
                            <td class="resul">
                                <asp:CheckBox runat="server" ID="chkGtosExt" CssClass="resul" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>* Tipo Gasto:</td>
                            <td class="resul">
                                <asp:DropDownList runat="server" ID="cmbGtoExt" CssClass="selectBBVA"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Permite Cálculo Pago Inicial:</td>
                            <td class="resul">
                                <asp:CheckBox runat="server" ID="chkPagoIni" CssClass="resul" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>* Fórmula Pago Inicial:</td>
                            <td class="resul">
                                <asp:DropDownList runat="server" ID="cmbForPagIni" CssClass="selectBBVA"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Cobrar IVA Pago:</td>
                            <td class="resul">
                                <asp:GridView runat="server" ID="gdvPerJur" Width="100%" AutoGenerateColumns="false" CssClass="resul">
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <Columns>
                                        <asp:BoundField DataField="tipo_persona" HeaderText="Tipo Personalidad" ItemStyle-Width="150px" ControlStyle-Width="140px" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Sobre Interés" ItemStyle-Width="15px" ControlStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Sobre Capital" ItemStyle-Width="15px" ControlStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id_per_juridica" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <br />
            <fieldset>
                <table style="width: 100%;">
                    <tr id="trBotones">
                        <td align="center" style="background-color: White;">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                        <td align="center" style="background-color: White;">
                            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>

</asp:Content>

