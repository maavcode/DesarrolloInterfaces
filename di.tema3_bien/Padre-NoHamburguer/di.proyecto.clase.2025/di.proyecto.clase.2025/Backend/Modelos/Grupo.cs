using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

[Table("grupo")]
[Index("Nombre", Name = "nombre_UNIQUE", IsUnique = true)]
public partial class Grupo
{
    /// <summary>
    /// grupos de clase
    /// </summary>
    [Key]
    [Column("idgrupo")]
    [StringLength(10)]
    public string Idgrupo { get; set; } = null!;

    [Column("nombre")]
    [StringLength(45)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("GrupoNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
