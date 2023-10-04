<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado.aspx.cs" Inherits="YaguareteSystem.UI.Accesos.AccesoDenegado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Responsive Admin &amp; Dashboard Template based on Bootstrap 5">
    <meta name="author" content="Yaguarete System">

    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link rel="shortcut icon" href="img/icons/icon-48x48.png" />

    <title>Cartones Yaguarete S.A.</title>
    <link href="/Content/static/css/app.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;600&display=swap" rel="stylesheet">
</head>

<body>
    <form runat="server"> 
            
            <div class="card-body">
                <div class="row">
                    <div class="col-lg-3"></div>
                    <div class="col-lg-6">
                        <div class="alert alert-danger alert-dismissible" role="alert">
                             
                            <div class="alert-message">
                                <h4 class="alert-heading">Hola
                                <h4 class="alert-heading" runat="server" id="lblUsuario"></h4>
                                </h4>
                                <p>
                                    Te comento que no tiene permiso para está Area por favor contacte con el Administrador
                                </p>
                                <hr>
                                <div class="row">
                                    <div class="col-lg-9"></div>
                                    <div class="col-lg-2">
                                        <div class="text-l mt-3">
                                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-lg center-block btn-danger" OnClick="btnAceptar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div> 
    </form>
    <script src="/Content/static/js/app.js"></script>
</body>
</html>
