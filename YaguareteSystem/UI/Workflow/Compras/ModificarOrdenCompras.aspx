<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarOrdenCompras.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.Compras.ModificarOrdenCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <!-- alert --> 
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Orden de compras</h1>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- Input addon -->
                    <form runat="server">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Modificar orden de compras</h3>
                                <div class="card-tools pull-right">
                                    <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" CssClass="btn btn-danger btn-lg" Text="Salir" />
                                </div>
                            </div>
                            <div class="card-body">
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        Tipo compras
                                        <asp:DropDownList runat="server" ID="ddlTipoLegajo" CssClass="form-control" DataSourceID="sdsTipoLegajo" DataTextField="tipDocDescripcion" DataValueField="tipoDocumento"></asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="sdsTipoLegajo" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_tipoLegajo"></asp:SqlDataSource>
                                    </div>
                                    <div class="col-lg-3">
                                        Nro. OC
                                        <asp:TextBox runat="server" ID="txtNroOC" CssClass="form-control" placeholder="Nro. OC"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        Proveedor
                                        <asp:DropDownList Style="width: 100%;" ID="ddlProveedor" CssClass="form-control select2bs4" runat="server" DataSourceID="sdsProveedor" DataTextField="proveedor" DataValueField="provID"></asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="sdsProveedor" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_proveedores"></asp:SqlDataSource>
                                    </div>
                                    <div class="col-lg-3">
                                        Tipo moneda
                                        <asp:DropDownList runat="server" ID="ddlMoneda" CssClass="form-control" DataSourceID="sdsMoneda" DataTextField="monDescripcion" DataValueField="monID"></asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="sdsMoneda" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_monedas"></asp:SqlDataSource>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        Monto total
                                        <asp:TextBox runat="server" ID="txtMontoTotal" placeholder="Monto total" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        Empresa
                                        <asp:DropDownList runat="server" ID="ddlEmpresas" DataSourceID="sdsListaEmpresas" CssClass="form-control" DataTextField="empNombre" DataValueField="empCod"></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsListaEmpresas" runat="server" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_empresas"></asp:SqlDataSource>

                                    </div>
                                    <div class="col-lg-3">
                                        Solicitante
                                        <asp:DropDownList Style="width: 100%;" ID="ddlSolicitante" CssClass="form-control select2bs4" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        Nro SP
                                        <asp:TextBox runat="server" ID="txtNroSP" placeholder="Nro. SP" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        Estado
                                        <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                                            <asp:ListItem Value="A" Text="Activo"></asp:ListItem>
                                            <asp:ListItem Value="I" Text="Inactivo"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <label class="form-check-label">
                                            <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="ckEsProyecto_CheckedChanged" ID="ckEsProyecto" />
                                            Es proyecto</label>
                                        <asp:TextBox runat="server" ID="txtNroOrdenInterna" CssClass="form-control" placeholder="Nro. OC"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        Es DEG
                                        <asp:DropDownList runat="server" ID="ddlEsDEG" CssClass="form-control">
                                            <asp:ListItem Value="NO" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="SI" Text="SI"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        Nro. Contrato Marco
                                        <asp:TextBox runat="server" ID="txtNroContratoMarco" placeholder="Nro. SP" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        Es Direccionado
                                        <asp:DropDownList runat="server" ID="ddlDireccionado" CssClass="form-control">
                                            <asp:ListItem Value="NO" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="SI" Text="SI"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1">
                                        <br />
                                        <label class="form-check-label" for="flexSwitchCheckChecked">
                                            <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="ckProcesado_CheckedChanged" ID="ckProcesado" />
                                            Procesar</label>
                                    </div>
                                    <div class="col-lg-2  custom-control custom-checkbox">
                                        <br />
                                        <input class="custom-control-input" type="checkbox" id="ckNotificarCostosIndirectos">
                                        <label for="ckNotificarCostosIndirectos" class="custom-control-label">Notificar Costo indirecto</label>
                                    </div>
                                </div>
                                <br />
                                <div class="row" runat="server" id="pnlImportar">
                                    <div class="col-lg-2">
                                        Documento
                                        <asp:DropDownList runat="server" ID="ddlTipoDocumentoAdjunto" CssClass="form-control" DataSourceID="sdsTipoDocumentosAdjuntos" DataTextField="docNombre" DataValueField="docTipo"></asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="sdsTipoDocumentosAdjuntos" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="sp_TipoDocumentosAdjunto" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="txtNroOC" Name="nroOC" PropertyName="Text" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div class="col-lg-9">
                                        <div class="form-group">
                                            Selecciona el Archivo
                                            <asp:FileUpload ID="fuArchivo" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-1">
                                        .
                                        <asp:Button runat="server" ID="btnImportaDatos" Text="Adjuntar"
                                            OnClick="btnImportaDatos_Click" CssClass="btn btn-danger btn-block" />
                                    </div>
                                    <div class="col-lg-12">
                                        <asp:GridView ID="gvDetalle" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" DataSourceID="sdsDocumentos">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ES ACTUALIZACION" InsertVisible="False" SortExpression="RUTA" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEsActualizacion" runat="server" Text='<%# Bind("docActualizacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RUTA" InsertVisible="False" SortExpression="RUTA" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRuta" runat="server" Text='<%# Bind("docRutaVirtual") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("docID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="docOC" HeaderText="NRO OC" SortExpression="docOC" />
                                                <asp:BoundField DataField="obligatorio" HeaderText="OBLIGATORIO" SortExpression="obligatorio" />
                                                <asp:BoundField DataField="docNombre" HeaderText="DOCUMENTO" SortExpression="docNombre" />
                                                <asp:TemplateField HeaderText="Ver" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <a class="btn btn-success"
                                                            href='<%# Eval("docRutaVirtual")%>'
                                                            target="_blank">Ver<span><span></span></span></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="...">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CssClass="btn btn-warning" runat="server" OnClientClick="return confirm('En verdad desea eliminar este adjunto.?');" OnClick="btnEliminar_Click" ID="btnEliminar">Eliminar</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource runat="server" ID="sdsDocumentos" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="sp_RetornaDatosDocumentos" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="txtNroOC" Name="nroOC" PropertyName="Text" Type="String" />
                                                <asp:ControlParameter ControlID="lblDepartamento" Name="departamento" PropertyName="Text" Type="String" />
                                                <asp:ControlParameter ControlID="lblDepartamento" Name="nroFactura" PropertyName="Text" Type="String" />
                                            </SelectParameters>

                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                                <div class="alert alert-success alert-dismissible" id="alerCorrecto" runat="server" visible="false">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <h5><i class="icon fas fa-check"></i>Alert!</h5>
                                    <strong runat="server" id="lblMensajeOK"></strong>
                                </div>
                                <div class="alert alert-danger alert-dismissible" id="alertError" runat="server" visible="false">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <h5><i class="icon fas fa-ban"></i>Alert!</h5>
                                    <strong runat="server" id="lblMensajeError"></strong>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        Comentario
                                        <textarea class="form-control" id="txtComentario" runat="server" rows="3" placeholder="Ingrese comentario ..."></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="card-footer">
                                <div class="row">
                                    <div class="col-lg-11">
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary btn-lg" Text="Guardar" />
                                        <asp:Button runat="server" ID="btnActualizar" OnClick="btnActualizar_Click" CssClass="btn btn-success btn-lg" Text="Actualizar" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:Label runat="server" ID="lblUsuario" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblNombreUsuario" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblNombreCreador" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblPass" Style="display: none"></asp:Label> 
    <label runat="server" id="lblControl" visible="false"></label>
    <label runat="server" id="lblProcesado" visible="false"></label>
    <label runat="server" id="lblID" visible="false"></label>
    <label runat="server" id="lblCodigoSap" visible="false"></label>
    <label runat="server" id="lblRuc" visible="false"></label>
    <label runat="server" id="lblFechaAdd" visible="false"></label>
    <label runat="server" id="lblUsuCreador" visible="false"></label>
    <label runat="server" id="lblNombreProveedor" visible="false"></label>
    <label runat="server" id="lblEsProyecto" visible="false"></label>
    <asp:Label runat="server" ID="lblDepartamento" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblActualizacion" Visible="false"></asp:Label>
    <script src="/Content/plugins/jquery/jquery.min.js"></script> 
    <script src="../../../Content/dist/sweetalert.min.js"></script>
     
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGuardar.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnActualizar.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <script type="text/javascript">
        $('#ckNotificarCostosIndirectos').on('click', function () {
            if ($(this).is(':checked')) {
                // Hacer algo si el checkbox ha sido seleccionado
                enviarNotificacion();
            } else {
                // Hacer algo si el checkbox ha sido deseleccionado
                //alert("NO");
            }
        });


        function enviarNotificacion() {

            var usu = $('#<%= lblUsuario.ClientID%>').html();
            var usuNombre = $('#<%= lblNombreUsuario.ClientID%>').html();
            var pass = $('#<%= lblPass.ClientID%>').html();
            var nroOC = $('#<%= txtNroOC.ClientID%>').val();
          
            var settings = {
                "url": "/../Clases/webServices/ysWebServices.asmx/notificarSolicitanteCostosIndirectosCargados",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({
                    "nroOC": nroOC,
                    "usuario": usu,
                    "pass": pass,
                    "nombreUsuario": usuNombre
                }),
            };

            $.ajax(settings).done(function (response) {
                
                if (response.d.estado) {
                    swal({
                        title: "Excelente!",
                        text: "Notificación enviada",
                        icon: "success",
                        button: "Aceptar",
                    });
                } else {

                    swal({
                        title: "Ups hubo un problema!",
                        text: "Problemas para enviar notificacion " + response.d.mensaje,
                        icon: "error",
                        button: "Aceptar",
                    }); 
                }


            });
        }
    </script>
</asp:Content>
