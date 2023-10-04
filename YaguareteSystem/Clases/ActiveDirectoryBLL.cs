using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using YaguareteSystem.Models;

namespace YaguareteSystem.Clases
{
    public class ActiveDirectoryBLL
    {
        private string _path;
        private string _filterAttribute;
        public string info;
        private ArrayList listaPropiedades = new ArrayList();

        public ActiveDirectoryBLL(string path)
        {
            _path = path;
        }

        public ArrayList getListaPropiedades()
        {
            return listaPropiedades;
        }

        //retorna el nombre del usuario
        public string getCN()
        {
            return _filterAttribute;
        }

        public string getInfo()
        {
            return info;
        }
        public bool autenticado(string dominio, string usuario, string pass)
        {
            bool autenticado = true;

            DirectoryEntry entry = new DirectoryEntry(_path, usuario, pass);
            entry.AuthenticationType = AuthenticationTypes.Secure;
            try
            {
                object obj = entry.NativeObject;
                //object obj = entry.NativeObject; 
                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + usuario + ")";
                string[] requiredProperties = new string[] { "cn", "givenname", "sn" };
                foreach (String property in requiredProperties)
                    search.PropertiesToLoad.Add(property);

                SearchResult result = search.FindOne();

                if (null == result)
                {
                    autenticado = false;
                }
                else
                {

                    foreach (String property in requiredProperties)
                        foreach (Object myCollection in result.Properties[property])
                            listaPropiedades.Add(myCollection.ToString());
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (string)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error de autenticación. " + ex.Message);
            }

            return autenticado;
        }

        public DataTable getTodosUsuarios()
        {
            //String property = Console.ReadLine();
            ArrayList cnUsuarios = new ArrayList();
            List<UserActiveDirectoryModel> lstADUsers = new List<UserActiveDirectoryModel>();
            DataTable tabla = new DataTable();
            tabla.Columns.Add("usuario");
            tabla.Columns.Add("nombre");
            try
            {
                int cantidad = 0;
                string DomainPath = ConfigurationManager.AppSettings.Get("ActiveDirectoryListaUsuarios");
                DirectoryEntry searchRoot = new DirectoryEntry(DomainPath); 

                DirectorySearcher directory = new DirectorySearcher(searchRoot, "(objectClass=user)");
                directory.PageSize = 5000;
                // var resultado = directory.FindAll();
                SearchResult result;
                SearchResultCollection resultCol = directory.FindAll();
                if (resultCol != null)
                { 
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        string UserNameEmailString = string.Empty;
                        result = resultCol[counter];
                        if (result.Properties.Contains("samaccountname") && result.Properties.Contains("mail") && result.Properties.Contains("displayname"))
                        {
                            DataRow row2 = tabla.NewRow();
                            if (cantidad == 0)
                            {
                                cantidad = 1;
                                row2["usuario"] = "-1";
                                row2["nombre"] = "Todos";
                                tabla.Rows.Add(row2);
                            }else
                            {
                                UserActiveDirectoryModel objSurveyUsers = new UserActiveDirectoryModel();
                                row2["usuario"] = (String)result.Properties["samaccountname"][0];
                                row2["nombre"] = (String)result.Properties["displayname"][0];
                                tabla.Rows.Add(row2);
                            } 
                        }
                    }
                }

                return tabla;
            }
            catch (Exception ex)
            {
                return tabla;
            }
        }

        public string modificaPass(string nuevapass, string usuario, string viejapass)
        {
            string mensaje;
            try
            {
                DirectoryEntry entry = new DirectoryEntry();
                entry.Path = _path;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + usuario + ")";
                search.PropertiesToLoad.Add("password");
                SearchResult result = search.FindOne();

                if (result != null)
                {
                    // create new object from search result  
                    DirectoryEntry entryToUpdate = result.GetDirectoryEntry();
                    entryToUpdate.Invoke("ChangePassword", new object[] { viejapass, nuevapass });
                    entryToUpdate.CommitChanges();
                    mensaje = "Contraseña modificada :)";
                }

                else mensaje = "No se pudo cambiar contraseña. Usuario no valido :(";
            }
            catch (Exception e)
            {
                mensaje = "Error: " + e.ToString();
            }
            return mensaje;
        }

        public ArrayList GetGroups(string cn)
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + cn + ")";
            search.PropertiesToLoad.Add("memberOf");
            ArrayList grupos = new ArrayList();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    grupos.Add(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return grupos;
        }
    }
}
