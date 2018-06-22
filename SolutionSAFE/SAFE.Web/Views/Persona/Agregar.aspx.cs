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
    public partial class Agregar : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ComprobarSesion();
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

        private void LimpiaMensajes()
        {
            Lerror.Text = string.Empty;
            Lexito.Text = string.Empty;
            LerrorRut.Text = string.Empty;
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {

            LimpiaMensajes();
            int parseTel;
            
            SSF_PERSONA p = new SSF_PERSONA();
            if (TbRut.Text.Trim().Equals(string.Empty))
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
            else if (TbNombre.Text.Trim().Equals(string.Empty))
            {
                Lerror.Text = "Nombre no puede quedar en blanco";
            }
            else if (!TbTelefono.Text.Trim().Equals(string.Empty) && !int.TryParse(TbTelefono.Text.Trim(), out parseTel))
            {
                Lerror.Text = "Error en número de teléfono";
            }
            else if (!TbCorreo.Text.Trim().Equals(string.Empty) && !Utilidad.ValidaCorreo(TbCorreo.Text.Trim()))
            {
                Lerror.Text = "Correo inválido";
            }
            else if (!TbFechaNac.Text.Trim().Equals(string.Empty) && !ComprobarFechaNac())
            {
                Lerror.Text = "Fecha inválida";
            }
            else
            {
                p.RUT = Utilidad.FormatRutIngreso(TbRut.Text.Trim());
                p.NOMBRE = TbNombre.Text.Trim();
                p.AP_PATERNO = TbApellido1.Text.Trim();
                p.AP_MATERNO = TbApellido2.Text.Trim();
                if (!TbCorreo.Text.Trim().Equals(string.Empty))
                {
                    p.CORREO = TbCorreo.Text.Trim();
                }
                if (!TbTelefono.Text.Trim().Equals(string.Empty))
                {
                    p.TELEFONO = int.Parse(TbTelefono.Text.Trim());
                }
                if (!TbFechaNac.Text.Trim().Equals(string.Empty))
                {
                    p.FECHA_NAC = parseFech;
                }

                if (new SsfPersonaBO().AddSP(p))
                {
                    Lexito.Text = "Persona agregada con éxito.";
                }
                else
                {
                    Lerror.Text = "No se pudo agregar.";
                }
            }
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
            if (ppall == null)
            {
                ppall = new SsfPersonaBO().GetAllSP();
            }

            foreach (SSF_PERSONA p in ppall)
            {
                if (p.RUT != null)
                {
                    if (p.RUT.ToLower().Equals(Utilidad.FormatRutIngreso(rut)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}