namespace RepairWebPlatform.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Item
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Required]
        public string Condition { get; init; }
        
        [Required]
        public string City { get; init; }

        [Required]
        public string ImageUrl { get; init; }

        [Required]
        public decimal Price { get; init; }

        [Required]
        [MinLength(DescriptionMinLength)]
        public string Description { get; init; }
         
        public int CategoryId { get; init; }

        public Category Category{ get; init; }
    }
}
