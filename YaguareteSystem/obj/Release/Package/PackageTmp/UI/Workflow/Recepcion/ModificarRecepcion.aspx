<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarRecepcion.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.Recepcion.ModificarRecepcion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Datos de recepción</h1>
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
                <div class="card card-info">
                    <div class="card-header">
                        <h3 class="card-title">Gestión de documento</h3>
                        <div class="card-tools pull-right">
                            <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" CssClass="btn btn-danger" Text="Salir" />
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row" id="pnlModal" runat="server">
                            <div class="modal" id="modal-default">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content" style="width: 1100px;">
                                        <div class="modal-header">
                                            <h4 class="modal-title">Lista de comentarios</h4>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <asp:GridView ID="gvListaComentarios" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="USUARIO" HeaderText="USUARIO" />
                                                    <asp:BoundField DataField="DEPARTAMENTO" HeaderText="DEPARTAMENTO" />
                                                    <asp:BoundField DataField="FECHA" HeaderText="FECHA" />
                                                    <asp:BoundField DataField="MOTIVO" HeaderText="MOTIVO" />
                                                    <asp:BoundField DataField="COMENTARIO" ItemStyle-Width="450" ItemStyle-Font-Size="Small" HeaderText="COMENTARIO" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="modal-footer justify-content-between">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>
                        </div>
                    </div>
                    <div class="row" id="pnlGestion" runat="server" visible="false">
                        <div class="col-lg-12">
                            <div class="card card-secondary">
                                <div class="card-header">
                                    <h3 class="card-title">Gestionar documento</h3>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h5>Departamento</h5>
                                            <asp:DropDownList ID="ddlDepartamento" Style="width: 100%;" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server" DataSourceID="sdsDepartamento" DataTextField="fNombre" DataValueField="fNombreCorto"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="sdsDepartamento" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="sp_filtroDepartamentos" SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="lblDepartamento" Name="departamento" PropertyName="Text" Type="String" />
                                                    <asp:ControlParameter ControlID="lblTipoGestion" Name="TipoGestion" PropertyName="Text" Type="String" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </div>
                                        <div class="col-lg-6">
                                            <h5>Motivo</h5>
                                            <asp:DropDownList ID="ddlMovimiento" Style="width: 100%;" CssClass="form-control" runat="server" DataSourceID="sdsMovimentos" DataTextField="movDescripcion" DataValueField="movID"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="sdsMovimentos" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="sp_filtraMovimientos" SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="lblDepartamento" Name="departamentoActual" PropertyName="Text" Type="String" />
                                                    <asp:ControlParameter ControlID="lblTipoGestion" Name="tipoMovimiento" PropertyName="Text" Type="String" />
                                                    <asp:ControlParameter ControlID="ddlDepartamento" Name="departamentoSiguiente" PropertyName="SelectedValue" Type="String" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <h5>Comentario</h5>
                                            <textarea class="form-control" id="txtComentarios" runat="server" rows="3" placeholder="Ingrese comentario ..."></textarea>
                                            <br />
                                            <div class="alert alert-success alert-dismissible" id="alerCorrectoMotivo" runat="server" visible="false">
                                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                                <h5><i class="icon fas fa-check"></i>Alert!</h5>
                                                <strong runat="server" id="lblMensajeOKMotivo"></strong>
                                            </div>
                                            <div class="alert alert-danger alert-dismissible" id="alertErrorMotivo" runat="server" visible="false">
                                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                                <h5><i class="icon fas fa-ban"></i>Alert!</h5>
                                                <strong runat="server" id="lblMensajeErrorMotivo"></strong>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer">
                                    <div class="row">
                                        <div class="col-lg-10">
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Button runat="server" ID="btnAtras" OnClick="btnAtras_Click" CssClass="btn btn-secondary btn-lg" Text="Atras" />
                                            <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary btn-lg" Text="Guardar" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="pnlDocumentos" runat="server">
                        <div class="col-lg-12">
                            <div class="card card-danger">
                                <div class="card-header">
                                    <h3 class="card-title">Ultimo comentario</h3>
                                </div>
                                <div class="card-body">
                                    <asp:GridView ID="gvUltimoComentario" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" AllowSorting="True">
                                        <Columns>
                                            <asp:BoundField DataField="USUARIO" HeaderText="USUARIO" />
                                            <asp:BoundField DataField="DEPARTAMENTO" HeaderText="DEPARTAMENTO" />
                                            <asp:BoundField DataField="FECHA" HeaderText="FECHA" />
                                            <asp:BoundField DataField="MOTIVO" HeaderText="MOTIVO" />
                                            <asp:BoundField DataField="COMENTARIO" ItemStyle-Width="600px" ItemStyle-Font-Size="Small" HeaderText="COMENTARIO" />
                                        </Columns>
                                    </asp:GridView>
                                    <button type="button" class="btn btn-default" data-toggle="modal" data-target="#modal-default">Ver mas</button>
                                </div>
                            </div>
                            <div class="card card-secondary">
                                <div class="card-header">
                                    <h3 class="card-title">Datos documentos</h3>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <h5>Nro. Orden Compras</h5>
                                            <asp:DropDownList ID="ddlNroOC" Enabled="false" Style="width: 100%;" CssClass="form-control select2bs4" runat="server" DataSourceID="sdsNroOc" DataTextField="descripcion" DataValueField="valor"></asp:DropDownList>
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
                                        </div>
                                        <div class="col-lg-3">
                                            <h5>Tipo Factura</h5>
                                            <asp:DropDownList runat="server" ID="ddlTipoFactura" CssClass="form-control" DataSourceID="sdsTipoFactura" DataTextField="tipDocDescripcion" DataValueField="tipoDocumento"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="sdsTipoFactura" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_tipoLegajo"></asp:SqlDataSource>
                                        </div>
                                        <div class="col-lg-3">
                                            <h5>Nro. Factura</h5>
                                            <asp:TextBox runat="server" ID="txtNroFactura" CssClass="form-control" placeholder="Nro. Factura"></asp:TextBox>
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
                                            <h5>Timbrado</h5>
                                            <asp:TextBox runat="server" ID="txtTimbrado" CssClass="form-control" placeholder="Nro. timbrado"></asp:TextBox>
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
                                            <h5>Tipo documento</h5>
                                            <asp:DropDownList Style="width: 100%;" ID="ddlTipoDocumento" CssClass="form-control" runat="server" DataSourceID="sdsTipoDocumento" DataTextField="tipDocDescripcion" DataValueField="tipoDocumento"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="sdsTipoDocumento" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_tipoDocumentos"></asp:SqlDataSource>

                                        </div>

                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <h5>Monto factura</h5>
                                            <asp:TextBox runat="server" ID="txtMontoTotal" placeholder="34.025,44" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-3">
                                            <h5>Estado documento</h5>
                                            <asp:DropDownList ID="ddlEstadoDocumento" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="Original" Text="Original"></asp:ListItem>
                                                <asp:ListItem Value="Copia" Text="Copia"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            <h5>Nro. Despacho</h5>
                                            <asp:TextBox runat="server" ID="txtNroDespacho" CssClass="form-control" placeholder="Nro. despacho"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-3">
                                            <h5>Nro. Factura Asociada</h5>
                                            <asp:TextBox runat="server" ID="txtFacturaAsociadaNotaCredito" CssClass="form-control" placeholder="Nro. factura asociada"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-11">
                                            <div class="form-group">
                                                <h5>Selecciona el Archivo</h5>
                                                <asp:FileUpload ID="fuArchivo" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <h5>.</h5>
                                            <asp:Button runat="server" ID="btnImportaDatos" Text="Importar Datos"
                                                OnClick="btnImportaDatos_Click" CssClass="btn btn-danger btn-block" />
                                            <%-- <asp:Button runat="server" ID="btnActualizar" Text="Actualizar" OnClick="btnActualizar_Click" CssClass="btn btn-success" />--%>
                                        </div>
                                        <div class="col-lg-12">
                                            <asp:GridView ID="gvDetalle" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                    <%-- //<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />--%>
                                                    <asp:BoundField DataField="OC" HeaderText="Orden Compras" SortExpression="OC" />
                                                    <asp:BoundField DataField="DOCNOMBRE" HeaderText="Nombre documento" SortExpression="DOCNOMBRE" />
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
                                                    <asp:TemplateField HeaderText="..." SortExpression="HistoricoComentarios">
                                                        <ItemTemplate>
                                                            <a class="btn btn-warning"
                                                                href='<%# Eval("docRutaVirtual")%>'
                                                                target="_blank">Visualizar<span><span></span></span></a>
                                                            <%--<a class="btn btn-danger"
                                                                href='<%# Eval("ELIMINAR")%>'
                                                                target="_blank">Eliminar<span><span></span></span></a>--%>
                                                            <asp:LinkButton CssClass="btn btn-danger" runat="server" OnClientClick="return confirm('En verdad desea eliminar este adjunto.?');" OnClick="btnEliminar_Click" ID="btnEliminar">Eliminar</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="..." SortExpression="HistoricoComentarios">
                                                        <ItemTemplate>
                                                            <a class="btn btn-warning"
                                                                href='<%# Eval("RUTA")%>'
                                                                target="_blank">Visualizar<span><span></span></span></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
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
                            </div>
                            <div class="card card-secondary">
                                <div class="card-header">
                                    <h3 class="card-title">Gestionar documento</h3>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-3"></div>
                                        <div class="col-lg-2">
                                            <asp:Button ID="btnDevolver" CssClass="btn btn-block  btn-primary" OnClick="btnDevolver_Click" Text="&lt;&lt; Devuelto" runat="server" />
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Button ID="btnRechazar" CssClass="btn btn-block  btn-danger" OnClick="btnRechazar_Click" Text="( X ) Rechazado" runat="server" />
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Button ID="btnContinuar" CssClass="btn btn-block  btn-success" OnClick="btnContinuar_Click" Text="Continua &gt;&gt;" runat="server" />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                    </div>
                </div>
            </form>
        </div>
    </section>
    <asp:Label runat="server" ID="lblNroOC" Visible="false"></asp:Label>
    <label runat="server" id="lblControl" visible="false"></label>
    <label runat="server" id="lblCantidad" visible="false"></label>
    <asp:Label runat="server" ID="lblDepartamento" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblTipoGestion" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblSolicitanteNombre" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblCompradorNombre" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblID" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblTipoLegajo" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblFechaAddFactura" Visible="false"></asp:Label>
</asp:Content>
