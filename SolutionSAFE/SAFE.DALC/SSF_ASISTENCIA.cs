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
    
    public partial class SSF_ASISTENCIA
    {
        public decimal ID { get; set; }
        public Nullable<System.DateTime> FECH_CREACION { get; set; }
        public Nullable<short> ESTADO { get; set; }
        public Nullable<decimal> ID_CAPACITACIONDIA { get; set; }
        public Nullable<decimal> ID_ALUMCAPAEMPRESA { get; set; }
        public Nullable<decimal> ASISTE { get; set; }
    
        public virtual SSF_CAPACITACIONDIA SSF_CAPACITACIONDIA { get; set; }
    }
}
