<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaPlazos.aspx.vb" Inherits="aspx_consultaPlazos" %>

<%--BBV-P-412:BUG-PC-10 JRHM: 18/11/2016 Se agregaron evento autopostback a dropdownlist--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: relative; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;">
            <legend>PLAZOS</legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <center>
            <table class="resulbbva" style="width:90%;">
		        <tr>
                    <td style="width:5%;">Periodicidad: </td>
                    <td style="width:27%;"><asp:DropDownList runat="server" ID="ddlPeriodicidad" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList></td>
                    <td style="width:1%; text-align:right;">Estatus: </td>
                    <td style="width:27%;"><asp:DropDownList runat="server" ID="ddlestatus" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList></td>
            	</tr>
	    </table>
        </center>
                <center>
            <table class="resulbbva">
                <tr>
                    <td style="padding-top:5px;">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                    <td style="padding-top:5px;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="buttonBBVA2"></asp:Button><br />
                    </td> 
                </tr>
            </table>
        </center>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <table style="width: 100%;">
                    <tr>
                        <td style="background-color: White;">
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="plazoId" CommandArgument='<%# Eval("ID_PLAZO") %>'><%#Eval("ID_PLAZO")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="VALOR" HeaderText="Valor" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="PERIODICIDAD" HeaderText="Periodicidad" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="TEXTO" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" ControlStyle-Width="10px" />
                                    <%--                                <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Width="16" Height="16" CommandName="MarAge" CommandArgument='<%# Eval("id_plazo") %>' ImageUrl="../img/house_go.png" AlternateText="Relaciona Agencias" />
                                    </ItemTemplate>                                            
                                </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <table style="width: 100%;">
                    <tr id="trBotones">
                        <td colspan="2" align="center" background-color:"White;">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="60px" CssClass="buttonBBVA2"></asp:Button>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

