namespace RepairWebPlatform.Models.Item
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddItemFormModel
    { 

        [Required (ErrorMessage = "Name is required.")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }

        [Required]
        public string Condition { get; init; }

        [Required]
        public string City { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; init; }

        [Required]
        public decimal Price { get; init; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; init; }
        public IEnumerable<ItemTypesViewModel> Categories { get; set; }

    }
}
