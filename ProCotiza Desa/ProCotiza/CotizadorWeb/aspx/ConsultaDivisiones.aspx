<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="ConsultaDivisiones.aspx.vb" Inherits="aspx_ConsultaDivisiones" %>

<%--BBV-P-412:AVH:06/07/2016 RQ19: SE CREA CATALOGO DE DIVISIONES--%>
<%--BUG-PC-11: AVH: 23/11/2016 SE AGREGA VALIDACION AL CAMPO DIVISION--%>
<%--BBVA-P-412:BUG-PC-19 JRHM 24/11/2016 Se cambiaron eventos keypress y keyup de textbox--%>
<%--BUG-PC-39 25/01/17 Correccion de errores multiples--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
    <legend>DIVISIONES</legend>
        <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
		        <tr>
                    <td style="width:5%;">&nbsp;&nbsp;Estatus:</td>
                    <td style="width:27%;"><asp:DropDownList ID="cmbEstatus" runat="server" AutoPostBack="true" CssClass="selectBBVA" ></asp:DropDownList></td>
                    <td style="width:1%; text-align:right;">División:</td>
                    <td style="width:27%;"><asp:TextBox ID="txtDivision" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                    </td>
            	</tr>
	    </table>
        </center>
        <center>
            <table>
                <tr>
                    <td style="width:10%; text-align:right;">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" width="60px" CssClass="buttonSecBBVA2"></asp:Button><br /></td>
                    <td style="width:10%; text-align:right;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" width="60px" CssClass="buttonBBVA2"></asp:Button><br />
                    </td> 
                </tr>
            </table>
        </center>
        </fieldset>
        <br />
        <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
            <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                           AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px"
                                         EmptyDataText ="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="DivisionId" CommandArgument='<%# Eval("ID_DIVISION") %>'><%#Eval("ID_DIVISION")%></asp:LinkButton>
                                        </ItemTemplate>                                            
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Division" HeaderText="Division" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="URL" HeaderText="URL" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center"/>
                                <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <div class="tooltip">
                                            <asp:ImageButton ID="ImageButton1" runat="server" Width="16" Height="16" CommandName="AsigDivision"  CommandArgument='<%# Eval("ID_DIVISION") %>' ImageUrl="../img/house_go.png" AlternateText="Relaciona Divisiones" />
                                            <span class="tooltiptext">Consulta Divisiones Asignadas</span>
                                        </div>
                                    </ItemTemplate>                                            
                                </asp:TemplateField>

                                </Columns>              
                            </asp:GridView>
                        </td>
                    </tr>
            </table>
        </fieldset>
        <br />
        <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <table  style="width: 100%;">
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

