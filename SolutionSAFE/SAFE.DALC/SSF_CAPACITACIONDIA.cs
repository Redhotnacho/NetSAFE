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
    
    public partial class SSF_CAPACITACIONDIA
    {
        public SSF_CAPACITACIONDIA()
        {
            this.SSF_ASISTENCIA = new HashSet<SSF_ASISTENCIA>();
        }
    
        public decimal ID { get; set; }
        public Nullable<System.DateTime> FECH_CREACION { get; set; }
        public Nullable<short> ESTADO { get; set; }
        public Nullable<System.DateTime> DIA { get; set; }
        public Nullable<decimal> ID_CAPAEMPRESA { get; set; }
    
        public virtual ICollection<SSF_ASISTENCIA> SSF_ASISTENCIA { get; set; }
        public virtual SSF_CAPACITACIONEMPRESA SSF_CAPACITACIONEMPRESA { get; set; }
    }
}
