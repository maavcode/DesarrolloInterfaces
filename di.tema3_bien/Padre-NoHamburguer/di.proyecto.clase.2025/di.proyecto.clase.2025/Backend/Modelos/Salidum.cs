using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

[Table("salida")]
[Index("Articulo", Name = "fk_articulos_salida_idx")]
[Index("Usuario", Name = "fk_usuarios_salida_idx")]
public partial class Salidum
{
    [Key]
    [Column("idsalida")]
    public int Idsalida { get; set; }

    [Column("usuario")]
    public int Usuario { get; set; }

    [Column("articulo")]
    public int Articulo { get; set; }

    [Column("fechasalida", TypeName = "datetime")]
    public DateTime Fechasalida { get; set; }

    [Column("fechadevolucion", TypeName = "datetime")]
    public DateTime? Fechadevolucion { get; set; }

    [ForeignKey("Articulo")]
    [InverseProperty("Salida")]
    public virtual Articulo ArticuloNavigation { get; set; } = null!;

    [ForeignKey("Usuario")]
    [InverseProperty("Salida")]
    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
