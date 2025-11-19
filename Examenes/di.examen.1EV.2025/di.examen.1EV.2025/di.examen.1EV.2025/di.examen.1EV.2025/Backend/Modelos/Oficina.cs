using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.examen._1EV._2025.Backend.Modelos;

[Table("oficinas")]
public partial class Oficina
{
    [Key]
    [StringLength(10)]
    public string CodigoOficina { get; set; } = null!;

    [StringLength(30)]
    public string Ciudad { get; set; } = null!;

    [StringLength(50)]
    public string Pais { get; set; } = null!;

    [StringLength(50)]
    public string? Region { get; set; }

    [StringLength(10)]
    public string CodigoPostal { get; set; } = null!;

    [StringLength(20)]
    public string Telefono { get; set; } = null!;

    [StringLength(50)]
    public string LineaDireccion1 { get; set; } = null!;

    [StringLength(50)]
    public string? LineaDireccion2 { get; set; }

    [InverseProperty("CodigoOficinaNavigation")]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
