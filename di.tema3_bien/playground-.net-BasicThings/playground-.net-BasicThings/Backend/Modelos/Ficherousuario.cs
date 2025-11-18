using System;
using System.Collections.Generic;

namespace playground_.net_BasicThings.Backend.Modelos;

public partial class Ficherousuario
{
    public int Idficherousuario { get; set; }

    /// <summary>
    /// usuario al que pertenece el fichero
    /// </summary>
    public int Usuario { get; set; }

    /// <summary>
    /// tipo de informacion que contiene
    /// </summary>
    public string? Tipo { get; set; }

    /// <summary>
    /// nombre del fichero
    /// </summary>
    public string? Nombre { get; set; }

    public byte[]? Contenido { get; set; }

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
