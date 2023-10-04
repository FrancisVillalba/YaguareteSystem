<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="YaguareteSystem.Default" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" href="/Content/Imagenes/logito.png" />
    <title>Cartones Yaguarete S.A.</title>

    <!-- Google Font: Source Sans Pro -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="Content/plugins/fontawesome-free/css/all.min.css">
    <!-- icheck bootstrap -->
    <link rel="stylesheet" href="Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="Content/dist/css/adminlte.min.css">
</head>
<body class=" login-page">
    <br />
    <div class="text-center">
        <img src="/Content/Imagenes/yaguarete.PNG" alt="Charles Hall" class="img-fluid" />
    </div>
    <br />   
        <div class="text-center mt-4">
            <h1 class="h2">Bienvenido a YAGUARETE SYSTEM</h1>
        </div>
        <!-- /.login-logo -->
        <div class="">
            <div class="login-card-body">
                <p class="login-box-msg">Inicie sesión con su cuenta de windows.</p>

            <form runat="server">
                <div class="input-group mb-3">
                    <input type="text" runat="server" id="txtUsuario" class="form-control" placeholder="Usuario">
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-envelope"></span>
                        </div>
                    </div>
                </div>
                <div class="input-group mb-3">
                    <input type="password" runat="server" id="txtPass" class="form-control" placeholder="Password">
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-lock"></span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <%-- <div class="icheck-primary">
                                <input type="checkbox" id="remember">
                                <label for="remember">
                                    Recordar pass
             
                                </label>
                            </div>--%>

                        <!-- /.col -->

                        <asp:Button runat="server" ID="btnLogear" CssClass="btn-lg btn-block btn-primary" OnClick="btnLogear_Click" Text="Login" />
                    </div>
                </div>
                <br />

                <div runat="server" class="alert alert-danger alert-dismissible" id="alertError">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <h5><i class="icon fas fa-ban"></i>Alert!</h5>
                    <div class="alert-message">
                        <strong runat="server" id="lblMensaje"></strong>
                    </div>
                </div>
            </form>
        </div>
        <!-- /.login-card-body -->
    </div>

    <!-- /.login-box -->

    <!-- jQuery -->
    <script src="Content/plugins/jquery/jquery.min.js"></script>
    <!-- Bootstrap 4 -->
    <script src="Content/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <!-- AdminLTE App -->
    <script src="Content/dist/js/adminlte.min.js"></script>
</body>
</html>

<%--<!DOCTYPE html> 
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Responsive Admin &amp; Dashboard Template based on Bootstrap 5">
    <meta name="author" content="AdminKit">
    <meta name="keywords" content="adminkit, bootstrap, bootstrap 5, admin, dashboard, template, responsive, css, sass, html, theme, front-end, ui kit, web">

    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link rel="shortcut icon" href="../Content/static/img/icons/icon-48x48.png" />

    <title>Login | Yaguarete System S.A.</title>

    <link href="/Content/static/css/app.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;600&display=swap" rel="stylesheet">
</head>

<body>
    <main class="d-flex w-100">
        <div class="container d-flex flex-column">
            <div class="row vh-100">
                <div class="col-sm-10 col-md-8 col-lg-6 mx-auto d-table h-100">
                    <div class="d-table-cell align-middle">

                        <div class="text-center mt-4">
                            <h1 class="h2">Bienvenido a YAGUARETE SYSTEM</h1>
                            <p class="lead">
                                Inicie sesión con su cuenta de windows.
                            </p>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="m-sm-4">
                                    <div class="text-center">
                                         <img src="/Content/Imagenes/yaguarete.PNG" alt="Charles Hall" class="img-fluid"/>
                                    </div>
                                    <form runat="server" id="pnlForm">
                                        <div class="mb-3">
                                            <label class="form-label">Usuario</label>
                                            <input class="form-control form-control-lg" id="txtUsuario" runat="server"  type="text" name="text" placeholder="Ingrese usuario" />
                                        </div>
                                        <div class="mb-3">
                                            <label class="form-label">Password</label>
                                            <input class="form-control form-control-lg" id="txtPass" runat="server" type="password" name="password" placeholder="Ingrese su contraseña" />
                                        </div>
                                        <div>
                                            <label class="form-check">
                                                <input class="form-check-input" type="checkbox" value="remember-me" name="remember-me" checked>
                                                <span class="form-check-label">Recordar mi contraseña</span>
                                            </label>
                                        </div>
                                        <div class="alert alert-danger alert-dismissible" runat="server" id="alertError" visible="false" role="alert">
											<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
											<div class="alert-message">
												<strong runat="server" id="lblMensaje"></strong>
											</div>
										</div>
                                        <div class="text-center mt-3">
                                            
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main> 
    <script src="/Content/static/js/app.js"></script> 
</body> 
</html>--%>
