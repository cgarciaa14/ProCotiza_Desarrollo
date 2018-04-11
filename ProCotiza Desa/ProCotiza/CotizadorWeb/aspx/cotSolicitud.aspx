<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="cotSolicitud.aspx.vb" Inherits="aspx_cotSolicitud" %>

<%--BBV-P-412  RQ-E  gvargas   12/10/2016 Actualizacion de referencias. --%>
<%--  BUGPC16 24/11/2016: GVARGAS: Correccion Error Date Picker--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <!--<link rel="stylesheet" href="//code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css"/>-->
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/gridviewScroll.min.js"></script> 
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <style type="text/css">
        .ui-datepicker 
        {
            font-size:63%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="width:63%;">
         <fieldset class="fieldset" style=" padding-left:20px; padding-bottom:20px;">
         <legend>Datos Solicitante</legend>

            <table class="resul2">
                <tr>
                    <td>N° Solicitud</td>
                    <td><asp:TextBox CssClass="txt3" ID="txtNumSolicitud" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color:#EFEFEF;">N° Propuesta</td>
                    <td><asp:TextBox CssClass="txt3" ID="txtPropuesta" runat="server"></asp:TextBox></td>
                </tr>
            </table>
            <br />
            <table class="resul2">
                <tr>
                    <td style="background-color:white;">1er Nombre</td>
                    <td>2do Nombre</td>
                    <td>Apellido Paterno</td>
                    <td>Apellido Materno</td>
                </tr>
                <tr>
                    <td style="background-color:White;"><asp:TextBox CssClass="txt3" ID="txt1Nombre" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txt2Nombre" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtApePatCliente" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtApeMatCliente" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="background-color:white;">RFC con Homoclave</td>
                    <td>(Lada) Telefono Particular</td>
                    <td>Telefono Movil</td>
                    <td>Correo Electrónico</td>
                </tr>
                <tr>
                    <td style="background-color:White;"><asp:TextBox CssClass="txt3" ID="txtRFCCliente" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td>
                        <asp:TextBox CssClass="txt3" ID="txtLada" runat="server" Width="40px" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox>
                        <asp:TextBox CssClass="txt3" ID="txtTelCliente" runat="server" Width="96px" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox>
                    </td>
                    <td><asp:TextBox CssClass="txt3" ID="txtTelMovilCliente" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtEmailCliente" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>                
                </tr>
                <tr>
                    <td style="background-color:white;">Código Postal</td>
                    <td>Estado</td>
                    <td>Ciudad</td>
                    <td>Delegación o Municipio</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox runat="server" ID="txtCodPostCliente" CssClass="txt3" AutoPostBack="True" onkeypress="ManejaCar('N',0,this.value)"></asp:TextBox></td>
                    <td><asp:DropDownList CssClass="select" Width="160px" ID="cmbEstado" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                    <td><asp:DropDownList CssClass="select" Width="160px" ID="cmbCiudadCliente" runat="server"></asp:DropDownList></td>
                    <td><asp:DropDownList CssClass="select" Width="160px" ID="cmbDelMunCliente" runat="server"></asp:DropDownList></td>                                                                        
                </tr>
                <tr>
                    <td style="background-color:white;">Colonia</td>
                    <td>Calle</td>
                    <td>Numero Ext</td>
                    <td>Numero Int</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:DropDownList CssClass="select" Width="160px" ID="cmbColoniaCliente" runat="server" AutoPostBack="True" ></asp:DropDownList></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtCalleCliente" runat="server" onkeypress="ManejaCar('A',1,this.value)" MaxLength="100"></asp:TextBox></td>                            
                    <td><asp:TextBox CssClass="txt3" ID="txtNumExtCliente" runat="server" onkeypress="ManejaCar('A',1,this.value)" MaxLength="30"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtNumIntCliente" runat="server" onkeypress="ManejaCar('A',1,this.value)" MaxLength="30"></asp:TextBox></td>
                                
                </tr>
            </table>
         </fieldset>
         <br />
         <fieldset class="fieldset" style=" padding-left:20px; padding-bottom:20px;">
            <legend>Datos Personales</legend>
            <table class="resul2">
                <tr>
                    <td style="background-color:white;">Sexo</td>
                    <td>Fecha de Nacimiento</td>
                    <td>Nacionalidad</td>
                    <td>Estado Civil</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:DropDownList CssClass="select" Width="160px"  ID="cmbSexo" runat="server">
                        </asp:DropDownList></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtFecNacCliente" Width="120px" onkeypress="ManejaCar('F',1,this.value)" runat="server"></asp:TextBox>
<%--                    <img id="imgFecNac" runat="server" title="Abre Calendario" style="cursor:pointer" 
                            src="../img/calendar.jpg" visible="True" alt=""/>--%>
                    </td>
                    <td><asp:DropDownList CssClass="select" Width="160px"  ID="cmbNacionalidad" runat="server" ></asp:DropDownList></td>
                    <td><asp:DropDownList CssClass="select" Width="160px" ID="cmbEstadoCivil" runat="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="background-color:white;">País de nacimiento</td>
                    <td>Estado de nacimiento</td>
                    <td>Vive en casa</td>
                    <td>Propiedad a su nombre</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:DropDownList CssClass="select" Width="160px" ID="cmbPaisNac" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                    <td><asp:DropDownList CssClass="select" Width="160px" ID="cmbEstadoNac" runat="server"></asp:DropDownList></td>
                    <td><asp:DropDownList CssClass="select" Width="160px"  ID="cmbVive" runat="server"></asp:DropDownList></td>
                    <td><asp:DropDownList CssClass="select" Width="160px" ID="cmbPropiedad" runat="server"></asp:DropDownList></td>
                </tr>
               <tr>
                    <td style="background-color:white;">Años Residencia en el Domicilio</td>
                    <td>Años Residencia en la Ciudad</td>
                    <td colspan="2">Beneficiario en caso de fallecimiento</td>
                    <td></td>
                </tr>
                 <tr>
                    <td style="background-color:white;"><asp:DropDownList CssClass="select" Width="160px" ID="cmbAnioRecDomCliente" runat="server" ></asp:DropDownList></td>
                    <td><asp:DropDownList CssClass="select" Width="160px" ID="cmbAnioRecCiudadCliente" runat="server" ></asp:DropDownList></td> 
                    <td colspan="2"><asp:TextBox CssClass="txt3" Width="330px" ID="txtBeneficiario1Cliente" runat="server" onkeypress="ManejaCar('C',1,this.value)" AutoPostBack="True"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="background-color:white;">CURP</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtCURPCliente" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                </tr>  
            </table>
         </fieldset>
         <br />
         <fieldset class="fieldset" style=" padding-left:20px; padding-bottom:20px;">
            <legend>Datos de Empleo</legend>
            <table class="resul2">
                <tr>
                    <td style="background-color:white;">Compañia</td>
                    <td>Puesto</td>
                    <td>Departamento o Área</td>
                    <td>Sueldo Mensual</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtCompaniaCliEmpleo" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtPuestoCliEmpleo" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtDepAreaCliEmpleo" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtSueMenCliEmpleo" runat="server" onkeypress="ManejaCar('N',0,this.value)"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="background-color:white;">(Lada)Teléfono</td>
                    <td>Extensión</td>
                    <td>Años de Antiguedad</td>
                </tr>
                <tr>
                                    
                    <td style="background-color:white;">
                        <asp:TextBox ID="txtLadaEmpleo" runat="server" CssClass="txt3" Width="40px" ></asp:TextBox>
                        <asp:TextBox CssClass="txt3" ID="txtTelCliEmpleo" runat="server" Width="96px" onkeypress="ManejaCar('N',0,this.value)"></asp:TextBox>
                    </td>
                    <td><asp:TextBox CssClass="txt3" ID="txtTelExtCliEmpleo" runat="server" onkeypress="ManejaCar('N',0,this.value)"></asp:TextBox></td>
                    <td><asp:DropDownList CssClass="select" Width="160px" ID="cmbDesdeAnioCliEmpleo" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color:white;">Calle</td>
                    <td>N° Exterior</td>
                    <td>N° Interior</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtCalleCliEmpleo" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtNumExtCliEmpleo" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtNumIntCliEmpleo" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                </tr>
            </table>
         </fieldset>
         <br />
         <fieldset class="fieldset" style=" padding-left:20px; padding-bottom:20px;">
            <legend>Referencias Personales</legend>
            <table class="resul2">
                <tr>
                    <td style="background-color:white;">Nombre</td>
                    <td>Parentesco</td>
                    <td>Teléfono Celular</td>
                    <td>Correo Electrónico</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtNombre1RefPer" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:DropDownList CssClass="select" Width="160px" ID="cmbParentesco1RefPer" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtTel1RefPer" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtEmail1RefPer" runat="server" onkeypress="ManejaCar('P',1,this.value)"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="background-color:white;">Nombre</td>
                    <td>Parentesco</td>
                    <td>Teléfono Celular</td>
                    <td>Correo Electrónico</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtNombre2RefPer" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:DropDownList CssClass="select" Width="160px" ID="cmbParentesco2RefPer" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtTel2RefPer" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtEmail2RefPer" runat="server" onkeypress="ManejaCar('P',1,this.value)"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="background-color:white;">Nombre</td>
                    <td>Parentesco</td>
                    <td>Teléfono Celular</td>
                    <td>Correo Electrónico</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtNombre3RefPer" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:DropDownList CssClass="select" Width="160px" ID="cmbParentesco3RefPer" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtTel3RefPer" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtEmail3RefPer" runat="server" onkeypress="ManejaCar('P',1,this.value)"></asp:TextBox></td>
                </tr>
            </table>
         </fieldset>
         <br />
         <fieldset class="fieldset" style=" padding-left:20px; padding-bottom:20px;">
            <legend>Referencias Bancarias</legend>
            <table class="resul2">
                <tr>
                    <td style="background-color:white;">Banco</td>
                    <td>N° de Tarjeta de Crédito</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:DropDownList CssClass="select" Width="160px" ID="cmbBanco1RefBancaria" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtTar1RefBancaria" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                </tr>             
            </table>
         </fieldset>
         <br />
         <fieldset class="fieldset" style=" padding-left:20px; padding-bottom:20px;">
            <legend>Cargo Directo</legend>
            <table class="resul2">
                <tr>
                    <td style="background-color:white;">Banco</td>
                    <td><asp:Label runat="server" ID="lblcardir"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:DropDownList CssClass="select" ID="cmbBancoCarDir" runat="server" Width="160px" AutoPostBack="true" ></asp:DropDownList></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtCLABECarDir" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                </tr>
            </table>
         </fieldset>
         <br />
         <fieldset class="fieldset" style=" padding-left:20px; padding-bottom:20px;">
            <legend>Coacreditado en su caso</legend>
            <table class="resul2">
                <tr style="height:20px">
                    <td style="background-color:white;">1er Nombre</td>
                    <td>2do Nombre</td>
                    <td>Apellido Paterno</td>
                    <td>Apellido Materno</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtNombre1Coa" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtNombre2Coa" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtApePatCoa" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtApeMatCoa" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="background-color:white;">RFC con Homoclave</td>
                    <td>(Lada)Telefono Particular</td>
                    <td>Telefono Movil</td>
                    <td>Fecha de Nacimiento</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtRFCCoa" runat="server" onkeypress="ManejaCar('A',1,this.value)" MaxLength="13"></asp:TextBox></td>
                    <td><asp:TextBox Width="40px" ID="txtLadaCoa" runat="server" CssClass="txt3"></asp:TextBox>
                        <asp:TextBox CssClass="txt3" Width="96px" ID="txtTelCoa" runat="server"  onkeypress="ManejaCar('N',0,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtMovilCoa" runat="server" onkeypress="ManejaCar('N',0,this.value)" MaxLength="50"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtFecNactCoa" Width="120px" runat="server" AutoPostBack="true"  onkeypress="ManejaCar('F',1,this.value)"></asp:TextBox>
                        <%--<img id="imgFecNactCoa" runat="server" title="Abre Calendario" style="CURSOR: hand" src="../img/calendar.jpg"/>--%>
                    </td>
                </tr>
                <tr>
                    <td style="background-color:white;">C.P.</td>
                    <td>Estado</td>
                    <td>Ciudad</td>
                    <td>Delegacion o Municipio</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox runat="server" ID="txtCodPostCoa" CssClass="txt3" AutoPostBack="True" onkeypress="ManejaCar('N',0,this.value)"></asp:TextBox></td>
                    <td><asp:DropDownList CssClass="select" ID="cmbEstadoCoa" runat="server" Width="160px" AutoPostBack="True"/></td> 
                    <td><asp:DropDownList CssClass="select" ID="cmbCiudadCoa" runat="server" Width="160px"></asp:DropDownList></td>
                    <td><asp:DropDownList CssClass="select" ID="cmbDelMunCoa" runat="server" Width="160px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="background-color:white;">Colonia</td>
                    <td>Calle</td>
                    <td>N° Exterior</td>
                    <td>N° Interior</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:DropDownList CssClass="select" Width="160px" ID="cmbColoniaCoa" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtCalleCoa" runat="server"  onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>                            
                    <td><asp:TextBox CssClass="txt3" ID="txtNumExtCoa" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtNumInttCoa" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td> 
                </tr>
                <tr>
                    <td style="background-color:white;">Correo Electrónico</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtEmailCoa" runat="server" onkeypress="ManejaCar('P',1,this.value)"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="background-color:white;">Compañia</td>
                    <td>Puesto</td>
                    <td>Departamento o Área</td>
                    <td>(Lada)Telefono</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtCompaniaCoaEmp" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtPuestoCoaEmp" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtDepAreaCoaEmp" runat="server" onkeypress="ManejaCar('C',1,this.value)"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtLadaCoaEmp" runat="server" CssClass="txt3" Width="40px"></asp:TextBox>
                        <asp:TextBox CssClass="txt3" ID="txtTelCoaEmp" runat="server" Width="96px" onkeypress="ManejaCar('N',0,this.value)"></asp:TextBox>
                    </td>
                </tr>
                <tr>                                    
                    <td style="background-color:white;">Extensión</td>
                    <td>Años de Antiguedad</td>
                    <td>Sueldo Mensual</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtExtCoaEmp" runat="server" onkeypress="ManejaCar('N',0,this.value)"></asp:TextBox></td>
                    <td><asp:DropDownList CssClass="select" ID="cmbDesdeAnioCoaEmp" runat="server" Width="160px"></asp:DropDownList></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtSueldoCoaEmp" runat="server" onkeypress="ManejaCar('N',0,this.value)"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="background-color:white;">Calle</td>
                    <td>N° Exterior</td>
                    <td>N° Interior</td>
                </tr>
                <tr>
                    <td style="background-color:white;"><asp:TextBox CssClass="txt3" ID="txtCalleCoaEmp" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtNumExtCoaEmp" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                    <td><asp:TextBox CssClass="txt3" ID="txtNumIntCoaEmp" runat="server" onkeypress="ManejaCar('A',1,this.value)"></asp:TextBox></td>
                </tr> 
            </table>
         </fieldset>
         <br />
         <fieldset class="fieldset" style=" padding-left:20px; padding-bottom:10px;">
            <center>
            <table class="resul2">
                <tr>
                    <td style="background-color:white;">
                        <asp:Button CssClass="button" runat="server" ID="cmdGuarda" Text="Guarda Solicitud" Width="110px"/>
                    </td>
                    <td>
                        <asp:Button CssClass="button" runat="server" ID="cmdImprime" Text="Imprime Solicitud" Width="110px"/>
                    </td>
                </tr>
            </table>
            </center>
         </fieldset>
         <br />
    </div>
    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    <script type="text/javascript">
        $(document).ready(function () {
            pickerSettins3();
        });
    </script>
</asp:Content>

