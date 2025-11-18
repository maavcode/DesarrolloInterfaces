using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

[Table("tipousuario")]
[Index("Nombre", Name = "nombre_UNIQUE", IsUnique = true)]
public partial class Tipousuario
{
    /// <summary>
    /// Para diferenciar tipos de usuario, independientemente del rol que juegan, y poder hacer operaciones masivas con ellos: alumnos, profesores, pas, ...
    /// </summary>
    [Key]
    [Column("idtipousuario")]
    public int Idtipousuario { get; set; }

    /// <summary>
    /// descripción del tipo de usuario
    /// </summary>
    [Column("nombre")]
    [StringLength(45)]
    public string? Nombre { get; set; }

    [InverseProperty("TipoNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
