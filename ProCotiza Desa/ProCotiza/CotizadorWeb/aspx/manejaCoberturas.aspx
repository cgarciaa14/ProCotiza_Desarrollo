<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="manejaCoberturas.aspx.vb" Inherits="aspx_manejaCoberturas" %>

<%--RQ-PC8: CGARCIA: 09/05/2018: SE CREA PANTALLA--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <script type="text/javascript">

            function Limpiar() {
                var varSelect

                $('#<%= txtName.ClientID%>').val('');
                $('#<%= txtIdExterno.ClientID%>').val('');

            varSelect = $(".Cherry");

            $.each(varSelect, function () {
                $(this).val('0')
            });
            $("#ContentPlaceHolder1_btnGuardar").prop("disabled", false);
            };

            function Obligatorios() {
               
                var varResult = true;
                var msg = "";
                var varNombreLeng = $('#<%= txtName.ClientID%>').val().length;
                var varClasificacionLeng = $('#<%= ddlClasificacion2.ClientID%>').val();
                var varAlianzaLeng = $('#<%= ddlAlianza2.ClientID%>').val();
                var varEstatusLeng = $('#<%= ddlEstatus.ClientID%>').val();
                var varIdExternoLeng = $('#<%= txtIdExterno.ClientID%>').val().length;

                var varNombre = $('#<%= txtName.ClientID%>').val().toString();
                var varClasificacion = $('#<%= ddlClasificacion2.ClientID%>').val()
                var varIdExterno = $('#<%= txtIdExterno.ClientID%>').val().toString();
                var varAlianza = $('#<%= ddlAlianza2.ClientID%>').val();
                var varEstatus = $('#<%= ddlEstatus.ClientID%>').val();
                
                var varIDCob = $.urlParam("idCobertura").toString();


                if (varNombreLeng <= 0) {
                    msg = "Tiene que ingresar un nombre a la cobertura.";
                    varResult = false;
                } else if (varClasificacionLeng <= 0) {
                    msg= "Elegir clasificación";
                    varResult = false;
                } else if (varAlianzaLeng <= 0) {
                    msg = "Elegir alianza";
                    varResult = false;
                } else if (varEstatusLeng <= 0) {
                    msg= "Elegir estatus";
                    varResult = false;
                } else if (varIdExternoLeng <= 0) {
                    msg = "Tiene que ingresar Id Externo";
                    varResult = false;
                }

                if (msg != "") {              
                    alert(msg.toString());
                }
                else {
                    $("#ContentPlaceHolder1_btnGuardar").prop("disabled", true);
                    //var buttonProcesar = document.getElementById('<%= btnProcesar.ClientID%>');                   
                    //buttonProcesar.click();                   
                   
                    var settings = {
                        type: "POST", url: "manejaCoberturas.aspx/SaveCobertura", async: false,
                        data: "{'varIDCob': '" + varIDCob + "', 'varNombre': '" + varNombre + "', 'varClasificacion': '" + varClasificacion + "', \
                                'varIdExterno': '" + varIdExterno + "', 'varAlianza': '" + varAlianza + "', 'varEstatus': '" + varEstatus +"'}",
                        contentType: "application/json; charset=utf-8", dataType: "json",
                        success: function OnSuccess_showDetails(response) {
                            //alert(response.d.toString());
                            var items = $.parseJSON(response.d.toString());
                            //alert(items.code.toString());
            
                            if (items.code.toString() == 'OK'){
                                alert(items.message.toString());
                                window.location.href = items.url.toString();
                            }
                            else{
                                alert(items.message.toString());
                            }                           
                        }
                    };
                    $.ajax(settings);
                }
            };

            $(document).ready(function () {
                $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } };
            
                var varIDCob = $.urlParam("idCobertura").toString();
                if (varIDCob != 0) {
                    
                    $("#ContentPlaceHolder1_txtName").prop("disabled", true);
                    $("#ContentPlaceHolder1_ddlAlianza2").prop("disabled", true);
                    $("#ContentPlaceHolder1_ddlClasificacion2").prop("disabled", true);
                    $("#ContentPlaceHolder1_ddlEstatus").prop("disabled", true);
                    $("#ContentPlaceHolder1_txtIdExterno").prop("disabled", true);

                    $('#<%= btnLimpiar.ClientID%>').hide()
                    $('#<%= btnEditar.ClientID%>').show()
                    

                }
            });

            function Edita() {
         
                $("#ContentPlaceHolder1_txtName").prop("disabled", false);
                $("#ContentPlaceHolder1_ddlAlianza2").prop("disabled", false);
                $("#ContentPlaceHolder1_ddlClasificacion2").prop("disabled", false);
                $("#ContentPlaceHolder1_ddlEstatus").prop("disabled", false);
                $("#ContentPlaceHolder1_txtIdExterno").prop("disabled", false);
                
            }

            function pageLoad() {

                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function (s, e) {
                    //alert('Postback!');
                var varIDCob = $.urlParam("idCobertura").toString();
                if (varIDCob != 0) {
                    $('#<%= btnLimpiar.ClientID%>').hide()
                    $('#<%= btnEditar.ClientID%>').show()
                }
                });
                
            }

            function ShowMsj(mensaje) {                
                alert(mensaje);
            }

                

        </script>
 <asp:ScriptManager ID="scpInfo" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="updtInfo" runat="server">
                <ContentTemplate> 
    <div style="position: relative; top: 15px;">
        <fieldset class="fieldsetBBVA" style="padding-bottom: 15px;">
            <legend>COBERTURAS</legend>
             
                    <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                        <center>
                            
                    <table class="resulbbva" style="width:90%;">                                  
                    <tr>
                        <td></td>
                        <td style="width:10%; text-align:right;">Nombre:<span style="color: Red;">*</span></td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="txt3BBVA" MaxLength="30"></asp:TextBox>
                        </td>
                        <td style="width:10%; text-align:right;">Clasificación:<span style="color: Red;">*</span></td>
                        <td>
                            <asp:DropDownList ID="ddlClasificacion2" runat="server" CssClass="selectBBVA Cherry"></asp:DropDownList>
                        </td>
                        <td style="width:10%; text-align:right;">id Externo:<span style="color: Red;">*</span></td>
                        <td>
                            <asp:TextBox ID="txtIdExterno" runat="server" CssClass="txt3BBVA" MaxLength="10"></asp:TextBox>
                        </td>                                                
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width:10%; text-align:right;">Alianza:<span style="color: Red;">*</span></td>
                        <td>
                            <asp:DropDownList ID="ddlAlianza2" runat="server" CssClass="selectBBVA Cherry"></asp:DropDownList>
                        </td>
                        <td style="width:10%; text-align:right;">Estatus:<span style="color: Red;">*</span></td>
                        <td>
                            <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="selectBBVA Cherry"></asp:DropDownList>
                        </td>                        
                    </tr>                       
                        </table>
                            <br />
                            <table>                       
                    <tr>
                        <td>
                            <asp:Button ID="btnEditar" runat="server" Width="20%" CssClass="buttonBBVA2" Text="Editar" OnClientClick='Edita();' style="display: none;" />
                            <asp:Button ID="btnLimpiar" runat="server" Width="20%" CssClass="buttonBBVA2" Text="Limpiar" OnClientClick='Limpiar();'/>
                        </td>
                        <td style="width:10%; text-align:right;">
                            <asp:Label Text="label1" Visible="false" runat="server"></asp:Label>
                        </td>
                        <td>                           
                           <button type="submit"  value="Guardar" class="buttonBBVA2" onclick="Obligatorios();">Guardar </button> 
                        </td>
                    </tr>
                </table>
                            </center>
                            </fieldset>
                        </center>
               
        
        <br />
                <div id="divGrid" runat="server" visible="false">
                    <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
                <table style="width: 100%;" id="tblGrid">
                    <tr>
                        <td>
                            <asp:GridView ID="grvInfo" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="10" Width="100%" BorderWidth="0px"
                                EmptyDataText="No se encontró información con los parámetros proporcionados.">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridItem" />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                    <asp:BoundField DataField="NAME" HeaderText="Cobertura" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />                                    
                                    <asp:BoundField DataField="ESTATUS" HeaderText="estatus" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                    <asp:BoundField DataField="CLASIFICACION" HeaderText="Clasificación" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                    <asp:BoundField DataField="FECHA_ALTA" HeaderText="Fecha Alta" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
                    </div>
            <br />
                   
                    <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                        <center>
                        <table style="width: 100%;">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" Width="20%" CssClass="buttonBBVA2" OnClick="btnRegresar_Click" />
                                </td>
                            </tr>
                            </table>
                            </center>
                        </fieldset>
               
            </fieldset>
<div id="divHiddenButton" style="visibility: collapse">
                <asp:Button runat="server" ID="btnProcesar" Text="Procesar" OnClick="btnGuardar_Click" />
            </div>
    </div>
                     </ContentTemplate>
            </asp:UpdatePanel>
     
</asp:Content>

