using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.examen._1EV._2025.Backend.Modelos;

[Table("pedidos")]
[Index("CodigoCliente", Name = "Pedidos_Cliente")]
public partial class Pedido
{
    [Key]
    public int CodigoPedido { get; set; }

    [Column(TypeName = "date")]
    public DateTime FechaPedido { get; set; }

    [Column(TypeName = "date")]
    public DateTime FechaEsperada { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaEntrega { get; set; }

    [StringLength(15)]
    public string Estado { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Comentarios { get; set; }

    public int CodigoCliente { get; set; }

    [ForeignKey("CodigoCliente")]
    [InverseProperty("Pedidos")]
    public virtual Cliente CodigoClienteNavigation { get; set; } = null!;

    [InverseProperty("CodigoPedidoNavigation")]
    public virtual ICollection<Detallepedido> Detallepedidos { get; set; } = new List<Detallepedido>();
}
