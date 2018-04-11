<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master"  CodeFile="relacionaAseguradorasBroker.aspx.vb" Inherits="aspx_relacionaAseguradorasBroker" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <title></title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<%--</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div id="cuerpoConsul" style="position: relative; margin-left: 20px; top: 15px;">
            <fieldset class="fieldsetBBVA" style="width: 700px; padding: 15px;">
                <legend>RELACIONA BROKERS</legend>
                <fieldset>
                    <center>
                        <table class="resulbbva" style="width:100%;">
                            <tr>
                                <td style="width:7%;">Aseguradora:</td>
                                <td style="width:27%;">
                                    <asp:DropDownList ID="ddlAseguradora" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                                <td style="width:1%; text-align:right;">Broker:</td>
                                <td style="width:27%;">
                                    <asp:TextBox ID="txtBroker" runat="server" CssClass="txt3BBVA" MaxLength="50" Width="194px" Onkeypress="return validarNro('A1',event);"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width:7%;">&nbsp;&nbsp;</td>
                                    <td style="width:27%;">
                                    <asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaBroker" Text="Buscar" /></td>
                                <td style="width:10%;">&nbsp;&nbsp;</td>
                                <td style="width:27%;">
                                    <asp:Button ID="btnNinguna" runat="server" Text="Ninguna" CssClass="buttonBBVA2"></asp:Button>
                                </td>
                                <td style="width:8%;">&nbsp;&nbsp;</td>
                                <td style="width:27%;">
                                    <asp:Button ID="btnTodas" runat="server" Text="Todas" CssClass="buttonSecBBVA2"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </center>
                </fieldset>
                <br />
                <fieldset style="padding-bottom: 10px;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="20" Width="100%" BorderWidth="0px"
                                    DataKeyNames="ID_BROKER, ASIG">
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <Columns>
                                        <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Broker" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="37px" ControlStyle-Width="100px" />
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" ControlStyle-Width="10px" ItemStyle-CssClass="resul2wrap" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="AsegId" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" AlternateText="Relación" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID_BROKER" HeaderText="" Visible="false" />
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
