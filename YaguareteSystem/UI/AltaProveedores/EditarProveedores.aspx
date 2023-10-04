<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarProveedores.aspx.cs" Inherits="YaguareteSystem.UI.AltaProveedores.EditarProveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Alta de proveedores para OpenKM</h1>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>

    <!-- Main content -->
    <section class="content">
        <form runat="server">
            <div class="container-fluid">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- Input addon -->
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Editar proveedor</h3>
                                <div class="card-tools pull-right">
                                    <asp:Button runat="server" ID="btnCrear" OnClick="btnCrear_Click" CssClass="btn btn-danger" Text="Salir" />
                                </div>

                            </div>
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-lg-3">
                                        <label>Razon social:</label>
                                        <asp:TextBox runat="server" ID="txtRazonSociall" CssClass="form-control"></asp:TextBox>
                                        <%--<input class="form-control" type="text" placeholder="Razón social" id="txtRazonSocial" runat="server" />--%>
                                    </div>
                                    <div class="col-lg-3">
                                        <label>Ruc:</label>
                                        <asp:TextBox runat="server" ID="txtRucc" CssClass="form-control"></asp:TextBox>
                                       <%-- <input class="form-control" type="text" placeholder="RUC" id="txtRuc" runat="server"  />--%>
                                    </div>
                                    <div class="col-lg-3">
                                        <label>Correo:</label>
                                        <asp:TextBox runat="server" ID="txtCorreo" CssClass="form-control"></asp:TextBox>
                                       <%-- <input class="form-control" type="text" placeholder="RUC" id="txtRuc" runat="server"  />--%>
                                    </div>
                                    <div class="col-lg-3">
                                        <label>Código SAP:</label>
                                         <asp:TextBox runat="server" ID="txtCodigoSapp" CssClass="form-control"></asp:TextBox>
                                       <%-- <input class="form-control" type="text" placeholder="Código SAP" id="txtCodigoSap" runat="server" />--%>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-2"></div>
                                    <div class="col-lg-8">
                                        <div class="row">
                                            <asp:Button CssClass="btn btn-primary btn-block" ID="btnAltaProveedores" Text="Modificar proveedor" runat="server" OnClick="btnAltaProveedores_Click" />
                                        </div>
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
                    </div>
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
        </form>
    </section>
    <label runat="server" id="lblID" visible="false"></label>  
    <asp:Label runat="server" ID="lblUsuario" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblPass" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblControl" Visible="false"></asp:Label>
    <script src="/Content/plugins/jquery/jquery.min.js"></script>
     
</asp:Content>
