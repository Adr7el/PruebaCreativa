using System;
using System.Collections.Generic;

namespace PruebaCreativa.Models;

public partial class Prestamo
{
    public string Persona { get; set; } = null!;

    public string NombreMarca { get; set; } = null!;

    public string NombreEquipo { get; set; } = null!;

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public string? Estado { get; set; }

    public virtual Marca Nombre_de_la_Marca { get; set; } = null!;
}
