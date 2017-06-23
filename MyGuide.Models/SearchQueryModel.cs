using System.ComponentModel.DataAnnotations;

namespace MyGuide.Models
{
    public class SearchQueryModel
    {
        [Display(ResourceType = typeof(Resources.App_GlobalResources.Resource), Name="PlaceLabel")]
        public long? PlaceId { get; set; }

        [Display(ResourceType = typeof(Resources.App_GlobalResources.Resource), Name = "CategoryLabel")]
        public long? CategoryId { get; set; }

        [Display(ResourceType = typeof(Resources.App_GlobalResources.Resource), Name = "KeywordsLabel")]
        public string Keywords { get; set; }
    }
}
