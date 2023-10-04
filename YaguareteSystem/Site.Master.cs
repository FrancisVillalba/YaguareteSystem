using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;

namespace YaguareteSystem
{
    public partial class SiteMaster : MasterPage
    {
        AccesosBLL ac = new AccesosBLL();
        AlertasBLL al = new AlertasBLL();
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    int datoAccedo = 0;
        //    if (Session["usuario"] == null)
        //    {
        //        Response.Redirect("/Default.aspx");
        //    }

        //    lblEmpleado.InnerText = Session["NombreApellido"].ToString();

        //    string nomInterfaz = System.IO.Path.GetFileName(Request.ServerVariables["SCRIPT_NAME"]);

        //    DataTable dt = (DataTable)Session["grupos"];

        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {  
        //            datoAccedo = ac.ControlaAcceso(nomInterfaz, row["ROL"].ToString());

        //            if (datoAccedo == 1)
        //            { 
        //                return;
        //            }
        //        } 

        //        Response.Redirect("/UI/Accesos/AccesoDenegado.aspx");
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int datoAccedo = 0;
            if (Session["usuario"] == null)
            {
                Response.Redirect("/Default.aspx");
            }

            lblEmpleado.InnerText = Session["NombreApellido"].ToString();

            string nomInterfaz = System.IO.Path.GetFileName(Request.ServerVariables["SCRIPT_NAME"]);

            ArrayList list = (ArrayList)Session["grupos"];
            //dt = ac.RetornaRollxInterfaz(nomInterfaz);

            if (list.Count > 0)
            { 
                foreach (string element in list)
                {
                    datoAccedo = ac.ControlaAcceso(nomInterfaz, element);

                    if (datoAccedo == 1)
                    {
                        return;
                    }
                }

                Response.Redirect("/UI/Accesos/AccesoDenegado.aspx");
            }
        }
    }
}