using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email Is Required")]
        public string Email { get; set; } = null!;
    }
}
