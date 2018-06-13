<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="relacionaPaquetesProductos.aspx.vb" Inherits="aspx_relacionaPaquetesProductos" %>

<%--BBVA-P-412: 06-07-2016: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08 SE AGREGA FUNCIONALIDAD PARA LA RELACIÓN PAQUTES PRODUCTOS--%>
<%--BUG-PC-25 MAUT 15/12/2016 Se valida Modelo--%>
<%--BBV-P-412:BUG-PC-35 18/01/2017 JRHM Se cambio orden de ejecucion de scripts--%>
<%--BUG-PC-42 JRHM 30/01/17 Se agrega funcionalidad de boton limpiar--%>
<%--RQ-MN2-6: RHERNANDEZ: 15/09/17: SE AGREGAN NUEVOS FILTROS PARA LA OPCION 11 DE CONSULTA PAQUETES-PRODUCTOS--%>
<%--BUG-PC-137 29/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--'RQ-PC9: CGARCIA: 21/05/2018: SE CRA FILTRO DE ALIANZA--%>
<%--'BUG-PC-204: CGARCIA: 06/06/2018: SE REVERSA FILTRO RQ-PC9--%>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PAQUETES - PRODUCTOS</title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/gridviewScroll.min.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<%--</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="cuerpoConsul" style="position: relative; margin-left: 20px; top: 15px;">
        <fieldset class="fieldsetBBVA" style="width: 700px; padding: 15px;">
            <legend>RELACIONA PAQUETES - PRODUCTOS</legend>
            <fieldset>
                <table class="resulbbva">
                    <tr>
                        <%--BBVA-P-412--%>
                        <td style="width: 7%;">Marca:</td>
                        <td style="width: 27%;">
                            <asp:DropDownList ID="cmbMarca" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                        <td style="width: 10%; text-align: right;">Submarca: </td>
                        <td style="width: 27%;">
                            <asp:DropDownList ID="cmbSubmarca" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                        <td style="width: 9%; text-align: right;">Modelo:</td>
                        <td style="width: 27%;">
                            <asp:TextBox ID="txtmodelo" runat="server" CssClass="txt3BBVA" Onkeypress="return validarNro('N',event);"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Clasificacion: </td>
                        <td>
                            <asp:DropDownList ID="cmbclasif" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                        <td style="text-align: right;">Broker: </td>
                        <td>
                            <asp:DropDownList ID="cmbBroker" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                        <%--BUG-PC-25 MAUT 15/12/2016 Se cambia ValidarNro a N--%>


                        <td style="text-align: right;">Producto:</td>
                        <td>
                            <asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return validarNro('A1',event);"></asp:TextBox></td>
                    </tr>
                    <%--<tr>
                        <td style="text-align: right;">Alianza: </td>
                        <td>
                            <asp:DropDownList ID="ddlAlianza" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList>
                        </td>
                    </tr>--%>
                </table>
                <table class="resulbbva">
                    <tr>
                        <td>
                            <asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaProd" Text="Buscar" /></td>
                        <td>
                            <asp:Button ID="btnNinguna" runat="server" Text="Ninguna" Width="60px" CssClass="buttonSecBBVA2" /></td>
                        <td>
                            <asp:Button runat="server" CssClass="buttonSecBBVA2" ID="btnLimpiar" Text="Limpiar" /></td>
                        <%--<td><asp:Button ID="btnTodas" runat="server" Text="Todas" width="60px" CssClass="button"></asp:Button><br /></td>--%>
                        <td>
                            <asp:Button ID="btnTodas" runat="server" Text="Todas" Width="60px" CssClass="buttonSecBBVA2" /></td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 210px; overflow-x: hidden">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <%--BBVA-P-412--%>
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="20" Width="100%" BorderWidth="0px"
                                DataKeyNames="ID_PRODUCTO, ID_MARCA, ASIG"
                                EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:BoundField DataField="MARCA" HeaderText="Marca" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" ControlStyle-Width="10px" />
                                    <asp:BoundField DataField="SUBMARCA" HeaderText="Submarca" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="PRODUCTO" HeaderText="Producto" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="AÑO" HeaderText="Modelo" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="CLASIF" HeaderText="Clasificación" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="IDBROKER" HeaderText="Broker" ItemStyle-HorizontalAlign="Center" />
                                    <%--<asp:BoundField DataField="ALIANZA" HeaderText="Alianza" ItemStyle-HorizontalAlign="Center" />--%>
                                   <%-- <asp:BoundField DataField="AGENCIA" HeaderText="Agencia" ItemStyle-HorizontalAlign="Center" />--%>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="ProdID" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" AlternateText="Relación" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID_PRODUCTO" HeaderText="" Visible="false" />
                                    <asp:BoundField DataField="ID_MARCA" HeaderText="" Visible="false" />
                                    <asp:BoundField DataField="ASIG" HeaderText="" Visible="false" />

                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>
<%-- </form>
</body>
</html>--%>
