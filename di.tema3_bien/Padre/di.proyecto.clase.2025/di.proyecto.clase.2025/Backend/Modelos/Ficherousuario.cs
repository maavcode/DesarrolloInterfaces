using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

[Table("ficherousuario")]
[Index("Usuario", Name = "fk_usuarios_ficherousuario_idx")]
public partial class Ficherousuario
{
    [Key]
    [Column("idficherousuario")]
    public int Idficherousuario { get; set; }

    /// <summary>
    /// usuario al que pertenece el fichero
    /// </summary>
    [Column("usuario")]
    public int Usuario { get; set; }

    /// <summary>
    /// tipo de informacion que contiene
    /// </summary>
    [Column("tipo")]
    [StringLength(45)]
    public string? Tipo { get; set; }

    /// <summary>
    /// nombre del fichero
    /// </summary>
    [Column("nombre")]
    [StringLength(45)]
    public string? Nombre { get; set; }

    [Column("contenido", TypeName = "blob")]
    public byte[]? Contenido { get; set; }

    [ForeignKey("Usuario")]
    [InverseProperty("Ficherousuarios")]
    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
