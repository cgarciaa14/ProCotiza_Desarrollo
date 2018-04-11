<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaProductos.aspx.vb" Inherits="aspx_manejaProductos" %>
<%--BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.--%>
<%--BUG-PC-02:  09/11/2016: AMR: Error en manejaProductos--%>
<%--BUG-PC-18:  25/11/2016: AMR: CUTARPR01-El campo dice Tipo, se solicitó Tipo de Producto.--%>
<%--BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.--%>
<%--BUG-PC-48 JRHM 15/02/17 SE OCULTA EL CAMPO DE NOMBRE DE LA IMAGEN DEBIDO A QUE ES INNECESARO--%>
<%--RQ-SEGIP : RHERNANDEZ: 19/07/17: SE SECCION DE PAGINA PARA AGREGAR LAS CONSTANTES DEACUERDO AL PRODUCTO PARA SEGUROS POR FACTOR--%>
<%--BUG-PC-105: RHERNANDEZ: 07/09/17: SE AGREGA VALOR CUOTA PARA LOS CALCULO DE SEGUROS POR FACTOR--%>
<%--RQ-MN2-6:RHERNANDEZ: 07/09/17: SE AGREGA ID BROKER PARA LA BUSQUEDA MAS AGIL DE PRODUCTOS--%>
<%--BUG-PC-137 21/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
<script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>PRODUCTOS</legend>
        <fieldset>
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:5%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;">
                        <asp:Label runat="server" ID="lblId"></asp:Label>
                    </td>
               
                    <td style="width:10%; text-align:right;">* Tipo de Producto:</td> <%--BBVA-P-412--%>
                    <td style="width:27%;">
                        <asp:DropDownList runat="server" ID="cmbTipoProd" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                
                    <td style="width:9%; text-align:right;">* Clasificación:</td>
                    <td style="width:27%;">
                        <asp:DropDownList runat="server" ID="cmbClasif" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">* Marca:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbMarca" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList>
                    </td>
                <%--BBVA-P-412--%>
                    <td style="text-align:right;">* SubMarca:</td>
                    <td><asp:DropDownList runat="server" ID="ddlsubmarca" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                
                    <td style="text-align:right;">* Version:</td>
                    <td><asp:DropDownList runat="server" ID="ddlversion" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
                </tr> 
                <tr>
                    <td style="text-align:right;">* Año Modelo:</td>
                    <td>
                        <%--<asp:TextBox runat="server" ID="txtModelo" CssClass="txt3" MaxLength="4" Onkeypress="return validarNro('N',event);"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" ID="ddlanio" CssClass="selectBBVA"></asp:DropDownList>
                    </td>                      
                <%--<tr>
                    <td>* Descripción:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNom" CssClass="txt3" MaxLength="100" Onkeypress="return validarNro('P1',event);"></asp:TextBox>
                    </td>
                </tr>--%>
                    <td style="text-align:right;">* Estatus:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
<%--BBVA-P-412--%>
<%--                <tr>
                    <td>&nbsp;&nbsp;&nbsp;Familia:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbFamilia" CssClass="select" Width="160px"></asp:DropDownList>
                    </td>
                </tr>     --%>   
                    <td style="text-align:right;">
                        * Broker
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlbroker" runat="server" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                </tr>
                <tr style="display:none">
                    <td>&nbsp;&nbsp;&nbsp;Nombre Imagen:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtImagen" CssClass="txt3BBVA" Width="190px" MaxLength="100" Onkeypress="return validarNro('p',event);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Id Externo:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtUid" CssClass="txt3BBVA" Width="190px" MaxLength="20" Onkeypress="return validarNro('A',event);"></asp:TextBox>
                    </td>
       
                    <td style="text-align:right;"*> Precio Base:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPrecio" CssClass="txt3BBVA" Width="190px" MaxLength="100" Onkeypress="return checkDecimals(event, this.value, 10)"></asp:TextBox>
                    </td>
              
                    <td style="text-align:right;">* Precio SemiNuevo:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtpreciosm" CssClass="txt3BBVA" Width="190px" MaxLength="100" Onkeypress="return validarNro('D',event);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;"> &nbsp;&nbsp;&nbsp;Default:</td>
                    <td>
                        <asp:CheckBox ID="chkDefault" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
             <div style="padding-top:0px; margin:auto;">
                    <%--<asp:GridView runat="server" ID="grvAccesorios" Width="50%"></asp:GridView>--%>
                    <%--BUG-PC-09--%>
                    <asp:gridview ID="grvAdicionales"  runat="server"
                                                 AutoGenerateColumns="false"   
                                                  OnRowCreated="grvAdicionales_RowCreated">
                    <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                        <Columns>   
                            <asp:BoundField DataField="RowNumber" HeaderText="" />
                            <asp:TemplateField HeaderText="* Aseguradora" ItemStyle-Width="150px" ControlStyle-Width="150px">   
                                <ItemTemplate>  
                                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" CssClass="selectBBVA">   
                                    </asp:DropDownList> 
                                </ItemTemplate>   
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="* Cuota" ItemStyle-Width="150px" ControlStyle-Width="150px" ItemStyle-HorizontalAlign="Center">   
                                <ItemTemplate>   
                                     <asp:TextBox runat="server" ID="txtCuota" CssClass="txt3BBVA" Text='<%#Bind("Column2")%>' MaxLength="16" Onkeypress="return newcheckDecimals(event, this.value, 2, 7);"></asp:TextBox>   
                                </ItemTemplate>   
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="* Constante" ItemStyle-Width="150px" ControlStyle-Width="150px" ItemStyle-HorizontalAlign="Center">   
                                <ItemTemplate>   
                                     <asp:TextBox runat="server" ID="txtConstante" CssClass="txt3BBVA" Text='<%#Bind("Column3")%>' MaxLength="16"  Onkeypress="return newcheckDecimals(event, this.value, 8, 7);"></asp:TextBox>   
                                </ItemTemplate>   
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cadena" ItemStyle-Width="90px" ControlStyle-Width="90px" Visible="false" ItemStyle-HorizontalAlign="Center">   
                                <ItemTemplate>   
                                <asp:TextBox ID="txtCadena" runat="server" CssClass="txt3BBVA" Text='<%#Bind("Column4")%>' ></asp:TextBox>
                                <asp:Label ID="lblCadena" runat="server" Text='<%#Bind("Column4")%>' Visible="False"></asp:Label>                                
                                </ItemTemplate>   
                            </asp:TemplateField>  
                            <asp:TemplateField ItemStyle-Width="16px" ControlStyle-Width="16px">   
                                <ItemTemplate>   
                                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/img/delete.png" CommandArgument='<%#Eval("Column4")%>' onclick="ImageButton1_Click"/>
                                </ItemTemplate>   
                            </asp:TemplateField>   
                        </Columns>   
                    </asp:gridview>

                    <div>
                    <table class="resul2">
                        <tr>
                            <td style="background-color:White; width:135px;">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="padding:0px; width:30px;">Agregar:</td>
                            <td><asp:ImageButton runat="server" ID="cmdAgregaAcc" ImageUrl="~/img/add.png"/> </td>
                        </tr>
                    </table>
                    </div>

                </div>

        </fieldset>
        <br />
        <fieldset>
            <table  style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                    <td colspan="2" align="center" style="background-color:White;">
                    	<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

