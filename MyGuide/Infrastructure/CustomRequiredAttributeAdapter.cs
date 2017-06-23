using Resources.App_GlobalResources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyGuide.Infrastructure
{
    public class CustomRequiredAttributeAdapter : RequiredAttributeAdapter
    {
        public CustomRequiredAttributeAdapter(
        ModelMetadata metadata,
        ControllerContext context,
        RequiredAttribute attribute
    ) : base(metadata, context, attribute)
    {
            attribute.ErrorMessageResourceType = typeof(Resource);
            attribute.ErrorMessageResourceName = "PropertyValueRequired";
        }
    }
}