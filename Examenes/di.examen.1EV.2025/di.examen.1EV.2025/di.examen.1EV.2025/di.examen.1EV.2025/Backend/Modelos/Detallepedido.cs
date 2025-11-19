using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.examen._1EV._2025.Backend.Modelos;

[PrimaryKey("CodigoPedido", "CodigoProducto")]
[Table("detallepedidos")]
[Index("CodigoProducto", Name = "DetallePedidos_ProductoFK")]
public partial class Detallepedido
{
    [Key]
    public int CodigoPedido { get; set; }

    [Key]
    [StringLength(15)]
    public string CodigoProducto { get; set; } = null!;

    public int Cantidad { get; set; }

    [Precision(15)]
    public decimal PrecioUnidad { get; set; }

    public short NumeroLinea { get; set; }

    [ForeignKey("CodigoPedido")]
    [InverseProperty("Detallepedidos")]
    public virtual Pedido CodigoPedidoNavigation { get; set; } = null!;

    [ForeignKey("CodigoProducto")]
    [InverseProperty("Detallepedidos")]
    public virtual Producto CodigoProductoNavigation { get; set; } = null!;
}
