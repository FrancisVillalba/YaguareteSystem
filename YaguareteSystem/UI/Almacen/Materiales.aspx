<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Materiales.aspx.cs" Inherits="YaguareteSystem.UI.Almacen.Materiales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Materiales</h1>
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
                                <h3 class="card-title">Lista de materiales</h3>
                            </div>
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:DropDownList Style="width: 100%;" ID="ddlMateriales" CssClass="form-control select2bs4" runat="server" DataSourceID="sdsMateriales" DataTextField="matNombre" DataValueField="matID"></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsMateriales" runat="server" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select -1 matID, '- Todos' matNombre union all
                                            select matID, CONCAT( matCodigo,' - ',  matNombre) matNombre from ys_Materiales t 
                                            inner join ys_CentroDeCostos c on t.matCentroCostos = c.cenCodigo
                                            where t.matEstado = 'S' and c.cenEmpresaCysa = @codEmpresa
                                            ">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="lblCodEmpresa" Name="codEmpresa" PropertyName="Text" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-primary " Text="Buscar" OnClick="btnBuscar_Click" />
                                        <asp:Button runat="server" ID="btnExportExcel" CssClass="btn btn-success " Text="Exportar excel" OnClick="btnExportExcel_Click" />
                                    </div> 
                                    <div class="col-lg-1">
                                        <asp:Button runat="server" ID="btnCrearMaterial" CssClass="btn btn-success" Text="+ Crear" OnClick="btnCrearMaterial_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <br />
                                        <asp:GridView ID="gvMaterial" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False"
                                            AllowPaging="True" OnPageIndexChanging="gvMaterial_PageIndexChanging" PageSize="20">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                                <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL" SortExpression="MATERIAL" />
                                                <asp:BoundField DataField="CODIGO" HeaderText="CÓDIGO" SortExpression="CODIGO" />
                                                <asp:BoundField DataField="CANTIDAD" HeaderText="CANTIDAD" SortExpression="CANTIDAD" />
                                                <asp:BoundField DataField="CENTRO_COSTO" HeaderText="CENTRO COSTO" SortExpression="CENTRO_COSTO" />
                                                <asp:BoundField DataField="UBICACION" HeaderText="UBICACIÓN" SortExpression="UBICACION" />
                                                <asp:BoundField DataField="UNIDAD_MEDIDA" HeaderText="UNIDAD MEDIDA" SortExpression="UNIDAD_MEDIDA" />
                                                <asp:BoundField DataField="FECHA" HeaderText="FECHA" SortExpression="FECHA" />
                                                <asp:TemplateField HeaderText="...">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" OnClick="btnEditar_Click" ID="btnEditar"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
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

    <asp:Label runat="server" ID="lblCodEmpresa" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblEditar" Visible="false"></asp:Label>
    
</asp:Content>
