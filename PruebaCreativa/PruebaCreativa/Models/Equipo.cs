using System;
using System.Collections.Generic;

namespace PruebaCreativa.Models;

public partial class Equipo
{
    public string NumeroSerie { get; set; } = null!;

    public string NombreMarca { get; set; } = null!;

    public string NombreEquipo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual Marca Nombre_de_la_Marca { get; set; } = null!;
}
