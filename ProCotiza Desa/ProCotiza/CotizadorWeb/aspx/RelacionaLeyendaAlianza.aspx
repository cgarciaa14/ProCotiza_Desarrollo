<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="RelacionaLeyendaAlianza.aspx.vb" Inherits="aspx_RelacionaLeyendaAlianza" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<%--BBV-P-412_RQCOT-05:AVH:31/08/2016: SE CREA PARA ASIGNAR LEYENDAS--%>
<%--BUG-PC-06: AVH: 14/11/2016 se utiliza la funcion ValCarac--%>
<%--BUG-PC-17: AVH 25/11/2016 SE OCULTA BOTON LIMPIAR--%>
<%--BUG-PC-39 JRHM 25/01/17 Se agrega boton limpiar--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-48 JRHM 21/02/17 SE AGREGA WRAP PARA EVITAR QUE LA PANTALLA SE DEFORME--%>
<%--BUG-PC-137 21/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ASIGNA LEYENDAS</title>--%>
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
        <fieldset class="fieldsetBBVA" style=" width:100%; padding: 15px;">
            <legend>
                <asp:Label runat="server" ID="lblTitulo" Text=""></asp:Label>
            </legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <center>
            <table class="resulbbva" style="width:90%;">
		            <tr>
                        <td style="width:5%;">Seccion:</td>
                        <td style="width:27%;"><asp:DropDownList ID="cmbSeccion" runat="server" CssClass="selectBBVA"
                                AutoPostBack="true"></asp:DropDownList></td>
                        <td style="width:5%; text-align:right;">Leyenda:</td>
                        <td style="width:27%;"><asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="250" 
                                Width="150px" Onkeypress="return ValCarac(event,12);"></asp:TextBox></td>
                        <td><asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaProd" Text="Buscar"/></td>
            	    </tr>
	        </table>
            </center>
                <center>
                <table class="resulbbva" style="width:100%;">
                    <tr>
                        <td align="center" style="width:10%;">
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" width="60px" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                        <td align="center" style="width:10%;">
                            <asp:Button ID="btnNinguna" runat="server" Text="Ninguna" width="60px" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                        <td align="center" style="width:10%;">
                            <asp:Button ID="btnTodas" runat="server" Text="Todas" width="60px" CssClass="buttonSecBBVA2"></asp:Button><br />
                        </td> 
                    </tr>
                </table>
            </center>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 220px; overflow-x: hidden">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="20" Width="100%" BorderWidth="0px"
                                DataKeyNames="ID_LEYENDA, TEXTO,ESTATUS,ID_ALIANZA"
                                EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:BoundField DataField="ID_LEYENDA" HeaderText="ID" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="8PX" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="TEXTO" HeaderText="LEYENDA" ItemStyle-HorizontalAlign="LEFT" ItemStyle-CssClass="resul2wrap">
                                        <ItemStyle Width="500PX" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ESTATUS" HeaderText="" Visible="false" ItemStyle-CssClass="resul2wrap" />
                                    <asp:BoundField DataField="NOM_SECCION" HeaderText="SECCION" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                    <asp:BoundField DataField="ORDEN" HeaderText="ORDEN" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="8PX" />
                                    </asp:BoundField>


                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" ControlStyle-Width="10px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="ID_ALIANZA" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" AlternateText="Relación" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID_ALIANZA" HeaderText="" Visible="false" />
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
