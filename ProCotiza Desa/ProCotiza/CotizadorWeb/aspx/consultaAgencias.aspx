<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaAgencias.aspx.vb" Inherits="aspx_consultaAgencias" %>
<%--BUG-PC-33: JRHM: 10/01/2017 Se prohiben simbolos y acentos en texto de busqueda--%>
<%--BBV-P-412:BUG-PC-35 18/01/2017 JRHM Se cambia tamaño de celdas en grid para nombres de agencia de 100 caracteres--%>
<%--BUG-PC-38 MAUT 23/01/2017 Se cambia maxlength de filtro de Agencia--%>
<%--BUG-PC-43 01/02/17 JRHM Se agrego clase wrap a tabla para casos donde nombre de agencia sea demasiado grande para tabla--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-46 13/02/17 JRHM SE MODIFICA LA VALIDACION DEL NOMBRE DE LA AGENCIA PARA QUE ACEPTE NUMEROS Y SIMBOLOS ESPECIALES --%>
<%--BUG-PC-48 15/02/17 JRHM SE DEJA SOLO LA VALIDACION DE ACENTOS PARA LOS NOMBRES DE LAS AGENCIAS--%>
<%--RQADM2-04 13/09/2017 ERODRIGUEZ Se agrego busqueda por ID de agencia.--%>
<%--BUG-PC-108 21/09/2017 ERODRIGUEZ Se corrigio busqueda por ID de agencia limitando la longitud del número--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="position: relative; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;">
    <legend>AGENCIAS</legend>
        <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
		        <tr>
<%--                    <td style="background-color:White;">Marca:</td>
                    <td><asp:DropDownList ID="cmbMarca" runat="server" AutoPostBack="true" CssClass="select"></asp:DropDownList></td>--%>
                    <td style="text-align:right">&nbsp;&nbsp;Estatus:</td>
                    <td style="width: 27%;"><asp:DropDownList ID="cmbEstatus" runat="server" AutoPostBack="true" CssClass="selectBBVA" ></asp:DropDownList></td>
                     <td style="text-align:right">Id:</td>
                    <td style="width: 27%;"><asp:TextBox ID="txtIdAgencia" runat="server" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return ValCarac(event,7);"></asp:TextBox></td>
                    <td style="text-align:right">Nombre:</td>
                    <td style="width: 27%;"><asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="90" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                    <%--BUG-PC-38 MAUT 23/01/2017 Se cambia maxlength a 90--%>
            	</tr>
	    </table>
        <table class="resulbbva">
            <tr>
                <td style="padding-top:5px;">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="buttonSecBBVA2"></asp:Button><br />
                </td>
                <td style="padding-top:5px;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="buttonBBVA2"></asp:Button><br />
                </td> 
            </tr>
        </table>
        </center>
        </fieldset>
        <br />
        <fieldset style="width:96.5%; padding-top:10px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden;">
            <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                          AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px" 
                                         EmptyDataText ="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="agenciaId" CommandArgument='<%# Eval("ID_AGENCIA") %>'><%#Eval("ID_AGENCIA")%></asp:LinkButton>
                                        </ItemTemplate>                                            
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap"/>
                                    <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px"/>
                                <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <div class="tooltip">
                                            <asp:ImageButton ID="ImageButton1" runat="server" Width="16" Height="16" CommandName="MarcasAsig"  CommandArgument='<%# Eval("id_agencia") %>' ImageUrl="../img/find.png" AlternateText="Relaciona Agencias" />
                                            <span class="tooltiptext">Consulta Marcas Asignadas</span>
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

