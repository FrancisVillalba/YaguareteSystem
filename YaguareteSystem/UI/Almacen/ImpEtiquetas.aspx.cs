using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;
using YaguareteSystem.pdf;

namespace YaguareteSystem.UI.Almacen
{
    public partial class ImpEtiquetas : System.Web.UI.Page
    {
        ImprimirEtiqueta imp = new ImprimirEtiqueta();
        EtiquetaBLL etiqueta = new EtiquetaBLL();
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
                            lblEmpresa.Text = "003";
                            break;
                        case "KartotecTodos":
                            lblEmpresa.Text = "004";
                            break;
                    }
                }
            }

            //if (Session["codEmpresa"] != null)
            //{
            //    lblEmpresa.Text = Session["codEmpresa"].ToString();
            //}
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            byte[] bytesStream = imp.generaPdfEtiqueta(Convert.ToInt32(ddlMateriales.SelectedValue));

            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "Etiquetas.pdf"));
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytesStream);
            Response.End();
        }
    }
}