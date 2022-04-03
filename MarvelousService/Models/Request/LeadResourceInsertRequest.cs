using System.ComponentModel.DataAnnotations;

namespace MarvelousService.API.Models
{
    public class LeadResourceInsertRequest
    {
        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int ServiceId { get; set; }

        [Range(1, 4, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Period { get; set; }
    }
}
