using System;
using System.Collections.Generic;

namespace playground_.net_BasicThings.Backend.Modelos;

public partial class Tipousuario
{
    /// <summary>
    /// Para diferenciar tipos de usuario, independientemente del rol que juegan, y poder hacer operaciones masivas con ellos: alumnos, profesores, pas, ...
    /// </summary>
    public int Idtipousuario { get; set; }

    /// <summary>
    /// descripción del tipo de usuario
    /// </summary>
    public string? Nombre { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
