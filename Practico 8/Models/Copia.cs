using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practico_8.Models;

public partial class Copia
{
    public int Id { get; set; }

    public int IdPelicula { get; set; }

    public string Deteriorada { get; set; } = null!;

    public string Formato { get; set; } = null!;

    public int PrecioAlquiler { get; set; }

    public bool Alquilada { get; set; }

    public virtual ICollection<Alquilere> Alquileres { get; set; } = new List<Alquilere>();

    public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;
}
