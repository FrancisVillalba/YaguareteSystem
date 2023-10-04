<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaMateriales.aspx.cs" Inherits="YaguareteSystem.UI.Almacen.AltaMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Alta de materiales</h1>
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
                                <h3 class="card-title">Alta de materiales</h3>
                            </div>
                            <div class="card-body"> 
                                <div class="row">
                                    <div class="col-lg-3">
                                        <h5>Nombre material</h5>
                                        <asp:TextBox runat="server" ID="txtNombreMaterial" CssClass="form-control" placeholder="Nombre material"></asp:TextBox>
                                    </div>

                                    <div class="col-lg-3">
                                        <h5>Código SAP</h5>
                                        <input class="form-control" id="txtCodigo" type="text" placeholder="Código SAP" runat="server">
                                        <asp:CompareValidator runat="server" CssClass="text-danger" ID="CompareValidator2" ControlToValidate="txtCodigo"
                                            MaxLength="50" Display="Dynamic" ErrorMessage="Ingresar solo números" Operator="DataTypeCheck"
                                            Type="Integer"> </asp:CompareValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Centro de costos</h5>
                                        <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="form-control" DataSourceID="sdsCentroCosto" DataTextField="cenNombre" DataValueField="cenCodigo"></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsCentroCosto" runat="server" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_centroCosto"></asp:SqlDataSource>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Cantidad</h5>
                                        <input class="form-control" id="txtCantidad" type="text" placeholder="Cantidad" runat="server">
                                        <asp:CompareValidator runat="server" CssClass="text-danger" ID="CompareValidator1" ControlToValidate="txtCantidad"
                                            MaxLength="50" Display="Dynamic" ErrorMessage="Ingresar solo números" Operator="DataTypeCheck"
                                            Type="Integer"> </asp:CompareValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <h5>Unidad de medida</h5>
                                        <asp:DropDownList ID="ddlUnidadMedida" runat="server" CssClass="form-control" DataSourceID="sdsUnidadMedida" DataTextField="medNombre" DataValueField="medID"></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsUnidadMedida" runat="server" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select * from vw_unidadMedida"></asp:SqlDataSource>
                                    </div>

                                    <div class="col-lg-3">
                                        <h5>Ubicación</h5>
                                        <asp:TextBox runat="server" ID="txtUbicacion" CssClass="form-control" placeholder="Ubicación"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-3">
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
                                        <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" CssClass="btn btn-secondary" Text="Salir" />
                                        <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" Text="Guardar" />
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


    <%-- <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
    </head>
    <body>
        <form runat="server">
            <div class="container-fluid p-0">

                <h1 class="h3 mb-3">Alta de proveedores</h1>
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-10">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <h5 class="card-title mb-0">Razón social</h5>
                                        <input class="form-control" type="text" placeholder="Razón social" id="txtRazonSocial" runat="server" required="required" />
                                    </div>
                                    <div class="col-lg-4">
                                        <h5 class="card-title mb-0">Ruc</h5>
                                        <input class="form-control" type="text" placeholder="RUC" id="txtRuc" runat="server" required="required" />
                                    </div>
                                    <div class="col-lg-4">
                                        <h5 class="card-title mb-0">Código SAP</h5>
                                        <input class="form-control" type="text" placeholder="Código SAP" id="txtCodigoSap" runat="server" required="required" />
                                        <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtCodigoSap" CssClass="text-danger"
                                            MaxLength="50" Display="Dynamic" ErrorMessage="Ingresar solo Números sin punto(.) ni comas(,)" Operator="DataTypeCheck" Type="Integer"> </asp:CompareValidator>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                                <div class="row">
                                    <asp:Button CssClass="btn btn-primary btn-block" ID="btnAltaProveedores" Text="Alta proveedores" runat="server" OnClick="btnAltaProveedores_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="alert alert-danger alert-dismissible" runat="server" id="alertError" visible="false" role="alert">
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                            <div class="alert-message">
                                <strong runat="server" id="lblMensajeError"></strong>
                            </div>
                        </div>
                        <div class="alert alert-success alert-dismissible" runat="server" id="alerCorrecto" visible="false" role="alert">
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                            <div class="alert-message">
                                <strong runat="server" id="lblMensajeOK"></strong>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </form>
    </body>
    </html>--%>
</asp:Content>
