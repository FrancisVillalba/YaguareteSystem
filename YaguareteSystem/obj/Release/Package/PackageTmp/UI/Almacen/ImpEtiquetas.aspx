<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImpEtiquetas.aspx.cs" Inherits="YaguareteSystem.UI.Almacen.ImpEtiquetas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Imprimir etiquetas</h1>
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
                            <h3 class="card-title">Impresión de etiquetas</h3>
                        </div>
                        <div class="card-body">
                            <form runat="server">
                                <div class="row">
                                    <div class="col-lg-4">
                                        Material:
                                        <asp:DropDownList Style="width: 100%;" ID="ddlMateriales" CssClass="form-control select2bs4" runat="server" DataSourceID="sdsMateriales" DataTextField="matNombre" DataValueField="matID"></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsMateriales" runat="server" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="sp_retornaListaMateriales" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="lblEmpresa" Name="empresa" PropertyName="Text" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div class="col-lg-1">
                                        <br />
                                        <asp:Button CssClass="btn btn-success" ID="btnImprimir" Text="Imprimir" runat="server" OnClick="btnImprimir_Click" />
                                    </div>
                                </div>
                                <br />
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
                        </div><div class="card-footer">
                    </div>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
            </div>
    </section>
     <asp:Label runat="server" ID="lblEmpresa" Visible="false"></asp:Label>
</asp:Content>
