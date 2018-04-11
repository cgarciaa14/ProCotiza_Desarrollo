<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false"
    CodeFile="manejaPaquetes.aspx.vb" Inherits="aspx_manejaPaquetes" MaintainScrollPositionOnPostback="true" %>

<%--BBVA-P-412: 06-07-2016: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08 SE AGREGA FUNCIONALIDAD PARA LOS CAMPOS NUEVOS--%>
<%--BBVA-P-412: AVH: 29/07/2016 CAMBIO DE NOMBRE DE LA COLUMNA Importe Mínimo G - Monto Minimo del Credito --%>
<%--BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.--%>
<%--BBV-P-412  RQ-E  gvargas   12/10/2016 Actualizacion de referencias. --%>
<%--BBVA-P-412 RQ WSA. WS GetLoan Products (RQ06, RQ09): AMR: 25/10/2016 WS GetLoan Products--%>
<%--BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.--%>
<%--  BUGPC16 24/11/2016: GVARGAS: Correccion Error Date Picker--%>
<%--BUG-PC-21: AMR: 28/11/2016 El nombre de la Marca se muestra el texto en Mayúsculas.--%>
<%--BUG-PC-37: AVH: 20/01/2017 Se deshabilitar boton que consume WS--%>
<%--BUG-PC-38 MAUT 23/01/2017 Se valida porcentaje maximo de accesorios--%>
<%--BUG-PC-44 JRHM 02/02/17 JRHM Se quita % a titulo que antes era % anualidades. --%>
<%--BUG-PC-46 JRHM 13/02/17  Se modifica etiqueta de textbox por % UDI. --%>
<%--BUG-PC-49 17/03/2017 MAPH Corrección de Validación de CheckBoxes para Cajas de Texto realizada desde Page_Load, adición del autopostback=true--%>
<%--RQ-MN2-2: ERODRIGUEZ: 21/08/2017 Requerimiento para agregar Tasa Nominal Dos, Comision Dos, para cuando el tipo de calculo sea igual a Balloon--%>
<%--RQ-MN2-2.2 ERODRIGUEZ: 27/09/2017 Se elimina porcetaje de refinanciamiento--%>
<%--'BUG-PC-131: CGARCIA: 28/11/2017: SE HABILITA COLUMNAS DEL GRID DE CONDICIONES CUANDO EN RELACIONES SE TENGA ARRENDAMIENTO PURO.--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 16/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>--%>
    <style type="text/css">
        @import url(../css/jquery-ui.css);
    </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
    <%--<script type="text/javascript" src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>--%>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../ExternalScripts/jquery.cookie.js"></script>
    <%--BBVA-P-412--%>
    <script type="text/javascript" src="../js/datepicker-es.js"></script>
    <script type="text/javascript">
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

        //BBVA-P-412
        function validarFormatoFecha(fecha) {
            var RegExPattern = /^\d{2,4}\-\d{1,2}\-\d{1,2}$/;
            if ((fecha.match(RegExPattern)) && (fecha != '')) {
                return true;
            } else {
                alert("El formato de fecha no es valido.");
            }
        }


        $(document).ready(
            function () {
                var check = $('[id*=chkclasif]');
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
        .ui-datepicker {
            font-size: 63%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: relative; margin-left: 20px; top: 15px;">
        <fieldset class="fieldsetBBVA" style="width: 900px; padding: 15px;">
            <legend>PAQUETES FINANCIEROS</legend>
            <fieldset>
                <asp:HiddenField runat="server" ID="current_tab" />
                <div class="tabs__links">
                    <a href="#seccion-1">INFORMACIÓN GENERAL</a> <a href="#seccion-2">RELACIONES</a>
                    <a href="#seccion-3">TIPOS DE SEGURO</a> <a href="#seccion-4">CONDICIONES</a> <a
                        href="#seccion-5">PROMOCIONES</a>
                </div>
                <div class="tabs__contents">
                    <%--INFORMACION GENERAL--%>
                    <div id="seccion-1" class="tabs__contents__item" style="padding-left: 25px;">
                        <table class="resulbbva">
                            <tr>
                                <td style="width: 150px; height: 25px;">&nbsp;&nbsp;&nbsp;Id:
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblid"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>* Nombre del Plan:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNom" CssClass="txt3BBVA" Width="180px" onkeyup="ReemplazaAcentos(event, this.id, this.value);"
                                        Onkeypress="return ValCarac(event,12)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>* Moneda:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbMoneda" CssClass="selectBBVA" Width="190px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>* Inicio Vigencia:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:TextBox runat="server" ID="txtIniVig" CssClass="txt3BBVA" Width="180px" Onkeypress="return ValCarac(event,13)"
                                        onchange="validarFormatoFecha(this.value);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>* Fin Vigencia:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFinVig" CssClass="txt3BBVA" Width="180px" Onkeypress="return ValCarac(event,13)"
                                        onchange="validarFormatoFecha(this.value);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>* Estatus:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA" Width="190px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>* Tipo Calendario:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbCalendario" CssClass="selectBBVA" Width="190px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>* Tipo Cálculo:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbTipoCalc" CssClass="selectBBVA" Width="190px" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>* Tipo Vencimiento:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbTipoVenc" CssClass="selectBBVA" Width="190px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Rentas Deposito Min:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDepGarMin" CssClass="txt3BBVA" Width="180px" Enabled="false" Onkeypress="return checkDecimals(event, this.value, 12)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Rentas Deposito Max:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDepGarMax" CssClass="txt3BBVA" Width="180px" Enabled="false" Onkeypress="return checkDecimals(event, this.value, 12)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>* % Max. Accesorios:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPtjAcc" CssClass="txt3BBVA" Width="180px" Onkeypress="return checkDecimals(event, this.value, 3)"
                                        MaxLength="5"></asp:TextBox>
                                </td>
                                <%--BUG-PC-38 MAUT Se pone maxlenght = 2--%>
                            </tr>
                            <tr>
                                <%--BBVA-P-412--%>
                                <td>* Producto UG:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlprod" CssClass="selectBBVA" Width="190px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>* SubProducto UG:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlsubprod" CssClass="selectBBVA" Width="190px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px;">&nbsp;&nbsp;&nbsp;Primer Pago Irreg:
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkPagoIrreg" Enabled="false" Checked="true" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px;">&nbsp;&nbsp;&nbsp;Suma Enganche Accesorios:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkEngAcc" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <%--BBVA-P-412--%>
                                <td style="height: 25px;">&nbsp;&nbsp;&nbsp;Anualidades:
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkPagosEsp" />
                                </td>
                            </tr>
                            <tr>
                                <%--BBVA-P-412--%>
                                <td>&nbsp;&nbsp;&nbsp;Anualidad:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtporanualidad" CssClass="txt3BBVA" Width="180px" Enabled="false" Onkeypress="return checkDecimals(event, this.value, 12)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px;">&nbsp;&nbsp;&nbsp;Permite Monto Subsidio:
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkSubsidio" AutoPostBack="true" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px;">&nbsp;&nbsp;&nbsp;Permite Ptj. Subsidio:
                                </td>
                                <%--BUG-PC-49 17/03/2017 MAPH Corrección de Validación de CheckBoxes para Cajas de Texto realizada desde Page_Load, adición del autopostback=true--%>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkPtjSubsidio" AutoPostBack="true" />
                                </td>
                            </tr>
                            <%--BBVA-P-412--%>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;% Subsidio Armadora:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtporsubArma" CssClass="txt3BBVA" Width="180px" Onkeypress="return checkDecimals(event, this.value, 2)"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;% Subsidio Agencia:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtporsubAgencia" CssClass="txt3BBVA" Width="180px" Onkeypress="return checkDecimals(event, this.value, 2)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                            </tr>
                            <%--                            <tr>
                                <td style="height:25px;">&nbsp;&nbsp;&nbsp;Gracia Total(Capital/Interes):</td>
                                <td><asp:CheckBox runat="server" ID="chkgraciatot"/></td>
                            </tr>--%>
                            <tr>
                                <td style="height: 25px;">&nbsp;&nbsp;&nbsp;Permite Gracia Capital:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkGraCap" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px;">&nbsp;&nbsp;&nbsp;Permite Gracia Interés:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkGraInt" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px;">&nbsp;&nbsp;&nbsp;Plantilla Cotizador Abierto:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkCotAb" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px;">&nbsp;&nbsp;&nbsp;Captura Manual Seguro:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkCaptSegManual" Enabled="false" />
                                </td>
                            </tr>
                            <%--BBVA-P-412--%>
                            <%--                            <tr>
                                <td style="height:25px;">&nbsp;&nbsp;&nbsp;Vía Calculo Seguro Producto: </td>
                                <td><asp:DropDownList runat="server" ID="cmbTipoCalcSeg" CssClass="select"></asp:DropDownList></td>
                            </tr>--%>
                            <tr>
                                <td style="height: 25px;">&nbsp;&nbsp;&nbsp;Incentivo Ventas:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:TextBox runat="server" ID="txtIncentivoVtas" CssClass="txt3BBVA" Width="180px" Onkeypress="return checkDecimals(event, this.value, 12)"></asp:TextBox>
                                </td>
                            </tr>
                            <%--BBVA-P-412--%>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;% UDI:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtComvtaseg" CssClass="txt3BBVA" Width="180px" Onkeypress="return checkDecimals(event, this.value, 12)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;% Comisión Vendedor:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtComvendMin" CssClass="txt3BBVA" Width="70px" placeholder="Mínimo"
                                        Onkeypress="return checkDecimals(event, this.value, 12)"></asp:TextBox>
                                    &nbsp;<->&nbsp;
                                    <asp:TextBox runat="server" ID="txtComvendMax" CssClass="txt3BBVA" Width="70px" placeholder="Máximo"
                                        Onkeypress="return checkDecimals(event, this.value, 12)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;% Comisión Agencia:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtComAgenMin" CssClass="txt3BBVA" Width="70px" placeholder="Mínimo"
                                        Onkeypress="return checkDecimals(event, this.value, 12)"></asp:TextBox>
                                    &nbsp;<->&nbsp;
                                    <asp:TextBox runat="server" ID="txtComAgenMax" CssClass="txt3BBVA" Width="70px" placeholder="Máximo"
                                        Onkeypress="return checkDecimals(event, this.value, 12)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <%--BBVA-P-412:AVH: CAMBIO DE NOMBRE DE LA COLUMNA Importe Mínimo G - Monto Minimo del Credito --%>
                                <td>* Monto Mínimo del Crédito
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtimpming" CssClass="txt3BBVA" Width="180px" Onkeypress="return checkDecimals(event, this.value, 12);"></asp:TextBox>
                                </td>
                                <%--ValCarac(event,9) .name--%>
                            </tr>
                            <tr>
                                <td>* Monto Máximo del Crédito
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtimpmaxg" CssClass="txt3BBVA" Width="180px" Onkeypress="return checkDecimals(event, this.value, 12)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px;">&nbsp;&nbsp;&nbsp;Paquete Default:
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkDefault" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--RELACIONES--%>
                    <div id="seccion-2" class="tabs__contents__item" style="padding-left: 25px;">
                        <%--BBVA-P-412--%>
                        <p class="p">
                            * TIPOS DE PRODUCTO
                        </p>
                        <asp:GridView runat="server" ID="gdvTP" RowStyle-CssClass="resul" Width="50%" AutoGenerateColumns="false"
                            CssClass="resulbbva" HeaderStyle-HorizontalAlign="Left"
                            DataKeyNames="ID_TIPO_PRODUCTO, ESTATUS_TIPO_PROD, ID_ESTATUS">
                            <HeaderStyle CssClass="GridHeader" />
                            <RowStyle CssClass="GridItem" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px" ControlStyle-Width="25px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID='chkClase' runat="server" CommandName="idtipoprod" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--BBVA-P-412--%>
                                <asp:BoundField DataField="tipo_producto" HeaderText="Tipos Producto" />
                                <asp:BoundField DataField="ID_TIPO_PRODUCTO" Visible="True" />
                                <asp:BoundField DataField="ESTATUS_TIPO_PROD" Visible="false" />
                                <asp:BoundField DataField="ID_ESTATUS" Visible="false" />
                            </Columns>
                        </asp:GridView>
                        <%--BBVA-P-412--%>
                        <p class="p">
                            * CLASIFICACIÓN DE PRODUCTO
                        </p>
                        <asp:GridView runat="server" ID="gdvClasif" RowStyle-CssClass="resulbbva" Width="50%"
                            AutoGenerateColumns="false" CssClass="resulbbva"
                            HeaderStyle-HorizontalAlign="Left" DataKeyNames="ID_CLASIFICACION, ESTATUS_CLASIF, ID_ESTATUS">
                            <HeaderStyle CssClass="GridHeader" />
                            <RowStyle CssClass="GridItem" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px" ControlStyle-Width="25px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID='chkclasif' runat="server" CommandName="idclasifprod" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="clasificacion" HeaderText="Clasificación Producto" />
                                <asp:BoundField DataField="ID_CLASIFICACION" Visible="true" />
                                <asp:BoundField DataField="ID_ESTATUS" Visible="false" />
                            </Columns>
                        </asp:GridView>
                        <%--BBVA-P-412--%>
                        <p class="p">
                            * TIPO DE OPERACIÓN
                        </p>
                        <asp:GridView runat="server" ID="gdvTO" RowStyle-CssClass="resulbbva" Width="50%" AutoGenerateColumns="false"
                            CssClass="resulbbva" HeaderStyle-HorizontalAlign="Left"
                            DataKeyNames="ID_TIPO_OPERACION, ESTATUS_TIPO_OPER, ID_ESTATUS">
                            <HeaderStyle CssClass="GridHeader" />
                            <RowStyle CssClass="GridItem" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px" ControlStyle-Width="25px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID='chkTO' runat="server" AutoPostBack="true" OnCheckedChanged="chkTO_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tipo_operacion" HeaderText="Tipos de Operación" />
                                <asp:BoundField DataField="ID_TIPO_OPERACION" Visible="true" />
                                <asp:BoundField DataField="ID_ESTATUS" Visible="false" />
                            </Columns>
                        </asp:GridView>
                        <%--BBVA-P-412--%>
                        <p class="p">
                            * TIPO DE PERSONALIDAD JURÍDICA
                        </p>
                        <asp:GridView runat="server" ID="gdvPerJur" RowStyle-CssClass="resulbbva" Width="50%"
                            AutoGenerateColumns="false" CssClass="resulbbva"
                            HeaderStyle-HorizontalAlign="Left" DataKeyNames="id_per_juridica, ESTATUS_PER_JUR, ID_ESTATUS">
                            <HeaderStyle CssClass="GridHeader" />
                            <RowStyle CssClass="GridItem" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px" ControlStyle-Width="25px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID='chkPlazo' runat="server" CommandName="idperjur" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tipo_persona" HeaderText="Tipo Personalidad" />
                                <asp:BoundField DataField="id_per_juridica" Visible="true" />
                                <asp:BoundField DataField="ESTATUS_PER_JUR" Visible="false" />
                                <asp:BoundField DataField="ID_ESTATUS" Visible="false" />
                            </Columns>
                        </asp:GridView>
                        <%--BBVA-P-412--%>
                        <p class="p">
                            * TIPOS DE CANAL
                        </p>
                        <asp:GridView runat="server" ID="gdvCanales" RowStyle-CssClass="resulbbva" Width="50%"
                            AutoGenerateColumns="false" CssClass="resulbbva"
                            HeaderStyle-HorizontalAlign="Left" DataKeyNames="ID_CANAL, ESTATUS_CANAL, ID_ESTATUS">
                            <HeaderStyle CssClass="GridHeader" />
                            <RowStyle CssClass="GridItem" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px" ControlStyle-Width="25px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID='chkPlazo' runat="server" CommandName="idcanal" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CANAL" HeaderText="Tipo Canal" />
                                <asp:BoundField DataField="ID_CANAL" Visible="true" />
                                <asp:BoundField DataField="ESTATUS_CANAL" Visible="false" />
                                <asp:BoundField DataField="ID_ESTATUS" Visible="false" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <%--TIPOS DE SEGUROS--%>
                    <div id="seccion-3" class="tabs__contents__item" style="padding-left: 25px;">
                        <%--BBVA-P-412--%>
                        <fieldset class="fieldsetBBVA" style="width: 50%;">
                            <legend>Seguro Producto</legend>
                            <asp:GridView runat="server" ID="gdvTipoSeg" RowStyle-CssClass="resulbbva" Width="98%"
                                AutoGenerateColumns="false" CssClass="resulbbva"
                                HeaderStyle-HorizontalAlign="Left" DataKeyNames="ID_PARAMETRO, VALOR, ESTATUS_TIPO_SEG">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px" ControlStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID='chkTSeg' runat="server" CommandName="idtiposeg" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TIPO_SEGURO" HeaderText="Tipos de Seguro" />
                                    <asp:BoundField DataField="ID_PARAMETRO" Visible="true" />
                                    <asp:BoundField DataField="VALOR" Visible="false" />
                                    <asp:BoundField DataField="ESTATUS_TIPO_SEG" Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </fieldset>
                        <%--BBVA-P-412--%>
                        <br />
                        <fieldset class="fieldsetBBVA" style="width: 50%;">
                            <legend>Seguro de Vida</legend>
                            <asp:GridView runat="server" ID="gdvTipoSegVida" RowStyle-CssClass="resulbbva" Width="98%"
                                AutoGenerateColumns="false" CssClass="resulbbva"
                                HeaderStyle-HorizontalAlign="Left" DataKeyNames="ID_PARAMETRO, VALOR, ESTATUS_TIPO_SEG">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px" ControlStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID='chkTSegVida' runat="server" CommandName="idtiposegvida" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TIPO_SEGURO" HeaderText="Tipos de Seguro de Vida" />
                                    <asp:BoundField DataField="ID_PARAMETRO" Visible="true" />
                                    <asp:BoundField DataField="VALOR" Visible="false" />
                                    <asp:BoundField DataField="ESTATUS_TIPO_SEG" Visible="false" />
                                </Columns>
                            </asp:GridView>
                            <br />
                            <table class="resulbbva" style="display: none;">
                                <tr>
                                    <td>Web Service
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rdbsegvidaws" GroupName="segvida" AutoPostBack="true"
                                            Checked="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Broker (Factor)
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rdbsegvidafact" GroupName="segvida" AutoPostBack="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 160px;">Seguro de Vida Deudor:
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="cmbTipoSegVid" CssClass="selectBBVA" Width="180px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Factor Seguro de Vida:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtFactSegVid" CssClass="txt3BBVA" Width="60px" MaxLength="9"
                                            Onkeypress="return ValCarac(event,9)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Calcula IVA Seguro Vida Deudor:
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIvaSegVida" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <%--BBVA-P-412--%>
                        </fieldset>
                    </div>
                    <%--CONDICIONES--%>
                    <div id="seccion-4" class="tabs__contents__item" style="padding-left: 25px;">
                        <table class="resul2">
                            <tr>
                                <td style="width: 46%;">* Periodicidad:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbPeriodicidad" CssClass="selectBBVA" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;Permite Día Pago:
                                </td>
                                <%--BBVA-P-412--%>
                                <td>
                                    <asp:CheckBox ID="chkDiaPago" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <%--BBVA-P-412--%>
                            <%--                        <tr>
                            <td>&nbsp;&nbsp;&nbsp;Maneja Tasa de Interés Variable:</td>
                            <td><asp:CheckBox ID="chkTasaIntVar" runat="server" AutoPostBack="true"/></td>
                        </tr>
                        <tr>
                            <td>&nbsp;&nbsp;&nbsp;Tasa Interés Variable:</td>
                            <td><asp:DropDownList runat="server" ID="cmbTasaIntVar" CssClass="select" Enabled="False"></asp:DropDownList></td>
                        </tr>--%>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;0% Tasa Nominal:
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkcerocom" AutoPostBack="true" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView runat="server" ID="gdvPlazos" RowStyle-CssClass="resulbbva" Width="100%"
                            AutoGenerateColumns="false" CssClass="resulbbva"
                            DataKeyNames="ID_PLAZO, PLAZO" EmptyDataText="No se existen plazos quincenales o estan inactivos.">
                            <HeaderStyle CssClass="GridHeader" />
                            <RowStyle CssClass="GridItem" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px" ControlStyle-Width="25px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID='chkPlazo' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="nombre_plazo" HeaderText="Plazo" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="80px" ControlStyle-Width="80px" />
                                <asp:TemplateField HeaderText="% Enganche Mínimo" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Width="50px" CssClass="txt3BBVA" MaxLength="5"
                                            Onkeypress="return checkDecimals(event, this.value, 1);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% Tasa Nominal" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Width="50px" CssClass="txt3BBVA" MaxLength="5"
                                            Onkeypress="return checkDecimals(event, this.value, 1);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% Tasa Nominal Seguro" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Width="50px" CssClass="txt3BBVA" MaxLength="5"
                                            Onkeypress="return checkDecimals(event, this.value, 1);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ptos Seg X Cta Cliente" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%--BBVA-P-412--%>
                                        <asp:TextBox ID="TextBox4" runat="server" Width="50px" CssClass="txt3BBVA" MaxLength="5"
                                            Enabled="false" Onkeypress="return checkDecimals(event, this.value, 1);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% Comision X Apert." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Width="50px" CssClass="txt3BBVA" MaxLength="5"
                                            Onkeypress="return checkDecimals(event, this.value, 1);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% Opción de Compra" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%--BBVA-P-412--%>
                                        <asp:TextBox ID="TextBox6" runat="server" Width="50px" CssClass="txt3BBVA" MaxLength="5"
                                            Enabled="false" Onkeypress="return checkDecimals(event, this.value, 1);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--                                <asp:TemplateField HeaderText="Tasa PCP" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server" Width="50px" CssClass="resul" MaxLength="7" Onkeypress="return validarNro('D',event);"></asp:TextBox> 
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="% Valor Residual" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox8" runat="server" Width="50px" CssClass="txt3BBVA" MaxLength="5"
                                            Onkeypress="return checkDecimals(event, this.value, 1);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="id_plazo" Visible="true" />
                                <asp:BoundField DataField="PLAZO" Visible="true" />

                                <asp:TemplateField HeaderText="% Tasa Nominal Refinanciamiento" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox9" runat="server" Width="50px" CssClass="txt3BBVA" MaxLength="5"
                                            Onkeypress="return checkDecimals(event, this.value, 1);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="% Comision Refinanciamiento" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox10" runat="server" Width="50px" CssClass="txt3BBVA" MaxLength="5"
                                            Onkeypress="return checkDecimals(event, this.value, 1);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <%--PROMOCIONES--%>
                    <div id="seccion-5" class="tabs__contents__item" style="padding-left: 25px;">
                        <asp:GridView runat="server" ID="grvpromociones" RowStyle-CssClass="resulbbva" Width="50%"
                            AutoGenerateColumns="false" CssClass="resulbbva"
                            HeaderStyle-HorizontalAlign="Left" DataKeyNames="ID_PROMOCION, ID_PERIODICIDAD, ASIG">
                            <HeaderStyle CssClass="GridHeader" />
                            <RowStyle CssClass="GridItem" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px" ControlStyle-Width="25px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID='chkPromocion' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Promociónes" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ControlStyle-Width="100px"
                                    HeaderText="Periodos">
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlperiodos" CssClass="selectBBVA">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID_PROMOCION" Visible="true" />
                                <asp:BoundField DataField="ID_PERIODICIDAD" Visible="false" />
                                <asp:BoundField DataField="ASIG" Visible="false" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </fieldset>
            <br />
            <fieldset>
                <center>
                <table style="width:100%;">
                    <tr id="trBotones">
                        <td colspan="2" align="center" style="background-color:White;">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2" OnClientClick="this.disabled=true;"
                                UseSubmitBehavior="false"></asp:Button>
                        </td>
                            <td colspan="2" align="center" style="background-color:White;">
                            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                        </td>
                    </tr>
                </table>
                </center>
            </fieldset>
        </fieldset>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <asp:Label runat="server" ID="lblScript"></asp:Label>
    <script type="text/javascript">
        $(document).ready(function () {
            pickerSettins2();
        });
    </script>
</asp:Content>
