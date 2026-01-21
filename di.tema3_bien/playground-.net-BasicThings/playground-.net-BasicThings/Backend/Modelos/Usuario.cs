using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace playground_.net_BasicThings.Backend.Modelos;

public partial class Usuario
{
    [Key]
    [Column("idusuario")]
    public int Idusuario { get; set; }

    [Column("")]
    [Required(ErrorMessage = "El tipo del modelo es obligatorio")]
    public string Username { get; set; } = null!;
    [Column("")]
    [Required(ErrorMessage = "La Contraseña es obligatoria")]
    public string Password { get; set; } = null!;
    [Column("")]
    public int Tipo { get; set; }
    [Column("")]
    public int Rol { get; set; }
    [Column("")]
    public string? Grupo { get; set; }
    [Column("")]
    public int? Departamento { get; set; }
    [Column("")]
    public string? Nombre { get; set; }
    [Column("")]
    public string? Apellido1 { get; set; }
    [Column("")]
    public string? Apellido2 { get; set; }
    [Column("")]
    public string? Domicilio { get; set; }
    [Column("")]
    public string? Poblacion { get; set; }
    [Column("")]
    public string? Codpostal { get; set; }
    [Column("")]
    public string? Email { get; set; }
    [Column("")]
    public string? Telefono { get; set; }
    [InverseProperty("")]
    public virtual ICollection<Articulo> ArticuloUsuarioaltaNavigations { get; set; } = new List<Articulo>();
    [InverseProperty("")]
    public virtual ICollection<Articulo> ArticuloUsuariobajaNavigations { get; set; } = new List<Articulo>();
    [InverseProperty("")]
    public virtual Departamento? DepartamentoNavigation { get; set; }
    [InverseProperty("")]
    public virtual ICollection<Ficherousuario> Ficherousuarios { get; set; } = new List<Ficherousuario>();
    [ForeignKey("")]
    [InverseProperty("")]
    public virtual Grupo? GrupoNavigation { get; set; }
    [ForeignKey("")]
    [InverseProperty("")]
    [Required(ErrorMessage = "El rol es obligatorio")]
    public virtual Rol RolNavigation { get; set; } = null!;
    [ForeignKey("")]
    [InverseProperty("")]
    public virtual ICollection<Salida> Salida { get; set; } = new List<Salida>();
    [ForeignKey("")]
    [InverseProperty("")]
    [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
    public virtual Tipousuario TipoNavigation { get; set; } = null!;
}
