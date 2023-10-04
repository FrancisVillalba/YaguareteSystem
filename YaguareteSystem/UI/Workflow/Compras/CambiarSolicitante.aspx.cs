using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Workflow.Compras
{
    public partial class CambiarSolicitante : System.Web.UI.Page
    {
        LogsBLL lg = new LogsBLL();
        GestionDocumentosBLL ges = new GestionDocumentosBLL();
        ProcesosBLL pro = new ProcesosBLL();
        SharePointBLL sharePoint = new SharePointBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            alertError.Visible = false;
            alerCorrecto.Visible = false;

            if (lblControl.InnerText == "")
            {
                lblControl.InnerText = "cargado";
                lblDepartamento.Text = "Compras";
                llenarSolicitante();
            }
        }
        private void llenarSolicitante()
        {
            string path = ConfigurationManager.AppSettings.Get("ActiveDirectory");
            ActiveDirectoryBLL aut = new ActiveDirectoryBLL(path);

            DataTable dt = aut.getTodosUsuarios();
            ddlSolicitanteDe.DataSource = dt;
            ddlSolicitanteDe.DataValueField = "usuario";
            ddlSolicitanteDe.DataTextField = "nombre";
            ddlSolicitanteDe.DataBind();

            ddlSolicitantePara.DataSource = dt;
            ddlSolicitantePara.DataValueField = "usuario";
            ddlSolicitantePara.DataTextField = "nombre";
            ddlSolicitantePara.DataBind();
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Workflow/Compras/Compras.aspx", false);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                alertError.Visible = false;
                alerCorrecto.Visible = false;

                var ocs = sharePoint.get_ocs_filtrados_por_solicitante(ddlSolicitanteDe.SelectedValue, Session["usuario"].ToString(), Session["pass"].ToString());

                if (ocs.value.Count > 0)
                {
                    foreach (var v in ocs.value)
                    {
                        sharePoint.ActualizarSolicitantesMasivamente(v.ID, Session["usuario"].ToString(), Session["pass"].ToString(), ddlSolicitantePara.SelectedValue, ddlSolicitantePara.SelectedItem.ToString());
                    }
                }

                alerCorrecto.Visible = true;
                lblMensajeOK.InnerText = "Actualizado correctamente.!";
            }
            catch (Exception ex)
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Error: "+ ex.Message;
            }

        }

    }
}