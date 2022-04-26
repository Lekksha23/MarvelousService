using System.ComponentModel.DataAnnotations;

namespace MarvelousService.API.Models
{
    public class LeadResourceInsertRequest
    {
        public int ResourceId { get; set; }
        public int Period { get; set; }
    }
}
