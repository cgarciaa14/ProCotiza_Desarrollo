<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="relacionaPaquetesAgencias.aspx.vb" Inherits="aspx_relacionaPaquetesAgencias" %>

<%--BBVA-P-412: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08--%>
<%--BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.--%>
<%--'BUG-PC-22 02-11-2016 MAUT Se agrega segundo nombre--%>
<%-- BUG-PC-44 07/02/17 JRHM SE Se validacion del campo de agencia para reemplazo de acentos. --%>
<%-- RQADM2-04 13/09/2017 ERODRIGUEZ Se agrego busqueda por ID de agencia.--%>
<%-- BUG-PC-108 21/09/2017 ERODRIGUEZ Se corrigio busqueda por ID de agencia limitando la longitud del número--%>
<%-- BUG-PC-110 25/09/2017 ERODRIGUEZ Se corrigio validacion para numeros y letras en agencia--%>
<%--BUG-PC-137 29/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-160: 26/02/2018: CGARCIA: SE AGREGA CONTROL PARA CARGA DE ESQUEMAS--%>
<%--RQ-PC9: 18/05/2018: DCORNEJO: SE AGREGA AutoPostBack="true" en ddlEsquem--%>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Paquetes-Agencias</title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/gridviewScroll.min.js"></script>

</asp:Content>
<%--</head>
<body> --%> 
    <%--<form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div id="cuerpoConsul" style="position: relative; margin-left: 20px; top: 15px;">
            <fieldset class="fieldsetBBVA" style="width:100%; padding: 15px;">
                <legend>RELACIONA PAQUETES - AGENCIAS</legend>
                <fieldset>
                        <table class="resulbbva">
                            <tr>
                                <td style="width: 7%;">Paquete:</td>
                                <td style="width: 27%;">
                                    <asp:Label runat="server" ID="lbldesc"></asp:Label></td>

                                <td style="width: 10%; text-align: right;">Grupo:</td>
                                <td style="width: 27%;">
                                    <asp:DropDownList runat="server" ID="ddlgrupo" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>

                                <td style="width: 9%; text-align: right;">Alianza:</td>
                                <td style="width: 27%;">
                                    <asp:DropDownList runat="server" ID="ddlalianza" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <%--BBVA-P-412:RQ18--%>
                                <%--<td style="background-color:White;">Paquete:</td>
                        <td><asp:DropDownList ID="cmbPaquete" runat="server" CssClass="select" Width="190px" AutoPostBack="true"></asp:DropDownList></td>--%>
                                <%--BBVA-P-412--%>
                                <td style="text-align: right;">División:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddldivision" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList>
                                    <%--<asp:label runat="server" text="Agencia" style="background-color:White;"></asp:label>--%>
                                </td>
                                <td style="text-align: right;">Agencia:</td>
                                <td>
                                    <asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,12);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                                </td>

                                <td style="text-align: right;">Id:</td>
                                <td>
                                    <asp:TextBox ID="txtId" runat="server" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return ValCarac(event,7);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <%--BUG-PC-160: 26/02/2018: CGARCIA: SE AGREGA CONTROL PARA CARGA DE ESQUEMAS--%>
                                <td style="text-align: right;">Esquema</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlEsquem" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table class="resulbbva">
                            <tr>
                                <td>
                                    <asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaAgen" Text="Buscar" /></td>
                                <td>
                                    <asp:Button runat="server" CssClass="buttonSecBBVA2" ID="btnNinguna" Text="Ninguna" /></td>
                                <td>
                                    <asp:Button runat="server" CssClass="buttonSecBBVA2" ID="btnLimpiar" Text="Limpiar" /></td>
                                <td>
                                    <asp:Button runat="server" CssClass="buttonSecBBVA2" ID="btnTodas" Text="Todas" /></td>
                            </tr>
                        </table>
                </fieldset>
                <br />
                <fieldset style="width: 98%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 210px; overflow-x: hidden">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="20" Width="100%" BorderWidth="0px"
                                    DataKeyNames="ID_AGENCIA, ASIG"
                                    EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridItem" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <Columns>
                                        <%--<asp:BoundField DataField="PAQUETE" HeaderText="Paquete" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" ControlStyle-Width="100px"/>--%>
                                        <asp:BoundField DataField="ID_AGENCIA" HeaderText="Id" Visible="true" ItemStyle-HorizontalAlign="Center"  />
                                        <asp:BoundField DataField="AGENCIA" HeaderText="Agencia" ItemStyle-HorizontalAlign="Center"  />
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="AgeID" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" AlternateText="Relación" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField DataField="ID_AGENCIA" HeaderText="Id" Visible="true"/>--%>
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
    <%--</form>--%>
<%--</body>
</html>--%>
