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
    public partial class ListarProveedores : System.Web.UI.Page
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

            if (lblCargado.Text == "")
            {
                lblCargado.Text = "CARGADO";
                lblUsuario.Text = Session["usuario"].ToString();
                lblPass.Text = Session["pass"].ToString();
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/AltaProveedores/AltaProveedores.aspx");
        }



        //protected void btnAltaProveedores_Click(object sender, EventArgs e)
        //{
        //    alertError.Visible = false;
        //    lblMensajeError.InnerText = "";
        //    alerCorrecto.Visible = false;
        //    lblMensajeOK.InnerText = ""; 

        //    if (controlaCampos())
        //    {
        //        DataTable dt = ges.InsertaProveedor(txtCodigoSap.Value, txtRuc.Value, txtRazonSocial.Value);

        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                if (row["ID"].ToString() == "-1")
        //                {
        //                    lg.registrarLogs(Session["usuario"].ToString(), "AltaProveedores", "PROBLEMAS " + "El proveedor ya existe"+ ". DATOS:" + txtRuc.Value + " - " + txtRazonSocial.Value + " - " + txtCodigoSap.Value);

        //                    //limpiarCampos();
        //                    alertError.Visible = true;
        //                    lblMensajeError.InnerText = "Proveedor ya existe.";
        //                    alerCorrecto.Visible = false;
        //                    lblMensajeOK.InnerText = "";

        //                    return;
        //                }

        //                lg.registrarLogs(Session["usuario"].ToString(), "AltaProveedores", "Alta a nuevo proveedor. DATOS:" + txtRuc.Value + " - " + txtRazonSocial.Value + " - " + txtCodigoSap.Value);

        //                limpiarCampos();
        //                alerCorrecto.Visible = true;
        //                lblMensajeOK.InnerText = "Proveedor cargado correctamente.";
        //                alertError.Visible = false;
        //                lblMensajeError.InnerText = "";

        //            }

        //        }
        //    }
        //}

        //private bool controlaCampos()
        //{
        //    if (txtCodigoSap.Value == "")
        //    {
        //        alertError.Visible = true;
        //        lblMensajeError.InnerText = "Ingrese el código de SAP del proveedor.";
        //        return false;
        //    }

        //    if (txtRazonSocial.Value == "")
        //    {
        //        alertError.Visible = true;
        //        lblMensajeError.InnerText = "Ingrese razón social del proveedor.";
        //        return false;
        //    }

        //    return true;
        //}

        //private void limpiarCampos()
        //{
        //    txtCodigoSap.Value = "";
        //    txtRazonSocial.Value = "";
        //    txtRuc.Value = "";
        //}
    }
}