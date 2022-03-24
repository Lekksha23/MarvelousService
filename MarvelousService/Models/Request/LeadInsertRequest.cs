using System.ComponentModel.DataAnnotations;

namespace MarvelousService.API.Models
{
    public class LeadInsertRequest
    {
        [StringLength(30, ErrorMessage = "Максимальная длина 30 символов.")]
        public string Name { get; set; }

        [StringLength(30, ErrorMessage = "Максимальная длина 30 символов.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле BirthDate не может быть пустым.")]
        public DateTime BirthDate { get; set; }

        [Phone(ErrorMessage = "Телефон введен некорректно.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Email введен некорректно.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "Пароль должен иметь длину минимум 8 и максимум 30 символов.", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
