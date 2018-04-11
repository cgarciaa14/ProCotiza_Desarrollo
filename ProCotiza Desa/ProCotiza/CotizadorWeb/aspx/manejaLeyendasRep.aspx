<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaLeyendasRep.aspx.vb" Inherits="aspx_manejaLeyendasRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<%----BBV-P-412 RQCOT-05: AVH: 08/09/2016 SE AGREGA SECCION--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 17/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS JAGUAR--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: relative; margin-left: 20px; top: 15px;">
        <fieldset class="fieldsetBBVA" style="width: 900px; padding: 15px;">
            <legend>LEYENDAS REPORTE</legend>
            <fieldset>
                <table class="resulbbva" style="width: 100%;">
                    <tr>
                        <td style="width: 10%; height: 25px; background-color: White;">&nbsp;&nbsp;&nbsp;Id:</td>
                        <td>
                            <asp:Label runat="server" ID="lblId"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="top" style="background-color: White;">* Leyenda:</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtLeyenda" Width="200px" Height="120px" Rows="4" TextMode="MultiLine" MaxLength="255" CssClass="resulbbva"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: White;">* Estatus:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA" Width="206px"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: White;">* Seccion:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="cmbSeccion" CssClass="selectBBVA" Width="206px"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table class="resulbbva" style="width: 500px;">
                    <tr>
                        <td style="background-color: White; width: 5px;">
                            <br />
                            <asp:GridView runat="server" ID="gdvClasif" 
                                Width="80%" AutoGenerateColumns="false" 
                                HeaderStyle-CssClass="encabezados" HeaderStyle-HorizontalAlign="Left">
                                <Columns>
                                    <asp:BoundField DataField="id_clasificacion" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="clasificacion" HeaderText="* Clasificación Producto" ItemStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <table class="resulbbva" style="width: 500px;">
                    <tr>
                        <td style="background-color: White; width: 5px;">
                            <asp:GridView runat="server" ID="gdvPerJur" 
                                Width="80%" AutoGenerateColumns="false" 
                                HeaderStyle-CssClass="encabezados" HeaderStyle-HorizontalAlign="Left">
                                <Columns>
                                    <asp:BoundField DataField="id_personalidad" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="personalidad" HeaderText="* Personalidad Jurídica" ItemStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>

            </fieldset>
            <br />
            <fieldset>
                <center>
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
           </center>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

