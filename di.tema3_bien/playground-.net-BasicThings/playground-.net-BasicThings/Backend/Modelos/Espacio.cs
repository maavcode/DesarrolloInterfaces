using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using playground_.net_BasicThings.MVVM.Base;

namespace playground_.net_BasicThings.Backend.Modelos;

[Table("espacio")]
[Index("Padre", Name = "fk_espacios_espacio_idx")]
[Index("Nombre", Name = "nombre_UNIQUE", IsUnique = true)]
public partial class Espacio
{
    /// <summary>
    /// Cualquier lugar en el que se puede encontrar un artículo.
    /// Unos espacios pueden estar dentro de otros: relación jerárquica
    /// </summary>
    [Key]
    [Column("idespacio")]
    public int Idespacio { get; set; }

    [Column("nombre")]
    [StringLength(15)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(45)]
    public string? Descripcion { get; set; }

    [Column("padre")]
    public int? Padre { get; set; }

    [InverseProperty("EspacioNavigation")]
    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();

    [InverseProperty("PadreNavigation")]
    public virtual ICollection<Espacio> InversePadreNavigation { get; set; } = new List<Espacio>();

    [ForeignKey("Padre")]
    [InverseProperty("InversePadreNavigation")]
    public virtual Espacio? PadreNavigation { get; set; }
}
