<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="relacionAgenciaMarcas.aspx.vb" Inherits="aspx_relacionAgenciaMarcas" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<%--BUG-PC-137 21/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LA AGENCIA--%>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <style type="text/css">@import url(../css/procotiza.css); </style>
        <script type="text/javascript" src="../js/Funciones.js"></script>
        <script type="text/javascript" src="../js/jquery.js"></script>
        <script type="text/javascript" src="../js/jquery-ui.js"></script>
        <script type ="text/javascript" src="../js/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../js/gridviewScroll.min.js"></script>
</asp:Content>

<%--</head>
<body>
<form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
        <legend>Marcas Relacionadas</legend>
            <fieldset class="fieldsetBBVA" style="width: 700px; padding: 15px;">
            <center>
            <table  style="width:90%;">
                    <tr>
                        <td style="background-color:White;">
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                      AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px" 
                                      EmptyDataText ="Esta Agencia no tiene Marcas relacionadas.">
                                <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                                <Columns>
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Marca" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="DEFAULT" HeaderText="Default" ItemStyle-HorizontalAlign="Center"/>
                                </Columns>              
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                </center>
            
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
    <%--</form>--%>
</asp:Content>
<%--</body>
</html>--%>
