using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2025.Backend.Modelos;

[Table("permiso")]
[Index("Permisopadre", Name = "fk_permisos_padre_idx")]
[Index("Nombre", Name = "nombre_UNIQUE", IsUnique = true)]
public partial class Permiso
{
    /// <summary>
    /// Las distintas acciones que se pueden realizar sobre las entidades que maneja la aplicacion
    /// 
    /// </summary>
    [Key]
    [Column("idpermiso")]
    public int Idpermiso { get; set; }

    [Column("nombre")]
    [StringLength(45)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(45)]
    public string? Descripcion { get; set; }

    /// <summary>
    /// Permite jerarquizar los permisos de manera que unos dependand de otros.
    /// 
    /// Aqui se indica el id del permiso del que depende o nul si es independiente
    /// 
    /// Por ejemplo &quot;alta de usuario&quot; y &quot;baja de usuario&quot; pueden depender de &quot;acceso a usuarios&quot;
    /// </summary>
    [Column("permisopadre")]
    public int? Permisopadre { get; set; }

    [InverseProperty("PermisopadreNavigation")]
    public virtual ICollection<Permiso> InversePermisopadreNavigation { get; set; } = new List<Permiso>();

    [ForeignKey("Permisopadre")]
    [InverseProperty("InversePermisopadreNavigation")]
    public virtual Permiso? PermisopadreNavigation { get; set; }

    [InverseProperty("PermisoNavigation")]
    public virtual ICollection<Permisosrol> Permisosrols { get; set; } = new List<Permisosrol>();
}
