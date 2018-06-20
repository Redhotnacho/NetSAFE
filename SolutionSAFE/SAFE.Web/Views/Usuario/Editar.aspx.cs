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
    public partial class Editar : System.Web.UI.Page
    {
        private List<SSF_PERSONA> ppall;
        private List<SSF_USUARIO> uuall;
        private Decimal idPersona;

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
                CargaEditar();
                Inicializacion();
            }
            else
            {
                LimpiaMensajes();
                ComprobarRut();
            }
        }

        private void ComprobarRut()
        {
            LerrorIDPers.Text = String.Empty;
            if (uuall == null)
            {
                uuall = new SsfUsuarioBO().GetAll();
            }
            foreach (SSF_USUARIO u in uuall)
            {
                if (u.ID_PERSONA == Decimal.Parse(DdlPersona.SelectedValue))
                {
                    if (idPersona != Decimal.Parse(DdlPersona.SelectedValue))
                    {
                        LerrorIDPers.Text = "(Rut único ya está asociado a un usuario)";
                    }
                }
            }
        }

        private void ComprobarSesion()
        {
            if (Session["Usuario"] == null)
            {
                Server.Transfer("~/Views/Login.aspx");
            }
        }
        private void CargaEditar()
        {
            SSF_USUARIO u=null;
            string idUsuario = Request.Params["editar"];
            if (idUsuario != null)
            {
                if (!idUsuario.Equals(String.Empty))
                {
                    u = new SsfUsuarioBO().Find(int.Parse(idUsuario));
                    CargaPersona(u);
                    HdnID.Value = u.ID.ToString();
                    DdlPerfil.SelectedValue = u.SSF_PERFIL.ID.ToString();
                    DdlEmpresa.SelectedValue = u.SSF_EMPRESA.ID.ToString();
                    TbUsername.Text = u.USERNAME;
                }
            }
            if (u!=null)
            {
                idPersona = u.SSF_PERSONA.ID;
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
            Lerror.Text = String.Empty;
            Lexito.Text = String.Empty;
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

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            LimpiaMensajes();
            SSF_USUARIO u = new SsfUsuarioBO().Find(int.Parse(HdnID.Value));

            if (DdlPersona.Enabled == false || DdlPersona.Items.Count == 0)
            {
                Lerror.Text = "Debe seleccionar una persona";
            }
            else if (TbUsername.Text.Trim().Equals(String.Empty))
            {
                Lerror.Text = "Nombre de usuario no puede quedar en blanco";
            }
            else if (!TbContrasena.Text.Trim().Equals(String.Empty) && !TbContrasena2.Text.Trim().Equals(String.Empty) && !TbContrasena.Text.Trim().Equals(TbContrasena2.Text.Trim()))
            {
                Lerror.Text = "Las contraseñas no son iguales";
            }
            else
            {
                u.ID_PERSONA = Decimal.Parse(DdlPersona.SelectedValue);
                u.ID_PERFIL = Decimal.Parse(DdlPerfil.SelectedValue);
                u.ID_EMPRESA = Decimal.Parse(DdlEmpresa.SelectedValue);
                u.USERNAME = TbUsername.Text.Trim();
                if (!(TbContrasena.Text.Equals(String.Empty) && TbContrasena2.Text.Equals(String.Empty)))
                {
                    u.CONTRASENA = TbContrasena.Text.Trim();
                }
                if (new SsfUsuarioBO().UpdateSP(u))
                {
                    Lexito.Text = "Usuario modificado con éxito.";
                }
                else
                {
                    Lerror.Text = "No se pudo modificar.";
                }
            }
        }
    }
}