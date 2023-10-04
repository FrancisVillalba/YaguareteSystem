using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Workflow.Impuestos
{
    public partial class Impuestos : System.Web.UI.Page
    {
        ProcesosBLL pro = new ProcesosBLL();
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
                lblNombreUsuario.Text = Session["NombreApellido"].ToString();
            }
        }

        //protected void btnImportaDatos_Click(object sender, EventArgs e)
        //{

        //    if (!fuArchivo.HasFile)
        //    {
        //        alertError.Visible = true;
        //        lblMensajeError.InnerText = "Campo vacio";
        //    }
        //    else
        //    {

        //        ImportaArchivo();
        //    }
        //}
        //private void ImportaArchivo()
        //{
        //    DataTable dr = new DataTable();

        //    try
        //    {
        //        foreach (HttpPostedFile postedFile in fuArchivo.PostedFiles)
        //        {
        //            string PathFisico = ConfigurationManager.AppSettings.Get("RutaFisicaDocumentos") + Session["usuario"].ToString() + @"\";
        //            string PathVirtual = ConfigurationManager.AppSettings.Get("RutaVirtualDocumentos") + Session["usuario"].ToString() + @"/";

        //            var FileExtension = System.IO.Path.GetExtension(postedFile.FileName).Substring(0);
        //            string FileNombre = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);


        //            if (pro.creaDirectorio(PathFisico))
        //            {
        //                string direccionArchivoFisico = PathFisico + FileNombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + FileExtension;
        //                string direccionArchivoVirtual = PathVirtual + FileNombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + FileExtension;

        //                fuArchivo.SaveAs(direccionArchivoFisico);

        //                lblRutaFisica.Text = direccionArchivoFisico; 

        //            }
        //            else
        //            {
        //                alertError.Visible = true;
        //                lblMensajeError.InnerText = "No se pudo importar archivos. Verifique";
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        alertError.Visible = true;
        //        lblMensajeError.InnerText = "No se pudo Importar. Verifique";
        //    }
        //}
 
    }
}