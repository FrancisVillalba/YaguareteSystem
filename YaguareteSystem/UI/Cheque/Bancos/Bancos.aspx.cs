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

namespace YaguareteSystem.UI.Cheque
{
    public partial class Bancos : System.Web.UI.Page
    {
        ProcesosBLL p = new ProcesosBLL();
        LogsBLL lg = new LogsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGeneraJson_Click(object sender, EventArgs e)
        {
             
        }
        private void limpiarCampos()
        {
            
        }
        private bool controlCargaCampos()
        { 
            return true;
        }
    }
}