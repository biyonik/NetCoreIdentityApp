using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentityApp.WebUI.ViewModels
{
    public class UserResetPasswordViewModel
    {
        [Required(ErrorMessage = "{0} alanı zorunlu bir alandır!")]
        [Display(Name = "E-Posta Adresi")]
        [EmailAddress(ErrorMessage = "{0} alanı geçerli bir e-posta adresi olmalıdır!")]
        public string Email { get; set; }
    }
}