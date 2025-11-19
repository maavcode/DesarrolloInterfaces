using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.examen._1EV._2025.Backend.Modelos;

[Table("gamasproductos")]
public partial class Gamasproducto
{
    [Key]
    [StringLength(50)]
    public string Gama { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? DescripcionTexto { get; set; }

    [Column("DescripcionHTML", TypeName = "text")]
    public string? DescripcionHtml { get; set; }

    [Column(TypeName = "blob")]
    public byte[]? Imagen { get; set; }

    [InverseProperty("GamaNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
