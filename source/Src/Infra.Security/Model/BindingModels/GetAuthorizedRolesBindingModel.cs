using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class GetAuthorizedRolesBindingModel: AuthorizationBindingModel
    {
        [Required]
        [Display(Name = "Object Type")]
        public string ObjectType { get; set; }

        [Display(Name = "Application Code")]
        public string ApplicationCode { get; set; }

        [Display(Name = "Controller Name")]
        public string ControllerName { get; set; }

        [Display(Name = "Action Name")]
        public string ActionName { get; set; }

        [Display(Name = "Endpoint Type")]
        public string EndpointType { get; set; }

        [Display(Name = "Operation Name")]
        public string OperationName { get; set; }

        [Display(Name = "Operation Type")]
        public string OperationType { get; set; }
    }

    public class AuthObjectType
    {
        public const string View = "View";
        public const string Action = "Action";
        public const string Service = "Service";
    }
}