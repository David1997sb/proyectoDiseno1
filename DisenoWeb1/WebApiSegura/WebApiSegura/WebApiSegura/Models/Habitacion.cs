//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiSegura.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HABITACION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HABITACION()
        {
            this.RESERVA = new HashSet<RESERVA>();
        }
    
        public int HAB_CODIGO { get; set; }
        public int HAB_CANT_HUESP { get; set; }
        public string HAB_TIPO { get; set; }
        public double HAB_PRECIO { get; set; }
        public int HOT_CODIGO { get; set; }
        public string HAB_ESTADO { get; set; }
    
        public virtual HOTEL HOTEL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESERVA> RESERVA { get; set; }
    }
}
