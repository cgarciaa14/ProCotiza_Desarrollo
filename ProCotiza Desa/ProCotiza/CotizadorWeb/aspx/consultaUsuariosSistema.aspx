<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaUsuariosSistema.aspx.vb" Inherits="aspx_consultaUsuariosSistema" %>

<%--  BBV-P-412  RQ D	 gvargas   03/11/2016 Mod Structura Tabla ajuste Id de tamaño variable --%>
<%--  BUG-PC-04: AVH: 11/11/2016 se utiliza la funcion ValCarac--%>
<%-- BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA VISUALIZAR CORRECTAMENTE LA OPCION SELECCIONAR.--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-48 16/02/17 JRHM SE OCULTO LA OPCION DE RESETAR CONTRASEÑA--%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
<legend>USUARIOS DE SISTEMA</legend>
    <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:90%;">
            <tr>
            <%--BUG-PC-40:PVARGAS:27/01/2017:SE EXPANDE EL TAMAÑO DE LOS COMBOS PARA VISUALIZAR CORRECTAMENTE LA OPCION SELECCIONAR.--%>
                <td style="text-align:right">Perfil:</td>
                <td style="width:27%;"><asp:DropDownList ID="ddlperfil" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                <td style="text-align:right">Estatus:</td>
                <td style="width:27%;"><asp:DropDownList ID="cmbEstatus" runat="server" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                <td style="text-align:right">Username:</td>
                <td style="width:27%;"><asp:TextBox ID="txtusername" runat="server" MaxLength="20"  CssClass="txt3BBVA" Onkeypress="return ValCarac(event,5);"></asp:TextBox></td>
                <td style="text-align:right">Nombre:</td>
                <td style="width:27%;"><asp:TextBox ID="txtnombre" runat="server" MaxLength="50" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,12);"></asp:TextBox></td>  
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
    <fieldset style="width: 96.5%; padding-top: 10px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:GridView ID="grvConsulta" runat="server" AutoGenerateColumns="false" 
                                    AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px" 
                            EmptyDataText="No se encontró información con los parámetros proporcionados.">
                        <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                        <Columns>
                            <asp:TemplateField HeaderText="Id" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:LinkButton style="width: 60px;" ID="LinkButton1" runat="server" CommandName="usuId" CommandArgument='<%# Eval("id_usuario") %>'><%# Eval("id_usuario") %></asp:LinkButton>
                                </ItemTemplate>                                            
                            </asp:TemplateField>
                            <asp:BoundField DataField="username" HeaderText="Username" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap"/>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap"/>
                            <asp:BoundField DataField="perfil" HeaderText="Perfil" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap"/>
                            <asp:BoundField DataField="empresa" HeaderText="Empresa" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap"/>
                            <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap"/>
                            <asp:TemplateField HeaderText="Opc." ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap">
                                <ItemTemplate>
                                <div class="tooltip" style="display:none">
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="resetPwd" CommandArgument='<%# Eval("id_usuario") %>' ImageUrl="../img/key_go.png" AlternateText="Reinicia Contraseña"/> <%--ToolTip="Reinicia Contraseña"--%>
                                    <span class="tooltiptext">Reinicia Contraseña</span>
                                </div>
                                <div class="tooltip">
                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="AsigAge" CommandArgument='<%# Eval("id_usuario") %>' ImageUrl="../img/house_go.png" AlternateText="Asigna Agencia"/>
                                    <span class="tooltiptext">Asigna Agencia</span>
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
    <fieldset style="width:96.5%; padding-top:10px; border: .5px solid #D8D8D8;">
        <table style="width:100%;">
            <tr id="trBotones">
                <td colspan="2" align="center" background-color:"White;">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="buttonBBVA2"></asp:Button>
                </td>                
            </tr>
        </table>
    </fieldset>
</fieldset>
</div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

