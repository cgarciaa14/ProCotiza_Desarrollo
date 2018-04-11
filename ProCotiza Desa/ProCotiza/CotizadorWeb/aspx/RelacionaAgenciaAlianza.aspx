<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="RelacionaAgenciaAlianza.aspx.vb" Inherits="aspx_RelacionaAgenciaAlianza" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<%--BBV-P-412:AVH:28/06/2016 RQ16: SE CREA CATALOGO ALIANZAS--%>
<%--BUG-PC-06: AVH: 14/11/2016 se agrega boton Limpiar--%>
<%--BUG-PC-26: JRHM: 21/12/2016 Se movio funciones js para solucionar error al cargar pagina--%>
<%--BUG-PC-27 MAUT 22/12/2016 Se agrega EmptyDataText--%>
<%--BUG-PC-38 MAUT 23/01/2017 Se cambia maxlength de filtro de Agencia--%>
<%--BUG-PC-43 01/02/17 JRHM Se agrego clase wrap a tabla para casos donde nombre de agencia sea demasiado grande para tabla--%>
<%--BUG-PC-44 07/02/17 JRHM Se agrega clase resul2id a grids--%>
<%--BUG-PC-62:MPUESTO:13/05/2017:CORRECCION DE FUNCIONES DE LOS BOTONES NINGUNO Y TODOS--%>
<%--BUG-PC-137 21/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE AGENCIA--%>
<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <table class="resulbbva" style="width:90%;">
		            <tr>
                        <td style="width:5%;">Alianza:</td>
                        <td style="width:27%;"><asp:DropDownList ID="cmbAlianza" runat="server" CssClass="selectBBVA" Width="190px" 
                                AutoPostBack="true"></asp:DropDownList></td>
                        <td style="width:10%; text-align:right;">Nombre Agencia:</td>
                        <td style="width:27%;"><asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="90" 
                                Width="150px" Onkeypress="return ValCarac(event,11);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                        <%--<td style="width:8%; text-align:right;">&nbsp;</td>--%>
                        <td style="width:27%;"><asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaProd" Text="Buscar"/></td>
                        <%--BUG-PC-38 MAUT 23/01/2017 Se cambia maxlength a 90--%>
            	    </tr>
	        </table>
            </center>
                <center>
                <table class="resulbbva">
                    <tr>
                        <td align="center" style="width:10%;">
                            <asp:Button ID="btnLimpias" runat="server" Text="Limpiar" width="60px" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
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
            <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 210px; overflow-x: hidden">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="20" Width="100%" BorderWidth="0px"
                                DataKeyNames="ID_AGENCIA, NOMBRE,ID_ALIANZA"
                                EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:BoundField DataField="ID_AGENCIA" HeaderText="ID" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Agencia" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                    <asp:BoundField DataField="ALIANZA" HeaderText="Alianza" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
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
