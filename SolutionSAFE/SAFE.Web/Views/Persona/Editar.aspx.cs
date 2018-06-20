using SAFE.DALC;
using SAFE.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAFE.Web.Views.Persona
{
    public partial class Editar : System.Web.UI.Page
    {
        private List<SSF_PERSONA> ppall;
        private DateTime parseFech;
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

        private SSF_PERSONA Persona
        {
            get
            {
                if (Session["Persona"] == null)
                {
                    Session["Persona"] = new SSF_PERSONA();
                }
                return (SSF_PERSONA)Session["Persona"];
            }
            set
            {
                Session["Persona"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ComprobarSesion();
                CargaEditar();
            }
            else
            {
                LimpiaMensajes();
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
            string idPersona = Request.Params["editar"];
            if (idPersona != null)
            {
                if (!idPersona.Equals(String.Empty))
                {
                    Session["Persona"] = new SsfPersonaBO().Find(int.Parse(idPersona));
                    SSF_PERSONA p = (SSF_PERSONA)Session["Persona"];
                    HdnID.Value = p.ID.ToString();
                    TbRut.Text = Utilidad.FormatRutSalida(p.RUT);
                    TbNombre.Text = p.NOMBRE;
                    if (p.AP_PATERNO!=null)
                    {
                        TbApellido1.Text = p.AP_PATERNO;
                    }
                    if (p.AP_MATERNO != null)
                    {
                        TbApellido2.Text = p.AP_MATERNO;
                    }
                    if (p.CORREO != null)
                    {
                        TbCorreo.Text = p.CORREO;
                    }
                    if (p.TELEFONO != null)
                    {
                        TbTelefono.Text = p.TELEFONO.ToString();
                    }
                    if (p.FECHA_NAC != null)
                    {
                        TbFechaNac.Text = String.Format("{0:yyyy-MM-dd}", p.FECHA_NAC.Value); //p.FECHA_NAC.Value.ToShortDateString();
                    }
                }
            }
            
        }

        private void LimpiaMensajes()
        {
            Lerror.Text = String.Empty;
            Lexito.Text = String.Empty;
            LerrorRut.Text = String.Empty;
        }

        private bool ComprobarFechaNac()
        {
            if (!DateTime.TryParse(TbFechaNac.Text, out parseFech))
            {
                return false;
            }
            else
            {
                if (DateTime.Compare(parseFech, new DateTime(DateTime.Now.Year - 10, 1, 1)) >= 0)
                {
                    return false;
                }
            }
            return true;
        }

        private bool ExisteRut(string rut)
        {
            SSF_PERSONA pS = (SSF_PERSONA)Session["Persona"];

            if (ppall == null)
            {
                ppall = new SsfPersonaBO().GetAllSP();
            }

            foreach (SSF_PERSONA p in ppall)
            {
                if (p.RUT != null)
                {
                    if (p.RUT.ToLower().Equals(Utilidad.FormatRutIngreso(rut)) && !p.RUT.ToLower().Equals(Utilidad.FormatRutIngreso(pS.RUT).ToLower()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            LimpiaMensajes();

            if (TbRut.Text.Trim().Equals(String.Empty))
            {
                Lerror.Text = "RUT de usuario no puede quedar en blanco";
            }
            else if (!Utilidad.ValidaRut(TbRut.Text.Trim()))
            {
                Lerror.Text = "RUT no válido";
            }
            else if (ExisteRut(TbRut.Text.Trim()))
            {
                LerrorRut.Text = "RUT ya está registrado";
            }
            else if (TbNombre.Text.Trim().Equals(String.Empty))
            {
                Lerror.Text = "Nombre no puede quedar en blanco";
            }
            else if (!TbTelefono.Text.Trim().Equals(String.Empty) && !int.TryParse(TbTelefono.Text.Trim(), out int parseTel))
            {
                Lerror.Text = "Error en número de teléfono";
            }
            else if (!TbCorreo.Text.Trim().Equals(String.Empty) && !Utilidad.ValidaCorreo(TbCorreo.Text.Trim()))
            {
                Lerror.Text = "Correo inválido";
            }
            else if (!TbFechaNac.Text.Trim().Equals(String.Empty) && !ComprobarFechaNac())
            {
                Lerror.Text = "Fecha inválida";
            }
            else
            {
                SSF_PERSONA p = new SsfPersonaBO().Find(int.Parse(HdnID.Value));
                p.RUT = Utilidad.FormatRutIngreso(TbRut.Text.Trim());
                p.NOMBRE = TbNombre.Text.Trim();
                p.AP_PATERNO = TbApellido1.Text.Trim();
                p.AP_MATERNO = TbApellido2.Text.Trim();
                if (!TbCorreo.Text.Trim().Equals(String.Empty))
                {
                    p.CORREO = TbCorreo.Text.Trim();
                }
                if (!TbTelefono.Text.Trim().Equals(String.Empty))
                {
                    p.TELEFONO = int.Parse(TbTelefono.Text.Trim());
                }
                if (!TbFechaNac.Text.Trim().Equals(String.Empty))
                {
                    p.FECHA_NAC = parseFech;
                }

                if (new SsfPersonaBO().UpdateSP(p))
                {
                    Lexito.Text = "Persona agregada con éxito.";
                    Session["Persona"] = p;
                }
                else
                {
                    Lerror.Text = "No se pudo agregar.";
                }
            }
        }
    }
}