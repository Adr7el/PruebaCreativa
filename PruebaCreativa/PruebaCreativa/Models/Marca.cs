using System;
using System.Collections.Generic;

namespace PruebaCreativa.Models;

public partial class Marca
{
    public string NombreMarca { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? TipoH { get; set; }

    public decimal? Exactitud { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
