<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarMateriales.aspx.cs" Inherits="YaguareteSystem.UI.Almacen.ModificarMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Modificar materiales</h1>
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
                                <h3 class="card-title">Modificar materiales</h3>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <h5>Nombre material</h5>
                                        <asp:TextBox runat="server" ID="txtNombreMaterial" CssClass="form-control" placeholder="Nombre material"></asp:TextBox>
                                    </div>

                                    <div class="col-lg-3">
                                        <h5>Código SAP</h5>
                                        <input class="form-control" id="txtCodigo" type="text" placeholder="Cantidad" runat="server">
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
                                        <asp:Button runat="server" ID="btnModificar" OnClick="btnModificar_Click" CssClass="btn btn-primary" Text="Guardar" />
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
    <asp:Label ID="lblMaterialID" Visible="false" runat="server"></asp:Label>
</asp:Content>
