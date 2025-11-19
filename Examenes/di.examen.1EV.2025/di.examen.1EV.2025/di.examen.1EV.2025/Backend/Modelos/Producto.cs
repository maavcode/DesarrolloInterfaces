using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.examen._1EV._2025.Backend.Modelos;

[Table("productos")]
[Index("Gama", Name = "Productos_gamaFK")]
public partial class Producto
{
    [Key]
    [StringLength(15)]
    public string CodigoProducto { get; set; } = null!;

    [StringLength(70)]
    public string Nombre { get; set; } = null!;

    [StringLength(50)]
    public string Gama { get; set; } = null!;

    [StringLength(25)]
    public string? Dimensiones { get; set; }

    [StringLength(50)]
    public string? Proveedor { get; set; }

    [Column(TypeName = "text")]
    public string? Descripcion { get; set; }

    public short CantidadEnStock { get; set; }

    [Precision(15)]
    public decimal PrecioVenta { get; set; }

    [Precision(15)]
    public decimal? PrecioProveedor { get; set; }

    [InverseProperty("CodigoProductoNavigation")]
    public virtual ICollection<Detallepedido> Detallepedidos { get; set; } = new List<Detallepedido>();

    [ForeignKey("Gama")]
    [InverseProperty("Productos")]
    public virtual Gamasproducto GamaNavigation { get; set; } = null!;
}
