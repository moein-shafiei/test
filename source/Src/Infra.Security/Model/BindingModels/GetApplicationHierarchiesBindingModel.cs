using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class GetApplicationHierarchiesBindingModel
    {
        [Required]
        [Display(Name = "ApplicationCode")]
        public string ApplicationCode { get; set; }
    }
}