<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="relacionaAgencias.aspx.vb" Inherits="aspx_relacionaAgencias" %>

<%--BUG-PC-38 MAUT 23/01/2017 Se cambia maxlength de filtro de Agencia--%>
<%--BUG-PC-43 01/02/17 JRHM Se agrego clase wrap a tabla para casos donde nombre de agencia sea demasiado grande para tabla--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/gridviewScroll.min.js"></script>
</asp:Content>

<%--</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="cuerpoConsul" style="position: relative; margin-left: 20px; top: 15px;">
        <fieldset class="fieldsetBBVA" style="width: 100%; padding: 15px;">
            <legend>RELACIONA AGENCIAS</legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <table class="resulbbva">
                    <tr>
                        <td style="width: 5%;">Empresa:</td>
                        <td style="width: 27%;">
                            <asp:DropDownList ID="cmbEmpresa" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                        <td style="width: 1%; text-align: right;">Agencia: </td>
                        <td style="width: 27%;">
                            <asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="90" Onkeypress="return ValCarac(event,11);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                        <%--BUG-PC-38 MAUT 23/01/2017 Se cambia maxlength a 90--%>
                        <td>
                            <asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaAgen" Text="Buscar" /></td>
                        <%--<td><asp:DropDownList ID="ddlAgencia" runat="server" CssClass="select" Width="190px" AutoPostBack="true"></asp:DropDownList></td>--%>
                    </tr>
                </table>
                <table class="resulbbva">
                    <tr>
                        <td style="padding-top: 5px;">
                            <asp:Button ID="btnNinguna" runat="server" Text="Ninguna" CssClass="buttonSecBBVA2"></asp:Button><br />
                        </td>
                        <td style="padding-top: 5px;">
                            <asp:Button ID="btnTodas" runat="server" Text="Todas" CssClass="buttonSecBBVA2"></asp:Button><br />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 210px; overflow-x: hidden">
                <table style="width: 100%;">
                    <tr>
                        <td style="background-color: White;">
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="20" Width="100%" BorderWidth="0px"
                                DataKeyNames="ID_EMPRESA, ID_AGENCIA, ASIG">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:BoundField DataField="ID_EMPRESA" HeaderText="EMP ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="18%" Visible="false" />
                                    <asp:BoundField DataField="ID_AGENCIA" HeaderText="AGE ID" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                    <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%" />
                                    <asp:BoundField DataField="AGENCIA" HeaderText="Agencia" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="ageId" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" AlternateText="Relación" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ASIG" HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false" />
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
