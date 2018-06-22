using SAFE.DALC;
using SAFE.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAFE.Web.Views.Usuario
{
    public partial class Agregar : System.Web.UI.Page
    {
        List<SSF_PERSONA> ppall;
        List<SSF_USUARIO> uuall;

        private SSF_USUARIO Usuario
        {
            get
            {
                if (Session["Usuario"] == null)
                {
                    Session["Usuario"] = new SSF_USUARIO();
                }
                return (SSF_USUARIO)Session["Usuario"];
            }
            set
            {
                Session["Usuario"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ComprobarSesion();
                Inicializacion();
            }
            else
            {
                LimpiaMensajes();
                ComprobarRut();
            }
        }

        private void ComprobarSesion()
        {
            if (Session["Usuario"] == null)
            {
                Server.Transfer("~/Views/Login.aspx");
            }
        }

        private void ComprobarRut()
        {
            LerrorIDPers.Text = string.Empty;
            if (uuall == null)
            {
                uuall = new SsfUsuarioBO().GetAll();
            }
            foreach (SSF_USUARIO u in uuall)
            {
                if (u.ID_PERSONA == Decimal.Parse(DdlPersona.SelectedValue))
                {
                    LerrorIDPers.Text = "(Rut único ya está asociado a un usuario)";
                }
            }
        }
        private void CargaPersona(SSF_USUARIO u)
        {
            DdlPersona.Enabled = true;
            DdlPersona.Items.Clear();
            ListItem newItem = new ListItem
            {
                Text = String.Format("Rut: {0} - Nombre: {1} {2}", u.SSF_PERSONA.RUT, u.SSF_PERSONA.NOMBRE, u.SSF_PERSONA.AP_PATERNO),
                Value = u.SSF_PERSONA.ID.ToString()
            };
            DdlPersona.Items.Add(newItem);
        }

        private void Inicializacion()
        {
            ppall = new SsfPersonaBO().GetAll();
            uuall = new SsfUsuarioBO().GetAll();
        }

        protected void BtnBuscarPersona_Click(object sender, EventArgs e)
        {
            LimpiaMensajes();
            String busqueda = TbBuscarPersona.Text.Trim();
            //split(Pattern.quote(".")) - str.split("\\s+");
            if (busqueda.Length == 0)
            {
                TbBuscarPersona.Text = "Búsqueda no puede ser vacío";
            }
            else
            {
                String[] palabras = busqueda.Split(' ');
                String[] palabras2 = busqueda.Split('.');
                List<SSF_PERSONA> pp = new List<SSF_PERSONA>();
                if (ppall == null)
                {
                    SsfPersonaBO persBO = new SsfPersonaBO();
                    ppall = persBO.GetAll();
                }
                foreach (String s in palabras)
                {
                    foreach (SSF_PERSONA pers in ppall)
                    {
                        if (pers.RUT != null)
                        {
                            if (pp.Count != 0)
                            {
                                if (!ExisteIdPers(pp, pers) && pers.RUT.Contains(s))
                                {
                                    pp.Add(pers);
                                }
                            }
                            else
                            {
                                if (pers.RUT.Contains(s))
                                {
                                    pp.Add(pers);
                                }
                            }
                        }
                        if (pers.NOMBRE != null)
                        {
                            if (pp.Count != 0)
                            {
                                if (!ExisteIdPers(pp, pers) && pers.NOMBRE.ToLower().Contains(s.ToLower()))
                                {
                                    pp.Add(pers);
                                }
                            }
                            else
                            {
                                if (pers.NOMBRE.ToLower().Contains(s.ToLower()))
                                {
                                    pp.Add(pers);
                                }
                            }
                        }
                        if (pers.AP_PATERNO != null)
                        {
                            if (pp.Count != 0)
                            {
                                if (!ExisteIdPers(pp, pers) && pers.AP_PATERNO.ToLower().Contains(s.ToLower()))
                                {
                                    pp.Add(pers);
                                }
                            }
                            else
                            {
                                if (pers.AP_PATERNO.ToLower().Contains(s.ToLower()))
                                {
                                    pp.Add(pers);
                                }
                            }
                        }
                        if (pers.AP_MATERNO != null)
                        {
                            if (pp.Count != 0)
                            {
                                if (!ExisteIdPers(pp, pers) && pers.AP_MATERNO.ToLower().Contains(s.ToLower()))
                                {
                                    pp.Add(pers);
                                }
                            }
                            else
                            {
                                if (pers.AP_MATERNO.ToLower().Contains(s.ToLower()))
                                {
                                    pp.Add(pers);
                                }
                            }
                        }
                    }
                }
                foreach (String s in palabras2)
                {
                    foreach (SSF_PERSONA pers in ppall)
                    {
                        if (pers.RUT != null)
                        {
                            if (pp.Count != 0)
                            {
                                if (!ExisteIdPers(pp, pers) && pers.RUT.Contains(s))
                                {
                                    pp.Add(pers);
                                }
                            }
                            else
                            {
                                if (pers.RUT.Contains(s))
                                {
                                    pp.Add(pers);
                                }
                            }
                        }
                    }
                }
                if (pp.Count != 0)
                {
                    CargaPersonas(pp);
                }
                else
                {
                    TbBuscarPersona.Text = "Búsqueda sin resultados";
                    DdlPersona.Enabled = false;
                }

            }
        }

        private void LimpiaMensajes()
        {
            Lerror.Text = string.Empty;
            Lexito.Text = string.Empty;
        }

        private void CargaPersonas(List<SSF_PERSONA> pp)
        {
            DdlPersona.Enabled = true;
            DdlPersona.Items.Clear();
            //newItem.Text = "<Seleccione Persona>";
            //newItem.Value = "0";
            foreach (SSF_PERSONA p in pp)
            {
                ListItem newItem = new ListItem
                {
                    Text = String.Format("Rut: {0} - Nombre: {1} {2}", p.RUT, p.NOMBRE, p.AP_PATERNO),
                    Value = p.ID.ToString()
                };
                DdlPersona.Items.Add(newItem);
            }
        }

        private bool ExisteIdPers(List<SSF_PERSONA> pp, SSF_PERSONA pers)
        {
            foreach (SSF_PERSONA p in pp)
            {
                if (p.ID == pers.ID)
                {
                    return true;
                }
            }
            return false;
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            LimpiaMensajes();
            SSF_USUARIO u = new SSF_USUARIO();
            if(DdlPersona.Enabled == false || DdlPersona.Items.Count == 0)
            {
                Lerror.Text = "Debe seleccionar una persona";
            }
            else if (TbUsername.Text.Trim().Equals(string.Empty))
            {
                Lerror.Text = "Nombre de usuario no puede quedar en blanco";
            }
            else if (!TbContrasena.Text.Trim().Equals(string.Empty) && !TbContrasena2.Text.Trim().Equals(string.Empty) && !TbContrasena.Text.Trim().Equals(TbContrasena2.Text.Trim()))
            {
                Lerror.Text = "Las contraseñas no son iguales";
            }
            else if(TbContrasena.Text.Equals(string.Empty) && TbContrasena2.Text.Equals(string.Empty))
            {
                Lerror.Text = "Las contraseñas no pueden quedar en blanco";
            }
            else
            {
                u.ID_PERSONA = Decimal.Parse(DdlPersona.SelectedValue);
                u.ID_PERFIL = Decimal.Parse(DdlPerfil.SelectedValue);
                u.ID_EMPRESA = Decimal.Parse(DdlEmpresa.SelectedValue);
                u.USERNAME = TbUsername.Text.Trim();
                u.CONTRASENA = TbContrasena.Text.Trim();

                if (new SsfUsuarioBO().AddSP(u))
                {
                    Lexito.Text = "Usuario agregado con éxito.";
                }
                else
                {
                    Lerror.Text = "No se pudo agregar.";
                }
            }
        }
    }
}