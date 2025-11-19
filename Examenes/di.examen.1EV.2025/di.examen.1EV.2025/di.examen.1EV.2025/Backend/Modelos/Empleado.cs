using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.examen._1EV._2025.Backend.Modelos;

[Table("empleados")]
[Index("CodigoJefe", Name = "Empleados_EmpleadosFK")]
[Index("CodigoOficina", Name = "Empleados_OficinasFK")]
public partial class Empleado
{
    [Key]
    public int CodigoEmpleado { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [StringLength(50)]
    public string Apellido1 { get; set; } = null!;

    [StringLength(50)]
    public string? Apellido2 { get; set; }

    [StringLength(10)]
    public string Extension { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(10)]
    public string CodigoOficina { get; set; } = null!;

    public int? CodigoJefe { get; set; }

    [StringLength(50)]
    public string? Puesto { get; set; }

    [InverseProperty("CodigoEmpleadoRepVentasNavigation")]
    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    [ForeignKey("CodigoJefe")]
    [InverseProperty("InverseCodigoJefeNavigation")]
    public virtual Empleado? CodigoJefeNavigation { get; set; }

    [ForeignKey("CodigoOficina")]
    [InverseProperty("Empleados")]
    public virtual Oficina CodigoOficinaNavigation { get; set; } = null!;

    [InverseProperty("CodigoJefeNavigation")]
    public virtual ICollection<Empleado> InverseCodigoJefeNavigation { get; set; } = new List<Empleado>();

    override public string ToString()
    {
        return $"{Nombre} {Apellido1} {Apellido2}";
    }
}
