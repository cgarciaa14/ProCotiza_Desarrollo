<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Principal.master" CodeFile="cambiaPwd.aspx.vb" Inherits="aspx_cambiaPwd" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<%--BBV-P412 RQCOT-01.2: AMR 23/02/2017: Seguros.--%>
<%--BUG-PC-137 26/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 24/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<%--<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Cambio de Contraseña</title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url(../css/procotiza.css);
    </style>
    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <style runat="server" id="LessCSS" type="text/css" />
</asp:Content>
<%--</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: relative; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;">
            <legend>CAMBIO DE CONTRASEÑA</legend>
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                <center>
        <table class="resulbbva" width="95%">
			<%--<tr class="encabezados">    
				<td>CAMBIO DE CONTRASEÑA</td>
			</tr>
			<tr>
				<td>
					<table cellSpacing="2" align="center">--%>
						<tr>
							<td style="text-align:right;">Usuario:</td>
							<td><asp:Label Runat="server" ID="lblUsu"></asp:Label></td>
						</tr>
                        <tr>
							<td style="text-align:right;">* Contraseña Actual:</td>
							<td><asp:textbox onkeypress="ManejaCar('a',0,this.value);" onpaste="return false;" id="txtPwdAct" runat="server"
								 CssClass="txt3BBVA" MaxLength="12" TextMode="Password"></asp:textbox></td>
						</tr>
                        <tr>
							<td style="text-align:right;">* Contraseña Nueva:</td>
							<td>
								<asp:textbox id="txtPwd" runat="server" onkeypress="ManejaCar('a',0,this.value);" onpaste="return false;"
									CssClass="txt3BBVA" MaxLength="12" TextMode="Password"></asp:textbox>
							</td>
						</tr>
                        <tr>
							<td style="text-align:right;">* Confirmación:</td>
							<td>
								<asp:textbox id="txtConfirm" runat="server" onkeypress="ManejaCar('a',0,this.value);" onpaste="return false;"
									 CssClass="txt3BBVA" MaxLength="12" TextMode="Password"></asp:textbox>
							</td>
						</tr>
					<%--</table>
				</td>
			</tr>--%> 
            </table>
            </center>
                <center>
                <table style="width:100%;">
                    <tr class="botones">
				<td align="center">
					<asp:button id="btnGuarda" runat="server" Text="Guardar" Cssclass="buttonBBVA2"></asp:button></td>
                <td align="center"> 
				    <asp:button id="cmdCerrar" runat="server" Text="Cerrar" Cssclass="buttonSecBBVA2"></asp:button>
			    </td>
		    </tr>
                </table>
            </center>


                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </fieldset>
        </fieldset>
    </div>
</asp:Content>
<%--</form>
</body>
</html>--%>
