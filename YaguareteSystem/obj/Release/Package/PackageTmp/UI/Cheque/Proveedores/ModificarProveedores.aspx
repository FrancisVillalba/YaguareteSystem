<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarProveedores.aspx.cs" Inherits="YaguareteSystem.UI.Cheque.Proveedores.ModificarProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Modificar proveedores</h1>
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
                    <form runat="server">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Modificar proveedores</h3>
                            </div>
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" ID="txtRuc" CssClass="form-control" placeholder="Ingrese RUC del proveedor"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" ID="txtNomProveedor" CssClass="form-control" placeholder="Ingrese nombre del proveedor"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" Text="Guardar" CssClass="btn btn-success" />
                                        <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" Text="Salir" CssClass="btn btn-secondary" />
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
                            <div class="card-footer"></div>
                        </div>
                    </form>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>
     
</asp:Content>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
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
                            <div class="col-sm-1"></div>
                            <div class="col-sm-3">
                                <asp:TextBox runat="server" ID="txtRuc" CssClass="form-control" placeholder="Ingrese RUC del proveedor"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox runat="server" ID="txtNomProveedor" CssClass="form-control" placeholder="Ingrese nombre del proveedor"></asp:TextBox>
                            </div>
                            <div class="col-lg-2">
                                <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" Text="Modificar" CssClass="btn btn-success" />
                                <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" Text="Salir" CssClass="btn btn-secondary" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
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
            </div>
        </form>
    </body>
    </html>
    <asp:Label runat="server" ID="lblRucProveedor" Visible="false"></asp:Label>
</asp:Content>--%>

