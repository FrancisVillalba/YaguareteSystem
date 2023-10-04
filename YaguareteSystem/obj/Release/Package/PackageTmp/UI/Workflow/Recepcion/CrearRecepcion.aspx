<%@ Page Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CrearRecepcion.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.Recepcion.CrearRecepcion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Recepción</h1>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <!-- Input addon -->
            <form runat="server">
                <div class="row" id="pnlModal" runat="server">
                    <div class="modal" id="modal-default">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content" style="width: 1100px;">
                                <div class="modal-header">
                                    <h4 class="modal-title">Agregar proveedor</h4>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Razon social:</label>
                                            <input class="form-control" type="text" placeholder="Razón social" id="txtRazonSocial" runat="server" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Ruc:</label>
                                            <input class="form-control" type="text" placeholder="RUC" id="txtRuc" runat="server" />
                                        </div>
                                        <br />
                                    </div>
                                </div>
                                <div class="modal-footer justify-content-between">
                                    <asp:Button CssClass="btn btn-primary" ID="btnAltaProveedores" Text="Guardar" runat="server" OnClick="btnAltaProveedores_Click" />
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                </div>
                <div class="card card-info">
                    <div class="card-header">
                        <h3 class="card-title">Cargar facturas</h3>
                    </div>
                    <div class="card-body">

                        <div class="row">
                            <div class="col-lg-3">
                                <h5>Nro. Orden Compras</h5>
                                <asp:DropDownList ID="ddlNroOC" Style="width: 100%;" CssClass="form-control select2bs4" runat="server" DataSourceID="sdsNroOc" DataTextField="descripcion" DataValueField="valor"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="sdsNroOc" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from wv_listaNroOrdenCompras"></asp:SqlDataSource>
                            </div>
                            <div class="col-lg-3">
                                <h5>Proveedor</h5>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlProveedor" CssClass="form-control select2bs4" runat="server" DataSourceID="sdsProveedorRecepcion" DataTextField="recepProveedorNombre" DataValueField="recepProveedorId"></asp:DropDownList>
                                    <%--<span class="input-group-append">
                                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modal-default">+</button>
                                                </span>--%>
                                    <asp:SqlDataSource runat="server" ID="sdsProveedorRecepcion" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="SELECT recepProveedorId ,recepProveedorNombre FROM vw_ProveedorRecepcion"></asp:SqlDataSource>
                                </div>
                                <%-- <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtProveedor" onchange="buscadorProveedor(this);" placeholder="Buscar.." CssClass="form-control"></asp:TextBox>
                                   <input class="form-control" type="text" onchange="buscadorProveedor(this);" placeholder="Razón social" id="txtProveedor" />--%>
                                <%-- <asp:DropDownList ID="ddlProveedor" CssClass="form-control select2bs4" runat="server"></asp:DropDownList>--%>
                            </div>

                            <div class="col-lg-3">
                                <h5>Tipo Factura</h5>
                                <asp:DropDownList runat="server" ID="ddlTipoFactura" CssClass="form-control" DataSourceID="sdsTipoFactura" DataTextField="tipDocDescripcion" DataValueField="tipoDocumento"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="sdsTipoFactura" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_tipoLegajo"></asp:SqlDataSource>
                            </div>
                            <div class="col-lg-3">
                                <h5>Nro. Factura</h5>
                                <asp:TextBox runat="server" ID="txtNroFactura" onchange="controlFacturaExistente(this);" CssClass="form-control" placeholder="Nro. Factura"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3">
                                <h5>Tipo moneda</h5>
                                <asp:DropDownList runat="server" ID="ddlMoneda" CssClass="form-control" DataSourceID="sdsMoneda" DataTextField="monDescripcion" DataValueField="monID"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="sdsMoneda" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_monedas"></asp:SqlDataSource>
                            </div>
                            <div class="col-lg-3">
                                <h5>Fecha factura</h5>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                                    </div>
                                    <input type="text" class="form-control" runat="server" id="txtFechaFactura" data-inputmask-alias="datetime" data-inputmask-inputformat="dd/mm/yyyy" data-mask>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <h5>Timbrado</h5>
                                <asp:TextBox runat="server" ID="txtTimbrado" CssClass="form-control" placeholder="Nro. timbrado"></asp:TextBox>
                            </div>
                            <div class="col-lg-3">
                                <h5>Tipo documento</h5>
                                <asp:DropDownList Style="width: 100%;" onchange="control(this);" ID="ddlTipoDocumento" CssClass="form-control" runat="server" DataSourceID="sdsTipoDocumento" DataTextField="tipDocDescripcion" DataValueField="tipoDocumento"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="sdsTipoDocumento" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_tipoDocumentos"></asp:SqlDataSource>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3">
                                <h5>Monto factura</h5>
                                <asp:TextBox runat="server" ID="txtMontoTotal" placeholder="Monto total Ej: 24.555,44" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-3">
                                <h5>Estado documento</h5>
                                <asp:DropDownList ID="ddlEstadoDocumento" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="Original" Text="Original"></asp:ListItem>
                                    <asp:ListItem Value="Copia" Text="Copia"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-3">
                                <asp:TextBox runat="server" ID="txtFacturaAsociadaNotaCredito" Style="display: none" CssClass="form-control" placeholder="Nro. factura asociada"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row" runat="server" id="pnlImportar">

                            <div class="col-lg-10">
                                <div class="form-group">
                                    <h5>Selecciona el Archivo</h5>
                                    <asp:FileUpload ID="fuArchivo" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="col-2">
                                <h5>.</h5>
                                <asp:Button runat="server" ID="btnImportaDatos" Text="Adjuntar" OnClick="btnImportaDatos_Click" CssClass="btn btn-danger" />
                            </div>
                            <div class="col-lg-12">
                                <asp:GridView ID="gvDetalle" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" DataSourceID="sdsDocumentos"
                                    AllowPaging="True" AllowSorting="True">
                                    <Columns>
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
                                        <asp:BoundField DataField="OC" HeaderText="NRO OC" SortExpression="OC" />
                                        <asp:BoundField DataField="docNombre" HeaderText="DOCUMENTO" SortExpression="docNombre" />
                                        <asp:TemplateField HeaderText="...">
                                            <ItemTemplate>
                                                <asp:LinkButton CssClass="btn btn-warning" runat="server" OnClientClick="return confirm('En verdad desea eliminar este adjunto.?');" OnClick="btnEliminar_Click" ID="btnEliminar">Eliminar</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource runat="server" ID="sdsDocumentos" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="sp_RetornaDatosDocumentos" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlNroOC" Name="nroOC" PropertyName="Text" Type="String" />
                                        <asp:ControlParameter ControlID="lblDepartamento" Name="departamento" PropertyName="Text" Type="String" />
                                        <asp:ControlParameter ControlID="txtNroFactura" Name="nroFactura" PropertyName="Text" Type="String" />
                                    </SelectParameters>

                                </asp:SqlDataSource>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12">
                                <h5>Comentario</h5>
                                <textarea class="form-control" id="txtComentario" runat="server" rows="3" placeholder="Ingrese comentario ..."></textarea>
                            </div>
                        </div>
                        <br />
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
                    </div>
                    <div class="card-footer">
                        <div class="row">
                            <div class="col-lg-10">
                            </div>
                            <div class="col-lg-2">
                                <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" CssClass="btn btn-secondary btn-lg" Text="Salir" />
                                <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary btn-lg" Text="Guardar" />
                            </div>
                        </div>
                    </div>
                </div>
            </form>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <label runat="server" id="lblControl" visible="false"></label>
    <label runat="server" id="lblCantidad" visible="false"></label>
    <asp:Label runat="server" ID="lblDepartamento" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblUsuario" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblPass" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblID" Style="display: none"></asp:Label>
    <script type="text/javascript">  
        $(document).ready(function () {
            $("#<%=txtFacturaAsociadaNotaCredito.ClientID %>").hide();
        });
    </script>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGuardar.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <script type="text/javascript">
        function control(element) {

            if (element.value == "NotaCredito") {
                <%--document.getElementById("<%=ddlTipoDocumento.ClientID %>"). = true;--%>
                $("#<%=txtFacturaAsociadaNotaCredito.ClientID %>").show();
            } else {
                $("#<%=txtFacturaAsociadaNotaCredito.ClientID %>").hide();
            }
        }
    </script>

    <script type="text/javascript">
        function controlFacturaExistente(element) {
            var nroOC = $("#<%=ddlNroOC.ClientID %>").val();
            var nroFactura = element.value;
            var usu = $('#<%= lblUsuario.ClientID%>').html();
            var pass = $('#<%= lblPass.ClientID%>').html();

            var settings = {
                "url": "/../Clases/webServices/ysWebServices.asmx/verificaSiFacturaExisteOC",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({
                    "nroOc": nroOC,
                    "nroFactura": nroFactura,
                    "usu": usu,
                    "pass": pass
                }),
            };

            $.ajax(settings).done(function (response) {
                var obj = jQuery.parseJSON(response.d.Data);
                console.log(response.d.Data);
                $.each(obj.value, function (i, item) {

                    if (item['docDepartamento'] != null) {
                        alert("Esta factura ya existe en esta OC");
                    }
                });
            });
        } 
    </script>
</asp:Content>
