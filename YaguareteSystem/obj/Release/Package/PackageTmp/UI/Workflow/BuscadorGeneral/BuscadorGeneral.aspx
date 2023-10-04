<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuscadorGeneral.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.BuscadorGeneral.BuscadorGeneral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Buscador general</h1>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <form runat="server">
        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- Input addon -->

                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Buscador</h3>
                            </div>
                            <div class="card-body">
                                <div class="card card-gray">
                                    <div class="card-header">
                                        <h3 class="card-title">Filtrar datos</h3>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <h5>Departamento</h5>
                                                <asp:DropDownList runat="server" ID="ddlDepartamento" DataSourceID="sdsDepartamento" CssClass="form-control" DataTextField="fNombre" DataValueField="fNombreCorto"></asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsDepartamento" runat="server" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="SELECT '-1' fNombreCorto, 'Todos' fNombre union all SELECT fNombreCorto, fNombre FROM ys_WorkflowFases s"></asp:SqlDataSource>

                                            </div>
                                            <div class="col-lg-2">
                                                <h5>Tipo legajo</h5>
                                                <asp:DropDownList runat="server" ID="ddlTipoLegajo" CssClass="form-control" DataSourceID="sdsTipoLegajo" DataTextField="tipDocDescripcion" DataValueField="tipoDocumento"></asp:DropDownList>
                                                <asp:SqlDataSource runat="server" ID="sdsTipoLegajo" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select '-1' tipoDocumento, 'Todos' tipDocDescripcion
                                                union all
                                                select S.tipoDocumento, S.tipDocDescripcion from ys_WorkflowTipoLegajo s where s.tipDocEstado = 'S'"></asp:SqlDataSource>
                                            </div>

                                            <div class="col-lg-2">
                                                <h5>Nro. Factura</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNroFactura" placeholder="Número factura"></asp:TextBox>
                                            </div>

                                            <div class="col-lg-2">
                                                <h5>Nro. Proyecto</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNroProyecto" placeholder="Número proyecto"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <h5>Nro. OC</h5>
                                                <asp:DropDownList ID="ddlNroOC" CssClass="form-control select2bs4" runat="server" DataSourceID="sdsNroOc" DataTextField="descripcion" DataValueField="valor"></asp:DropDownList>
                                                <asp:SqlDataSource runat="server" ID="sdsNroOc" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="SELECT '-1' valor, 'Todos' descripcion 
                                        union all
                                        SELECT   oComNroOC valor,oComNroOC descripcion
                                          FROM ys_OrdenCompras"></asp:SqlDataSource>
                                            </div>
                                            <div class="col-lg-2">
                                                <h5>Acciones</h5>
                                                <asp:DropDownList ID="ddlAccion" CssClass="form-control select2bs4" runat="server" DataSourceID="sdsTipoAccion" DataTextField="movDescripcion" DataValueField="movID"></asp:DropDownList>
                                                <asp:SqlDataSource runat="server" ID="sdsTipoAccion" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="  select * from vw_listaAccion"></asp:SqlDataSource>
                                            </div>
                                            <%-- <div class="col-lg-2">
                                        <h5>Proveedor</h5>
                                        <asp:DropDownList ID="ddlProveedor" CssClass="form-control select2bs4" runat="server" DataSourceID="sdsProveedor" DataTextField="proveedor" DataValueField="provID"></asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="sdsProveedor" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select -1 provID, 'Todos' proveedor union all
                                        select s.provCodigoSAP, CONCAT(s.provRUC,' - ',s.provRasonSocial) proveedor from ys_Proveedores s where s.provEstado = 'S'"></asp:SqlDataSource>
                                    </div>--%>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <h5>Comprador</h5>
                                                <asp:DropDownList ID="ddlListaCompradores" CssClass="form-control select2bs4" runat="server" DataSourceID="sdsListaCompradores" DataTextField="comNombre" DataValueField="comUsuario"></asp:DropDownList>
                                                <asp:SqlDataSource runat="server" ID="sdsListaCompradores" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="  select * from vw_listaCompradores order by comNombre asc"></asp:SqlDataSource>

                                            </div>
                                            <div class="col-lg-2">
                                                <h5>DEG</h5>
                                                <asp:DropDownList runat="server" ID="ddlEsDEG" CssClass="form-control">
                                                    <asp:ListItem Value="-1" Text="Todos"></asp:ListItem>
                                                    <asp:ListItem Value="SI" Text="SI"></asp:ListItem>
                                                    <asp:ListItem Value="NO" Text="NO"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-8">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer">
                                        <div class="row">
                                            <div class="col-lg-10">
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:Button runat="server" ID="btnExportExcel" CssClass="btn btn-success " Text="Excel" OnClick="btnExportExcel_Click" />
                                                <asp:Button runat="server" ID="btnBuscar" OnClick="btnBuscar_Click" CssClass="btn btn-primary" Text="Buscar" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:GridView ID="gvBuscador" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                                    <asp:BoundField DataField="NroFactura" HeaderText="Nro Factura" SortExpression="NroFactura" />
                                                    <asp:BoundField DataField="NroOC" HeaderText="Nro OC" SortExpression="NroOC" />
                                                    <asp:BoundField DataField="Departamento" HeaderText="Departamento" SortExpression="Departamento" />
                                                    <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo legajo" SortExpression="TipoDocumento" />
                                                    <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" SortExpression="Proveedor" />
                                                    <asp:BoundField DataField="Comprador" HeaderText="Comprador" SortExpression="Ruc" />
                                                    <%--<asp:BoundField DataField="CodigoSAP" HeaderText="Código SAP" SortExpression="CodigoSAP" />--%>
                                                    <asp:BoundField DataField="Accion" HeaderText="Acción" SortExpression="Accion" />
                                                    <asp:BoundField DataField="Comentario" ItemStyle-Font-Size="Small" HeaderText="Comentario" SortExpression="Comentario" />
                                                    <asp:TemplateField HeaderText="Información" SortExpression="HistoricoComentarios">
                                                        <ItemTemplate>
                                                            <a
                                                                href='/UI/Workflow/BuscadorGeneral/InformacionFactura.aspx?ID=<%# Eval("ID")%>'
                                                                target="_blank">Ver<span><span></span></span></a>
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
                            <div class="card-footer">
                            </div>
                        </div>
                        <!-- /.row -->
                    </div>
                    <!-- /.container-fluid -->
                </div>
            </div>
        </section>
    </form>
    <asp:Label runat="server" ID="lblCargado" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblUsuario" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblPass" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblNombreUsuario" Style="display: none"></asp:Label>
    <script src="/Content/plugins/jquery/jquery.min.js"></script>
</asp:Content>
