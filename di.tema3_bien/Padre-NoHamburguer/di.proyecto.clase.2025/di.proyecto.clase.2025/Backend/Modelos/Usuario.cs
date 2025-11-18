using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

[Table("usuario")]
[Index("Departamento", Name = "fk_departamentos_usuario_idx")]
[Index("Grupo", Name = "fk_grupos_usuario_idx")]
[Index("Grupo", Name = "fk_grupos_usuario_idx1")]
[Index("Rol", Name = "fk_roles_usuario_idx")]
[Index("Tipo", Name = "fk_tipos_usuario_idx")]
[Index("Username", Name = "username_UNIQUE", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("idusuario")]
    public int Idusuario { get; set; }

    /// <summary>
    /// Usuarios de la aplicacion
    /// 
    /// </summary>
    [Column("username")]
    [StringLength(20)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(200)]
    public string Password { get; set; } = null!;

    [Column("tipo")]
    public int Tipo { get; set; }

    [Column("rol")]
    public int Rol { get; set; }

    [Column("grupo")]
    [StringLength(10)]
    public string? Grupo { get; set; }

    [Column("departamento")]
    public int? Departamento { get; set; }

    [Column("nombre")]
    [StringLength(45)]
    public string? Nombre { get; set; }

    [Column("apellido1")]
    [StringLength(45)]
    public string? Apellido1 { get; set; }

    [Column("apellido2")]
    [StringLength(45)]
    public string? Apellido2 { get; set; }

    [Column("domicilio")]
    [StringLength(45)]
    public string? Domicilio { get; set; }

    [Column("poblacion")]
    [StringLength(45)]
    public string? Poblacion { get; set; }

    [Column("codpostal")]
    [StringLength(10)]
    public string? Codpostal { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string? Email { get; set; }

    [Column("telefono")]
    [StringLength(20)]
    public string? Telefono { get; set; }

    [InverseProperty("UsuarioaltaNavigation")]
    public virtual ICollection<Articulo> ArticuloUsuarioaltaNavigations { get; set; } = new List<Articulo>();

    [InverseProperty("UsuariobajaNavigation")]
    public virtual ICollection<Articulo> ArticuloUsuariobajaNavigations { get; set; } = new List<Articulo>();

    [ForeignKey("Departamento")]
    [InverseProperty("Usuarios")]
    public virtual Departamento? DepartamentoNavigation { get; set; }

    [InverseProperty("UsuarioNavigation")]
    public virtual ICollection<Ficherousuario> Ficherousuarios { get; set; } = new List<Ficherousuario>();

    [ForeignKey("Grupo")]
    [InverseProperty("Usuarios")]
    public virtual Grupo? GrupoNavigation { get; set; }

    [ForeignKey("Rol")]
    [InverseProperty("Usuarios")]
    public virtual Rol RolNavigation { get; set; } = null!;

    [InverseProperty("UsuarioNavigation")]
    public virtual ICollection<Salidum> Salida { get; set; } = new List<Salidum>();

    [ForeignKey("Tipo")]
    [InverseProperty("Usuarios")]
    public virtual Tipousuario TipoNavigation { get; set; } = null!;

//Añadido personal, para que salta todo el nombre completo
    override
        public string ToString()
    {
        return Nombre + " " + Apellido1 + " " + Apellido2;
    }
}
