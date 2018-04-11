<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false"
    CodeFile="Cotizador.aspx.vb" Inherits="aspx_Cotizador" %>

<%--BBVA-P-412: AMR: RQ02,RQ03,RQ04,RQ05,RQ06,RQ08--%>
<%--BBVA-P-412: 22/07/2016: AMR: RQ18 Cambio por Agregar Version y Año del Auto.--%>
<%--/*BBVA-P-412: 25-08-2016: AMR: RQCOT-01 Ajustes a la pantalla de Cotización.*/--%>
<%--BBVA-P-412 RQ06: AMR: 20/09/2016 Administración de Planes de Financiamiento: Promociones--%>
<%--BBV-P-412  RQ-E  gvargas   12/10/2016 Actualizacion de referencias. --%>
<%--BBV-P-412 RQ WSC: AVH: 27/10/2016 SUDOKU--%>
<%--  BBV-P-412  RQ F    gvargas   07/11/2016 Cambios CSS dinamico. --%>
<%--  BBV-P-412  BUG-PC-05 MARREDONDO 11/11/2016 Se permite ocultar la sección de agencia--%>
<%--BUG-PC-20: AVH: 30/11/2016 SE AGREGAN tooltips--%>
<%--BBV-P-412:BUG-PC-23 JRHM: 29/11/2016 Se quita placeholder de campo de correo electronico--%>
<%--BUG-PC-24 MAUT 15/12/2016 Se avisa que el campo es obligatorio--%>
<%--BUG-PC-25 MAUT 15/12/2016 Se manda campo segundo nombre opcional--%>
<%--BUG-PC-26: JRHM: 20/12/2016 Se modifico funcion de checkbox de agencia para no perder valor --%>
<%--BUG-PC-27 MAUT 23/12/2016 Se agregan labels no visibles para montos de accesorios--%>
<%--BUG-PC-30 AMR 03/01/2017: Al ingresar al portal procotiza e ingresar a la opcion de cotizacion manda un mensaje de warning.--%>
<%--BUG-PC-32 GVARGAS 06/01/2017 Tool Tips--%>
<%--BUG-PC-33: JRHM: 10/01/2017 Se Agregan tooltips de anualidades--%>
<%--BUG-PC-34 MAUT 16/01/2017 Se corrigen tooltips y se cambia focus de RFC--%>
<%--BUG-PC-38 MAUT 23/01/2017 Se quita el calculo automatico del RFC--%>
<%--BUG-PC-44 07/02/17 JRHM SE AGREGAN CAMBIOS PARA LA PRESENTACION DE LOS CCUADROS DE TEXTO DE VALOR RESIDUAL(TIPO CALCULO BALLON) Y PORCENTAJE SUBSIDIO--%>
<%--BUG-PC-46 JRHM 09/02/17 Se rehabilita autopostback de fecha de nacimiento para validacion de fecha Y SE QUITAN EVENTOS SCROLL() DE ONCHANGE DE DDLS--%>
<%--BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.--%>
<%--BUG-PC-48 JRHM 15/02/17 SE QUITA UN ALERT EN LA VALIDACION DE AGENCIA DEBIDO A QUE ENVIABA UN DOBLE MENSAJE--%>
<%--BUG-PC-49 MAPH 21/03/17 Cambios en interfaz solicitados por Amado Mata--%>
<%--BUG-PC-50 JRHM 11/04/17 SE MODIFICA VALIDACION DE RFC y SE CAMBIO SEXO POR GÉNERO EN DATOS DE CONDUCTOR RECURRENTE --%>
<%--BUG-PC-57 JRHM 03/05/17 Se cambia valcarac de txtnombrecte --%>
<%--BUG-PC-58:AMATA:03/05/2017:Seguros Ordas--%>
<%--BUG-PC-68:MPUESTO:26/05/2017:Correccion del campo Nombre de Conductor recurrente para no permitir números, simbolos y letras acentuadas--%>
<%--RQ-IN2B17: ERODRIGUEZ: 13/07/2017: SE AGREGO CANDADO PARA NO PERMITIR GARANTIA EXTENDIDA CUANDO LA CLASIFICACION DEL PRODUCTO SEA SEMINUEVO--%>
<%--RQ-MN2-2 ERODRIGUEZ 14/09/2017 Se agregaron tasas para compra inteligente. --%>
<%--RQ-MN2-4 : CGARCIA: 11/09/2017 :  SE AGREGO LA PARTE DE GUARDAR EL DEDUCIBLE SOLO PARA SEGUROS BANCOMER BROKER 5 Y 9 --%>
<%--'RQ-MN2-4.2 : CGARCIA: 28/09/2017 :  SE ELIMINO EL TIPO DE DEDUCIBLE  --%>
<%---- RQ-PI7-PC3: CGARCIA: 09/10/2017: CREACION DE NUEVA ALIANZA KIA PARA AGENCIA KIA, INDIAN, POLARIAS Y DEDUCIBLES--%>
<%--RQ-PI7-PC1:RHERNANDEZ: 12/10/17: Se modifica pantalla cotizador para administrar la nueva cascada de submarca>clasificacion>anio>version--%>
<%-- BUG-PC-125  RIGLESIAS: 14/11/2017: Se agrego nuevo combolist para indemnización  --%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>
<%--'BUG-PC-154: CGARCIA: 19/02/2018: se actualiza evento de coberturas--%>
   <%--BUG-PC-172: DJUAREZ: 02/04/2018: Correccion de blur en codigo postal para evitar que salgan muchos alerts con el valor vacio --%>
