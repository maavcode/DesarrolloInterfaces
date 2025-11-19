using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.examen._1EV._2025.Backend.Modelos;

[PrimaryKey("CodigoCliente", "Idtransaccion")]
[Table("pagos")]
public partial class Pago
{
    [Key]
    public int CodigoCliente { get; set; }

    [StringLength(40)]
    public string FormaPago { get; set; } = null!;

    [Key]
    [Column("IDTransaccion")]
    [StringLength(50)]
    public string Idtransaccion { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime FechaPago { get; set; }

    [Precision(15)]
    public decimal Cantidad { get; set; }

    [ForeignKey("CodigoCliente")]
    [InverseProperty("Pagos")]
    public virtual Cliente CodigoClienteNavigation { get; set; } = null!;
}
