//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAFE.DALC
{
    using System;
    using System.Collections.Generic;
    
    public partial class SSF_EMPRESA
    {
        public SSF_EMPRESA()
        {
            this.SSF_ALUMNO = new HashSet<SSF_ALUMNO>();
            this.SSF_CAPACITACIONEMPRESA = new HashSet<SSF_CAPACITACIONEMPRESA>();
            this.SSF_EVALUACION = new HashSet<SSF_EVALUACION>();
            this.SSF_USUARIO = new HashSet<SSF_USUARIO>();
        }
    
        public decimal ID { get; set; }
        public Nullable<System.DateTime> FECH_CREACION { get; set; }
        public Nullable<short> ESTADO { get; set; }
        public string NOMBRE { get; set; }
        public Nullable<long> TELEFONO { get; set; }
        public string DIRECCION { get; set; }
    
        public virtual ICollection<SSF_ALUMNO> SSF_ALUMNO { get; set; }
        public virtual ICollection<SSF_CAPACITACIONEMPRESA> SSF_CAPACITACIONEMPRESA { get; set; }
        public virtual ICollection<SSF_EVALUACION> SSF_EVALUACION { get; set; }
        public virtual ICollection<SSF_USUARIO> SSF_USUARIO { get; set; }
    }
}