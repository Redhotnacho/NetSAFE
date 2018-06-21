using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAFE.DALC;
using SAFE.Negocio;

namespace SAFE.Web.Views.Evaluaciones
{
    public partial class Agregar : System.Web.UI.Page
    {
        private List<SSF_EVALUACION> ppall;
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
            Lerror.Text = String.Empty;
            Lexito.Text = String.Empty;
           
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {

            LimpiaMensajes();
            

            SSF_EVALUACION eva = new SSF_EVALUACION();
            if (TbId.Text.Trim().Equals(String.Empty))
            {
                Lerror.Text = "ID de Evaluación no puede quedar en blanco";
            }
            
            else if (ExisteEva(TbId.Text.Trim()))
            {
                Lerror.Text = "Evaluación ya está registrada";
            }
            else if (TbNombre.Text.Trim().Equals(String.Empty))
            {
                Lerror.Text = "Nombre no puede quedar en blanco";
            }
            else if (TbTevaluacion.Text.Trim().Equals(String.Empty))
            {
                Lerror.Text = "Tipo de Evaluación no puede quedar en blanco";
            }
            else if (TbNombre.Text.Trim().Equals(String.Empty))
            {
                Lerror.Text = "Fecha de Creación no puede quedar en blanco";
            }

            else
            {
                
                eva.NOMBRE = TbNombre.Text.Trim();
               
                if (!TbFechaCrea.Text.Trim().Equals(String.Empty))
                {
                    eva.FECH_CREACION = parseFech;
                }
                

                if (new SsfEvaluacionBO().AddSP(eva))
                {
                    Lexito.Text = "Evaluacion agregada con éxito.";
                }
                else
                {
                    Lerror.Text = "No se pudo agregar.";
                }
            }
        }


        // completar
        private bool ExisteEva(string id)
        {
            if (ppall == null)
            {
                ppall = new SsfEvaluacionBO().GetAllSP();
            }

            return false;
        }




    }
}