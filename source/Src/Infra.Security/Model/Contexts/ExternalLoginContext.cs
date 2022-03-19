namespace DotFramework.Infra.Security.Model
{
    public class ExternalLoginContext
    {
        public ExternalLoginContext(string redirectUrl)
        {
            RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get; private set; }
    }
}