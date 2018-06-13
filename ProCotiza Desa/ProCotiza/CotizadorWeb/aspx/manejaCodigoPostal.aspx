<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaCodigoPostal.aspx.vb" Inherits="aspx_manejaCodigoPostal" %>

<%--RQ-PI7-PC6: CGARCIA: 19/10/2017: SE CREA PANTALLA PARA CREAR, ACTUALIZAR DOMICILIOS--%>
<%--BUG-PC-137 21/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<%--BUG-PC-161: 27/02/2018: CGARCIA: SE CAMBIAN VALIDACIONES DE AGREGAR COLONIAS--%>
<%--BUG-PC-198: CGARCIA: 23/05/2018: SE VALIDA DUPLICADO DE ID DE MUNICIPIOS Y FILTRO DE CUIDADES DEJA DE DEPENDER DE MUNICIPIO--%>
<script runat="server">

    Protected Sub grvInserta_RowCreated(sender As Object, e As GridViewRowEventArgs)

    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As ImageClickEventArgs)

    End Sub
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function NumCaracteres() {
            var var1 = $('#<%= txtCP.ClientID%>').val().length
            if (var1 < 5) {
                $('#ContentPlaceHolder1_divMostrarExistente').hide();
                alert("El número de caracteres del código postal es incorrecto")
                return false;
            }
            return true;
        }

        function SelectCombos() {
            debugger;
            var varResult = false;
            var varEntidad = $('#<%= cmbNvoEntidadFederativa.ClientID%>').val();
            var varCiudad = $('#<%= cmbCiudad.ClientID%>').val()
            var varMunicipio = $('#<%= cmbNvoMunicipio.ClientID%>').val();
            var vartxtCol = $('#<%= txtNvoColonia.ClientID%>').val().length;
            var varEstatus = $('#<%= cmbNvoEstatus.ClientID%>').val();
            var vartxtCP = $('#<%= txtNvoCP.ClientID%>').val().length;
            var count = 0

            if (varEntidad <= 0) {
                alert("Debe de selecionar una entidad federativa.");
                varResult = false
                return varResult;
            }
            else {
                count = count + 1;
            }

            if (varMunicipio <= 0) {
                alert("Debe de seleccionarse un municipio.");
                varResult = false
                return varResult;
            }
            else {
                count = count + 1;
            }

            if (varCiudad <= -1) {
                alert("Debe de elegir una ciudad.");
                varResult = false
                return varResult;
            }
            else {
                count = count + 1;
            }

            if (vartxtCol <= 0) {
                alert("Tiene que ingresar una colonia.");
                varResult = false
                return varResult;
            }
            else {
                count = count + 1;
            }
           
            if (varEstatus <= 0) {
                alert("Tiene que seleccionar un estatus para el nuevo código postal.");
                varResult = false
                return varResult;
            }
            else {
                count = count + 1;
            }                  
            
            varResult = true;
            return varResult;
            
        }

   
    </script>

    <div style="position: relative; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;">
            <legend>CODIGOS POSTALES</legend>
            <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
                <legend>BUSQUEDA</legend>
                <center>
                <table class="resulbbva">
                    <tr>
                        <td style="width:10%; text-align:right;">&nbsp;&nbsp;Código Postal:</td>
                        <td>
                            <asp:TextBox ID="txtCP" runat="server" CssClass="txt3BBVA" onkeypress="return ValCarac(event,7);" MaxLength="5"></asp:TextBox></td>
                    </tr>
                    
                </table>
                </center>
                <center>
                <table class="resulbbva" style="width:100%;">
                    <tr>
                        <td style="width:7%;">
                            <td style="width:27%;">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="60px" CssClass="buttonBBVA2" OnClientClick='return NumCaracteres();' />
                        </td>
                        <td style="width:10%;">
                            <td style="width:27%;">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="60px" CssClass="buttonBBVA2" OnClientClick='return NumCaracteres();' />
                        </td>
                        <td style="width:9%;">
                            <td style="width:27%;">
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" Width="60px" CssClass="buttonSecBBVA2" OnClick="btnLimpiar_Click" />
                        </td>
                    </tr>
                </table>
                </center>
            </fieldset>
            <br />
            <fieldset class="fieldsetBBVA" id="divControlesInsert" runat="server" style="padding-top: 0px; margin: auto;" visible="false">
                <legend>DATOS PARA INSERTAR</legend>
                <div>
                    <center>
                    <table class="resulbbva" style="width:100%;">
                        <tr>
                            <td style="width:8%;">Entidad federativa:<span style="color: Red;">*</span> </td>
                            <%--<td>&nbsp;</td>--%>
                            <td style="width:27%;">
                                <asp:DropDownList ID="cmbNvoEntidadFederativa" runat="server" CssClass="selectBBVA" AutoPostBack="true" OnSelectedIndexChanged="cmbNvoEntidadFederativa_SelectedIndexChanged1"></asp:DropDownList></td>
                            <%--<td>&nbsp;</td>--%>
                            
                            <td style="width:8%; text-align:right;">Municipio:<span style="color: Red;">*</span></td>  
                            <td style="width:27%;"> <%--OnSelectedIndexChanged="cmbNvoMunicipio_SelectedIndexChanged" AutoPostBack="true"--%> <%--'BUG-PC-198:--%>
                                <asp:DropDownList ID="cmbNvoMunicipio" runat="server" CssClass="selectBBVA" ></asp:DropDownList></td>
                           <%-- <td>&nbsp;</td>--%>

                            <td style="width:10%; text-align:right;">Ciudad:<span style="color: Red;">*</span> </td>
                            <td style="width:27%;">
                                <asp:DropDownList ID="cmbCiudad" runat="server" CssClass="selectBBVA" ></asp:DropDownList></td>
                            <%--<td>&nbsp;</td>--%>                                               
                        </tr>
                        
                        <tr>
                            <td style="text-align:right;">&nbsp;&nbsp;Colonia:<span style="color: Red;">*</span></td>
                           <%-- <td>&nbsp;</td>--%>
                            <td>
                                <asp:TextBox ID="txtNvoColonia" runat="server" CssClass="txt3BBVA"></asp:TextBox></td>
                            <%--<td>&nbsp;</td>--%>


                            <td style="text-align:right;">&nbsp;&nbsp;Estatus:<span style="color: Red;">*</span></td>
                            <%--<td>&nbsp;</td>--%>
                            <td>
                                <asp:DropDownList ID="cmbNvoEstatus" runat="server" CssClass="selectBBVA"></asp:DropDownList></td>
                            <%--<td>&nbsp;</td>--%>


                            <td style="text-align:right;">&nbsp;&nbsp;Nuevo Código Postal:<span style="color: Red;">*</span></td>
                            <td>
                                <asp:TextBox ID="txtNvoCP" runat="server" CssClass="txt3BBVA" onkeypress="return ValCarac(event,7);" MaxLength="5" Enabled="false"></asp:TextBox></td>         
                        </tr>
                    </table>
                    </center>
                    <center>
                        <table style="width:100%;">
                            <tr>                          
                            <td align="center">
                                <asp:Button ID="btnGuardarNvo" runat="server" Text="Guardar" Width="60px" CssClass="buttonBBVA2" OnClientClick='return SelectCombos();' /></td>
                            <td align="center">
                                <asp:Button ID="btnUpdate" runat="server" Text="Actualizar" Width="70px" CssClass="buttonBBVA2" OnClientClick='return SelectCombos();' Visible="false" />
                            </td>
                        </tr>
                        </table>
                    </center>
                    <br />
                </div>
            </fieldset>
            <br />
            <div id="div_3" style="overflow-y: scroll; max-height: 300px;">
                <fieldset class="fieldsetBBVA" id="divMostrarExistente" runat="server" visible="false" style="position: relative; top: 116%; left: 0%; width: 98%; max-height: 50%;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <div style="position: relative; overflow: auto; width: 100%; height: 100%;">
                                    <asp:GridView ID="grvMuestraExistente" runat="server"
                                        AutoGenerateColumns="false" OnRowCreated="grvMuestraExistente_RowCreated"
                                        HorizontalAlign="Center" OnRowCommand="grvMuestraExistente_RowCommand" OnRowDataBound="grvMuestraExistente_RowDataBound">
                                        <HeaderStyle CssClass="GridHeader" />
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:TemplateField HeaderText="Entidad federetiva" ItemStyle-Width="150px" ControlStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtConsul_EF" runat="server" CssClass="txt3BBVA" Text='<%#CutText(Eval("ENTIDAD_FEDERATIVA"))%>' Enabled="false" title='<%#Eval("ENTIDAD_FEDERATIVA")%>'> ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CIUDAD" ItemStyle-Width="150px" ControlStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCiu" runat="server" CssClass="txt3BBVA" Text='<%#CutText(Eval("CIUDAD"))%>' Enabled="false" title='<%#Eval("CIUDAD")%>'> ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Municipio" ItemStyle-Width="150px" ControlStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtConsul_Municipio" runat="server" CssClass="txt3BBVA" Text='<%# CutText(Eval("MUNICIPIO"))%>' Enabled="false" title='<%#Eval("MUNICIPIO")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Colonia" ItemStyle-Width="150px" ControlStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtConsul_Colonia" runat="server" CssClass="txt3BBVA" Text='<%# CutText(Eval("COLONIA"))%>' Enabled="false" title='<%#Eval("COLONIA")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="16px" ControlStyle-Width="16px" HeaderText="Opciones">
                                                <ItemTemplate>
                                                    <div class="tooltip">
                                                        <asp:ImageButton runat="server" ID="btnElimina" ImageUrl="~/img/delete.png" OnClick="btnElimina_Click" CommandArgument='<%#Bind("ID")%>' CommandName="Coman_Delete" />
                                                        <span class="tooltiptext">Eliminar registro</span>
                                                    </div>
                                                    <div class="tooltip">
                                                        <asp:ImageButton runat="server" ID="btnEdit" ImageUrl="~/img/script_edit.png" OnClick="btnEdit_Click" CommandArgument='<%#Bind("ID")%>' CommandName="Coman_Edit" />
                                                        <span class="tooltiptext">Editar registro</span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

