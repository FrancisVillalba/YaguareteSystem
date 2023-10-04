<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambiarSolicitante.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.Compras.CambiarSolicitante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Cambiar de solicitante de las ordenes de compras en forma masiva</h1>
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
                                <h3 class="card-title">Cambiar de solicitante las ordenes de compras en forma masiva</h3>

                                <div class="card-tools pull-right">
                                    <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" CssClass="btn btn-danger btn-lg" Text="Salir" />
                                </div>
                            </div>
                            <div class="card-body"> 
                                <div class="row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-3">
                                        De:
                                        <asp:DropDownList Style="width: 100%;" ID="ddlSolicitanteDe" CssClass="form-control select2bs4" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        Para:
                                        <asp:DropDownList Style="width: 100%;" ID="ddlSolicitantePara" CssClass="form-control select2bs4" runat="server"></asp:DropDownList>
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
                                    <div class="col-lg-11">
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary btn-lg" Text="Guardar" />
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
    <label runat="server" id="lblControl" visible="false"></label>
    <asp:Label runat="server" ID="lblDepartamento" Visible="false"></asp:Label>
</asp:Content>
