using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

[Table("ficheromodelo")]
[Index("Modelo", Name = "fk_modelos_ficheromodelo_idx")]
public partial class Ficheromodelo
{
    /// <summary>
    /// Permite asociar ficheros a cada modelo de articulo
    /// </summary>
    [Key]
    [Column("idficheromodelo")]
    public int Idficheromodelo { get; set; }

    /// <summary>
    /// Modelo al que pertenece
    /// </summary>
    [Column("modelo")]
    public int Modelo { get; set; }

    /// <summary>
    /// Nombre del fichero
    /// </summary>
    [Column("nombre")]
    [StringLength(45)]
    public string? Nombre { get; set; }

    /// <summary>
    /// tipo de informacion que contiene
    /// </summary>
    [Column("tipo")]
    [StringLength(45)]
    public string? Tipo { get; set; }

    [Column("contenido", TypeName = "blob")]
    public byte[]? Contenido { get; set; }

    [ForeignKey("Modelo")]
    [InverseProperty("Ficheromodelos")]
    public virtual Modeloarticulo ModeloNavigation { get; set; } = null!;
}
