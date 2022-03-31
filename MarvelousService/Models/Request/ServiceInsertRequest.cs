using System.ComponentModel.DataAnnotations;

namespace MarvelousService.API.Models
{
    public class ServiceInsertRequest
    {
        [RegularExpression(@"^[а-яА-ЯёЁa-zA-Z]+$", ErrorMessage = "Посторонние символы. Используйте только буквы латинского алфавита или Кириллицу")]
        public string Name { get; set; }
        public string Description { get; set; }

        [RegularExpression(@"[0-9]+.[0-9]{2}$", ErrorMessage = "Введите корректную цену")]
        public decimal Price { get; set; }

        [Range(1, 2, ErrorMessage = "Введите цифру 1 - Разовая услуга, 2 - Подписка")]
        public int Type { get; set; }
    }
}
