using System;
using System.Collections.Generic;

namespace playground_.net_BasicThings.Backend.Modelos;

public partial class Ficheromodelo
{
    /// <summary>
    /// Permite asociar ficheros a cada modelo de articulo
    /// </summary>
    public int Idficheromodelo { get; set; }

    /// <summary>
    /// Modelo al que pertenece
    /// </summary>
    public int Modelo { get; set; }

    /// <summary>
    /// Nombre del fichero
    /// </summary>
    public string? Nombre { get; set; }

    /// <summary>
    /// tipo de informacion que contiene
    /// </summary>
    public string? Tipo { get; set; }

    public byte[]? Contenido { get; set; }

    public virtual Modeloarticulo ModeloNavigation { get; set; } = null!;
}
