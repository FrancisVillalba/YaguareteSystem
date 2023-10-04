using ClosedXML.Excel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Almacen
{
    public partial class Materiales : System.Web.UI.Page
    {
        AlmacenBLL p = new AlmacenBLL();
        LogsBLL lg = new LogsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("/Default.aspx");
            }

            ArrayList list = (ArrayList)Session["grupos"];
            if (list.Count > 0)
            {
                foreach (string element in list)
                {
                    switch (element)
                    {
                        case "CysaTodos":
                            lblCodEmpresa.Text = "003";
                            break;
                        case "KartotecTodos":
                            lblCodEmpresa.Text = "004";
                            break;
                    }
                }
            }

            if(lblEditar.Text == "")
            {
                llenarGrilla("TODO", 0, lblCodEmpresa.Text);
                lblEditar.Text = "Cargado";
            }
            
        }

        private void llenarGrilla(string accion, int materialID, string codEmpresa)
        {
            gvMaterial.DataSource = null;
            DataTable dt = p.retornaDatosMaterial(accion, materialID, codEmpresa);
            gvMaterial.DataSource = dt;
            gvMaterial.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            Session["materialID"] = clickedRow.Cells[0].Text; 

            Response.Redirect("/UI/Almacen/ModificarMateriales.aspx");
        }

        protected void btnCrearMaterial_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Almacen/AltaMateriales.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrilla("FILTRADO", Convert.ToInt32(ddlMateriales.SelectedValue), lblCodEmpresa.Text);
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            XLWorkbook wb = new XLWorkbook(); 

            DataTable dt = p.retornaDatosMaterial("TODO", 0, lblCodEmpresa.Text);
            wb.Worksheets.Add(dt, "WorksheetName");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        protected void gvMaterial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}