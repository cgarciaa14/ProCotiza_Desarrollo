<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="relacionaTiposOperacionGiros.aspx.vb" Inherits="aspx_relacionaTiposOperacionGiros" %>

<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE AGENCIA--%>
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
    <%--    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=grvConsulta.ClientID%>').gridviewScroll({
                width: 672,
                height: 200,
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "../img/arrowvt.png",
                varrowbottomimg: "../img/arrowvb.png",
                harrowleftimg: "../img/arrowhl.png",
                harrowrightimg: "../img/arrowhr.png"
            });
        }
    </script>--%>
<%--</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="cuerpoConsul" style="position: relative; margin-left: 20px; top: 15px;">
        <fieldset class="fieldsetBBVA" style="width: 100%; padding: 15px;">
                <legend>RELACIONA GIRO - TIPO OPERACIÓN</legend>
                <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8;">
                    <table class="resulbbva">
                        <tr>
                            <td style="background-color: White;">&nbsp;&nbsp;Nombre Giro:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtGiro" CssClass="txt3BBVA" MaxLength="200" Onkeypress="return PermiteCaracteres(event,0);"></asp:TextBox></td>
                            <td>
                                <asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaGiro" Text="Buscar" /></td>
                        </tr>
                    </table>
                    <center>
                        <table class="resulbbva">
                            <tr>
                                <td align="center" style="padding-top: 5px;">
                                    <asp:Button ID="btnNinguna" runat="server" Text="Ninguna" CssClass="buttonSecBBVA2"></asp:Button><br />
                                </td>
                                <td align="center" style="padding-top: 5px;">
                                    <asp:Button ID="btnTodas" runat="server" Text="Todas" CssClass="buttonSecBBVA2"></asp:Button><br />
                                </td>
                            </tr>
                        </table>
                    </center>
                </fieldset>
                <br />
               <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="background-color: White;">
                                <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="8"
                                    Width="100%" BorderWidth="0px">
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <Columns>
                                        <asp:BoundField DataField="id_giro" HeaderText="id" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%" />
                                        <asp:BoundField DataField="nombre" HeaderText="Giro" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="giroId" AlternateText="Relación" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
