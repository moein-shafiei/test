using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class CreateSessionBindingModel
    {
        [Display(Name = "IP")]
        public string IP { get; set; }

        [Display(Name = "Is Application Session")]
        public bool IsApplicationSession { get; set; }

        [Display(Name = "Application Code")]
        public string ApplicationCode { get; set; }

        [Display(Name = "Browser Detail")]
        public string BrowserDetail { get; set; }
        
        [Display(Name = "Operating System Detail")]
        public string OperatingSystemDetail { get; set; }

        [Display(Name = "Url Referrer")]
        public string UrlReferrer { get; set; }
    }
}
