<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InformacionFactura.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.BuscadorGeneral.InformacionFactura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Datos de Impuestos</h1>
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
                        <h3 class="card-title">Gestión de impuestos</h3>
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
                        <div class="row" id="pnlDocumentos" runat="server">
                            <div class="col-lg-12">
                                <div class="row" runat="server" id="pnlRechazoImpuestos">
                                    <div class="col-lg-3">
                                        <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="ckEsProyecto_CheckedChanged" ID="ckEsProyecto" />
                                        <label class="form-check-label" for="flexSwitchCheckChecked">Factura física rechazada</label>
                                        <div class="input-group">
                                            <asp:TextBox runat="server" Enabled="false" ID="txtEntregado" placeholder="Comentario a quien entrego" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-append">
                                                <%--<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modal-default">+</button>--%>
                                                <asp:Button runat="server" ID="btnGuardarRechazado" CssClass="btn-primary btn" Text="Guardar" OnClick="btnGuardarRechazado_Click" />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-lg-2"></div>
                                    <div class="col-lg-6">
                                    </div>
                                </div>
                                <asp:Label runat="server" CssClass="text-danger" ID="lblMensajeFacturaRechazada"></asp:Label>
                                <br />
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
                                                <h5>Nro. Factura</h5>
                                                <asp:TextBox runat="server" Enabled="false" ID="txtNroFactura" CssClass="form-control" placeholder="Nro. Factura"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Tipo documento</h5>
                                                <asp:DropDownList Style="width: 100%;" Enabled="false" ID="ddlTipoDocumento" CssClass="form-control" runat="server" DataSourceID="sdsTipoDocumento" DataTextField="tipDocDescripcion" DataValueField="tipoDocumento"></asp:DropDownList>
                                                <asp:SqlDataSource runat="server" ID="sdsTipoDocumento" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_tipoDocumentos"></asp:SqlDataSource>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Fecha factura</h5>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                                                    </div>
                                                    <asp:TextBox Enabled="false" runat="server" CssClass="form-control" ID="txtFechaFactura"></asp:TextBox>
                                                    <%--<input type="text"  class="form-control" runat="server" id="txtFechaFactura" data-inputmask-alias="datetime" data-inputmask-inputformat="dd/mm/yyyy" data-mask>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <h5>Timbrado</h5>
                                                <asp:TextBox runat="server" Enabled="false" ID="txtTimbrado" CssClass="form-control" placeholder="Nro. timbrado"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Monto factura</h5>
                                                <asp:TextBox runat="server" Enabled="false" ID="txtMontoTotal" placeholder="Monto total" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <%-- <div class="col-lg-2">--%>
                                                <h5>Estado documento</h5>
                                                <div class="input-group"> 
                                                    <asp:DropDownList ID="ddlEstadoDocumento" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="Original" Text="Original"></asp:ListItem>
                                                        <asp:ListItem Value="Copia" Text="Copia"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <span class="input-group-append"> 
                                                        <asp:Button runat="server" ID="btnEditar1" OnClick="btnEditar1_Click" CssClass="btn btn-primary" Text="+"></asp:Button>
                                                        <%--<button type="button" runat="server" id="btnEditar" class="btn btn-primary"><i class="nav-icon fas fa-edit"></i></button>--%>
                                                    </span>
                                                    <%--</div>--%>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Nro. asiento</h5>
                                                <asp:TextBox runat="server" Enabled="false" ID="txtNroAsientoCuentaPagar" CssClass="form-control" placeholder="Nro. asiento"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <div class="form-group">
                                                    <h5>Selecciona el Archivo</h5>
                                                    <asp:FileUpload ID="fuArchivo" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <h5>.</h5>
                                                <asp:Button runat="server" ID="btnImportaDatos" OnClick="btnImportaDatos_Click" Text="Importar Datos" CssClass="btn btn-danger" />
                                            </div>
                                            <div class="col-lg-12">
                                                <asp:GridView ID="gvDetalle" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False"
                                                    AllowSorting="True">
                                                    <Columns>
                                                        <%----<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />--%>
                                                        <asp:BoundField DataField="OC" HeaderText="Orden Compras" SortExpression="OC" />
                                                        <asp:BoundField DataField="DOCNOMBRE" HeaderText="Nombre documento" SortExpression="DOCNOMBRE" />
                                                        <asp:TemplateField HeaderText="RUTA" InsertVisible="False" SortExpression="RUTA" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRuta" runat="server" Text='<%# Bind("RUTA") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="..." SortExpression="HistoricoComentarios">
                                                            <ItemTemplate>
                                                                <a class="btn btn-warning"
                                                                    href='<%# Eval("RUTA")%>'
                                                                    target="_blank">Visualizar<span><span></span></span></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                            </div>
                        </div>
                        <div class="card-footer">
                        </div>

                    </div>
                </div>
            </form>
        </div>
    </section>
    <asp:Label runat="server" ID="lblNroOC" Visible="false"></asp:Label>
    <label runat="server" id="lblControl" visible="false"></label>
    <label runat="server" id="lblCantidad" visible="true"></label>
    <asp:Label runat="server" ID="lblDepartamento" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblTipoGestion" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblID" Style="display: none"></asp:Label>

</asp:Content>
