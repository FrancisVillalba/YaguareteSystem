using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.AltaProveedores
{
    public partial class EditarProveedores : System.Web.UI.Page
    {
        ProcesosBLL p = new ProcesosBLL();
        LogsBLL lg = new LogsBLL();
        GestionDocumentosBLL ges = new GestionDocumentosBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("/Default.aspx");

            }
            if (lblControl.Text == "")
            {
                lblControl.Text = "Cargado";
                lblID.InnerText = Page.Request.QueryString["ID"].ToString();
                cargaDatos();
            }
        }

        private void cargaDatos()
        {

            DataTable dt = ges.RetornaDatosPorProveedor(Convert.ToInt32(lblID.InnerText));

            foreach (DataRow row in dt.Rows)
            {
                txtCodigoSapp.Text = row["provCodigoSAP"].ToString();
                txtRazonSociall.Text = row["provRazonSocial"].ToString();
                txtRucc.Text = row["provRUC"].ToString();
                txtCorreo.Text = row["provCorreo"].ToString();
            }
        }
        protected void btnAltaProveedores_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            lblMensajeError.InnerText = "";
            alerCorrecto.Visible = false;
            lblMensajeOK.InnerText = "";

            if (controlaCampos())
            {
                int resultado = ges.EditarProveedores(txtCodigoSapp.Text, txtRucc.Text, txtRazonSociall.Text, txtCorreo.Text,Convert.ToInt32(lblID.InnerText));

                if (resultado == 1)
                { 
                        lg.registrarLogs(Session["usuario"].ToString(), "EditarProveedor", "Modificación del proveedor. DATOS:" + txtRucc.Text + " - " + txtRazonSociall.Text + " - " + txtRazonSociall.Text);

                        limpiarCampos();
                        alerCorrecto.Visible = true;
                        lblMensajeOK.InnerText = "Proveedor cargado correctamente.";
                        alertError.Visible = false;
                        lblMensajeError.InnerText = ""; 
                }else
                {
                    alerCorrecto.Visible = false;
                    lblMensajeOK.InnerText = "";
                    alertError.Visible = true;
                    lblMensajeError.InnerText = "Problemas para actualizar proveedor";
                }
            }
        }

        private bool controlaCampos()
        {
            if (txtCodigoSapp.Text == "")
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Ingrese el código de SAP del proveedor.";
                return false;
            }

            if (txtRazonSociall.Text == "")
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Ingrese razón social del proveedor.";
                return false;
            }

            if (txtRucc.Text == "")
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Ingrese ruc del proveedor.";
                return false;
            }

            //if (txtCorreo.Text == "")
            //{
            //    alertError.Visible = true;
            //    lblMensajeError.InnerText = "Ingrese correo del proveedor.";
            //    return false;
            //}

            return true;
        }

        private void limpiarCampos()
        {
            txtCodigoSapp.Text = "";
            txtRazonSociall.Text = "";
            txtRucc.Text = "";
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/AltaProveedores/ListarProveedores.aspx");
        }
    }
}