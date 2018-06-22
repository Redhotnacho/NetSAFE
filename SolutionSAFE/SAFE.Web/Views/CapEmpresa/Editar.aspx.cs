﻿using SAFE.DALC;
using SAFE.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAFE.Web.Views.CapEmpresa
{
    public partial class Editar : System.Web.UI.Page
    {
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
            }
            else
            {
                LimpiaMensajes();
            }
        }

        private void LimpiaMensajes()
        {
            Lerror.Text = string.Empty;
            Lexito.Text = string.Empty;
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
            SSF_CAPACITACIONEMPRESA ce = null;
            string idCapempresa = Request.Params["editar"];
            if (idCapempresa != null)
            {
                if (!idCapempresa.Equals(string.Empty))
                {
                    ce = new SsfCapacitacionEmpresaBO().Find(int.Parse(idCapempresa));
                    CargaUsuarios();
                    HdnID.Value = ce.ID.ToString();
                    DdlCapacitacion.SelectedValue = ce.SSF_CAPACITACION.ID.ToString();
                    DdlEmpresa.SelectedValue = ce.SSF_EMPRESA.ID.ToString();
                    DdlEstadoCap.SelectedValue = ce.SSF_ESTADOCAPAEMPRESA.ID.ToString();
                    DdlUsuario.SelectedValue = ce.SSF_USUARIO.ID.ToString();
                    TbNroAlumnos.Text = ce.CANTIDAD_ALUMNOS.ToString();
                }
            }
        }

        private void CargaUsuarios()
        {
            foreach (SSF_USUARIO u in new SsfUsuarioBO().GetAll())
            {
                if (u.ID_PERFIL == 22 || u.ID_PERFIL == 24 || u.ID_PERFIL == 25)
                {
                    ListItem newItem = new ListItem
                    {
                        Text = String.Format("{0} - Rut: {1} - Nombre: {2} {3}", u.SSF_PERFIL.PERFIL, Utilidad.FormatRutSalida(u.SSF_PERSONA.RUT), u.SSF_PERSONA.NOMBRE, u.SSF_PERSONA.AP_PATERNO),
                        Value = u.ID.ToString()
                    };
                    DdlUsuario.Items.Add(newItem);
                }
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            int parseTel = 0;
            LimpiaMensajes();
            if (!TbNroAlumnos.Text.Trim().Equals(string.Empty) && !int.TryParse(TbNroAlumnos.Text.Trim(), out parseTel))
            {
                Lerror.Text = "Error en cantidad de alumnos";
            }
            else if(parseTel < 1)
            {
                Lerror.Text = "Error en cantidad de alumnos, no puede ser 0 ni número negativo";
            }
            else
            {
                SSF_CAPACITACIONEMPRESA ce = new SsfCapacitacionEmpresaBO().Find(int.Parse(HdnID.Value));
                ce.ID_USUARIO = Decimal.Parse(DdlUsuario.SelectedValue);
                ce.ID_CAPACITACION = Decimal.Parse(DdlCapacitacion.SelectedValue);
                ce.ID_EMPRESA = Decimal.Parse(DdlEmpresa.SelectedValue);
                ce.ID_ESTADOCAPACITACION = Decimal.Parse(DdlEstadoCap.SelectedValue);
                ce.CANTIDAD_ALUMNOS = int.Parse(TbNroAlumnos.Text);
                if (new SsfCapacitacionEmpresaBO().UpdateSP(ce))
                {
                    Lexito.Text = "Capacitación para empresa modificada con éxito.";
                }
                else
                {
                    Lerror.Text = "No se pudo modificar.";
                }
            }
        }


    }
}