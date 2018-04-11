<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaSubMarcas.aspx.vb" Inherits="aspx_consultaSubMarcas" %>

<%--BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.--%>
<%--BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca--%>
<%--BUG-PC-34 12/01/2017 MAUT Se reemplazan acentos--%>
<%--BUG-PC-39 JRHM 25/01/17 Correccion de errores multiples--%>
<%--BUG-PC-43 01/02/17 JRHM Se agrego clase wrap a tabla para casos donde nombre de marca y descripcion sea demasiado grande para tabla--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 21/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
    <legend>SUBMARCAS</legend>
        <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
		        <tr>
                    <td style="width:5%;">Estatus:</td>
                    <td style="width:27%;"><asp:DropDownList ID="cmbEstatus" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                    <td style="width:10%; text-align:right;">Marca:</td>
                    <td style="width:27%;"><asp:DropDownList runat="server" ID="ddlmarca" CssClass="selectBBVA"  AutoPostBack="true"></asp:DropDownList></td>
                    <td style="width:8%; text-align:right;">Submarca:</td> <%--BUG-PC-09--%>
                    <td style="width:27%;"><asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="90" Width="150" Onkeypress="return ValCarac(event,12);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td> <%--BUG-PC-09--%>
                    <%--BUG-PC-34 12/01/2017 MAUT Se reemplazan acentos--%>
            	</tr>
	    </table>
        </center>
        <center>
            <table class="resulbbva">
                <tr>
                     <td style="padding-top:5px;">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" width="60px" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                     <td style="padding-top:5px;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" width="60px" CssClass="buttonBBVA2"></asp:Button><br />
                    </td> 
                </tr>
            </table>
        </center>
        </fieldset>
        <br />
        <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
                <table style="width: 90%;">
                    <tr>
                        <td style="background-color:White;">
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                      AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px" 
                                      EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="submarcaId" CommandArgument='<%# Eval("ID_SUBMARCA") %>'><%#Eval("ID_SUBMARCA")%></asp:LinkButton>
                                        </ItemTemplate>                                            
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MARCA" HeaderText="Marca" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap"/>
                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="SubMarca" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap"/>
                                    <asp:BoundField DataField="ESTATUS_DESC" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap"/>
                                </Columns>              
                            </asp:GridView>
                        </td>
                    </tr>
            </table>
        </fieldset>
        <br />
        <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="height:40px; background-color:White;">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" width="60px" CssClass="buttonBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

