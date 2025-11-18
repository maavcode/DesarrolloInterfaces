using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

[Table("tipoarticulo")]
[Index("Padre", Name = "fk_padre_tipoarticulo_idx")]
public partial class Tipoarticulo
{
    /// <summary>
    /// tipos de articulo: relacion jerárquica\nMobiliario\n- Mesa\n -- Mesa despacho\n...
    /// </summary>
    [Key]
    [Column("idtipoarticulo")]
    public int Idtipoarticulo { get; set; }

    [Column("nombre")]
    [StringLength(45)]
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// tipo de articulo del que depende: relacion jerarquica
    /// </summary>
    [Column("padre")]
    public int? Padre { get; set; }

    [InverseProperty("PadreNavigation")]
    public virtual ICollection<Tipoarticulo> InversePadreNavigation { get; set; } = new List<Tipoarticulo>();

    [InverseProperty("TipoNavigation")]
    public virtual ICollection<Modeloarticulo> Modeloarticulos { get; set; } = new List<Modeloarticulo>();

    [ForeignKey("Padre")]
    [InverseProperty("InversePadreNavigation")]
    public virtual Tipoarticulo? PadreNavigation { get; set; }
}
