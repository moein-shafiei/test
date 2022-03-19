namespace DotFramework.Infra.Security
{
    public static class CustomClaimTypes
    {
        public const string GroupToken = "http://schemas.xmlsoap.org/ws/2014/12/identity/claims/group-token";
        public const string ExternalIdentity = "http://schemas.xmlsoap.org/ws/2014/12/identity/claims/externalidentity";
        public const string ProfilePicture = "http://schemas.xmlsoap.org/ws/2017/04/identity/claims/profilepicture";
        public const string DisplayName = "http://schemas.xmlsoap.org/ws/2017/04/identity/claims/displayname";
        public const string ClientName = "http://schemas.xmlsoap.org/ws/2017/04/identity/claims/clientname";
        public const string SSOAccessToken = "http://schemas.xmlsoap.org/ws/2017/04/identity/claims/ssotoken";
        public const string Initiator = "http://schemas.xmlsoap.org/ws/2017/04/identity/claims/initiator";
        public const string APIKey = "http://schemas.xmlsoap.org/ws/2017/04/identity/claims/apikey";
        public const string UserType = "http://schemas.xmlsoap.org/ws/2017/04/identity/claims/usertype";
        public const string TokenIdentifier = "http://schemas.xmlsoap.org/ws/2017/04/identity/claims/token-identifier";
    }

    public static class UserTypes
    {
        public const string User = "User";
        public const string Client = "Client";
    }
}
