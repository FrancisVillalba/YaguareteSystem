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

namespace YaguareteSystem.UI.MarcarTransferido
{
    public partial class MarcarTransferidoCompras : System.Web.UI.Page
    {
        ProcesosBLL p = new ProcesosBLL();
        LogsBLL lg = new LogsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool controlaCampos()
        {

            if (txtNroProceso.Value == "")
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Ingrese número de proceso.";
                return false;
            }

            return true;
        }

        private void limpiarCampos()
        {
            txtNroProceso.Value = "";
        }

        protected void btnMarcarTransferido_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (controlaCampos())
            {
                ds = p.sp_marcarTransferidoCompras(txtNroProceso.Value);
                string nroProceso = txtNroProceso.Value;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["ID"].ToString() != "1")
                    {
                        lg.registrarLogs(Session["usuario"].ToString(), "MarcarTransferidoCompras", "Error al marcar como transferido proceso Nro: " + txtNroProceso.Value);

                        limpiarCampos();
                        alertError.Visible = true;
                        lblMensajeError.InnerText = row["MENSAJE"].ToString();
                        alerCorrecto.Visible = false;
                        lblMensajeOK.InnerText = "";

                        return;
                    }
                    lg.registrarLogs(Session["usuario"].ToString(), "MarcarTransferidoCompras", "Se marcó como transferido el proceso: " + txtNroProceso.Value);
                    limpiarCampos(); 
                    alerCorrecto.Visible = true;
                    lblMensajeOK.InnerText = "Proceso Nro. "+ nroProceso + " marcado como transferido. ";
                    alertError.Visible = false;
                    lblMensajeError.InnerText = ""; 
                }
            }
        }
    }
}