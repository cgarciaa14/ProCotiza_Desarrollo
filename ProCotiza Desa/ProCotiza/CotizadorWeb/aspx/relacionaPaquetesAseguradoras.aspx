<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="relacionaPaquetesAseguradoras.aspx.vb" Inherits="aspx_relacionaPaquetesAseguradoras" %>

<%--BBVA-P-412: 06-07-2016: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08 SE AGREGA FUNCIONALIDAD PARA LA RELACIÓN PAQUETES ASEGURADORAS--%>
<%--BUG-PC-137 27/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Paquetes - Aseguradoras</title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/gridviewScroll.min.js"></script>
    <style runat="server" id="LessCSS" type="text/css" />
</asp:Content>
<%--</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="cuerpoConsul" style="position: relative; margin-left: 20px; top: 15px;">
        <fieldset class="fieldsetBBVA" style="width: 100%; padding: 15px;">
                <legend>RELACIONA PAQUETES - ASEGURADORAS</legend>
                <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                    <table class="resulbbva">
                        <tr>
                            <td style="width: 5%;">Broker:</td>
                            <td style="width: 27%;">
                                <asp:DropDownList ID="ddlbroker" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                            <td style="width: 1%; text-align: right;">Aseguradora:</td>
                            <td style="width: 27%;">
                                <asp:TextBox ID="txtAseg" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return validarNro('A1',event);"></asp:TextBox></td>
                            <td>
                                <asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaAseg" Text="Buscar" /></td>

                        </tr>
                    </table>
                    <center>
                    <table class="resulbbva">
                        <tr>
                            <td style="padding-top: 5px;" align="center">
                                <asp:Button ID="btnNinguna" runat="server" Text="Ninguna" CssClass="buttonSecBBVA2" /></td>

                            <td style="padding-top: 5px;" align="center">
                                <asp:Button ID="btnTodas" runat="server" Text="Todas" CssClass="buttonSecBBVA2" />
                            </td>
                        </tr>
                    </table>
                    </center>
                <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <br />
                <fieldset style="padding-bottom: 10px;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="background-color: White;">
                                <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="20" Width="100%" BorderWidth="0px"
                                    DataKeyNames="ID_ASEGURADORA, ASIG">
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <Columns>
                                        <asp:BoundField DataField="NOMBRE" HeaderText="ASEGURADORA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="22%" />
                                        <asp:BoundField DataField="BROKER" HeaderText="BROKER" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="AsegID" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" AlternateText="Relación" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID_ASEGURADORA" HeaderText="" Visible="false" />
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
    <%--</form>
</body>
</html>--%>
