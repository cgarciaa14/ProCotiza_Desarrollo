<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaAgencias.aspx.vb" Inherits="aspx_manejaAgencias" %>

<%--'--BBV-P-412:AUBALDO:06/07/2016 RQ10 – Agregar funcionalidad en la pantalla Alta de Agencias (Brecha 63,64, Ampliación de Brecha)--%>
<%--'BBV-P-412:AUBALDO:26/07/2016 RQ A – Copia de Alta de Agencias de ProCotiza a Prodesk--%>
<%--BBV-P-412:AVH:04/08/2016 RQ20.2 OPCION BonoCC--%>
<%--BBV-P-412  RQ-E  gvargas   12/10/2016 Actualizacion de referencias. --%>
<%--BBVA-P-412 RQ F GAVRGAS: 07/11/2016 Cambios referencias--%>
<%--BUG-PC-14 25/11/2016 MAUT Validaciones--%>	
<%--  BUGPC16 24/11/2016: GVARGAS: Correccion Error Date Picker--%>
<%--BUG-PC-24 MAUT 24/12/2016 Se validan Udis--%>
<%-- BBV-P-423: 04/01/17: JRHM RQCONYFOR-06 Se Agrego checkbox valida bloqueo agencias--%>
<%--BUG-PC-33: JRHM: 10/01/2017 Se prohiben acentos en texto de nombre de agencia--%>
<%--BBV-P-412:BUG-PC-35 18/01/2017 JRHM Se cambia comportamiento de la pagina en base al status y el evento al consultar agencia del combo de telefono--%>
<%--BUG-PC-37: AVH: 20/01/2017 Se deshabilitar boton que consume WS--%>
<%--BUG-PC-38 MAUT 23/01/2017 Se cambia maxlength de nombre de Agencia y domicilio, Responsable de facturas como obligatorio--%>
<%--BUG-PC-39 25/01/17 JRHM  Correccion de errores multiples--%>
<%--BUG-PC-44 02/02/17 JRHM SE VALIDA QUE EL RESPONSABLE DE FACTURA SOLO PUEDA COLOCAR LETRAS ESPACIOS Y .--%>
<%--BUG-PC-46 13/02/17 JRHM SE MODIFICA LA VALIDACION DEL NOMBRE DE LA AGENCIA PARA QUE ACEPTE NUMEROS Y SIMBOLOS ESPECIALES --%>
<%--BUG-PC-48 15/02/17 JRHM SE DEJA SOLO LA VALIDACION DE ACENTOS PARA LOS NOMBRES DE LAS AGENCIAS--%>
<%--RQTARESQ-06 18/04/2017 cgarcia se agrega el combobox y la etiqueta de esquemas --%>
<%--BUG-PC-64:MPUESTO:13/05/2017 CORRECCION DE VALIDACION DE NUMERO DE CUENTA Y TITULAR.--%>
<%--BUG-PC-75:ERODRIGUEZ:15/06/2017:Se agrego checkbox para validar cuando la agencia cuenta con biometrico.--%>
<%--RQ-INB217: RHERNANDEZ: 15/08/17: SE AGREGO LA OPCION DE SELECCIONAR UNA MARCA DEFAULT A UNA AGENCIA--%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>--%>
    <style type="text/css">@import url(../css/jquery-ui.css); </style>
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
    <%--<script type="text/javascript" src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>--%>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/datepicker-es.js"></script>
    <script type="text/javascript" src="../ExternalScripts/jquery.cookie.js"></script>
        <script type="text/javascript">
            function validarEmail() {
                var email = $("input[id$=TextEmail]").val();
                if (!email == '') {
                    expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    if (!expr.test(email))
                        alert("El formato de la dirección de correo no es valido.");
                }
            }

            function limpiaTitular() {
                $('#<%= TextTCuenta.ClientID%>').val('');
                $('#<%= hdnTitularCuenta.ClientID%>').val('');
            }
            //BUG-PC-60:MPUESTO:13/05/2017 CORRECCION DE VALIDACION DE NUMERO DE CUENTA Y TITULAR.
            function assignValue()
            {
                $('#<%= hdnTitularCuenta.ClientID%>').val( $('#<%= TextTCuenta.ClientID%>').val());
            }

            $(document).ready(function () {
                var linksParent = $('.tabs__links');
                var links = linksParent.find('a');
                var items = $('.tabs__contents__item');
                links.eq(0).add(items.eq(0)).addClass('active');
                linksParent.on('click', 'a', function () {
                    var t = $(this);
                    var i = t.index();
                    t.add(items.eq(i))
                        .addClass('active')
                        .siblings().removeClass('active');
                });
                //BUG-PC-60:MPUESTO:13/05/2017 CORRECCION DE VALIDACION DE NUMERO DE CUENTA Y TITULAR.
                if ($('#<%= hdnCuentaAgencia.ClientID%>').val() == $('#<%= TextCuentaA.ClientID%>').val()) {
                    assignValue();
                }
                else
                {
                    $('#<%= TextTCuenta.ClientID%>').val('');
                    $('#<%= hdnTitularCuenta.ClientID%>').val('');
                }
            });

            $(document).ready(function () {
                var x = $('[id$=current_tab]').val();

                var linksParent = $('.tabs__links');
                var links = linksParent.find('a');
                var items = $('.tabs__contents__item');
                links.eq(x).add(items.eq(x)).addClass('active');
                links.eq(x).add(items.eq(x)).siblings().removeClass('active');
            });

            $(document).ready(
            function () {
                var check = $('[id*=chkPromocion]');
                check.change(function () {
                    var valorChequeado = $(this)[0].id
                    $.each(check, function () {
                        if ($(this)[0].id == valorChequeado) {
                            $(this).prop('checked', true)
                        }
                        else {
                            $(this).prop('checked', false)
                        }
                    })
                })
            })

        </script>

        <style type="text/css">
            .ui-datepicker 
            {
                font-size:63%;
            }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style=" position:relative; margin-left:20px; top:15px;">
    <fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
    <legend>AGENCIAS</legend>
        <fieldset style="padding-left:15px;">
        <asp:HiddenField runat="server" ID="current_tab"/>
        <asp:HiddenField runat="server" ID="hdfExpTelefono" />
        <asp:HiddenField runat="server" ID="hdfExpMail" />
                <div class="tabs__links">
                    <a href="#seccion-1">INFORMACIÓN GENERAL</a>
                    <a href="#seccion-2">DOMICILIO</a>
                    <a href="#seccion-3">TELEFONO</a>
                    <a href="#seccion-4">EMAIL</a>
                    </div>
                    <br />
                <div class="tabs__contents">
                    <%--INFORMACION GENERAL--%>
                    <div id="seccion-1" class="tabs__contents__item" style="padding-left:25px;">
                        <table class="resulbbva">
                            <tr>
                                <td style="width:20%; height:25px;">&nbsp;&nbsp;&nbsp;Id:</td>
                                <td>
                                    <asp:Label runat="server" ID="lblId"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td>* Nombre:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" MaxLength="90" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                                    <%--BUG-PC-14 2016-11-24 MAUT Se cambia el ValCarac a 11--%>
                                    <%--BUG-PC-38 MAUT Se cambia maxLength a 90--%>
                                </td>
                                <td style=" background-color:#FFFFFF;">* Cuenta Agencia:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextCuentaA" CssClass="txt3BBVA" MaxLength="20" onfocus="limpiaTitular();" Onkeypress="return ValCarac(event,7);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>* RFC:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextRFC" CssClass="txt3BBVA" MaxLength="15" Onkeypress="return ValCarac(event,16);"></asp:TextBox>
                                    <%--BUG-PC-14 2016-11-24 MAUT Se cambia el ValCarac a 16--%>
                                </td>
                                <td style=" background-color:#FFFFFF;">* Titular Cuenta:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextTCuenta" CssClass="txt3BBVA" MaxLength="100" Onkeypress="return ValCarac(event,16); assignValue();" ReadOnly="true" Enabled="false"></asp:TextBox>
                                    <%--BUG-PC-14 2016-11-24 MAUT Se hace el campo del titular de solo lectura y se deshabilita--%>
                                </td>
                                <td>
                                    <asp:Button ID="BtnValidarC" runat="server" Text="Validar" CssClass="buttonSecBBVA2"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td>* Estatus:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA" AutoPostBack="true"  Width="198px"></asp:DropDownList>
                                </td>
                                <td style=" background-color:#FFFFFF;">* Alianza:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbAlianza" CssClass="selectBBVA"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>* Comisión Agencia:</td>
                                <td>
                                    <asp:CheckBox ID="ChkComAgencia" runat="server" Checked="true" />
                                </td>
                                <td style=" background-color:#FFFFFF;">* Grupo:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbGrupo" CssClass="selectBBVA"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>* Comisión Vendedor:</td>
                                <td>
                                    <asp:CheckBox ID="ChkComVendedor" runat="server" />
                                </td>
                                <td style=" background-color:#FFFFFF;">* División:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbDivision" CssClass="selectBBVA"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                            <td></td>
                            <td></td>                            
                                 <td style=" background-color:#FFFFFF;">* Marca:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbMarca" CssClass="selectBBVA"></asp:DropDownList>
                                </td>        
                            </tr>
                            <tr>
                            <td></td>
                            <td></td>                            
                                <td style=" background-color:#FFFFFF;"> *Esquema</td>
                                <td><asp:DropDownList runat=server ID="CmbEsquema" CssClass="selectBBVA"  Visible="true"></asp:DropDownList></td>                                
                            </tr>
                            <tr>
                                <td>Usuario Ult Mod:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextUsuMod" CssClass="txt3BBVA" MaxLength="12" ReadOnly="true" Enabled="false"></asp:TextBox>
                                    <%--BUG-PC-14 2016-11-24 MAUT Se hace el campo de Usuario Ult Mod: de solo lectura y se deshabilita--%>
                                </td>
                                <td style=" background-color:#FFFFFF;">Fecha Ult Mod: </td>
                                    <td><asp:TextBox runat="server" ID="TextFecUltMod" CssClass="txt3BBVA" ReadOnly="true" Enabled="false"></asp:TextBox>                                                                                                                       
                                    <%--BUG-PC-14 2016-11-24 MAUT Se hace el campo de Fecha Ult Mod de solo lectura y se deshabilita--%>
                                </td>
                            </tr>
                            <tr>
                                <td>% Udis:</td>
                                <td>
                                      <asp:TextBox runat="server" ID="TextUdis" CssClass="txt3BBVA" MaxLength="5" Onkeypress="return checkDecimals(event,this.value,1);"></asp:TextBox>
                                    <%--BUG-PC-24 MAUT 24/12/2016 Se cambia checkDecimals--%>
                                </td>                                
                                <td style=" background-color:#FFFFFF;">* Responsable Facturas:</td>
                                <%--BUG-PC-38 MAUT 23/01/2017 Se agrega como campo obligatorio--%>
                                <td>
                                    <asp:TextBox runat="server" ID="TextRespFact" CssClass="txt3BBVA" MaxLength="100" Onkeypress="return ValCarac(event,18);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                                    <%--BUG-PC-14 2016-11-24 MAUT Se cambia el ValCarac a 16--%>
                                </td>                                
                            </tr>
                             <tr>
                                <td>Dividendos:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextDivi" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return checkDecimals(event,this.value,5)"></asp:TextBox>
                                </td>
                                <td style=" background-color:#FFFFFF;">* Fecha Registro: </td>
                                <td><asp:TextBox runat="server" ID="txtIniVig" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,13);"></asp:TextBox></td>     
                                <%--BUG-PC-14 2016-11-24 MAUT Se hace el campo de Fecha Registro de solo lectura--%>                                                                                                              
                            </tr>
                            <tr>
                                <td>Bono Especial Vendedor:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBonoV" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return checkDecimals(event,this.value,5)"></asp:TextBox>
                                </td>
                                <td style=" background-color:#FFFFFF; width:20%"><asp:Label ID="lblFinVig" runat="server" Text="Fin Vigencia: "/> </td>
                                <td><asp:TextBox runat="server" ID="txtFinVig" CssClass="txt3BBVA"></asp:TextBox></td>
                                <%--BUG-PC-14 2016-11-24 MAUT Se hace el campo de Fin Vigencia de solo lectura--%>                                                              
                            </tr>
                            <tr>
                                <%--<td>Bono por Créditos Colocados:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBonoC" CssClass="txt" MaxLength="12" Onkeypress="return ValCarac(event,9);"></asp:TextBox>
                                </td>--%>
                                <td style=" background-color:#FFFFFF;">Motivo Baja Agencia:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbMotivoBaja" CssClass="selectBBVA" Width="198px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Pago al F&I:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextPagoFI" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return checkDecimals(event,this.value,5)"></asp:TextBox>
                                </td>
                                <td style=" background-color:#FFFFFF;">Regalías/FEE:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextRegalias" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return checkDecimals(event,this.value,5)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;Seguro Regalado:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextSegReg" CssClass="txt3BBVA" MaxLength="9" Onkeypress="return checkDecimals(event,this.value,5)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;Default:</td>
                                <td>
                                    <asp:CheckBox ID="chkDefault" runat="server" />
                                </td>
								 <td>&nbsp;&nbsp;&nbsp;Valida Bloqueo Agencia:</td>
                                <td>
                                    <asp:CheckBox ID="chkServBloq" runat="server" />
                                </td>
                                 <td>&nbsp;&nbsp;&nbsp;Tiene Biometrico:</td>
                                <td>
                                    <asp:CheckBox ID="ChkBiom" runat="server" />
                                </td>
                            </tr>
                        </table>

                        <div style=" overflow:auto; "> <%--background-color:Red;--%>
                        <fieldset style="padding-top:10px; padding-bottom:20px;">
                            <fieldset class="fieldsetBBVA">
                            <legend>Bono por créditos colocados:</legend>

                                <div style="padding-top:0px; margin:auto;">
                                    <%--<asp:GridView runat="server" ID="grvAccesorios" Width="50%"></asp:GridView>--%>
                                    <div>
                                    <table class="resulbbva">
                                        <tr>
                                            <td style="background-color:White; width:135px;">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td style="background-color:White; padding:0px; width:30px;">Agregar:</td>
                                            <td><asp:ImageButton runat="server" ID="cmdAgregaAcc" ImageUrl="~/img/add.png"/> </td>
                                        </tr>
                                    </table>
                    </div>
                                    <asp:gridview ID="grvBonoCC"  runat="server"
                                                                 AutoGenerateColumns="false"   
                                                                  OnRowCreated="grvBonoCC_RowCreated">
                                    <HeaderStyle CssClass="GridHeader"/> 
                                <RowStyle CssClass="GridItem"/> 
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                                        <Columns>   
                                            <asp:BoundField DataField="RowNumber" HeaderText="" />
                                            <asp:TemplateField HeaderText="# Créditos Colocados" ItemStyle-Width="150px" ControlStyle-Width="150px">   
                                                <ItemTemplate>  
                                                    <asp:TextBox runat="server" ID="txtNoCreditos" CssClass="txt3BBVA" Text='<%#Bind("Column1")%>' MaxLength="6" Onkeypress="return ValCarac(event,7)"></asp:TextBox> 
                                                </ItemTemplate>   
                                            </asp:TemplateField> 
                                            <asp:TemplateField  HeaderText="Importe Bono" ItemStyle-Width="150px" ControlStyle-Width="150px">   
                                                <ItemTemplate>   
                                                     <asp:TextBox runat="server" ID="txtImporte" CssClass="txt3BBVA" Text='<%#Bind("Column2")%>' MaxLength="9" Onkeypress="return checkDecimals(event,this.value,5)"></asp:TextBox>   
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
                                </div>
                                <table class="resulbbva">                   
                                </table>
                            </fieldset>
                        </fieldset>
                    </div>
                    </div>

                    
                    <%--DOMICILIO--%>
                    <div id="seccion-2" class="tabs__contents__item" style="padding-left:25px;">
                        <table class="resulbbva">                            
                            <tr>
                                <td>* Tipo de Domicilio:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbTDomicilio" CssClass="selectBBVA" Width="198px" AutoPostBack="true" OnSelectedIndexChanged="cmbTDomicilio_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>* Código Postal:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextCodPos" CssClass="txt3BBVA" MaxLength="5" AutoPostBack="True" onkeypress="return ValCarac(event,7);"></asp:TextBox>
                                    <%--BUG-PC-14 2016-11-25 MAUT Se cambia Maxlength a 5 y solo acepta números--%>
                                </td>
                            </tr>                            
                            <tr>
                                <td>* Estado:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbEstados" CssClass="selectBBVA" Width="198px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>* Ciudad:</td>
                                <td>
                                    <asp:DropDownList CssClass="selectBBVA" ID="cmbCiudadCliente" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>* Delegación o Municipio:</td>
                                <td>
                                    <asp:DropDownList CssClass="selectBBVA" ID="cmbDelMunCliente" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>* Colonia:</td>
                                <td>
                                    <asp:DropDownList CssClass="selectBBVA" ID="cmbColoniaCliente" runat="server" AutoPostBack="True" ></asp:DropDownList>
                                </td>
                            </tr>             
                            <tr>
                                <td>* Calle:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDomi" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,16);"></asp:TextBox>
                                    <%--BUG-PC-14 2016-11-24 MAUT Se cambia el ValCarac a 16--%>
                                    <%--BUG-PC-38 MAUT 23/01/2017 Se cambia maxlenght a 50--%>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;No. Exterior:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextNoext" CssClass="txt3BBVA" MaxLength="20" Onkeypress="return ValCarac(event,16);"></asp:TextBox>
                                    <%--BUG-PC-14 2016-11-24 MAUT Se cambia el ValCarac a 16--%>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;No. Interior:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextNoint" CssClass="txt3BBVA" MaxLength="20" Onkeypress="return ValCarac(event,16);"></asp:TextBox>
                                    <%--BUG-PC-14 2016-11-24 MAUT Se cambia el ValCarac a 16--%>
                                </td>
                            </tr>                                           
                        </table>
                    </div>
                    <%--TELEFONO--%>
                    <div id="seccion-3" class="tabs__contents__item" style="padding-left:25px;">
                    <table class="resulbbva">
                        <tr>
                            <td> Nombre del Contacto</td>
                            <td>
                                <asp:TextBox runat="server" ID="TextContacto" CssClass="txt3BBVA" MaxLength="100" Onkeypress="return ValCarac(event,18);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                                <%--BUG-PC-14 2016-11-24 MAUT Se cambia el ValCarac a 16--%>
                            </td>
                        </tr>
                        <tr>
                            <td>* Tipo de Teléfono:</td>
                            <td>
                                <asp:DropDownList runat="server" ID="cmbTipoTel" CssClass="selectBBVA" AutoPostBack="True" OnSelectedIndexChanged="cmbTipoTel_SelectedIndexChanged" ></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--Larga Distancia--%>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="textLDist" CssClass="txt3BBVA" MaxLength="3" Onkeypress="return ValCarac(event,17);" Visible="false"></asp:TextBox>
                                <%--BUG-PC-14 2016-11-24 MAUT Se cambia el ValCarac a 17 para clave lada--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--Clave Lada--%>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TextLada" CssClass="txt3BBVA" MaxLength="3" Onkeypress="return ValCarac(event,7);" Visible="false"></asp:TextBox>
                                <%--BUG-PC-14 2016-11-24 MAUT Se cambia el maxlength a 3--%>
                            </td>
                        </tr>
                        <tr>
                            <td>* Teléfono:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtTel" CssClass="txt3BBVA" MaxLength="10" Onkeypress="return ValCarac(event,7);"></asp:TextBox>
                                <%--BUG-PC-14 2016-11-24 MAUT Se cambia el MaxLength a 10--%>
                            </td>                            
                        </tr>
                        <tr>                            
                            <td>
                                Extensión
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TextExt" CssClass="txt3BBVA" MaxLength="5" Onkeypress="return ValCarac(event,7);"></asp:TextBox>
                                <%--BUG-PC-14 2016-11-24 MAUT Se cambia el MaxLength a 5--%>
                            </td>
                        </tr>                        
                    </table>
                  </div>
                  <%--EMAIL--%>
                    <div id="seccion-4" class="tabs__contents__item" style="padding-left:25px;">
                    <table class="resulbbva">
                        <tr>
                            <td> Contacto:</td>
                            <td>
                                <asp:TextBox runat="server" ID="TextEmailContact" CssClass="txt3BBVA" MaxLength="100" Onkeypress="return ValCarac(event,18);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                                <%--BUG-PC-14 2016-11-24 MAUT Se cambia el ValCarac a 16--%>
                            </td>
                        </tr>
                        <tr>
                            <td> Email:</td>
                            <td>
                                <asp:TextBox runat="server" ID="TextEmail" CssClass="txt3BBVA" MaxLength="200" onblur="validarEmail();"></asp:TextBox>
                            </td>
                        </tr>                        
                    </table>
                  </div>
                </div>            
        </fieldset>
        <br />
        <fieldset>
            <table style="width:100%;">
                <tr id="trBotones">
                    <td colspan="2" align="center" style="height:40px; background-color:White;">
                    	<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"></asp:Button>
                    <td colspan="2" align="center" style="height:40px; background-color:White;">
                    	<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                    </td>                
                </tr>
            </table>
        </fieldset>
    </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <%--BUG-PC-60:MPUESTO:13/05/2017 CORRECCION DE VALIDACION DE NUMERO DE CUENTA Y TITULAR.--%>
    <asp:HiddenField runat="server"  ID="hdnTitularCuenta" />
    <asp:HiddenField runat="server"  ID="hdnCuentaAgencia" />
    <script type="text/javascript">
        $(document).ready(function () {
            pickerSettins2();
        });
    </script>
</asp:Content>

