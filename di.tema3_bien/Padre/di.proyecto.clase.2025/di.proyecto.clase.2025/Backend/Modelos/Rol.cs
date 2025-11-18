using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

[Table("rol")]
[Index("Nombre", Name = "nombre_UNIQUE", IsUnique = true)]
public partial class Rol
{
    /// <summary>
    /// Roles que juegan los usuarios de la aplicacion
    /// </summary>
    [Key]
    [Column("idrol")]
    public int Idrol { get; set; }

    [Column("nombre")]
    [StringLength(45)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(45)]
    public string? Descripcion { get; set; }

    [InverseProperty("RolNavigation")]
    public virtual ICollection<Permisosrol> Permisosrols { get; set; } = new List<Permisosrol>();

    [InverseProperty("RolNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
