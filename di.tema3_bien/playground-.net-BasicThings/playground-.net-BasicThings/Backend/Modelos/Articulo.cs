using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using playground_.net_BasicThings.MVVM.Base;

namespace playground_.net_BasicThings.Backend.Modelos;

[Table("articulo")]
[Index("Departamento", Name = "fk_departamentos_articulo_idx")]
[Index("Espacio", Name = "fk_espacios_articulo_idx")]
[Index("Dentrode", Name = "fk_estaen_articulo_idx")]
[Index("Modelo", Name = "fk_modelos_articulo_idx")]
[Index("Usuarioalta", Name = "fk_usuarioalta_articulo_idx")]
[Index("Usuariobaja", Name = "fk_usuariobaja_modeloarticulo_idx")]
public partial class Articulo : ValidatableViewModel
{
    [Key]
    [Column("idarticulo")]
    public int Idarticulo { get; set; }

    [Column("numserie")]
    [StringLength(45)]
    public string? Numserie { get; set; }

    [Column("estado")]
    [StringLength(45)]
    public string? Estado { get; set; }

    /// <summary>
    /// fecha en que se introdujo en el sistema
    /// </summary>
    [Column("fechaalta", TypeName = "date")]
    public DateTime? Fechaalta { get; set; }

    [Column("fechabaja", TypeName = "date")]
    public DateTime? Fechabaja { get; set; }

    [Column("usuarioalta")]
    public int? Usuarioalta { get; set; }

    /// <summary>
    /// usuario que lo dio de baja
    /// </summary>
    [Column("usuariobaja")]
    public int? Usuariobaja { get; set; }

    [Column("modelo")]
    public int Modelo { get; set; }

    /// <summary>
    /// departamento al que pertenece o del que depende
    /// </summary>
    [Column("departamento")]
    public int? Departamento { get; set; }

    /// <summary>
    /// espacio en que se encuentra
    /// </summary>
    [Column("espacio")]
    public int Espacio { get; set; }

    /// <summary>
    /// Indica que este artículo forma parte de otro
    /// </summary>
    [Column("dentrode")]
    public int? Dentrode { get; set; }

    [Column("observaciones", TypeName = "mediumtext")]
    public string? Observaciones { get; set; }

    [ForeignKey("Dentrode")]
    [InverseProperty("InverseDentrodeNavigation")]
    public virtual Articulo? DentrodeNavigation { get; set; }

    [ForeignKey("Departamento")]
    [InverseProperty("Articulos")]
    public virtual Departamento? DepartamentoNavigation { get; set; }

    [ForeignKey("Espacio")]
    [InverseProperty("Articulos")]
    [Required(ErrorMessage = "El espacio es obligatorio")]
    public virtual Espacio EspacioNavigation { get; set; } = null!;

    [InverseProperty("DentrodeNavigation")]
    public virtual ICollection<Articulo> InverseDentrodeNavigation { get; set; } = new List<Articulo>();

    [ForeignKey("Modelo")]
    [InverseProperty("Articulos")]
    [Required(ErrorMessage = "El modelo es obligatorio")]
    public virtual Modeloarticulo ModeloNavigation { get; set; } = null!;

    [InverseProperty("ArticuloNavigation")]
    public virtual ICollection<Salida> Salida { get; set; } = new List<Salida>();

    [ForeignKey("Usuarioalta")]
    [InverseProperty("ArticuloUsuarioaltaNavigations")]
    public virtual Usuario? UsuarioaltaNavigation { get; set; }

    [ForeignKey("Usuariobaja")]
    [InverseProperty("ArticuloUsuariobajaNavigations")]
    public virtual Usuario? UsuariobajaNavigation { get; set; }
}
