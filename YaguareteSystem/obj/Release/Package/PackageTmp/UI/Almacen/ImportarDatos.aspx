<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="ImportarDatos.aspx.cs" Inherits="YaguareteSystem.UI.Almacen.ImportarDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Actualizar cantidad de materiales</h1>
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
                                <h3 class="card-title">Actualizar cantidad de materiales</h3>
                            </div>
                            <div class="card-body">
                                <div class="row" runat="server" id="pnlImportar">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <h5>Selecciona el Archivo</h5>
                                                <asp:FileUpload ID="fuArchivo" accept=".csv" CssClass="form-control" runat="server" />
                                                <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="fuArchivo"
                                                    ErrorMessage="Solo se permiten Archivos con extensión CSV"
                                                    ValidationExpression="(.*\.([Cc][Ss][Vv])$)"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                          <div class="col-lg-12">
                                            <asp:Button runat="server" ID="btnImportaDatos" Text="Importar Datos"
                                                OnClick="btnImportaDatos_Click" CssClass="btn btn-danger" />

                                            <asp:Label ID="lblUsuario" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblDirArchivo" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblExtension" runat="server" Visible="False"></asp:Label>
                                        </div>
                                    </div> 
                                </div>
                                <div class="row" runat="server" id="pnlLoad" visible="false">
                                    <div class="card-header">
                                        <img src="/Content/Imagenes/esperar.png" />
                                    </div>
                                    <div class="card-body">
                                        <h4 class="text-muted">Procesando...</h4>
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
