using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

[Table("departamento")]
[Index("Nombre", Name = "nombre_UNIQUE", IsUnique = true)]
public partial class Departamento
{
    /// <summary>
    /// Departamentos del instituto
    /// </summary>
    [Key]
    [Column("iddepartamento")]
    public int Iddepartamento { get; set; }

    [Column("nombre")]
    [StringLength(45)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("DepartamentoNavigation")]
    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();

    [InverseProperty("DepartamentoNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
