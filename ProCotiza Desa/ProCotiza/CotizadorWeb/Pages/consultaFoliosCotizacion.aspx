<%@ Page Title="" Language="VB" MasterPageFile="~/Pages/Responsive.master" AutoEventWireup="false" CodeFile="consultaFoliosCotizacion.aspx.vb" Inherits="Pages_consultaFoliosCotizacion" %>

<%-- BUG-PC-129 GVARGAS 14/12/2017 Consulta Cotizacion Responsivo--%>
<%-- BUG-PC-138 GVARGAS 17/12/2017 Consulta Cotizacion CSS--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            $.datepicker.regional['es'] = {
                closeText: 'Cerrar',
                prevText: '< Ant',
                nextText: 'Sig >',
                currentText: 'Hoy',
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            };
            $.datepicker.setDefaults($.datepicker.regional['es']);

            var Settings_DatePicker = { minDate: new Date(2015, 12, 01),
                                        dateFormat: 'dd/mm/yy',
                                        maxDate: new Date(),
                                        showButtonPanel: true,
                                        changeMonth: true,
                                        changeYear: true,
                                        showOtherMonths: true,
                                        selectOtherMonths: true };

            $(".datepicker").datepicker(Settings_DatePicker).attr('readonly', 'true').attr('onkeydown', 'return false');

        });
    </script>
    <style>
        .center-block-right {
            display: block;
            margin-right: 0;
            margin-left: auto;
        }

        .center-block-left {
            display: block;
            margin-right: auto;
            margin-left: 0;
        }

        .form-control {
            height: 28px;
        }

        #workArea {
            min-width: 65% !Important;
            max-width: 65% !Important;
            margin: 0 auto 0 auto;
        }

        label {
            font-size: 7pt;
        }

        .form-control {
            font-size: 9px;
        }

        .label-center {
            padding-top: 30%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-header">
        <h5>CONSULTA COTIZACIONES</h5>
    </div>
    <div class="col-sm-12 col-md-6 col-lg-4">
        <div class="form-group row">
            <div class="col-xs-12 col-sm-12 col-lg-12 col">
                <div class="col-xs-3 col-sm-3 col-lg-3">
                    <label for="ContentPlaceHolder1_txtFolio" class="label-center">Folio: </label>
                </div>
                <div class="col-xs-9 col-sm-9 col-lg-9">
                    <asp:TextBox ID="txtFolio" runat="server" CssClass="form-control" onkeypress="return ValCarac(event,7);"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-lg-12 col">
                <div class="col-xs-3 col-sm-3 col-lg-3">
                    <label for="ContentPlaceHolder1_txtFecIni" style="margin-bottom: 0; ">Fecha del: </label>
                </div>
                <div class="col-xs-9 col-sm-9 col-lg-9">
                    <div class="input-group date">
                        <asp:TextBox ID="txtFecIni" CssClass="form-control datepicker" onpaste="false" runat="server"></asp:TextBox>
                        <div class="input-group-addon">
                            <i class="fa fa-calendar" style="font-size:13px"></i>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-lg-12 col">
                <div class="col-xs-3 col-sm-3 col-lg-3">
                    <label for="ContentPlaceHolder1_txtFecFin" class="label-center">Al: </label>
                </div>
                <div class="col-xs-9 col-sm-9 col-lg-9">
                    <div class="input-group date">
                        <asp:TextBox type="text" ID="txtFecFin" CssClass="form-control datepicker" onpaste="false" runat="server"></asp:TextBox>
                        <div class="input-group-addon">
                            <i class="fa fa-calendar" style="font-size:13px"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-6 col-lg-4">
        <div class="form-group row">
            <div class="col-xs-12 col-sm-12 col-lg-12 col">
                <div class="col-xs-3 col-sm-3 col-lg-3">
                    <label for="ContentPlaceHolder1_cmbAgencia" class="label-center">Agencia: </label>                
                </div>
                <div class="col-xs-9 col-sm-9 col-lg-9">
                    <asp:DropDownList runat="server" ID="cmbAgencia" CssClass="form-control" OnSelectedIndexChanged="cmbAgencia_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-lg-12 col">
                <div class="col-xs-3 col-sm-3 col-lg-3">
                    <label for="ContentPlaceHolder1_cmbAlianza" class="label-center">Alianza: </label>                                
                </div>
                <div class="col-xs-9 col-sm-9 col-lg-9">
                    <asp:DropDownList runat="server" ID="cmbAlianza" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-lg-12 col">
                <div class="col-xs-3 col-sm-3 col-lg-3">
                    <label for="ContentPlaceHolder1_cmbGrupo" class="label-center">Grupo: </label>                                    
                </div>
                <div class="col-xs-9 col-sm-9 col-lg-9">
                    <asp:DropDownList runat="server" ID="cmbGrupo" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-6 col-lg-4">
        <div class="form-group row">
            <div class="col-xs-12 col-sm-12 col-lg-12 col">
                <div class="col-xs-3 col-sm-3 col-lg-3">
                    <label for="ContentPlaceHolder1_cmbDivision" class="label-center">Division: </label>                
                </div>
                <div class="col-xs-9 col-sm-9 col-lg-9">
                    <asp:DropDownList runat="server" ID="cmbDivision" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-lg-12 col">
                <div class="col-xs-3 col-sm-3 col-lg-3">
                    <label for="ContentPlaceHolder1_cmbEstado" class="label-center">Estado: </label>                                
                </div>
                <div class="col-xs-9 col-sm-9 col-lg-9">
                    <asp:DropDownList runat="server" ID="cmbEstado" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-lg-12 col">
                <div class="col-xs-3 col-sm-3 col-lg-3">
                    <label for="ContentPlaceHolder1_txtNombreCli">Nombre Cliente: </label>                                                        
                </div>
                <div class="col-xs-9 col-sm-9 col-lg-9">
                    <asp:TextBox runat="server" ID="txtNombreCli" CssClass="form-control" Onkeypress="return ValCarac(event,18)" onkeyup="ReemplazaAcentos(event, this.id, this.value);" ></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 col-lg-12">        
        <div class="form-group row">
            <div class="col-xs-6 col-sm-6 col-lg-6">
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-default center-block-right" OnClick="btnLimpiar_Click" />
            </div>
            <div class="col-xs-6 col-sm-6 col-lg-6">
                <asp:button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary center-block-left" OnClick="btnBuscar_Click" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="col-sm-12 col-md-12 col-lg-12">
        <div class="table-responsive">
            <asp:GridView id="grvConsulta"
                            runat="server"
                            AutoGenerateColumns="false" 
                            AllowPaging="true"
                            PageSize="8"
                            CssClass="table-hover">
                <HeaderStyle CssClass="GridHeader"/> 
                <RowStyle CssClass="GridItem"/> 
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager"/>
                <Columns>            
                    <asp:BoundField DataField="id_cotizacion"      HeaderText="Folio" ItemStyle-Width="5%" ControlStyle-Width="5%" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="nombre"             HeaderText="Nombre del cliente" />
                    <asp:BoundField DataField="Agencia"            HeaderText="Agencia" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="Categoria"          HeaderText="Categoria" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Producto"           HeaderText="Producto" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="Modelo"             HeaderText="Modelo" ItemStyle-HorizontalAlign="Center"/>              
                    <asp:BoundField DataField="Plazo"              HeaderText="Plazo" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="pago_periodo_total" HeaderText="Pago" DataFormatString="{0:C}" HtmlEncode="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="fecha_cot"          HeaderText="Fecha" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="tipo_cot"           HeaderText="Tipo Cot." ItemStyle-HorizontalAlign="Center"/> 
                    <asp:BoundField DataField="vigencia_cot"       HeaderText="Vigencia" ItemStyle-HorizontalAlign="Center"/>
                    <asp:TemplateField                             HeaderText="Edit." ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgEdit" runat="server" CssClass="resul" CommandName="EditId" CommandArgument='<%# Eval("id_cotizacion") %>' ImageUrl="../img/script_edit.png" AlternateText="Edita cotización" />
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ver" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CssClass="resul" CommandName="folioId" CommandArgument='<%# Eval("id_cotizacion") %>' ImageUrl="../img/printer.png" AlternateText="Cargar cotización"/>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sol." ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgBtnSol" runat="server" CssClass="resul" CommandName="SolId" CommandArgument='<%# Eval("id_cotizacion") %>' ImageUrl="../img/script_edit.png" AlternateText="Carga Solicitud"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>              
            </asp:GridView>
            <asp:Label runat="server" ID="lblIdAse" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="lblIdProm" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="lblIdVend" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="Label1"></asp:Label>
        </div>
    </div>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

