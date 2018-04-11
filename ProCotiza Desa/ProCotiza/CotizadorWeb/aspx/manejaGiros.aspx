<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaGiros.aspx.vb" Inherits="aspx_manejaGiros" %>
<%--BBV-P-412:BUG-PC-10 JRHM: 18/11/2016 Se oculto la seccion tipo de operacion para que el usuario no la modifique al dar de alta el giro--%>
<%--BBV-P-412:BUG-PC-23 JRHM: 30/11/2016 Se agrego funcion para reemplazar acentos y quito opcion de simbolos a textbox--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>GIROS</legend>
        <fieldset style="padding-left:15px;">
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:8%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblId"></asp:Label>
                    </td>
           
                    <td style="width:10%; text-align:right;">* Nombre:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" Width="190px" MaxLength="150" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                    </td>
                
                    <td style="width:9%; text-align:right;">&nbsp;&nbsp;&nbsp;ID Externo:</td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtUID" CssClass="txt3BBVA" Width="190px" MaxLength="20" Onkeypress="return  ValCarac(event,7);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">* Estatus:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
         
                    <td style="display:none;">* Tipo de Operación</td>
                    <td style="display:none">

                            <asp:GridView ID="grdvtipoopr" runat="server" AutoGenerateColumns="false" CssClass="camposCot" HeaderStyle-CssClass="encabezados" Width="35%">
                                <Columns>
                                    <asp:TemplateField HeaderText ="Sel." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" ControlStyle-Width="10px">
                                        <ItemTemplate>
                                            <asp:checkBox ID="checkbx" runat="server"  Checked='<%# IIf(Eval("ESTATUS")=2, "true", "false") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ESTATUS" HeaderText="ESTATUS"></asp:BoundField>                                                        
                                    <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE"></asp:BoundField>
                                    <asp:BoundField DataField="ID_TIPO_OPERACION" HeaderText="TIPO"></asp:BoundField>                                                        
                                </Columns>
                            </asp:GridView>

                    </td>
                
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Default:</td>
                    <td>
                        <asp:CheckBox ID="chkDefault" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" background-color:"White">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                    <td colspan="2" align="center" background-color:"White">
                    	<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

