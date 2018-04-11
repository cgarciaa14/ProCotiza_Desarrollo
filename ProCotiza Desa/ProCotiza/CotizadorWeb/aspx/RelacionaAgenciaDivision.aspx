<%@  Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="RelacionaAgenciaDivision.aspx.vb" Inherits="aspx_RelacionaAgenciaDivision" %>

<%--BBV-P-412:AVH:06/07/2016 RQ19: SE CREA RELACION DE DIVISIONES--%>
<%--BUG-PC-24 08/12/2016 MAUT Se agrega EmptyDataText--%>
<%--BUG-PC-31 JRHM 03/01/17 Se modifico orden de ejecucion de funciones.js --%>
<%--BUG-PC-34 12/01/2017 MAUT Se cambian validaciones a filtro de agencia--%>
<%--BUG-PC-38 MAUT 23/01/2017 Se cambia maxlength de filtro de Agencia--%>
<%--BUG-PC-39 JRHM 25/01/17 Se agrego boton limpiar y se aplico su funcionalidad--%>
<%--BUG-PC-43 01/02/17 JRHM Se agrego clase wrap a tabla para casos donde nombre de agencia sea demasiado grande para tabla--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RELACION AGENCIA</title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);

        .style1 {
            width: 93px;
        }
    </style>

    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/gridviewScroll.min.js"></script>
</asp:Content>
<%--</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div id="cuerpoConsul" style="position: relative; margin-left: 20px; top: 15px;">
            <fieldset class="fieldsetBBVA" style="width: 100%; padding: 15px;">
                <legend>
                    <asp:Label runat="server" ID="lblTitulo" Text=""></asp:Label>
                </legend>
                <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                    <center>
                        <table class="resulbbva">
                            <tr>
                                <td style="width:5%;">División:</td>
                                <td style="width:27%;">
                                    <asp:DropDownList ID="cmbDivision" runat="server" CssClass="selectBBVA"
                                        AutoPostBack="true">
                                    </asp:DropDownList></td>
                                <td style="width:10%; text-align:right;">Nombre Agencia:</td>
                                <td style="width:27%;">
                                    <asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="90"
                                        Width="150px" Onkeypress="return ValCarac(event,11);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                                <%--BUG-PC-34 12/01/2017 MAUT Se cambian validaciones--%>
                                <%--BUG-PC-38 MAUT 23/01/2017 Se cambia maxlength a 90--%>
                                <td>
                                    <asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaProd" Text="Buscar" /></td>
                            </tr>
                        </table>
                    </center>
                    <center>
                        <table class="resulbbva">
                            <tr>
                                <td align="center" style="width: 10%;">
                                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" Width="60px" CssClass="buttonSecBBVA2"></asp:Button><br />
                                </td>
                                <td align="center" style="width: 10%;">
                                    <asp:Button ID="btnNinguna" runat="server" Text="Ninguna" Width="60px" CssClass="buttonSecBBVA2"></asp:Button><br />
                                </td>
                                <td align="center" style="width: 10%;">
                                    <asp:Button ID="btnTodas" runat="server" Text="Todas" Width="60px" CssClass="buttonSecBBVA2"></asp:Button><br />
                                </td>
                            </tr>
                        </table>
                    </center>
                <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 210px; overflow-x: hidden">
                <br />
                <fieldset>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <%--BUG-PC-24 08/12/2016 MAUT Se agrega EmptyDataText--%>
                                <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="20" Width="100%" BorderWidth="0px"
                                    DataKeyNames="ID_AGENCIA, NOMBRE,ID_DIVISION"
                                    EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <Columns>
                                        <asp:BoundField DataField="ID_AGENCIA" HeaderText="ID" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="NOMBRE" HeaderText="Agencia" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                        <asp:BoundField DataField="DIVISION" HeaderText="Division" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" ControlStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="ID_DIVISION" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" AlternateText="Relación" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID_DIVISION" HeaderText="" Visible="false" />
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
    <%--</form>
</body>
</html>--%>