<%--BUG-PC-173: NUEVAS ALIANZAS Y MEJORAS DE SEGURO A TU MEDIDA--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                $('#<%=grvMultiCotiza.ClientID%>').gridviewScroll({
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
            if (cptxt.value != "") {
            alert("El Código Postal debe contener 5 caracteres.");
                cptxt.value = "";
            cptxt.focus();
            }
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
        $.datepicker.setDefaults($.datepicker.regional["es"]);
        $("[id$=txtfecnac]").datepicker({
            showOn: "button",
            buttonImage: "../img/calendar.png",
            buttonImageOnly: true,
            buttonText: "Select fecha",
            dateFormat: "dd-mm-yy",
            showAnim: "slide",
            changeMonth: true,
            changeYear: true,
            showOtherMonths: true,
            selectOtherMonths: true,
            autoSize: true,
            yearRange: "-100:-18",
            maxDate: '-18Y',

        });
    });

    $(function () {
        var div = document.getElementById("dvScroll");
        var position = 0
        div.onscroll = function () {
            position = div.scrollTop;
            document.getElementById("ContentPlaceHolder1_div_position").value = position;
        };
    });

    window.onload = function () {
        var pos = document.getElementById("ContentPlaceHolder1_div_position").value;
        var div = document.getElementById("dvScroll");
        div.scrollTop = pos;
    };

    $(function () {
        var div = document.getElementById("ContentPlaceHolder1_divfinadic");
        var position = 0

        if (div != null) {
            div.onscroll = function () {
                position = div.scrollTop;
                document.getElementById("ContentPlaceHolder1_div_position2").value = position;
            };
        }
    });

    window.onload = function () {
        var pos = document.getElementById("ContentPlaceHolder1_div_position").value;
        var div = document.getElementById("dvScroll");
        div.scrollTop = pos;

        var pos2 = document.getElementById("ContentPlaceHolder1_div_position2").value;
        var div2 = document.getElementById("ContentPlaceHolder1_divfinadic");
        if (div2 != null) {
            div2.scrollTop = pos2;
        }
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="div_position" runat="server" Value="0" />
    <asp:HiddenField ID="div_position2" runat="server" Value="0" />
    <div class="messageCot" style="position: absolute; width: 1185px; height: 45px; left: 5px;
        top: 0px; background-color: #F0F9FE; padding-top: 5px; padding-left: 15px;">
        <table>
            <tr>
                <td>
                    <img src="../img/Alert.png" alt="alert" />
                </td>
                <td>
                    <p class="CotizadotTagP" style="color: Gray; font-family: Arial; font-size: 11px;">
                        Para guardar la información es necesario ingresar todos los datos marcados con asterisco
                        <span style="color: Red;">*</span>.</p>
                </td>
            </tr>
        </table>
    </div>
    <div style="position: absolute; width: 400px; height: 500px; left: 5px; top: 55px;
        overflow: auto;" id="dvScroll">
        <fieldset style="padding: 10px;">
            <fieldset class="fieldsetBBVA">
                <legend>Cliente</legend>
                <table class="resulbbva">
                    <tr>
                        <td>
                            Tipo de Operación<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddltoperacion" CssClass="selectBBVA" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Personalidad Juridica<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlpj" CssClass="selectBBVA" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            Giro<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlgiro" CssClass="selectBBVA" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Datos del Cliente
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkdatoscte" />
                        </td>
                    </tr>
                </table>
                <%--Datos Cliente--%>
                <div id="divcte">
                    <table class="resulbbva">
                        <tr>
                            <td>
                                <div class="tooltip" id="divrelPaqAseg" runat="server">
                                    <asp:Label runat="server" ID="lblnomcte" Text="Nombre"></asp:Label><span style="color: Red;">*</span>:
                                    <span class="tooltiptext" id="Toolnommorfis" runat="server">Nombre tal y como aparece
                                        en la identificación oficial</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtnombrecte" CssClass="txtBBVA" Onkeypress="return ValCarac(event,18)"
                                    Onblur="valnombre(this.id,0);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtnombrectepf" CssClass="txtBBVA" Onkeypress="return ValCarac(event,18)"
                                    Onblur="valnombre(this.id,0);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="trnom2">
                            <td>
                                <div class="tooltip" id="div1" runat="server">
                                    2° Nombre: <span class="tooltiptext">2do nombre(s) tal y como aparece en la identificación
                                        oficial</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtnombrecte2" CssClass="txtBBVA" Onkeypress="return ValCarac(event,18)"
                                    onkeyup="ReemplazaAcentos(event, this.id, this.value);" Onblur="valnombre(this.id,1);"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="trpaterno">
                            <td>
                                <div class="tooltip" id="div2" runat="server">
                                    Paterno<span style="color: Red;">*</span>: <span class="tooltiptext">Apellido paterno
                                        tal y como aparece en la identificación oficial</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtpaterno" CssClass="txtBBVA" Onkeypress="return ValCarac(event,18)"
                                    onkeyup="ReemplazaAcentos(event, this.id, this.value);" Onblur="valapellidos(this.id,0);"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="trmaterno">
                            <td>
                                <div class="tooltip" id="div3" runat="server">
                                    Materno: <span class="tooltiptext">Apellido materno tal y como aparece en la identificación
                                        oficial</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtmaterno" CssClass="txtBBVA" Onkeypress="return ValCarac(event,18)"
                                    onkeyup="ReemplazaAcentos(event, this.id, this.value);" Onblur="valapellidos(this.id,1);"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="trnac">
                            <td>
                                <div class="tooltip" id="div4" runat="server">
                                    Fecha de Nacimiento<span style="color: Red;">*</span>:<span class="tooltiptext">DD/MM/AAAA</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtfecnac" CssClass="txtBBVA" Width="170px" Style="text-transform: uppercase"
                                    Onkeypress="return ValCarac(event,13)" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <%--BUG-PC-38 MAUT 23/01/2017 Se quita el AutoPostBack--%>
                        </tr>
                        <tr>
                            <td>
                                <div class="tooltip" id="div5" runat="server">
                                    RFC<span style="color: Red;">*</span>: <span class="tooltiptext" id="tooltiprfc"
                                        runat="server">Persona física: AAAA999999XXX, Persona moral: AAA999999XXX</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtrfc" CssClass="txtBBVA" Style="text-transform: uppercase"
                                    Onkeypress="return ValCarac(event,14)" MaxLength="13" onblur="validarRFC();"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="tooltip" id="div6" runat="server">
                                    Código Postal<span style="color: Red;">*</span>: <span class="tooltiptext">99999</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtcp" CssClass="txtBBVA" Style="text-transform: uppercase"
                                    Onkeypress="return ValCarac(event,7)" MaxLength="5" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <%--onblur="valcp();"--%>
                        </tr>
                        <tr style="display: none">
                            <td>
                                <div class="tooltip" id="div7" runat="server">
                                    Teléfono Casa/Oficina: <span class="tooltiptext">10 dígitos (Lada +teléfono)</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txttelfijo" CssClass="txtBBVA" MaxLength="10" Onkeypress="return ValCarac(event,7)"
                                    Onblur="valTel(this.id);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>
                                <div class="tooltip" id="div8" runat="server">
                                    Tel. Movil: <span class="tooltiptext">10 dígitos</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txttelmovil" CssClass="txtBBVA" MaxLength="10" Onkeypress="return ValCarac(event,7)"
                                    Onblur="valTel(this.id);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>
                                <div class="tooltip" id="div9" runat="server">
                                    e-mail</span>: <span class="tooltiptext">Ingrese un correo válido e indispensable</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtmail" CssClass="txtBBVA" onblur="validarEmail();"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <br />
            <fieldset class="fieldsetBBVA">
                <legend>Agencia</legend>
                <table class="resulbbva">
                    <tr>
                        <td style="width: 48px;">
                            Datos de la Agencia
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkDatosAgencia" />
                        </td>
                    </tr>
                </table>
                <div id="divAgencia">
                    <table class="resulbbva">
                        <tr>
                            <td>
                                Estado<span style="color: Red;">*</span>:
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddledo" CssClass="selectBBVA" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Alianza<span style="color: Red;">*</span>:
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlalianza" CssClass="selectBBVA" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Grupo<span style="color: Red;">*</span>:
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlgrupo" CssClass="selectBBVA" AutoPostBack="true"
                                    CausesValidation="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                División<span style="color: Red;">*</span>:
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddldivision" CssClass="selectBBVA" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Agencia<span style="color: Red;">*</span>:
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlagencia" CssClass="selectBBVA" AutoPostBack="true"
                                    CausesValidation="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                F&I<span style="color: Red;">*</span>:
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlejecutivo" CssClass="selectBBVA" AutoPostBack="true"
                                    CausesValidation="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Vendedor<span style="color: Red;">*</span>:
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlvendedor" CssClass="selectBBVA" AutoPostBack="true"
                                    CausesValidation="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </td>
                    </table>
                </div>
            </fieldset>
            <br />
            <fieldset class="fieldsetBBVA">
                <legend>Producto</legend>
                <table class="resulbbva">
                    <tr>
                        <td>
                            Marca<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlmarca" CssClass="selectBBVA" AutoPostBack="true"
                                CausesValidation="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sub Marca<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlmodelo" CssClass="selectBBVA" AutoPostBack="true"
                                CausesValidation="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Clasificación<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlclasif" CssClass="selectBBVA" AutoPostBack="true"
                                CausesValidation="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Año<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlanio" CssClass="selectBBVA" AutoPostBack="true"
                                CausesValidation="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="tooltip" id="div10" runat="server">
                                Versión<span style="color: Red;">*</span>: <span class="tooltiptext">Descripción larga
                                    del auto</span>
                            </div>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlversion" CssClass="selectBBVA" AutoPostBack="true"
                                CausesValidation="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <div class="tooltip" id="div11" runat="server">
                                Precio de Lista<span style="color: Red;">*</span>: <span class="tooltiptext">El precio
                                    mostrado es el sugerido</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtprecio" CssClass="txtBBVA" AutoPostBack="true"
                                Onkeypress="return checkDecimals(event,this.value,7)" MaxLength="11"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tasa IVA<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddliva" CssClass="selectBBVA" AutoPostBack="true"
                                CausesValidation="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Moneda Factura<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlmonedafact" CssClass="selectBBVA" AutoPostBack="true"
                                CausesValidation="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Número de Unidades<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUniProd" CssClass="txt2BBVA" Width="60px" Text="1"
                                ReadOnly="true" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <%--            <fieldset class="fieldsetBBVA">
            <legend>Financiamiento</legend>
            <table class="resulbbva">
                <tr>
                    <td>Moneda<span style="color:Red;">*</span>: </td>
                    <td><asp:DropDownList runat="server" ID="ddlmonedafin" CssClass="selectBBVA" AutoPostBack="true" CausesValidation="false"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Plan<span style="color:Red;">*</span>: </td>
                    <td><asp:DropDownList runat="server" ID="ddlplan" CssClass="selectBBVA" AutoPostBack="true" CausesValidation="false"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Plazos<span style="color:Red;">*</span>:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlplazos" Cssclass="select3BBVA" AutoPostBack="true"></asp:DropDownList>
                    </td>                
                </tr>
                <tr>
                    <td>Enganche<span style="color:Red;">*</span>: </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtenganche" Width="65px" Height="12px" CssClass="txt2BBVA" AutoPostBack="true" Onkeypress="return ValCarac(event,9)"></asp:TextBox>
                        &nbsp;%
                        <asp:TextBox runat="server" ID="txtporeng" Width="35px" Height="12px" CssClass="txt2BBVA" AutoPostBack="true" Onkeypress="return ValCarac(event,9)"></asp:TextBox>
                        &nbsp;Min
                        <asp:Label runat="server" ID="lblengmin" CssClass="label"></asp:Label>
                        %
                    </td>
                </tr>
                <tr>
                    <td>Tasa<span style="color:Red;">*</span>: </td>
                    <td><asp:TextBox runat="server" ID="txttasa" Width="65px" Height="12px" CssClass="txt2BBVA" Enabled="false"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>Valor Residual<span style="color:Red;">*</span>: </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtresidual" Width="65px" Height="12px" CssClass="txt2BBVA" Enabled = "false"></asp:TextBox>
                        &nbsp;%
                        <asp:TextBox runat="server" ID="txtporresidual" Width="35px" Height="12px" CssClass="txt2BBVA" Enabled = "false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Monto Subsidio<span style="color:Red;">*</span>: </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtmtosub" Width="65px" Height="12px" CssClass="txt2BBVA"></asp:TextBox>
                        &nbsp;%
                        <asp:TextBox runat="server" ID="txtporsub" Width="35px" Height="12px" CssClass="txt2BBVA"></asp:TextBox>
                    </td>
                </tr>
                </table>
            </fieldset>--%>
        </fieldset>
    </div>
    <div style="position: absolute; width: 785px; height: 500px; left: 410px; top: 55px;
        overflow: auto;" id="divfinadic" runat="server">
        <fieldset style="padding-top: 10px; padding-bottom: 20px;">
            <fieldset class="fieldsetBBVA">
                <legend>Financiamiento</legend>
                <table class="resulbbva">
                    <tr>
                        <td>
                            Moneda<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" Style="width: 100px;" ID="ddlmonedafin" CssClass="selectBBVA"
                                AutoPostBack="true" CausesValidation="false">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: right;">
                            <div class="tooltip" id="div12" runat="server">
                                Plan<span style="color: Red;">*</span>: <span class="tooltiptext">Mejor oferta sugerida</span>
                            </div>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlplan" CssClass="selectBBVA" AutoPostBack="true"
                                CausesValidation="false">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                            <div class="tooltip" id="div13" runat="server">
                                Plazos<span style="color: Red;">*</span>: <span class="tooltiptext">Plazos disponibles
                                    para el plan</span>
                            </div>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" Style="width: 100px;" ID="ddlplazos" CssClass="select3BBVA"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="tooltip" id="div16" runat="server">
                                <asp:Label id="lblt1" font-size="9px" runat="server" Text="Tasa" ToolTip="Tasa en función del plan seleccionado"></asp:Label>
                                <%--Tasa<span style="color: Red;">*</span>: <span class="tooltiptext">Tasa en función del
                                    plan seleccionado</span>--%>
                            </div>
                            <div class="tooltip" id="div36m" runat="server" Visible="false">
                                 <asp:Label id="lblt2" font-size="9px" runat="server" Text="Tasa" ToolTip="Tasa Compra Inteligente"></asp:Label>
                                <%--Tasa<span style="color: Red; width:80px;">*</span>: <span class="tooltiptext">Tasa Compra Inteligente</span>--%>
                            </div>
                          
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txttasa" Width="65px" Height="12px" CssClass="txt2BBVA"
                                Enabled="false"></asp:TextBox><asp:Label id="lbltxttasa" runat="server"></asp:Label>
                             <asp:TextBox runat="server" ID="txt36m" Width="65px" Height="12px" CssClass="txt2BBVA"
                                Enabled="false"></asp:TextBox><asp:Label ID="lbltxt36m" runat="server" ></asp:Label>
                             
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: right;">
                            <div class="tooltip" id="div14" runat="server">
                                Enganche<span style="color: Red;">*</span>: <span class="tooltiptext">Enganche en función
                                    del plan seleccionado</span>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtenganche" Width="65px" Height="12px" CssClass="txt2BBVA"
                                AutoPostBack="true" Onkeypress="return checkDecimals(event,this.value,7)"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtporeng" Width="35px" Height="12px" CssClass="txt2BBVA"
                                AutoPostBack="true" Onkeypress="return checkDecimals(event,this.value,1)"></asp:TextBox>
                            <%--BUG-PC-34 MAUT 13/01/2017 se cambia div--%>
                            <div class="tooltip" id="div15" runat="server">
                                &nbsp;Min
                                <asp:Label runat="server" ID="lblengmin" CssClass="label"></asp:Label>
                                %<span class="tooltiptext">% de enganche en función del plan seleccionado</span>
                            </div>
                        </td>
                        <td style="text-align: right;" id="tdEngancheCI" runat="server">
                            <asp:Label runat="server" ID="lblCI">% Enganche CI: </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlengancheCI" CssClass="selectBBVA" AutoPostBack="true"
                                Style="width: 100px;" CausesValidation="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
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
            </fieldset>
            <fieldset class="fieldsetBBVA">
                <legend>
                    <div class="tooltip" id="div23" runat="server">
                        Financiamiento Adicional
                        <%--BUG-PC-34 MAUT 16/01/2017 Se quita tooltip--%>
                        <%--<span class="tooltiptext">Seleccionar los aditamentos opcionales</span>--%>
                    </div>
                </legend>
                <div style="padding-top: 0px;">
                    <%--                    <div>
                    <table style="width:100%;" class="resulbbva">
                        <tr>
                            <td style="background-color:White; width:135px;">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="background-color:White; padding:0px; width:30px;">Agregar:</td>
                            <td><asp:ImageButton runat="server" ID="cmdAgregaAcc" ImageUrl="~/img/add.png"/> </td>
                        </tr>
                    </table>
                    </div>--%>
                    <%--BUG-PC-34 MAUT 16/01/2017 Se agrega tooltip--%>
                    <asp:GridView ID="grvAccesorios" runat="server" AutoGenerateColumns="false" OnRowCreated="grvAccesorios_RowCreated"
                        Width="99%" CssClass="resulbbva">
                        <%--                    <HeaderStyle CssClass="GridviewScrollHeader" /> 
                    <RowStyle CssClass="GridviewScrollItem" /> 
                    <PagerStyle CssClass="GridviewScrollPager" />--%>
                        <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="" ItemStyle-Width="10px" ControlStyle-Width="10px"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="<div class='tooltip'>Conceptos<span class='tooltiptext'>Seleccionar los aditamentos opcionales</span></div>"
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
                            <asp:TemplateField HeaderText="<div class='tooltip'>Precio<span class='tooltiptext'>Monto de los aditamentos</span></div>"
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
                <table class="resulbarra" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="messageCot">
                            <div class="tooltip" id="div24" runat="server">
                                Total Financiamiento Adicional: <span class="tooltiptext">Importe total de los aditamentos
                                    opcionales</span>
                            </div>
                        </td>
                        <td class="messageCot">
                            &nbsp;&nbsp;&nbsp;$<asp:Label runat="server" ID="lbltotalacc" Text="0.00" Font-Size="12px"></asp:Label>
                            <%--BUG-PC-27 MAUT 23/12/2016 Se agregan labels no visibles para montos de accesorios--%>
                            <asp:Label runat="server" ID="lblAccFinan" Text="0.00" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblAccConta" Text="0.00" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
                <center>
                    <table>
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnagregarprod" Text="Agregar Producto" CssClass="buttonSecBBVA2" />
                            </td>
                        </tr>
                    </table>
                </center>
            </fieldset>
            <br />
            <fieldset class="fieldsetBBVA">
                <legend>Financiamiento del Seguro</legend>
                <table class="resulbbva" style="width: 100%;">
                    <tr>
                        <td>
                            <div class="tooltip" id="div18" runat="server">
                                Estado<span style="color: Red;">*</span>: <span class="tooltiptext">Entidad federativa
                                    donde circulará el vehículo</span>
                            </div>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddledoseg" CssClass="select2BBVA">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            Tipo<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddltipounidad" CssClass="select2BBVA">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            <div class="tooltip" id="div20" runat="server">
                                Uso<span style="color: Red;">*</span>: <span class="tooltiptext">No aplica para taxis,
                                    Uber o similares</span>
                            </div>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddluso" CssClass="select2BBVA">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="tooltip" id="div19" runat="server">
                                Tipo de Seguro<span style="color: Red;">*</span>: <span class="tooltiptext">Esquema
                                    de pago del seguro</span>
                            </div>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddltiposeg" CssClass="select2BBVA" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            Cobertura<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlcobertura" CssClass="select2BBVA" OnSelectedIndexChanged="ddlcobertura_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            Aseguradora<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlaseguradora" CssClass="select2BBVA" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Vida<span style="color: Red;">*</span>:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlsegvida" CssClass="select2BBVA" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            <div class="tooltip">
                                Garantía Extendida: <span class="tooltiptext">Años que se extenderá la póliza de garantía.</span>
                            </div>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlGarantia" CssClass="select2BBVA" AutoPostBack="true">
                                <%--                                <asp:ListItem Text="< SELECCIONAR > " Value="0"></asp:ListItem>
                                <asp:ListItem Text="1 AÑO" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2 AÑOS" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3 AÑOS" Value="3"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            <div class="tooltip">
                                Precio Garantía Extendida: <span class="tooltiptext">Importe a financiar por concepto
                                    de garantía extendida.</span>
                            </div>
                        </td>
                        <td>
                            <div id="divmtogarantia" runat="server">
                                <asp:TextBox runat="server" ID="txtmtogarantia" CssClass="txt2BBVA" Onkeypress="return newcheckDecimals(event, this.value, 6, 2)"
                                    AutoPostBack="true"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                </table>
                <%---- RQ-PI7-PC3: CGARCIA: 09/10/2017: CREACION DE NUEVA ALIANZA KIA PARA AGENCIA KIA, INDIAN, POLARIAS Y DEDUCIBLES--%>
                <%--BUG-PC-173: NUEVAS ALIANZAS Y MEJORAS DE SEGURO A TU MEDIDA--%>
                <div id="divDeducible" runat="server" >
                    <fieldset class="fieldsetBBVA">
                        <legend>Personaliza tu Seguro de daños</legend>
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
                    <table>
                        <tr>
                            <td style="padding-bottom: 5px; background-color: White;">
                                <asp:Button runat="server" ID="btncotizaseg" Text="Cotizar Seguro" CssClass="buttonBBVA2"
                                    OnClientClick="ShowProgressAnimation()" />
                            </td>
                        </tr>
                    </table>
                </center>
                <div class="messageCot" style="width: 97%; height: 45px; background-color: #F0F9FE;
                    padding-top: 5px; padding-left: 15px;">
                    <table>
                        <tr>
                            <td>
                                <img src="../img/Alert.png" alt="alert" />
                            </td>
                            <td class="CotizadotTagP" style="color: Gray; font-family: Arial; font-size: 10px;">
                                <asp:Label runat="server" ID="lblpromoleyenda"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <br />
            <div id="finseg" runat="server">
                <fieldset class="fieldsetBBVA" style="height: 200px;">
                    <legend>Información del Seguro</legend>
                    <table>
                        <tr>
                            <td>
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
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <fieldset class="fieldsetBBVA" style="height: 330px;">
                <legend>Información de la Cotización</legend>
                <div style="padding-top: 10px; padding-bottom: 5px">
                    <asp:GridView runat="server" ID="grvMultiCotiza" Width="99%">
                        <HeaderStyle CssClass="GridviewScrollHeaderBBVA" HorizontalAlign="center" />
                        <RowStyle CssClass="GridviewScrollItemBBVA" HorizontalAlign="Right" />
                        <PagerStyle CssClass="GridviewScrollPagerBBVA" />
                    </asp:GridView>
                </div>
                <table class="resulbarra">
                    <tr>
                        <%--<td style="background-color:#EFEFEF;">&nbsp;&nbsp;&nbsp;Pagos Especiales: </td>--%>
                        <td class="messageCot" style="text-align: center;">
                            <asp:CheckBox runat="server" ID="chkPagosEsp" Checked="false" AutoPostBack="true"
                                Text="Anualidades" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <div id="tbPagosEspeciales" runat="server">
                <fieldset class="fieldsetBBVA" style="height: 250px;">
                    <legend>Anualidades</legend>
                    <table class="resulbbva" style="width: 100%;">
                        <tr>
                            <td style="padding-left: 70px;">
                                <div class="tooltip" id="div25" runat="server">
                                    Periodos <span class="tooltiptext">Seleccionar el mes de pago para Compra Inteligente
                                        y para Crédito Tradicional</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="cmbPeriodos" Width="145px" CssClass="select3BBVA"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <div class="tooltip" id="div26" runat="server">
                                    Monto <span class="tooltiptext">Monto en función del plan seleccionado</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtPagEsp" Width="90px" CssClass="txt3BBVA" AutoPostBack="true"
                                    Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
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
                    <br />
                    <div>
                        <center>
                            <asp:Button runat="server" ID="btnAplicaPE" Text="Aplicar" CssClass="buttonBBVA2" />
                        </center>
                    </div>
                </fieldset>
            </div>
            <br />
            <fieldset class="fieldsetBBVA" style="padding-bottom: 10px;">
                <center>
                    <table style="width:100%;">
                        <tr>
                            <td align="center">
                                <%--BUG-PC-49 21/03/17 Cambios en interfaz solicitados por Amado Mata--%>
                                <asp:Button runat="server" ID="btnGeneraCot" Text="Guardar Cotización" CssClass="buttonBBVA2"
                                    OnClientClick="ShowProgressAnimation2()" />
                                <asp:Button runat="server" ID="btnImprimeCot" Text="Imprimir Cotización" Visible="false"
                                    CssClass="buttonSecBBVA2" />
                                <asp:Button runat="server" ID="btnGeneraSol" Text="Generar Solicitud" CssClass="button2"
                                    Visible="false" />
                            </td>
                        </tr>
                    </table>
                </center>
            </fieldset>
        </fieldset>
    </div>
    <br />
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    <div id="loading-div-background">
        <div id="loading-div" class="ui-corner-all">
            <img style="height: 32px; width: 32px; margin: 30px;" src="../img/ajax-loader2.gif"
                alt="Loading.." />
            <p class="p">
                Estableciendo conexión con el Broker. Espere por favor...</p>
        </div>
    </div>
    <div id="loading-div-background2">
        <div id="loading-div2" class="ui-corner-all">
            <img style="height: 32px; width: 32px; margin: 30px;" src="../img/ajax-loader2.gif"
                alt="Loading.." />
            <p class="p">
                Guardando información. Espere por favor...</p>
        </div>
    </div>
</asp:Content>
