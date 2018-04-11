<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaUsuariosSistema.aspx.vb" Inherits="aspx_manejaUsuariosSistema" %>

<%--BBV-P-412:AVH:22/07/2016 RQB: SE AGREGA CAMPO CORREO ELECTRONICO--%>
<%--BBV-P-412:GVARGAS:27/10/2016 RQ WSE: Se agrega checkbox para saber si el usuario es interno--%>
<%--BUG-PC-04: AVH: 11/11/2016 se utiliza la funcion ValCarac--%>
<%--BUG-PC-11: AVH: 23/11/2016 SE AGREGA VALIDACION AL CORREO--%>
<%--BBV-P-412:BUG-PC-23 JRHM: 29/11/2016 Se quita placeholder de campo de correo electronico--%>
<%--BBV-P-412:RQ LOGIN GVARGAS: 03/01/2017 Se cambia ValCarac a valor 20 para permitir usuarios con punto--%>
<%--BUG-PC-37: AVH: 20/01/2017 Se deshabilitar boton que consume WS--%>
<%--BUG-PC-42 JRHM 30/01/17 Se limito nombres y apellidos a solo letras sin acentos y con solo simbolo .--%>
<%--BUG-PC-137 22/12/2017 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO--%>
<%--BUG-PC-146 18/01/2018 DCORNEJO SE MODIFICO EL DISEÑO DEL OBJETO PARA OBTENER LOS ESTILOS DE LESS-JAGUAR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css">@import url(../css/procotiza.css); </style>
        <script type="text/javascript" src="../js/Funciones.js"></script>

        <script type="text/javascript"> 
            function validarEmail() {
                var email = $("input[id$=txtCorreo]").val();
                if (!email == '') {
                    expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    if (!expr.test(email))
                        alert("El formato de la dirección de correo no es valido.");
                }
            }

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style=" position:relative; margin-left:20px; top:15px;">
<fieldset class="fieldsetBBVA" style="width:900px; padding:15px;">
<legend>ALTA / MODIFICACION DE USUARIOS DE SISTEMA</legend>
    <fieldset style="padding-left:50px">
                        <table class="resulbbva">
                            <tr>
                                <td style="width:7%;">Id:</td>
                                <td style="width:27%;">
                                    <asp:Label runat="server" ID="lblId" CssClass="resul"></asp:Label>
                                </td>
                          
                                <td style="width:10%; text-align:right;" >* User Name:</td>
                                <td style="width:27%;" >
                                    <asp:TextBox runat="server" ID="txtUser" CssClass="txt3BBVA" MaxLength="20" Onkeypress="return ValCarac(event,20);"></asp:TextBox>
                                </td>
                            
                                <td style="width:9%; text-align:right;" >*Nombre:</td>
                                <td style="width:27%;">
                                    <asp:TextBox runat="server" ID="txtnombre" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,18);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right">&nbsp;&nbsp;Paterno:</td>
                                <td >
                                    <asp:TextBox runat="server" ID="txtpaterno" CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,18);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                                </td>
                            
                                <td style="text-align:right">&nbsp;&nbsp;Materno:</td>
                                <td >
                                    <asp:TextBox runat="server" ID="txtmaterno"  CssClass="txt3BBVA" MaxLength="50" Onkeypress="return ValCarac(event,18);" onkeyup="ReemplazaAcentos(event, this.id, this.value);"></asp:TextBox>
                                </td>
                            
                                <td style="text-align:right">&nbsp;&nbsp;Correo Electrónico:</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCorreo" CssClass="txt3BBVA" onblur="validarEmail();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right">* Perfil:</td>
                                <td>
                <asp:DropDownList runat="server" ID="cmbPerfil" CssClass="selectBBVA"></asp:DropDownList>
                                </td>
                            
                                <td style="text-align:right">* Empresa:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbEmpresa" CssClass="selectBBVA"></asp:DropDownList>
                                </td>
                            
                                <td style="text-align:right">* Manejo Seguridad:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbTipoSeguridad" CssClass="selectBBVA"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right">* Estatus:</td>
                                <td >
                                    <asp:DropDownList runat="server" ID="cmbEstatus" CssClass="selectBBVA"></asp:DropDownList>
                                </td>
                            
                                <td style="text-align:right">* Usuario Interno</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="cbUsuarioInterno"  />
                                </td>
                            </tr>
                        </table>
    </fieldset>
        <br />        
    <fieldset>
        <table style="width:100%;">
            <tr>
                <td colspan="2" align="center" style="background-color:White;">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="buttonBBVA2" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"></asp:Button>
                </td>
                <td colspan="2" align="center" style="background-color:White;">
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2"></asp:Button>
                </td>
            </tr>
        </table>
    </fieldset>
</fieldset>
<asp:Label runat="server" ID="lblMensaje"></asp:Label>
</div>
</asp:Content>

