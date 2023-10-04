<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="ImportarDatosTesoreria.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.Tesoreria.ImportarDatosTesoreria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Actualizar datos de tesoreria</h1>
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
                                <h3 class="card-title">Adjuntar archivo csv.</h3>
                                <div class="card-tools pull-right">
                                    <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" CssClass="btn btn-danger" Text="Salir" />
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row" runat="server" id="pnlImportar">

                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <asp:FileUpload ID="fuArchivo" accept=".csv" CssClass="form-control" runat="server" AllowMultiple="true" />
                                            <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="fuArchivo"
                                                ErrorMessage="Solo se permiten Archivos con extensión CSV"
                                                ValidationExpression="(.*\.([Cc][Ss][Vv])$)"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Button runat="server" ID="btnImportaDatos" Text="Importar Datos"
                                            OnClick="btnImportaDatos_Click" CssClass="btn btn-danger" />

                                        <asp:Label ID="lblUsuario" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblDirArchivo" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblExtension" runat="server" Visible="False"></asp:Label>
                                    </div>
                                      <div class="col-lg-1">
                                        <asp:Button runat="server" ID="btnVista" Text="Visualizar Datos"
                                            OnClick="btnVista_Click" CssClass="btn btn-success" /> 
                                    </div>

                                </div>
                                <div class="row" runat="server" id="pnlLoad">
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
</asp:Content>
