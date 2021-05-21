using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels
{
    public class JobRoleViewModel : EntityViewModel
    {
        [Required(ErrorMessage = "O Campo {0} precisa ser fornecido")]
        [StringLength(255, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Campo {0} precisa ser fornecido")]
        [StringLength(255, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Description { get; set; }
    }
}