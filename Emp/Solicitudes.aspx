<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Solicitudes.aspx.cs" Inherits="webFacturas.Emp.Solicitudes" %>

<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Empleados | Solicitudes</title>

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&subset=latin,cyrillic-ext" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css">

    <!-- Bootstrap Core Css -->
    <link href="../Contents/plugins/bootstrap/css/bootstrap.css" rel="stylesheet">

    <!-- Waves Effect Css -->
    <link href="../Contents/plugins/node-waves/waves.css" rel="stylesheet" />

    <!-- Animation Css -->
    <link href="../Contents/plugins/animate-css/animate.css" rel="stylesheet" />

    <!-- Sweet Alert Css -->
    <link href="../Contents/plugins/sweetalert/sweetalert.css" rel="stylesheet" />
    <!-- SweetAlert Plugin Js -->
    <script src="../Contents/plugins/sweetalert/sweetalert.min.js"></script>

    <!-- Custom Css -->
    <link href="../Contents/css/style.css" rel="stylesheet">

    <!-- AdminBSB Themes. You can choose a theme from css/themes instead of get all themes -->
    <link href="../Contents/css/themes/theme-blue.css" rel="stylesheet" />

</head>

<body class="theme-blue">
    <!-- Page Loader -->
    <div class="page-loader-wrapper">
        <div class="loader">
            <div class="preloader">
                <div class="spinner-layer pl-blue">
                    <div class="circle-clipper left">
                        <div class="circle"></div>
                    </div>
                    <div class="circle-clipper right">
                        <div class="circle"></div>
                    </div>
                </div>
            </div>
            <p>Por favor, espere...</p>
        </div>
    </div>
    <!-- #END# Page Loader -->
    <!-- Overlay For Sidebars -->
    <div class="overlay"></div>
    <!-- #END# Overlay For Sidebars -->
    <!-- Top Bar -->
    <nav class="navbar">
        <div class="container-fluid">
            <div class="navbar-header">
                <a href="javascript:void(0);" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar-collapse" aria-expanded="false"></a>
                <a href="javascript:void(0);" class="bars"></a>
                <a class="navbar-brand" href="../Inicio.aspx"><strong>FULANOS S.A.</strong> - SISTEMA DE REEMBOLSO DE FACTURAS</a>
            </div>
            <div class="collapse navbar-collapse" id="navbar-collapse">
            </div>
        </div>
    </nav>
    <!-- #Top Bar -->
    <section>
        <!-- Left Sidebar -->
        <aside id="leftsidebar" class="sidebar">
            <!-- User Info -->
            <div class="user-info">
                <div class="image">
                    <img src="../Contents/images/user.png" width="48" height="48" alt="User" />
                </div>
                <div class="info-container">
                    <div class="name" aria-haspopup="true" aria-expanded="false">
                        <asp:Label ID="lbUsuario" runat="server"></asp:Label>
                    </div>
                    <div class="email">
                        <asp:Label ID="lbCodigoEmp" runat="server"></asp:Label>
                    </div>
                    <div class="btn-group user-helper-dropdown">
                        <i class="material-icons" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">keyboard_arrow_down</i>
                        <ul class="dropdown-menu pull-right">
                            <li><a href="../salir.aspx"><i class="material-icons">input</i>Cerrar Sesión</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- #User Info -->
            <!-- Menu -->
            <div class="menu">
                <ul class="list">
                    <li class="header">MENÚ PRINCIPAL</li>
                    <li class="active">
                        <a href="#">
                            <i class="material-icons">inbox</i>
                            <span>Solicitudes</span>
                        </a>
                    </li>
                    <li>
                        <a href="../Emp/GenerarSolicitud.aspx">
                            <i class="material-icons">add_circle</i>
                            <span>Nueva solicitud</span>
                        </a>
                    </li>
                </ul>
            </div>
            <!-- #Menu -->
            <!-- Footer -->
            <div class="legal">
                <div class="copyright">
                    &copy; 2020 <a href="javascript:void(0);">Fulanos S.A.</a>.
                </div>
                <div class="version">
                    <b>Version: </b>1.5.0
                </div>
            </div>
            <!-- #Footer -->
        </aside>
        <!-- #END# Left Sidebar -->
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="block-header">
                <h2>Solicitudes realizadas</h2>
            </div>
            <!-- Basic Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card">
                        <div class="header">
                            <h2>Solicitudes
                                <small>Clic en ver para modificar solicitud o ver detalles de esta</small>
                            </h2>
                        </div>
                        <div class="body table-responsive">
                            <form id="frmListaSolicitudes" runat="server">
                                <asp:GridView AutoGenerateColumns="False" CssClass="table" ID="grdViewSolicitudes" runat="server" BorderStyle="None" GridLines="None" OnRowCommand="grdViewSolicitudes_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" />
                                        <asp:BoundField DataField="NoFactura" HeaderText="No. Factura" />
                                        <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                                        <asp:BoundField DataField="MontoTotal" HeaderText="Monto Factura" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                        <asp:ButtonField CommandName="Ver" Text="Ver" ButtonType="Button">
                                            <ControlStyle BorderStyle="None" CssClass="btn btn-info m-t-15 waves-effect" />
                                        </asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </form>

                        </div>
                    </div>
                </div>
            </div>
            <!-- #END# Basic Table -->

        </div>
    </section>

    <!-- Jquery Core Js -->
    <script src="../Contents/plugins/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core Js -->
    <script src="../Contents/plugins/bootstrap/js/bootstrap.js"></script>

    <!-- Select Plugin Js -->
    <script src="../Contents/plugins/bootstrap-select/js/bootstrap-select.js"></script>

    <!-- Slimscroll Plugin Js -->
    <script src="../Contents/plugins/jquery-slimscroll/jquery.slimscroll.js"></script>

    <!-- Waves Effect Plugin Js -->
    <script src="../Contents/plugins/node-waves/waves.js"></script>

    <!-- Custom Js -->
    <script src="../Contents/js/admin.js"></script>
    <script src="../Contents/js/pages/forms/form-validation.js"></script>
    <script src="../Contents/js/pages/forms/basic-form-elements.js"></script>

    <!-- Demo Js -->
    <script src="../Contents/js/demo.js"></script>
</body>

</html>

