<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaVersiones.aspx.vb" Inherits="aspx_manejaVersiones" %>
<%--BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.--%>
<%--BBVA-P-412: 23/08/2016: AVH: RQ11 Se agrega Grid de Porcentajes Adicionales--%>
<%--BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca--%>
<%--BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.--%>
<%--BUG-PC-39 27/01/17 JRHM Se limito nombre de version a 90 caracteres --%>
<%--BUG-PC-44 07/02/17 JRHM Se agrego que el tipo plazo "<Seleccionar>" no sea aceptado--%>
<%--BUG-PC-90:10/07/17:MPUESTO:CORRECCIÓN DE LONGITUD DE LA DESCRIPCIÓN DE LA VERSION--%>
<%--RQ-MN2-6: RHERNANDEZ: 15/09/17: SE REDUCE EL NUMERO DE CARACTERES A 49 EN LA DESCRIPCION DE LA VERSION--%>
<%--BUG-PC-137 21/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>VERSIONES</legend>
        <fieldset>
            <table class="resulbbva" style="width:90%;">
                <tr>
                    <td style="width:5%;">&nbsp;&nbsp;&nbsp;Id:</td>
                    <td style="width:27%;"><asp:Label runat="server" ID="lblidversion"></asp:Label></td>
                
                    <td style="width:10%; text-align:right;">* Marca</td>
                    <td style="width:27%;"><asp:DropDownList runat="server" ID="ddlmarca" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td>
               
                    <td style="width:12%; text-align:right;">* SubMarca</td>
                    <td style="width:27%;"><asp:DropDownList runat="server" ID="ddlsubmarca" CssClass="selectBBVA"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>* Versión</td> <%--BUG-PC-09--%>
                    <td><asp:TextBox runat="server" ID="txtversion" CssClass="txt3BBVA" MaxLength="49" Onkeypress="return ValCarac(event,14);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox></td>
                
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;ID Externo</td>
                    <td><asp:TextBox runat="server" ID="txtidexterno" CssClass="txt3BBVA" Width="190px" Onkeypress="return ValCarac(event,7);"></asp:TextBox></td> <%--BUG-PC-09--%>
                
                    <td style="text-align:right;">* Estatus</td>
                    <td><asp:DropDownList runat="server" ID="ddlestatus" CssClass="selectBBVA"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="text-align:right;">&nbsp;&nbsp;&nbsp;Default:</td>
                    <td><asp:CheckBox runat="server" ID="chkdefault"/></td>
                </tr>
            </table>
        </fieldset>
        <br />
        <div style=" overflow:auto; height:160px;">
        <fieldset style="padding-top:10px; padding-bottom:20px;">
            <fieldset class="fieldsetBBVA">
            <legend>Compra Inteligente:</legend>

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
                            <asp:TemplateField HeaderText="* Plazo" ItemStyle-Width="150px" ControlStyle-Width="150px">   
                                <ItemTemplate>  
                                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" CssClass="selectBBVA">   
                                    </asp:DropDownList> 
                                </ItemTemplate>   
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="* % Enganche" ItemStyle-Width="150px" ControlStyle-Width="150px">   
                                <ItemTemplate>   
                                     <asp:TextBox runat="server" ID="txtEnganche" CssClass="txt3BBVA" Text='<%#Bind("Column2")%>' MaxLength="5" Onkeypress="return checkDecimals(event, this.value, 1);"></asp:TextBox>   
                                </ItemTemplate>   
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="* % Balloon Payment" ItemStyle-Width="150px" ControlStyle-Width="150px">   
                                <ItemTemplate>   
                                     <asp:TextBox runat="server" ID="txtBalloon" CssClass="txt3BBVA" Text='<%#Bind("Column3")%>' MaxLength="5" Onkeypress="return checkDecimals(event, this.value, 1);"></asp:TextBox>   
                                </ItemTemplate>   
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Cadena" ItemStyle-Width="90px" ControlStyle-Width="90px" Visible="false">   
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
                    <table class="resul">
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
                <table class="resulbbva">                   
                </table>
            </fieldset>
        </fieldset>
    </div>
        <br />
        <fieldset>
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="height:40px; background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2"></asp:Button></td>
                    <td colspan="2" align="center" style="height:40px; background-color:White;">
                    	<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>

    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
</asp:Content>

