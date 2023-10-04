<%@ Page Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Tesaka.aspx.cs" Inherits="YaguareteSystem.UI.Tesaka.Tesaka" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Genera archivo para Tesaka</h1>
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
                        <!-- Input addon -->
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Archivo Tesaka con buscador</h3>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-3">
                                        Fecha retención:
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                                            </div>
                                            <input type="text" class="form-control" runat="server" id="txtFechaRetencion001" data-inputmask-alias="datetime" data-inputmask-inputformat="dd/mm/yyyy" data-mask>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        Fecha autofactura:
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                                            </div>
                                            <input type="text" class="form-control" runat="server" id="txtFechaFacIni001" data-inputmask-alias="datetime" data-inputmask-inputformat="dd/mm/yyyy" data-mask>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        Empresa:
                                        <asp:DropDownList runat="server" ID="ddlEmpresas" DataSourceID="sdsListaEmpresas" CssClass="form-control" DataTextField="empNombre" DataValueField="empCod"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <br />
                                        <asp:Button CssClass="btn btn-primary" ID="btnGeneraJson" Text="General Json" runat="server" OnClick="btnGeneraJson_Click" />
                                        <asp:Button CssClass="btn btn-success" ID="btnExcel" Text="Control Excel" runat="server" OnClick="btnExcel_Click" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="alert alert-danger alert-dismissible" id="alertError" runat="server" visible="false">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <h5><i class="icon fas fa-ban"></i>Alert!</h5>
                                    <strong runat="server" id="lblMensaje"></strong>
                                </div>
                            </div>
                            <div class="card-footer"></div>
                        </div>
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Archivo Tesaka con importación</h3>
                            </div>
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-lg-3">
                                        Fecha retención:
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                                            </div>
                                            <input type="text" class="form-control" runat="server" id="txtRetencionImportacion" data-inputmask-alias="datetime" data-inputmask-inputformat="dd/mm/yyyy" data-mask>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        Importar Excel:
                                         <div class="form-group">
                                             <asp:FileUpload ID="fuArchivo" accept=".csv" CssClass="form-control" runat="server" />
                                             <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="fuArchivo"
                                                 ErrorMessage="Solo se permiten Archivos con extensión CSV"
                                                 ValidationExpression="(.*\.([Cc][Ss][Vv])$)"></asp:RegularExpressionValidator>

                                         </div>
                                        <asp:Label ID="lblUsuario" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblDirArchivo" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblExtension" runat="server" Visible="False"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <br />
                                        <asp:Button runat="server" ID="btnImportaDatos" Text="Generar Json" OnClick="btnImportaDatos_Click" CssClass="btn btn-primary" /> 
                                        <asp:Button CssClass="btn btn-success" ID="btnExcelImportacion" Text="Control Excel" runat="server" OnClick="btnExcelImportacion_Click" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="alert alert-danger alert-dismissible" id="alertError2" runat="server" visible="false">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <h5><i class="icon fas fa-ban"></i>Alert!</h5>
                                    <strong runat="server" id="lblMensaje2"></strong>
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
    <asp:SqlDataSource ID="sdsListaEmpresas" runat="server" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="SELECT empCod ,empNombre   FROM EmpresasCysa a   WHERE a.empEstado = 'A'"></asp:SqlDataSource>
    <!-- Minified JS library -->
</asp:Content>
