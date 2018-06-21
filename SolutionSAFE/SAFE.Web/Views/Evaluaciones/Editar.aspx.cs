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
    public partial class Editar : System.Web.UI.Page
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

        private SSF_EVALUACION Evaluacion
        {
            get
            {
                if (Session["Evaluacion"] == null)
                {
                    Session["Evaluacion"] = new SSF_EVALUACION();
                }
                return (SSF_EVALUACION)Session["Evaluacion"];
            }
            set
            {
                Session["Evaluacion"] = value;
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
            string idEvaluacion = Request.Params["editar"];
            if (idEvaluacion != null)
            {
                if (!idEvaluacion.Equals(String.Empty))
                {
                    Session["Evaluacion"] = new SsfEvaluacionBO().Find(int.Parse(idEvaluacion));
                    SSF_EVALUACION e = (SSF_EVALUACION)Session["Evaluacion"];
                    HdnID.Value = e.ID.ToString();
                    TbNombre.Text = e.NOMBRE;
                    if (e.NOMBRE != null)
                    {
                        TbNombre.Text = e.NOMBRE;
                    }
                    
                }
            }

        }

        private void LimpiaMensajes()
        {
            Lerror.Text = String.Empty;
            Lexito.Text = String.Empty;
           
        }

        
        //private bool ExisteId(string id)
        //{
        //    SSF_EVALUACION Es = (SSF_EVALUACION)Session["Evaluacion"];

        //    }
        //    return false;
        //}

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            LimpiaMensajes();

            if (TbId.Text.Trim().Equals(String.Empty))
            {
                Lerror.Text = "ID de Evaluacion no puede quedar en blanco";
            }
           
            else if (TbNombre.Text.Trim().Equals(String.Empty))
            {
                Lerror.Text = "Nombre no puede quedar en blanco";
            }
            
            else
            {
                SSF_EVALUACION Ev = new SsfEvaluacionBO().Find(int.Parse(HdnID.Value));
                HdnID.Value = Ev.ID.ToString();
                Ev.NOMBRE = TbNombre.Text.Trim();
                

                if (new SsfEvaluacionBO().UpdateSP(Ev))
                {
                    Lexito.Text = "Evaluación agregada con éxito.";
                    Session["Evaluacion"] = Ev;
                }
                else
                {
                    Lerror.Text = "No se pudo agregar.";
                }
            }
        }

    
    }
}