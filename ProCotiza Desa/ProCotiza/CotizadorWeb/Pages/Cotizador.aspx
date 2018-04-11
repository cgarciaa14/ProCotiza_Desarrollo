<%@ Page Title="" Language="VB" MasterPageFile="~/Pages/Responsive.master" AutoEventWireup="false" CodeFile="Cotizador.aspx.vb" Inherits="Pages_Cotizador" %>

<%-- BUG-PC-133 GVARGAS 07/12/2017 Resposive Design  --%>
<%-- BUG-PC-138 GVARGAS 18/12/2017 Resposive Message Alert  --%>
<%-- BUG-PC-141 GVARGAS 21/12/2017 Correcion Anualidades  --%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <style type="text/css">
        @import url(../css/jquery-ui.css);
    </style>
    <%--    <style type="text/css"> @import url(../css/jquery-ui.theme.css);</style>
    <style type="text/css"> @import url(../css/jquery-ui.theme.min.css);</style>--%>
    <!--<link rel="stylesheet" href="//code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css"/>-->
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/gridviewScroll.min.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/datepicker-es.js"></script>
    <script type="text/javascript" src="../ExternalScripts/jquery.cookie.js"></script>
    <style type="text/css">@import url(../css/procotiza.css); </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#loading-div-background").css({ opacity: 1.0 });
            muestradiv();
            muestradiv2();

        });

        function ShowProgressAnimation() {
            $("#loading-div-background").show();
        };

        $(document).ready(function () {
            $("#loading-div-background2").css({ opacity: 1.0 });
        });

        function ShowProgressAnimation2() {
            $("#loading-div-background2").show();
        };

        $(document).ready(function () {
            gridviewScroll();

            function gridviewScroll() {
                $('#<%=gdvPagEsp.ClientID%>').gridviewScroll({
                        width: 735,
                        height: 120,
                        freezesize: 1,
                        arrowsize: 30,
                        varrowtopimg: "../img/arrowvt.png",
                        varrowbottomimg: "../img/arrowvb.png",
                        harrowleftimg: "../img/arrowhl.png",
                        harrowrightimg: "../img/arrowhr.png"
                    });
                }

            });

            $(document).ready(function () {
                gridviewScroll();

                function gridviewScroll() {
                    $('#<%=grvMultiCotiza.UniqueID%>').gridviewScroll({
                    width: 735,
                    height: 253,
                    freezesize: 1,
                    arrowsize: 30,
                    varrowtopimg: "../img/arrowvt.png",
                    varrowbottomimg: "../img/arrowvb.png",
                    harrowleftimg: "../img/arrowhl.png",
                    harrowrightimg: "../img/arrowhr.png"
                });
            }

        });

        $(document).ready(function () {
            gridviewScroll();

            function gridviewScroll() {
                $('#<%=grvSeguro.ClientID%>').gridviewScroll({
                    width: 730,
                    height: 170,
                    freezesize: 2,
                    arrowsize: 30,
                    varrowtopimg: "../img/arrowvt.png",
                    varrowbottomimg: "../img/arrowvb.png",
                    harrowleftimg: "../img/arrowhl.png",
                    harrowrightimg: "../img/arrowhr.png"
                });
            }
        });

        function validarEmail() {
            var email = $("input[id$=txtmail]").val();
            if (!email == '') {
                expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!expr.test(email))
                    alert("El formato de la dirección de correo no es valido.");
            }
        }

        function valnombre(obj, opc) {
            var valor = (document.getElementById(obj).value);
            var large = valor;
            var idpj = $('[id$=ddlpj] option:selected').val();

            if (large.length > 0) {
                //(document.getElementById("txtnombrecte").value) = valor;
                if (idpj != 15) {
                    $("#ContentPlaceHolder1_txtnombrecte").val($("#ContentPlaceHolder1_txtnombrectepf").val());
                }

                if (large.length < 3) {
                    alert("Debe ingresar al menos 3 caracteres.")
                }

            }
            //BUG-PC-25 MAUT 15/12/2016 Opcional
            if (opc == 0) {
                //BUG-PC-24 MAUT 15/12/2016 Se avisa que el campo es obligatorio
                if (large.length == 0) {
                    alert("El nombre del cliente es obligatorio.")
                }
            }

        };
        function valapellidos(obj, opc) {
            var valor = (document.getElementById(obj).value);
            var large = valor;
            var idpj = $('[id$=ddlpj] option:selected').val();

            if (large.length > 0) {

                if (large.length < 3) {
                    alert("Debe ingresar al menos 3 caracteres.")
                }

            }
            if (opc == 0) {
                if (large.length == 0) {
                    alert("El Apellido del cliente es obligatorio.")
                }
            }

        };

        function valcp(obj) {
            var cplarge = (document.getElementById(obj).value);
            var cptxt = (document.getElementById(obj));
            if (cptxt.value == "00000") {
                alert("Código Postal incorrecto.");
                cptxt.value = ""
                cptxt.focus();
            } else if (cplarge.length < 5) {
                alert("El Código Postal debe contener 5 caracteres.");
                cptxt.value = ""
                cptxt.focus();
            }
        };

        function valTel(obj) {
            var valor = (document.getElementById(obj).value);
            var large = valor;
            //BUG-PC-30
            if (large.length > 0) {
                if (large.length < 10 || large.length > 10) {
                    alert("El teléfono debe contener 10 caracteres.");
                }
            }
        };


        function validarRFC() {
            var rfc = $("[id$=txtrfc]").val();
            var e = document.getElementById("[id$=ddlpj]");
            var idpj = $('[id$=ddlpj] option:selected').val();

            if (idpj == 15) {

                if (rfc.length < 9) {//12
                    alert("Longitud incorrecta del RFC AAA999999XXX");
                    //BUG-PC-34 MAUT 16/01/2017 Se cambia el focus
                    $("input[id$=txtnombrecte]").focus();
                }
                else {
                    //var valid = '^(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))';
                    var valid = '/^([A-ZÑ\x26]{3}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))((-)?([A-Z\d]{2}))?$/';
                }

            }
            else {
                if (rfc.length < 10) {//13
                    alert("Longitud incorrecta del RFC AAAA999999XXX");
                    //BUG-PC-34 MAUT 16/01/2017 Se cambia el focus
                    document.getElementById("ContentPlaceHolder1_txtrfc").value = ''
                    $("input[id$=txtnombrectepf]").focus();
                }
                else {
                    //var valid = '^(([A-Z]|[a-z]|\s){1})(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))';
                    var valid = /^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))((-)?([A-Z\d]{3}))?$/;
                }
            }

            rfc = rfc.toUpperCase();
            if (!rfc.match(valid)) {
                alert('El RFC ingresado no tiene el formato correcto.');
                document.getElementById("ContentPlaceHolder1_txtrfc").value = ''
                $("input[id$=txtrfc]").focus();
            }

        }

        function showContent() {
            element = document.getElementById("tbPagosEspeciales");

            check = document.getElementById("ContentPlaceHolder1_chkPagosEsp");
            if (check.checked) {
                element.style.display = 'block';
            }
            else {

                element.style.display = 'none';
            }
        }


        $(function () {
            $("[id$=txtedad]").spinner(
            {
                min: 18,
                max: 70,
                icons: { down: "ui-icon-triangle-1-s", up: "ui-icon-triangle-1-n" }
                //            icons: { down: "ui-icon-carat-1-s", up: "ui-icon-carat-1-n" }
            }

            );
        });


        $(function () {
            var chk = $('[id*=chkdatoscte]');
            var div = $('#divcte');
            chk.change(function () {
                if ($(this).is(":checked")) {
                    div.addClass("mostrar");
                    //var x = $("[id$=txtmail]").position();
                    // $("[id$=txtnombrecte]").focus();
                    //var posdiv = document.getElementById("dvScroll");
                    //posdiv.scrollTop = x.top;
                }
                else {
                    div.removeClass("mostrar");
                }
            });
        });

        $(function () {
            var chk = $('[id*=chkDatosAgencia]');
            var div = $('#divAgencia');
            chk.change(function () {
                if ($(this).is(":checked")) {
                    div.addClass("mostrar");
                }
                else {
                    div.removeClass("mostrar");
                }
            });
        });

        function valedad() {

            var edad = $("[id$=txtedad]").val();
            if (edad < 18 || edad > 70) {
                alert('La edad no esta dentro de los parametros establecidos.');
                $("[id$=txtedad]").val('18');
            }
        };

        function muestradiv() {
            var chk = $('[id*=chkdatoscte]')
            var div = $('#divcte');

            if (chk.prop('checked')) {
                div.addClass("mostrar");
            }
            else {
                div.removeClass("mostrar");
            }
        };

        function muestradiv2() {
            var chk = $('[id*=chkDatosAgencia]')
            var div = $('#divAgencia');

            if (chk.prop('checked')) {
                div.addClass("mostrar");
            }
            else {
                div.removeClass("mostrar");
            }
        };

        $(function () {
            //var div = document.getElementById("dvScroll");
            //var position = 0
            //div.onscroll = function () {
            //    position = div.scrollTop;
            //    document.getElementById("ContentPlaceHolder1_div_position").value = position;
            //};
        });

        window.onload = function () {
            //var pos = document.getElementById("ContentPlaceHolder1_div_position").value;
            //var div = document.getElementById("dvScroll");
            //div.scrollTop = pos;
        };

        $(function () {
            //var div = document.getElementById("ContentPlaceHolder1_divfinadic");
            //var position = 0

            //if (div != null) {
            //    div.onscroll = function () {
            //        position = div.scrollTop;
            //        document.getElementById("ContentPlaceHolder1_div_position2").value = position;
            //    };
            //}
        });

        window.onload = function () {
            //var pos = document.getElementById("ContentPlaceHolder1_div_position").value;
            //var div = document.getElementById("dvScroll");
            //div.scrollTop = pos;

            //var pos2 = document.getElementById("ContentPlaceHolder1_div_position2").value;
            //var div2 = document.getElementById("ContentPlaceHolder1_divfinadic");
            //if (div2 != null) {
            //    div2.scrollTop = pos2;
            //}
        };


    </script>
    <style type="text/css">
        .ui-datepicker
        {
            font-size: 63%;
        }

        #tts, #tts1
        {
            width: 180px;
            height: 25px;
            font-size: 10px;
            padding: 5px;
            top: -90%;
            left: 1%;
        }

        h5 {
            margin: 0;
        }

        .label-center {
            padding-top: 10%;
        }

        .col-lg-12 {
            height: 40px;
        }

        .page-header {
            border: 0;
        }

        .panel-default > .panel-heading {
            color: #31708f;
            background-color: #d9edf7;
            border-color: #bce8f1;
        }

        .span-label {
            font-family: Arial;
            font-size: 8pt;
            background-color: White;
            color: #666666;
            border-top: 1px solid White;
            border-bottom: 1px solid White;
            border-left: 1px solid White;
            border-right: 1px solid White;
            font-weight: bold;
        }

        .panel-body {
            padding: 5px 75px 0 75px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="alert alert-info">
        <i class="fa fa-exclamation-triangle" style="font-size: 1.5em;"></i> Para guardar la información es necesario ingresar todos los datos marcados con asterisco <span style="color:red;">*</span>.
    </div>
    <div class="panel-group" id="accordion_">
	    <div class="panel panel-default">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Cliente</a>
			    </h5>
		    </div>
		    <div id="collapse1" class="panel-collapse collapse in">
			    <div class="panel-body">
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label for="ContentPlaceHolder1_ddltoperacion">Tipo de Operación<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddltoperacion" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col" style="margin-bottom: 0;">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label for="ContentPlaceHolder1_ddlpj">Personalidad Juridica<span style="color:red;">*</span> </label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlpj" CssClass="form-control" AutoPostBack="true" />
                                    <asp:DropDownList runat="server" ID="ddlgiro" CssClass="selectBBVA" AutoPostBack="true" style="display: none" />
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <label for="ContentPlaceHolder1_chkdatoscte" class="label-center" style="padding-top: 5%;">Datos del Cliente</label>
                                </div>
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <div class="checkbox">
                                        <input type="checkbox" value="" runat="server" ID="chkdatoscte">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divcte">
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <div class="form-group row">
                                <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                    <div class="col-xs-4 col-sm-4 col-lg-4">
                                        <label class="label-center">Nombre<span style="color:red;">*</span></label>
                                    </div>
                                    <div class="col-xs-8 col-sm-8 col-lg-8">
                                        <asp:TextBox runat="server" ID="txtnombrecte" CssClass="form-control" Onkeypress="return ValCarac(event,18)"
                                                    Onblur="valnombre(this.id,0);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtnombrectepf" CssClass="form-control" Onkeypress="return ValCarac(event,18)"
                                                        Onblur="valnombre(this.id,0);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"
                                                        AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                    <div class="col-xs-4 col-sm-4 col-lg-4">
                                        <label for="ContentPlaceHolder1_txtnombrecte2" class="label-center">2° Nombre</label>
                                    </div>
                                    <div class="col-xs-8 col-sm-8 col-lg-8">
                                        <asp:TextBox runat="server" ID="txtnombrecte2" CssClass="form-control" Onkeypress="return ValCarac(event,18)"
                                                        onkeyup="ReemplazaAcentos(event, this.id, this.value);" Onblur="valnombre(this.id,1);"
                                                        AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                    <div class="col-xs-4 col-sm-4 col-lg-4">
                                        <label for="ContentPlaceHolder1_txtpaterno" class="label-center">Paterno<span style="color:red;">*</span></label>
                                    </div>
                                    <div class="col-xs-8 col-sm-8 col-lg-8">
                                        <asp:TextBox runat="server" ID="txtpaterno" CssClass="form-control" Onkeypress="return ValCarac(event,18)"
                                                        onkeyup="ReemplazaAcentos(event, this.id, this.value);" Onblur="valapellidos(this.id,0);"
                                                        AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <div class="form-group row">
                                <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                    <div class="col-xs-4 col-sm-4 col-lg-4">
                                        <label for="ContentPlaceHolder1_txtmaterno" class="label-center">Materno</label>
                                    </div>
                                    <div class="col-xs-8 col-sm-8 col-lg-8">
                                        <asp:TextBox runat="server" ID="txtmaterno" CssClass="form-control" Onkeypress="return ValCarac(event,18)"
                                                        onkeyup="ReemplazaAcentos(event, this.id, this.value);" Onblur="valapellidos(this.id,1);"
                                                        AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                    <div class="col-xs-4 col-sm-4 col-lg-4">
                                        <label for="ContentPlaceHolder1_txtfecnac">Fecha de Nacimiento<span style="color:red;">*</span></label>
                                    </div>
                                    <div class="col-xs-8 col-sm-8 col-lg-8">                        
                                        <div class="input-group date">
                                            <asp:TextBox runat="server" ID="txtfecnac" CssClass="form-control datepicker" Onkeypress="return ValCarac(event,13)" AutoPostBack="true" />
                                            <div class="input-group-addon">
                                                <span class="glyphicon glyphicon-th"></span>
                                            </div>
                                        </div>
                           
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                    <div class="col-xs-4 col-sm-4 col-lg-4">
                                        <label for="ContentPlaceHolder1_txtrfc" class="label-center">RFC<span style="color:red;">*</span></label>
                                    </div>
                                    <div class="col-xs-8 col-sm-8 col-lg-8">
                                        <asp:TextBox runat="server" ID="txtrfc" CssClass="form-control" Style="text-transform: uppercase"
                                                        Onkeypress="return ValCarac(event,14)" MaxLength="13" onblur="validarRFC();"
                                                        AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <div class="form-group row">
                                <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                    <div class="col-xs-4 col-sm-4 col-lg-4">
                                        <label for="ContentPlaceHolder1_txtcp">Código Postal<span style="color:red;">*</span></label>
                                    </div>
                                    <div class="col-xs-8 col-sm-8 col-lg-8">
                                        <asp:TextBox runat="server" ID="txtcp" CssClass="form-control" Style="text-transform: uppercase"
                                                        Onkeypress="return ValCarac(event,7)" MaxLength="5" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-lg-4 col" style="display: none;">
                                    <asp:TextBox runat="server" ID="txttelfijo" CssClass="txtBBVA" MaxLength="10" Onkeypress="return ValCarac(event,7)" Onblur="valTel(this.id);" />
                                    <asp:TextBox runat="server" ID="txttelmovil" CssClass="txtBBVA" MaxLength="10" Onkeypress="return ValCarac(event,7)" Onblur="valTel(this.id);" />
                                    <asp:TextBox runat="server" ID="txtmail" CssClass="txtBBVA" onblur="validarEmail();"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
		    </div>
	    </div>
	    <div class="panel panel-default">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Agencia</a>
			    </h5>
		    </div>
		    <div id="collapse2" class="panel-collapse collapse in">
			    <div class="panel-body">
                    <div class="col-sm-12 col-md-12 col-lg-12" style="display: none;">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <label for="ContentPlaceHolder1_chkDatosAgencia" class="label-center" style="padding-top: 5%;">Datos de la Agencia</label>
                                </div>
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <div class="checkbox">
                                        <label><input type="checkbox" value="" runat="server" ID="chkDatosAgencia">s</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Estado<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddledo" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Alianza<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlalianza" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Grupo<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlgrupo" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">División<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Agencia<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlagencia" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">F&I<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlejecutivo" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Vendedor<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlvendedor" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
			    </div>
		    </div>
	    </div>
	    <div class="panel panel-default">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">Producto</a>
			    </h5>
		    </div>
		    <div id="collapse3" class="panel-collapse collapse in">
			    <div class="panel-body">	
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Marca<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlmarca" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Sub Marca<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlmodelo" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Clasificación<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlclasif" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Año<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlanio" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Versión<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlversion" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4" style="padding-left 0; padding-right: 0;">
                                    <label class="label-center">Precio de Lista<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:TextBox runat="server" ID="txtprecio" CssClass="form-control" AutoPostBack="true" Onkeypress="return checkDecimals(event,this.value,7)" MaxLength="11"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Tasa IVA<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddliva" CssClass="form-control" AutoPostBack="true" ausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Moneda Factura<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlmonedafact" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4" style="padding-left 0; padding-right: 0;">
                                    <label class="label-center">Número de Unidades<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:TextBox runat="server" ID="txtUniProd" CssClass="form-control" Text="1" ReadOnly="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
			    </div>
		    </div>
	    </div>
        <div class="panel panel-default">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse4">Financiamiento</a>
			    </h5>
		    </div>
		    <div id="collapse4" class="panel-collapse collapse in">
			    <div class="panel-body">	
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Moneda<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlmonedafin" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Plan<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlplan" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Plazos<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlplazos" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-4 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <div id="div16" runat="server">
                                        <asp:label id="lblt1" runat="server" class="label-center span-label">Tasa</asp:label>
                                    </div>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:TextBox runat="server" ID="txttasa" CssClass="form-control" Enabled="false"></asp:TextBox><asp:Label id="lbltxttasa" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-8 col-lg-8 col">
                                <div class="col-xs-4 col-sm-4 col-lg-2">
                                    <label class="label-center" style="padding-top: 8%;">Enganche<span style="color:red;">*</span></label>
                                </div>

                                    <div class="col-xs-8 col-sm-8 col-lg-3" id="div14" runat="server" style="display: none;">
                                            Enganche<span style="color: Red;">*</span>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-lg-3">
                                        <asp:TextBox runat="server" ID="txtenganche" CssClass="form-control" AutoPostBack="true" Onkeypress="return checkDecimals(event,this.value,7)"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-lg-3">
                                        <asp:TextBox runat="server" ID="txtporeng" CssClass="form-control" AutoPostBack="true" Onkeypress="return checkDecimals(event,this.value,1)"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-2 col-sm-2 col-lg-3">
                                        <div class="tooltip" id="div15" runat="server">
                                            &nbsp;Min
                                            <asp:Label runat="server" ID="lblengmin" CssClass="label"></asp:Label>%
                                        </div>
                                    </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12" Visible="false">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col" id="div36m" runat="server">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                    <asp:Label id="lblt2" CssClass="label-center span-label" runat="server" Text="Tasa"></asp:Label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:TextBox runat="server" ID="txt36m" CssClass="form-control" Enabled="false"></asp:TextBox><asp:Label ID="lbltxt36m" runat="server" ></asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col" id="tdEngancheCI" runat="server">
                                <div class="col-xs-4 col-sm-4 col-lg-4" style="padding-left: 0; padding-right: 0;">
                                    <asp:Label runat="server" ID="lblCI" CssClass="span-label">%Enganche CI</asp:Label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlengancheCI" CssClass="form-control" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
			    </div>
		    </div>
	    </div>
        <div class="panel panel-default">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse5">Financiamiento Adicional</a>
			    </h5>
		    </div>
		    <div id="collapse5" class="panel-collapse collapse">
			    <div class="panel-body">
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <asp:GridView ID="grvAccesorios" runat="server" AutoGenerateColumns="false" OnRowCreated="grvAccesorios_RowCreated" Width="99%" CssClass="resulbbva">
                            <Columns>
                                <asp:BoundField DataField="RowNumber" HeaderText="" ItemStyle-Width="10px" ControlStyle-Width="10px"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Conceptos"
                                    ItemStyle-Width="250px" ControlStyle-Width="250px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" CssClass="selectBBVA"
                                            ItemStyle-HorizontalAlign="Center">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Condición" ItemStyle-Width="250px" ControlStyle-Width="250px"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true" CssClass="selectBBVA">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio"
                                    ItemStyle-Width="90px" ControlStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,9)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="16px" ControlStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/img/cross-white.png"
                                            OnClick="ImageButton1_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12" style="margin: 5px auto 20px auto;">
                        <div class="alert alert-info">
                            Total Financiamiento Adicional: <i class="fa fa-dollar" style="font-size: 1.5em;"></i>
                            <asp:Label runat="server" ID="lbltotalacc" Text="0.00"></asp:Label>
                            <asp:Label runat="server" ID="lblAccFinan" Text="0.00" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblAccConta" Text="0.00" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="col-md-12 text-center"> 
                            <asp:Button runat="server" ID="btnagregarprod" Text="Agregar Producto" CssClass="btn btn-primary"/>
                        </div>
                    </div>
			    </div>
		    </div>
	    </div>
        <div class="panel panel-default">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse6">Financiamiento del Seguro</a>
			    </h5>
		    </div>
		    <div id="collapse6" class="panel-collapse collapse">
			    <div class="panel-body">	
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                     <label class="label-center">Estado<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddledoseg" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                     <label class="label-center">Tipo<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddltipounidad" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                     <label class="label-center">Uso<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddluso" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                     <label>Tipo de Seguro<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddltiposeg" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                     <label class="label-center">Cobertura<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlcobertura" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                     <label class="label-center">Aseguradora<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlaseguradora" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                     <label class="label-center">Vida<span style="color:red;">*</span></label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlsegvida" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                     <label>Garantía Extendida</label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="ddlGarantia" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div class="col-xs-4 col-sm-4 col-lg-4">
                                     <label style="font-size: xx-small;">Precio Garantía Extendida</label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <div id="divmtogarantia" runat="server"> 
                                        <asp:TextBox runat="server" ID="txtmtogarantia" CssClass="form-control" Onkeypress="return newcheckDecimals(event, this.value, 6, 2)" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="col-md-12 text-center"> 
                            <asp:Button runat="server" ID="btncotizaseg" Text="Cotizar Seguro" CssClass="btn btn-primary" OnClientClick="ShowProgressAnimation()" />
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12" style="margin: 5px auto 20px auto;">
                        <div class="alert alert-info">
                            <i class="fa fa-exclamation-triangle" style="font-size: 1.5em;"></i><asp:Label runat="server" ID="lblpromoleyenda"></asp:Label>
                        </div>
                    </div>
			    </div>
		    </div>
	    </div>
        <div class="panel panel-default">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse7">Información de la Cotización</a>
			    </h5>
		    </div>
		    <div id="collapse7" class="panel-collapse collapse">
			    <div class="panel-body">
                    <div class="col-sm-12 col-md-12 col-lg-12" style="height: auto;">
                        <asp:GridView runat="server" ID="grvMultiCotiza" Width="100%" CssClass="table-responsive">
                            <HeaderStyle CssClass="GridviewScrollHeaderBBVA" HorizontalAlign="center" />
                            <RowStyle CssClass="GridviewScrollItemBBVA" HorizontalAlign="Right" />
                            <PagerStyle CssClass="GridviewScrollPagerBBVA" />
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12" style="margin: 5px auto 0 auto; height: auto;">
                        <div class="alert alert-info" style="padding: 0; margin-bottom: 5px;">
                            <div class="checkbox" style="background-color: none; border:0;">
                                <label><asp:CheckBox runat="server" ID="chkPagosEsp" Checked="false" AutoPostBack="true" value="" />Anualidades</label>
                            </div>
                        </div>
                    </div>
			    </div>
		    </div>
	    </div>
        <div class="panel panel-default" style="display:none;">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse8">Colapso 1</a>
			    </h5>
		    </div>
		    <div id="collapse8" class="panel-collapse collapse">
			    <div class="panel-body">	
			    </div>
		    </div>
	    </div>
        <div class="panel panel-default" style="display:none;">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse9">Colapso 2</a>
			    </h5>
		    </div>
		    <div id="collapse9" class="panel-collapse collapse">
			    <div class="panel-body">	
			    </div>
		    </div>
	    </div>
        <div class="panel panel-default">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse10">Información del Seguro</a>
			    </h5>
		    </div>
		    <div id="collapse10" class="panel-collapse collapse">
			    <div class="panel-body">
                    <div id="finseg" runat="server" class="col-sm-12 col-md-12 col-lg-12" style="height: auto;">
                        <asp:GridView ID="grvSeguro" runat="server" AutoGenerateColumns="false" Width="100%">
                            <HeaderStyle CssClass="GridviewScrollHeaderBBVA" />
                            <RowStyle CssClass="GridviewScrollItemBBVA" />
                            <PagerStyle CssClass="GridviewScrollPagerBBVA" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sel.">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="rdbSel" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID_PAQUETE" HeaderText="PLAZO"></asp:BoundField>
                                <asp:BoundField DataField="PRIMA NETA" HeaderText="PRIMA NETA"></asp:BoundField>
                                <asp:BoundField DataField="RECARGO" HeaderText="RECARGO"></asp:BoundField>
                                <asp:BoundField DataField="DERECHO" HeaderText="DERECHO"></asp:BoundField>
                                <asp:BoundField DataField="IVA" HeaderText="IVA"></asp:BoundField>
                                <asp:BoundField DataField="PRIMA TOTAL" HeaderText="PRIMA TOTAL"></asp:BoundField>
                                <asp:BoundField DataField="ASEGURADORA" HeaderText="ASEGURADORA"></asp:BoundField>
                                <asp:BoundField DataField="PAQUETE" HeaderText="COBERTURA"></asp:BoundField>
                                <asp:BoundField DataField="USO" HeaderText="USO"></asp:BoundField>
                                <asp:TemplateField HeaderText="URL Cotizacion">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkCot" runat="server" Text='<%# Bind("URL_COTIZACION")%>' NavigateUrl='<%# Bind("URL_COTIZACION")%>'
                                            Target="_blank" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PRIMA_NETA_GAP" HeaderText="PRIMA NETA GAP"></asp:BoundField>
                                <asp:BoundField DataField="IVA_GAP" HeaderText="IVA GAP"></asp:BoundField>
                                <asp:BoundField DataField="SEGURO_GAP" HeaderText="SEGURO GAP"></asp:BoundField>
                                <asp:BoundField DataField="SEGURO_VIDA" HeaderText="SEGURO VIDA"></asp:BoundField>
                                <asp:BoundField DataField="ID_COTIZACION" HeaderText="ID_COTIZACION"></asp:BoundField>
                                <asp:BoundField DataField="OBSERVACIONES" HeaderText="OBSERVACIONES"></asp:BoundField>
                                <asp:BoundField DataField="PRIMA_TOTAL_SG" HeaderText="PRIMA_TOTAL_SG"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
			    </div>
		    </div>
	    </div>
        <div class="panel panel-default">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse11">Anualidades</a>
			    </h5>
		    </div>
		    <div id="collapse11" class="panel-collapse collapse">
			    <div class="panel-body">	
                    <div id="tbPagosEspeciales" runat="server" class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group row">
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div id="div25" runat="server" class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Periodos</label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:DropDownList runat="server" ID="cmbPeriodos"  CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                                <div id="div26" runat="server" class="col-xs-4 col-sm-4 col-lg-4">
                                    <label class="label-center">Monto</label>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <asp:TextBox runat="server" ID="txtPagEsp" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-lg-4 col">
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <asp:GridView runat="server" ID="gdvPagEsp" AutoGenerateColumns="false" Width="99%">
                            <HeaderStyle CssClass="GridviewScrollHeaderBBVA" />
                            <RowStyle CssClass="GridviewScrollItemBBVA" />
                            <PagerStyle CssClass="GridviewScrollPagerBBVA" />
                            <Columns>
                                <asp:BoundField DataField="no_pago" HeaderText="Periodo" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="5%" ControlStyle-Width="5%" />
                                <asp:BoundField DataField="exigibilidad" HeaderText="<div id='tt' class='tooltip'>Fecha<span id='tts' class='tooltiptext'>Fecha de pago en función del plan <br /> seleccionado</span></div>"
                                    ItemStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"
                                    ItemStyle-Width="40%" ControlStyle-Width="40%" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<div id='tt1' class='tooltip'>Monto<span id='tts1' class='tooltiptext'>Importe a liquidar al final de la vida <br />  del crédito</span></div>"
                                    ItemStyle-Width="45%" ControlStyle-Width="45%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Width="100px" ReadOnly="true" CssClass="txt3BBVA"
                                            Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <asp:Button runat="server" ID="btnAplicaPE" Text="Aplicar" CssClass="btn btn-primary center-block" />
                    </div>
			    </div>
		    </div>
	    </div>
        <div class="panel panel-default">
		    <div class="panel-heading">
			    <h5 class="panel-title">
				    <a data-toggle="collapse" data-parent="#accordion" href="#collapse12">Guardar Cotizacion</a>
			    </h5>
		    </div>
		    <div id="collapse12" class="panel-collapse collapse">
			    <div class="panel-body">
                    <div class="col-sm-12 col-md-12 col-lg-12" style="height: auto;">
                        <div class="col-sm-4 col-md-4 col-lg-4">
                            <asp:Button runat="server" ID="btnImprimeCot" Text="Imprimir Cotización" CssClass="btn btn-primary center-block-left" Visible="false"/>
                        </div>
                        <div class="col-sm-4 col-md-4 col-lg-4">
                            <asp:Button runat="server" ID="btnGeneraCot"  Text="Guardar Cotización"  CssClass="btn btn-primary center-block" OnClientClick="ShowProgressAnimation2()" />
                        </div>
                        <div class="col-sm-4 col-md-4 col-lg-4">
                            <asp:Button runat="server" ID="btnGeneraSol"  Text="Generar Solicitud"   CssClass="btn btn-primary center-block-right" Visible="false" />
                        </div>
                    </div>
			    </div>
		    </div>
	    </div>
    </div> 






    <asp:HiddenField ID="div_position" runat="server" Value="0" />
    <asp:HiddenField ID="div_position2" runat="server" Value="0" />
