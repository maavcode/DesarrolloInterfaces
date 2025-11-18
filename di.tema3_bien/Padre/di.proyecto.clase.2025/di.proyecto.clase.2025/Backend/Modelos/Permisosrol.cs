using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

[Table("permisosrol")]
[Index("Permiso", Name = "fk_permisos_permisosrol_idx")]
[Index("Rol", Name = "fk_roles_permisosrol_idx")]
public partial class Permisosrol
{
    /// <summary>
    /// Permisos asignados a cada rol
    /// </summary>
    [Key]
    [Column("idpermisosrol")]
    public int Idpermisosrol { get; set; }

    [Column("rol")]
    public int Rol { get; set; }

    [Column("permiso")]
    public int Permiso { get; set; }

    /// <summary>
    /// Indica si el permiso se permite, deniega o hereda. En caso de heredarse, se hereda del padre inmediato.
    /// 0: denegado
    /// 1: permitido
    /// 2: heredado
    /// </summary>
    [Column("acceso")]
    public int? Acceso { get; set; }

    [ForeignKey("Permiso")]
    [InverseProperty("Permisosrols")]
    public virtual Permiso PermisoNavigation { get; set; } = null!;

    [ForeignKey("Rol")]
    [InverseProperty("Permisosrols")]
    public virtual Rol RolNavigation { get; set; } = null!;
}
