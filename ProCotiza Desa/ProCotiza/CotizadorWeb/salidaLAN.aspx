<%@ Page Language="VB" AutoEventWireup="false" CodeFile="salidaLAN.aspx.vb" Inherits="salida" %>
<%-- BUG-PC-36: GVARGAS: 19/01/2017 Mensaje de LogOUT usuarios internos.--%>

<!DOCTYPE HTML>

<html>
<head runat="server">
    <title></title>
        <script type="text/javascript" src="jss/jquery-3.1.1.min.js"></script>
        <script type="text/javascript" src="jss/bootstrap.min.js"></script>
        <style type="text/css">@import url(jss/bootstrap.min.css); </style>    <script>
        $(document).ready(function () {
            $.urlParam = function (name) {
                var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
                if (results == null) { return null; } else { return results[1] || 0; }
            }

            var msg = $.urlParam("msg");
            var mensaje = "";

            if (msg == null) { mensaje = "Gracias por utilizar la plataforma auto."; }
            else {
                if (msg.toString() == "1") { mensaje = "El usuario no existe"; }
                else { mensaje = "Gracias por utilizar la plataforma auto."; }
            }
            $('#myModalMsg').modal('show');
            $('#mensaje').html(mensaje.toString());

            $('body').click(function () {
                var settings = { type: "POST", url: "", data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
                settings.url = "salidaLAN.aspx/url";
                settings.success = function (response) { window.location.href = response.d.toString(); ; }
                settings.failure = function (response) { window.location.href = "http://www.bancomer.com/index.jsp"; ; }
                $.ajax(settings);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="modal fade" id="myModalMsg" role="dialog">
            <div class="modal-dialog modal-sm">
	            <div class="modal-content">
		            <div class="modal-body">
                        <p id="mensaje"></p>
                        <br />
		            </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" ID="btnSalida"  OnClick="btnSalida_Click" Text="Salir" CssClass="btn btn-default"/>
                    </div>
	            </div>
            </div>
        </div>
    </form>
</body>
</html>
