using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.examen._1EV._2025.Backend.Modelos;

[Table("clientes")]
[Index("CodigoEmpleadoRepVentas", Name = "Clientes_EmpleadosFK")]
public partial class Cliente
{
    [Key]
    public int CodigoCliente { get; set; }

    [StringLength(50)]
    public string NombreCliente { get; set; } = null!;

    [StringLength(30)]
    public string? NombreContacto { get; set; }

    [StringLength(30)]
    public string? ApellidoContacto { get; set; }

    [StringLength(15)]
    public string Telefono { get; set; } = null!;

    [StringLength(15)]
    public string Fax { get; set; } = null!;

    [StringLength(50)]
    public string LineaDireccion1 { get; set; } = null!;

    [StringLength(50)]
    public string? LineaDireccion2 { get; set; }

    [StringLength(50)]
    public string Ciudad { get; set; } = null!;

    [StringLength(50)]
    public string? Region { get; set; }

    [StringLength(50)]
    public string? Pais { get; set; }

    [StringLength(10)]
    public string? CodigoPostal { get; set; }

    public int? CodigoEmpleadoRepVentas { get; set; }

    [Precision(15)]
    public decimal? LimiteCredito { get; set; }

    [ForeignKey("CodigoEmpleadoRepVentas")]
    [InverseProperty("Clientes")]
    public virtual Empleado? CodigoEmpleadoRepVentasNavigation { get; set; }

    [InverseProperty("CodigoClienteNavigation")]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    [InverseProperty("CodigoClienteNavigation")]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
