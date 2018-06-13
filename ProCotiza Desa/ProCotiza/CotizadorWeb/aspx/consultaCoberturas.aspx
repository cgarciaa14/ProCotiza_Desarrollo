<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Principal.master" AutoEventWireup="false" CodeFile="consultaCoberturas.aspx.vb" Inherits="aspx_consultaCoberturas" %>

<%--RQ-PC8: CGARCIA: 09/05/2018: SE CREA PANTALLA--%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"> 
    <script type="text/javascript" src="../js/Funciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript">
         function Limpiar() {
             var varSelect
            
              varSelect = $(".Cherry");

              $.each(varSelect, function () {
                  $(this).val('0')
              });
             
         };

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
                        <td style="width:10%; text-align:right;">Alianza:</td>
                        <td>
                            <asp:DropDownList ID="ddlAlianza" runat="server" CssClass="selectBBVA Cherry" OnSelectedIndexChanged="ddlAlianza_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td style="width:10%; text-align:right;">Claificación:</td>
                        <td>
                            <asp:DropDownList ID="ddlClasificacion" runat="server" CssClass="selectBBVA Cherry" OnSelectedIndexChanged="ddlClasificacion_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                       
                         <td>
                            <asp:Button ID="btnConsulta" runat="server" Text="Limpiar" Width="20%" CssClass="buttonBBVA2" OnClientClick='Limpiar();' />
                        </td>
                    </tr>
                   
              
                </table>
            </center>
              
            <br />
            <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8; overflow: scroll; height: 260px; overflow-x: hidden">
                <table style="width: 100%;">
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
                                    <asp:BoundField DataField="ALIANZA" HeaderText="Alianza" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="resul2wrap" />
                                    <asp:TemplateField ItemStyle-Width="16px" ControlStyle-Width="16px" HeaderText="Opciones">
                                        <ItemTemplate>
                                            <div class="tooltip">
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/img/script_edit.png" OnClick="btnEdit_Click" CommandArgument='<%#Bind("ID")%>' CommandName="Command_edit" />
                                                <span class="tooltiptext">Editar registro</span>
                                            </div>
                                            <div class="tooltip">
                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/img/delete.png" OnClick="btnEliminar_Click" CommandArgument='<%#Bind("ID")%>' CommandName="Commnad_delete" Visible ="false" />
                                                <span class="tooltiptext">Eliminar registro</span>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <asp:UpdatePanel id="updAgregar" runat="server">
                <ContentTemplate>
                    <fieldset style="width: 96.5%; padding-top: 15px; border: .5px solid #D8D8D8;">
                        <center>
                        <table style="width: 100%;">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="20%" CssClass="buttonBBVA2" OnClick="btnAgregar_Click" />
                                </td>
                            </tr>
                            </table>
                            </center>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
      </ContentTemplate>
            </asp:UpdatePanel>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
</asp:Content>