<div style="display:none;">
    <div style="position: absolute; width: 785px; height: 500px; left: 410px; top: 1000px; overflow: auto;" id="divfinadic" runat="server">
        <fieldset style="padding-top: 10px; padding-bottom: 20px;">
                <table class="resulbbva" style="display: none;">
                    <tr>
                        <td id="tdmontosub" runat="server">
                            Monto Subsidio<span style="color: Red;">*</span>:
                        </td>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="txtmtosub" Width="65px" Height="12px" CssClass="txt2BBVA" MaxLength="11"
                                Onkeypress="return checkDecimals(event,this.value,7)" AutoPostBack="true"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtporsub" Width="35px" Height="12px" CssClass="txt2BBVA"
                                Onkeypress="return checkDecimals(event,this.value,1)" AutoPostBack="true"></asp:TextBox>
                            <asp:Label ID="lblptjsub" runat="server">&nbsp%</asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td id="tdlblresidual" runat="server" style="text-align: right;">
                            <div class="tooltip" id="div17" runat="server">
                                Valor Residual<span style="color: Red;">*</span>: <span class="tooltiptext">Importe
                                    a liquidar al final de la vida del crédito</span>
                            </div>
                        </td>
                        <td id="tdtxtresidual" runat="server">
                            <asp:TextBox runat="server" ID="txtresidual" Width="65px" Height="12px" CssClass="txt2BBVA"
                                Enabled="false"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtporresidual" Width="35px" Height="12px" CssClass="txt2BBVA"
                                Enabled="false"></asp:TextBox>
                            &nbsp;%
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>








            <fieldset class="fieldsetBBVA" style="display: none;">
                <legend>Financiamiento del Seguro</legend>

                <%---- RQ-PI7-PC3: CGARCIA: 09/10/2017: CREACION DE NUEVA ALIANZA KIA PARA AGENCIA KIA, INDIAN, POLARIAS Y DEDUCIBLES--%>
                <div id="divDeducible" runat="server" visible="true">
                    <fieldset class="fieldsetBBVA">
                        <legend>Deducible</legend>
                        <table class="resulbbva" style="width: 100%;" id="tblDeducible" runat="server" visible="true">
                            <tr align="left">
                                <td>Deducible Daños Materiales<span style="color: Red;">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbDedusibleDaños" runat="server" CssClass="select2BBVA">
                                    </asp:DropDownList>
                                    
                                </td>                             
                                <td>Deducible Robo Total<span style="color:red;">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbDeducibleRoboTotal" runat="server" CssClass="select2BBVA">
                                    </asp:DropDownList>
                                </td>
                                <td>Indemnización <span style="color:red;">*</span>
                                </td>
                                <td>
                                <asp:DropDownList ID="cmbIndemnizacion" runat="server" CssClass="select2BBVA">                                
                                </asp:DropDownList>
                                </td>
                            </tr>                         
                        </table>
                    </fieldset>
                </div>


                <center>
                    <table class="resulbbva" style="width: 100%; padding-top: 5px;">
                    <tr id="dvrecurrecte" runat="server">
                            <td style="width: 85px;">
                            <div class="tooltip" id="div21" runat="server">
                                Edad<span style="color: Red;">*</span>: <span class="tooltiptext">Edad del conductor
                                    habitual del vehículo</span>
                            </div>
                        </td>
                            <td style="width: 30px;">
                            <asp:TextBox runat="server" ID="txtedad" CssClass="txt3BBVA" Width="20px" Height="16px"
                                onblur="valedad();"></asp:TextBox>
                        </td>
                            <td style="text-align: right; width: 50px;">
                            <div class="tooltip" id="div22" runat="server">
                                Género<span style="color: Red;">*</span>: <span class="tooltiptext">Género del conductor
                                    habitual del vehículo</span>
                            </div>
                        </td>
                            <td style="width: 70px;">
                                <asp:RadioButton runat="server" ID="rdbsexh" GroupName="rdbtnsexo" Text="H" />
                                <asp:RadioButton runat="server" ID="rdbsexm" GroupName="rdbtnsexo" Text="M" />
                        </td>
                            <td style="text-align: right; width: 80px;">
                                <div class="tooltip" id="div27" runat="server">
                                    Código Postal<span style="color: Red;">*</span>: <span class="tooltiptext">99999</span>
                                </div>
                            </td>
                            <td style="width: 30px;">
                                <asp:TextBox runat="server" ID="txtcprecurrente" CssClass="txtBBVA" MaxLength="5"
                                    Onkeypress="return ValCarac(event,7)" onblur="valcp(this.id);" Width="35px"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 50px;">
                            <div class="tooltip" id="div28" runat="server">
                            Nombre<span style="color: Red;">*</span>:
                            </div>
                        </td>
                        <td>
                                <%--BUG-PC-68:MPUESTO:26/05/2017:Correccion del campo Nombre de Conductor recurrente para no permitir números, simbolos y letras acentuadas--%>
                                <%--<asp:TextBox runat="server" ID="txtconductor" CssClass="txt3BBVA" Width="250px" Style="text-transform: uppercase"></asp:TextBox>--%>
                                <asp:TextBox runat="server" ID="txtconductor" CssClass="txt3BBVA" Width="250px" Onkeypress="return ValCarac(event,18)"
                                    Onblur="valnombre(this.id,0);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"
                                    AutoPostBack="true"></asp:TextBox>

                        </td>
                    </tr>
                </table>
                </center>
            </fieldset>
            


        </fieldset>
    </div>
</div>


    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <div id="loading-div-background" style="background:rgba(0,0,0,0.5);">
        <div id="loading-div" class="ui-corner-all" style="background:rgba(0,0,0, 0);">
            <i class="fa fa-gear fa-spin" style="font-size:100px; color: black;"></i>
            <p class="p" style="color: white;">
                Estableciendo conexión con el Broker. Espere por favor...</p>
        </div>
    </div>
    <div id="loading-div-background2" style="background:rgba(0,0,0,0.5);">
        <div id="loading-div2" class="ui-corner-all" style="background:rgba(0,0,0, 0);">
            <i class="fa fa-gear fa-spin" style="font-size:100px; color: black;"></i>
            <p class="p" style="color: white;">
                Guardando información. Espere por favor...</p>
        </div>
    </div>
</asp:Content>

