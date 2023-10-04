<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarcarTransferidoCompras.aspx.cs" Inherits="YaguareteSystem.UI.MarcarTransferido.MarcarTransferidoCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Marcar como transferico para compras</h1>
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
                    <div class="card card-info">
                        <div class="card-header">
                            <h3 class="card-title">Marcar como transferido</h3>
                        </div>
                        <div class="card-body">
                            <form runat="server">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label>Nro. proceso:</label>
                                        <input class="form-control" type="text" placeholder="Nro. Proceso" id="txtNroProceso" runat="server" required="required" />
                                        <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtNroProceso" CssClass="text-danger"
                                            MaxLength="50" Display="Dynamic" ErrorMessage="Ingresar solo Números" Operator="DataTypeCheck" Type="Integer"> </asp:CompareValidator>
                                    </div>
                                    <div class="col-lg-2">
                                        <label>Procesar:</label>
                                        <asp:Button CssClass="btn btn-primary" ID="btnMarcarTransferido" Text="Marcar como transferido" runat="server" OnClick="btnMarcarTransferido_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="alert alert-success alert-dismissible" id="alerCorrecto" runat="server" visible="false">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <h5><i class="icon fas fa-check"></i>Exitoso!</h5>
                                    <strong runat="server" id="lblMensajeOK"></strong>
                                </div>

                                <div class="alert alert-danger alert-dismissible" id="alertError" runat="server" visible="false">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <h5><i class="icon fas fa-ban"></i>Error!</h5>
                                    <strong runat="server" id="lblMensajeError"></strong>
                                </div>
                            </form>
                        </div>
                        <div class="card-footer"></div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>

</asp:Content>
