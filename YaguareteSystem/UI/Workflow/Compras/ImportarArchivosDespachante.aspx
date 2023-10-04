<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImportarArchivosDespachante.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.Compras.ImportarArchivosDespachante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Vista para importar archivos del despachante</h1>
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
                        <div class="card card-dark">
                            <div class="card-header">
                                <h3 class="card-title">Importar archivo del despachante</h3>

                                <div class="card-tools pull-right">
                                    <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" CssClass="btn btn-danger" Text="Salir" />
                                </div>
                            </div>
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-lg-5">
                                        <div class="form-group">
                                            <asp:FileUpload ID="fuArchivo" CssClass="form-control" runat="server" />
                                            <asp:Label ID="lblRutaFisica" Style="display: none" runat="server"></asp:Label>
                                            <asp:Label ID="lblDatoAdjunto" Visible="false" runat="server"><code>Archivo adjunto.</code></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Button runat="server" ID="btnImportaDatos" Text="Adjuntar" OnClick="btnImportaDatos_Click" CssClass="btn btn-danger" />
                                        <%--<button type='button' id='btnProcesarMasivamente' class='btn btn-primary'>Procesar</button>--%>
                                    </div>

                                </div>
                                <div class="row" id="pnlPrincipal">
                                    <div class="col-lg-12">
                                         
                                             
                                    </div>
                                </div>
                            </div>
                            <div class="card-footer">
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
                    </form>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:Label runat="server" ID="lblCargado" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblUsuario" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblPass" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblNombreUsuario" Style="display: none"></asp:Label>
    <asp:Label ID="lblRutaVirtual" Style="display: none" runat="server"></asp:Label>
    <asp:Label ID="lblNombreArchivo" Style="display: none" runat="server"></asp:Label>
    <asp:Label ID="lblExtension" Style="display: none" runat="server"></asp:Label>
    <asp:Label ID="lblDirArchivo" runat="server" Visible="False"></asp:Label>

    <script src="/Content/plugins/jquery/jquery.min.js"></script>
    <%--<script type="text/javascript">  
        $(document).ready(function () {

        });
    </script>--%>
</asp:Content>
