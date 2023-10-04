
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using YaguareteSystem.Clases;

namespace YaguareteSystem.UI.Almacen
{
    public partial class ImportarDatos : System.Web.UI.Page
    {
        AlmacenBLL p = new AlmacenBLL();
        ProcesosBLL pro = new ProcesosBLL();
        LogsBLL lg = new LogsBLL();
        BackgroundWorker tarea = new BackgroundWorker();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImportaDatos_Click(object sender, EventArgs e)
        { 
            pnlLoad.Visible = true;
            pnlImportar.Visible = false;

            if (!fuArchivo.HasFile)
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Campo vacio";
            }
            else
            { 
                tarea.DoWork += ImportaArchivo;
                tarea.RunWorkerAsync();
            }
        }
        private void ImportaArchivo(object o, DoWorkEventArgs e)
        {
            DataTable dr = new DataTable();
            alertError.Visible = false;
            alerCorrecto.Visible = false;

            string FileName = Path.GetFileName(fuArchivo.PostedFile.FileName);
            lblExtension.Text = Path.GetExtension(fuArchivo.PostedFile.FileName);
            if (lblExtension.Text == ".csv" || lblExtension.Text == ".CSV")
            {
                try
                {
                    string FolderPath = @"C:\ruta\";
                    if (pro.creaDirectorio(FolderPath))
                    {
                        lblDirArchivo.Text = FolderPath + FileName;
                        fuArchivo.SaveAs(FolderPath + FileName);
                        dr = pro.importarTxtCamp(lblDirArchivo.Text);
                        if (dr.Rows.Count > 5000)
                        {
                            alertError.Visible = true;
                            lblMensajeError.InnerText = "Solo puede importar hasta 5.000 Registros. Verifique";
                        }
                        else
                        { 
                            int control = p.actualizaCantidad(dr);

                            if (control == 1)
                            {
                                pnlLoad.Visible = false;
                                pnlImportar.Visible = true;
                                alerCorrecto.Visible = true;
                                lblMensajeOK.InnerText = "Cantidades actualizado correctamente";
                            }
                            else
                            {
                                pnlLoad.Visible = false;
                                pnlImportar.Visible = true;
                                alertError.Visible = true;
                                lblMensajeError.InnerText = "Algo salio mal al actualizar las cantidades";
                            }
                            //habilitarPaneles(false, true);
                        }
                    }

                    else
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "No se pudo importar registros. Verifique";
                    }

                }
                catch
                {
                    alertError.Visible = true;
                    lblMensajeError.InnerText = "No se pudo Importar. Verifique";
                }
            }
            else
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Archivo No Valido. Verifique";
            }
        } 
    }
}