<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="relacionaBrokerAseguradora.aspx.vb" Inherits="aspx_relacionaBrokerAseguradora" %>

<%--BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.--%>
<%--BUG-PC-46 JRHM 09/02/17 Se modifica validacion de caracteres de nombre aseguradora--%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE AGENCIA--%>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RELACIONA ASEGURADORAS</title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/gridviewScroll.min.js"></script>
    <style runat="server" id="LessCSS" type="text/css" />
</asp:Content>
<%--</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div id="cuerpoConsul" style="position: relative; margin-left: 20px; top: 15px;">
            <fieldset class="fieldsetBBVA" style="width: 700px; padding: 15px;">
                <legend>RELACIONA ASEGURADORAS</legend>
                <fieldset>
                    <center>
                        <table class="resulbbva">
                            <tr>
                                <td style="background-color: White;">Broker:</td>
                                <td>
                                    <asp:DropDownList ID="ddlbroker" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                                <td>Aseguradora: </td>
                                <td>
                                    <asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,11);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                                <td>
                                    <asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaAseg" Text="Buscar" /></td>
                            </tr>
                        </table>
                    </center>
                    <center>
                        <table class="resulbbva">
                            <tr>
                                <td align="center" style="width: 10%;">
                                    <asp:Button ID="btnNinguna" runat="server" Text="Ninguna" Width="60px" CssClass="buttonBBVA2"></asp:Button><br />
                                </td>
                                <td align="center" style="width: 10%;">
                                    <asp:Button ID="btnTodas" runat="server" Text="Todas" Width="60px" CssClass="buttonSecBBVA2"></asp:Button><br />
                                </td
                            </tr>
                        </table>
                    </center>
                </fieldset>
                <br />
                <fieldset style="padding-bottom: 10px;">
                    <table class="resulbbva" style="width: 100%;">
                        <tr>
                            <td style="background-color: White;">
                                <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="20" Width="100%" BorderWidth="0px"
                                    DataKeyNames="ID_ASEGURADORA, ASIG">
                                    <Columns>
                                        <asp:BoundField DataField="ID_ASEGURADORA" HeaderText="ASEG ID" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="ASEGURADORA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="140px" ControlStyle-Width="140px" />
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12px" ControlStyle-Width="12px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="AsegId" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" AlternateText="Relación" />
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
