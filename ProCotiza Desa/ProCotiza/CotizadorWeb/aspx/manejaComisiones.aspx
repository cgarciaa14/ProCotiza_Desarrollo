<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaComisiones.aspx.vb" Inherits="aspx_manejaComisiones" %>

<%--BBV-P-412:AVH:21/07/2016 RQ20: SE CREA VENTANA DE COMISIONES--%>
<%--BBV-P-412:AVH:02/08/2016 RQ20.2 OPCION BonoCC--%>
<%--BUG-PC-06: AVH: 14/11/2016 se utiliza la funcion checkDecimals--%>
<%--BBV-P-412:BUG-PC-19 24/11/2016 Se agrego scroll para comisiones--%>
<%--BBV-P-412:BUG-PC-35 18/01/2017 JRHM Se agrega objeto para solucion de fallas--%>
<%--BUG-PC-42 JRHM 30/01/17 Se cambian caracteres permitidos en textbox de comisiones--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script> 

    <script type="text/javascript">
        function confirmation() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Esta operación modifica configuraciones previas. Desea continuar?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style=" position:relative; top:15px;">
    <fieldset class="fieldsetBBVA" style="padding-bottom:15px;">
    <legend>COMISIONES</legend>
    <asp:ScriptManager runat="server" ID="sm1"></asp:ScriptManager>
   <%-- <asp:UpdatePanel runat="server" ID="upSeleccion">
    <ContentTemplate>--%>
        <fieldset style=" width:96.5%; padding-top:15px; border: .5px solid #D8D8D8;">
            <center>
            <table class="resulbbva" style="width:100%;">
                <tr>
                    <td style="width:8%;">
                        <asp:RadioButton ID="rbAlianza" runat="server" GroupName="rbComisiones" Checked="true" Text="Alianza" AutoPostBack="true"/>
                    </td>
                    <td style="width:27%;">
                        <asp:DropDownList runat="server" ID="cmbAlianza" CssClass="selectBBVA" Enabled="true" AutoPostBack="true"></asp:DropDownList>
                    </td>
      
                    <td style="width:10%; text-align:right;">
                        <asp:RadioButton ID="rbGrupo" runat="server" GroupName="rbComisiones" Text="Grupo" AutoPostBack="true" />
                    </td>
                    <td style="width:27%;">
                        <asp:DropDownList runat="server" ID="cmbGrupo" CssClass="selectBBVA" Enabled="false" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td style="width:8%; text-align:right;">
                        <asp:RadioButton ID="rbDivision" runat="server" GroupName="rbComisiones" Text="Division" AutoPostBack="true" />
                    </td>
                    <td style="width:27%;">
                        <asp:DropDownList runat="server" ID="cmbDivision" CssClass="selectBBVA" Enabled="false" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>            
                
            </table>
            </center>
        </fieldset>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        <br />
        <fieldset style="width:96.5%; padding-top:10px; border: .5px solid #D8D8D8;">
            <table class="resulbbva" style="width:100%;">
                 <tr>
                    <td style="width:5%;">
                        <asp:Label runat="server" ID="lblUDIS" Text="% UDIS:"></asp:Label>
                    </td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtUDIS" MaxLength="5"  CssClass="txt3BBVA" Onkeypress="return newcheckDecimals(event, this.value, 2, 2)" placeholder="0.0"></asp:TextBox>                        
                    </td>
                    <td style="width:10%; text-align:right;">
                        <asp:Label runat="server" ID="lblPagoFI" Text="Pago al F&I:"></asp:Label>
                    </td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtPagoFI" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return newcheckDecimals(event, this.value, 6, 2)" placeholder="0.0"></asp:TextBox>
                    </td>
                    <td style="width:5%; text-align:right;">
                        <asp:Label runat="server" ID="lblDividendos" Text="Dividendos:"></asp:Label>
                    </td>
                    <td style="width:27%;">
                        <asp:TextBox runat="server" ID="txtDIvidendos" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return newcheckDecimals(event, this.value, 6, 2)" placeholder="0.0"></asp:TextBox>
                    </td>
                 </tr>
                <tr>
                    <td style="text-align:right">
                        <asp:Label runat="server" ID="lblRegalias" Text="Regalias / FEE:" placeholder="0.0"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRegalias" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return newcheckDecimals(event, this.value, 6, 2)" placeholder="0.0"></asp:TextBox>
                    </td>
                    <td style="text-align:right"><asp:Label runat="server" ID="lblBonoEspVen" Text="Bono Especial Vendedor:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtBonoEspVen" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return newcheckDecimals(event, this.value, 6, 2)" placeholder="0.0"></asp:TextBox>
                    </td>
                    <td style="text-align:right">
                        <asp:Label runat="server" ID="lblSeguroRegalado" Text="Seguro Regalado:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSeguroRegalado" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return newcheckDecimals(event, this.value, 6, 2)" placeholder="0.0"></asp:TextBox>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <asp:Label runat="server" ID="lblBonoCreCol" Text="Bono por créditos colocados:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtBonoCreCol" CssClass="txt" Onkeypress="return ValCarac(event,9);" placeholder="0.0"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="4">
                        
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <div style=" position:relative; top:15px;"> <%--background-color:Red;--%>
        <fieldset style="width:96.5%; padding-top:10px; border: .5px solid #D8D8D8;">
            <fieldset class="fieldsetBBVA">
            <legend>Bono por créditos colocados:</legend>

                <div style="padding-top:0px; margin:auto;">
                    <%--<asp:GridView runat="server" ID="grvAccesorios" Width="50%"></asp:GridView>--%>
                    
                    <asp:gridview ID="grvBonoCC"  runat="server" AutoGenerateColumns="false" OnRowCreated="grvBonoCC_RowCreated"
                    AllowPaging="true" PageSize="8" Width="100%" BorderWidth="0px" >
                    <%--<HeaderStyle CssClass="GridviewScrollHeader" /> 
                    <RowStyle CssClass="GridviewScrollItem" /> 
                    <PagerStyle CssClass="GridviewScrollPager" />--%>
                        <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                        <Columns>   
                            <asp:BoundField DataField="RowNumber" HeaderText="" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center"/>
                            <asp:TemplateField HeaderText="# Créditos Colocados" ItemStyle-Width="150px" ControlStyle-Width="150px" ItemStyle-HorizontalAlign="Center">   
                                <ItemTemplate>  
                                    <asp:TextBox runat="server" ID="txtNoCreditos" CssClass="txt3BBVA" Text='<%#Bind("Column1")%>' MaxLength="6" Onkeypress="return ValCarac(event, 7)"></asp:TextBox> 
                                </ItemTemplate>   
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Importe Bono" ItemStyle-Width="150px" ControlStyle-Width="150px" ItemStyle-HorizontalAlign="Center">   
                                <ItemTemplate>   
                                     <asp:TextBox runat="server" ID="txtImporte" CssClass="txt3BBVA" Text='<%#Bind("Column2")%>' MaxLength="9" Onkeypress="return newcheckDecimals(event, this.value, 6, 2)"></asp:TextBox>   
                                </ItemTemplate>   
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Cadena" ItemStyle-Width="90px" ControlStyle-Width="90px" Visible="false">   
                                <ItemTemplate>   
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="txt3BBVA" Text='<%#Bind("Column3")%>' ></asp:TextBox>
                                </ItemTemplate>   
                            </asp:TemplateField>                             
                            <asp:TemplateField ItemStyle-Width="16px" ControlStyle-Width="16px">   
                                <ItemTemplate>   
                                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/img/delete.png" CommandArgument='<%#Eval("Column3")%>' onclick="ImageButton1_Click"/>
                                </ItemTemplate>   
                            </asp:TemplateField>   
                        </Columns>   
                    </asp:gridview>
                    <div>
                    <table class="resulbbva" style="width:100%;">
                        <tr>
                            <td style="background-color:White; width:135px;">&nbsp;</td>
                            <td style="background-color:White; padding:0px; width:30px;">Agregar:</td>                            
                            <td><asp:ImageButton runat="server" ID="cmdAgregaAcc" ImageUrl="~/img/add.png"/></td>
                            <td>&nbsp;</td>
                            <td> </td>
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
        <fieldset style="width:96.5%; padding-top:10px; border: .5px solid #D8D8D8;">
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="height:40px; background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2" OnClientClick="confirmation();"></asp:Button>
                    	
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>

    
            
            
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

