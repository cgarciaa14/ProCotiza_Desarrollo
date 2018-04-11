<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="relacionaMarcasAgencias.aspx.vb" Inherits="aspx_relacionaMarcasAgencias" %>

<%--BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca--%>
<%--BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Marcas-Agencias</title>--%>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type ="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <style runat="server" id="LessCSS" type="text/css" />
<%--<script type="text/javascript" src="../js/gridviewScroll.min.js"></script>--%>

<%--   <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=grvConsulta.ClientID%>').gridviewScroll({
                width: 672,
                height: 191,
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "../img/arrowvt.png",
                varrowbottomimg: "../img/arrowvb.png",
                harrowleftimg: "../img/arrowhl.png",
                harrowrightimg: "../img/arrowhr.png"
            });
        }

    </script>--%>
</asp:Content>
<%--</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div id="cuerpoConsul" style=" position:relative; margin-left:20px; top:15px;">
        <fieldset class="fieldsetBBVA" style="width:100%; padding:15px;">
        <legend>RELACIONA AGENCIAS</legend>
            <fieldset>
                <center>
                <table class="resulbbva">
		            <tr>
                        <td>Marca:</td> <%--BUG-PC-09:AMR--%>
                        <td><asp:DropDownList ID="cmbMarca" runat="server" CssClass="selectBBVA" Width="190px" AutoPostBack="true"></asp:DropDownList></td>
                        <td class="resul">Agencia:</td>
                        <td><asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="200" Width="194px" Onkeypress="return ValCarac(event,11);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                        <td><asp:Button runat="server" CssClass="buttonBBVA2" ID="bntBuscaAgen" Text="Buscar"/></td>
            	    </tr>
	        </table>
            </center>
            <center>
                <table class="resulbbva">
                    <tr>
                        <td style="width:10%; text-align:center;"> <%--BUG-PC-09:AMR--%>
                            <asp:Button ID="btnNinguna" runat="server" Text="Ninguna" width="60px" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                        <td style="width:10%; text-align:center;">
                            <asp:Button ID="btnTodas" runat="server" Text="Todas" width="60px" CssClass="buttonSecBBVA2"></asp:Button><br />
                        </td> 
                    </tr>
                </table>
            </center>
            </fieldset>
            <br />
            <fieldset style="width: 98%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 220px; overflow-x: hidden">
                <table style="width:100%;">
                        <tr>
                            <td>
                                    <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                                  AllowPaging="true" PageSize="20" Width="100%" BorderWidth="0px" 
                                                  DataKeyNames="ID_MARCA, ID_AGENCIA, RELACION" HorizontalAlign="Center"    
                                        EmptyDataText ="No se encontró información con los parámetros proporcionados.">
                                        <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                                        <Columns>
                                            <asp:BoundField DataField="MARCA" HeaderText="Marca" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="AGENCIA" HeaderText="Agencia" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12px" ControlStyle-Width="12px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="MarcaId" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" AlternateText="Relación"/>
                                                </ItemTemplate>                                            
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID_MARCA" HeaderText="" Visible="false"/>
                                            <asp:BoundField DataField="ID_AGENCIA" HeaderText="" Visible="false"/>
                                            <asp:BoundField DataField="RELACION" HeaderText="" Visible="false"/>
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
