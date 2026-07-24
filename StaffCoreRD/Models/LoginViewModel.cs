using System.ComponentModel.DataAnnotations;

namespace StaffCoreRD.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de correo inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Recordarme")]
    public bool RecordarMe { get; set; }
}