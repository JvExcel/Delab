using System.ComponentModel.DataAnnotations;

namespace Delab.Shared.Entities;

public class Country
{
    [Key]
    public int IdCountry { get; set; }

    [Required(ErrorMessage ="El campo {0} es obligatorio")]
    [MaxLength(100, ErrorMessage ="El campo {0} debe tener máximo {1} caracteres")]
    [Display(Name ="País")]
    public string Name { get; set; } = null!;

    [MaxLength(10, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
    [Display(Name = "Cod Phone")]
    public string? CodPhone { get; set; }

    // Relaciones
    public ICollection<State>? States { get; set; }
}
