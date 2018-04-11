<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaTasasIva.aspx.vb" Inherits="aspx_consultaTasasIva" %>

<%--JRHM 10/11/16 BUG-PC-05 Cambio de regla de campos para aceptar alfanumericos.--%>
<%--BBVA-P-412: JRHM 14/11/2016: BUG-PC-08 – Inserccion de valor seleccionar a combobox--%>
<%--JRHM 23/11/16 BBVA-P-412:BUG-PC-15 Se agrego evento para reemplazar letras con acento y evento de alfanumericos--%>
<%--BBV-P-412:BUG-PC-23 JRHM: 30/11/2016 Se modifico campo nombre para que solo permitira texto, numeros y un simbolo % al final del texto.--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGO CLASE RESUL2ID PARA LA CORRECTA PRESENTACION DE ID EN LOS GRID DE BUSQUEDA Y RELACIONES--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript">
        function validapor() {
            var nombretasa = $("input[id$=txtNom]").val();
            if (!nombretasa == '') {
                expr = /^[a-zA-Z0-9\ ]+%?$/;
                if (!expr.test(nombretasa)) {
                    alert("El formato del normbre de la tasa es incorrecto solo puede llevar un '%' al final o ninguno.");
                    $("input[id$=txtNom]").val('');
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="position: relative; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;">
            <legend>TASAS IVA</legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <center>
            <table class="resulbbva" style="width:90%;">
		        <tr>
                    <td style="width:5%;">&nbsp;&nbsp;Estatus:</td>
                    <td><asp:DropDownList ID="cmbEstatus" runat="server" CssClass="selectBBVA"  AutoPostBack="true"></asp:DropDownList></td>  <%--JRHM 14/11/16 Se activo autopostback para actializar datos de busqueda al sleccionar valor de combo.--%>
                    <td>Nombre:</td>
                    <td><asp:TextBox ID="txtNom" runat="server" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,19);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" onblur="validapor();" ></asp:TextBox></td> <%--JRHM 10/11/16 Se cambio el parametro a 'A1' para que el campo ahora acepte caracteres alfanumericos.--%>
            	</tr>
	    </table>
        </center>
                <center>
            <table class="resulbbva">
                <tr>
                    <td style="padding-top:5px;">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar"  CssClass="buttonSecBBVA2"></asp:Button><br /></td>
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
                                    <asp:TemplateField HeaderText="Id" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="tasaId" CommandArgument='<%# Eval("id_tasa_iva") %>'><%#Eval("id_tasa_iva")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="tasa" HeaderText="Tasa" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="estatus_desc" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" />
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
                        <td colspan="2" align="center" style="height: 40px; background-color: White;">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="60px" CssClass="buttonBBVA2"></asp:Button>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

