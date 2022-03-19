using DotFramework.Core;
using DotFramework.Core.Web;
using DotFramework.Infra.Security.Model;
using DotFramework.Infra.Web.API;
using DotFramework.Web.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DotFramework.Infra.Security
{
    public partial class AuthService : SingletonProvider<AuthService>
    {
        #region Variables

        string _AuthEndpointPath;
        string _ClientID;
        string _ClientSecret;
        bool _IsClientSecretEncrypted;

        AuthenticateClientResponse _AuthenticateClientResponse;
        AuthorizationToken _ClientBasicAuthorizationToken;
        AuthorizationToken _ClientAuthorizationToken;

        private AbstractHttpUtility _HttpUtility;
        private AbstractHttpUtility HttpUtility
        {
            get
            {
                if (_HttpUtility == null)
                {
#if NET40
                    _HttpUtility = new WebRequestUtility(AuthEndpointPath);
#else
                    _HttpUtility = new HttpClientUtility(AuthEndpointPath);
#endif
                }

                return _HttpUtility;
            }
        }

        #endregion

        #region ReadOnly Properties

        public string AuthEndpointPath
        {
            get
            {
                if (String.IsNullOrEmpty(_AuthEndpointPath))
                {
                    throw new ArgumentNullException("AuthEndpointPath");
                }

                return _AuthEndpointPath;
            }
        }

        #endregion

        public void Initialize(string authEndpointPath, string clientID, string clientSecret, bool isClientSecretEncrypted = true)
        {
            _AuthEndpointPath = authEndpointPath;
            _ClientID = clientID;
            _ClientSecret = clientSecret;
            _IsClientSecretEncrypted = isClientSecretEncrypted;

            ResetClientBasicToken();
            ResetClientToken();
        }

        public AuthenticateClientResponse AuthenticateClient(AuthenticateClientRequest request)
        {
            try
            {
                var token = CreateBasicAuthorization(request);

                var content = new List<KeyValuePair<String, String>>
                {
                    new KeyValuePair<String, String>("grant_type", "client_credentials")
                };

                if (request == null || String.IsNullOrWhiteSpace(request.DeviceIdentifier))
                {
                    var initiator = GetInitiator();

                    if (!String.IsNullOrWhiteSpace(initiator))
                    {
                        content.Add(new KeyValuePair<String, String>("device_identifier", initiator));
                    }
                }

                return HttpUtility.Post<AuthenticateClientResponse, AuthErrorResult>("Token", content, token);
            }
            catch
            {
                throw;
            }
        }

        public TokenResponseModel Authenticate(LoginBindingModel model)
        {
            try
            {
                var content = new List<KeyValuePair<String, String>>
                {
                    new KeyValuePair<String, String>("grant_type", "password"),
                    new KeyValuePair<String, String>("userName", model.UserName),
                    new KeyValuePair<String, String>("password", model.Password)
                };

                return PostTokenWithRetry<TokenResponseModel>(content);
            }
            catch
            {
                throw;
            }
        }

        private T PostTokenWithRetry<T>(object content)
        {
            try
            {
                return HttpUtility.Post<T, AuthErrorResult>("Token", content, _ClientBasicAuthorizationToken);
            }
            catch (UnauthorizedHttpException)
            {
                ResetClientBasicToken();
                return HttpUtility.Post<T, AuthErrorResult>("Token", content, _ClientBasicAuthorizationToken);
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                if (ex.ErrorResult.Error == "invalid_clientId" || ex.ErrorResult.Error == "invalid_client_credential")
                {
                    ResetClientBasicToken();
                    return HttpUtility.Post<T, AuthErrorResult>("Token", content, _ClientBasicAuthorizationToken);
                }
                else
                {
                    throw;
                }
            }
        }

        public ExternalLoginContext ExternalLogin(string client_id, string provider, string redirect_url)
        {
            try
            {
                string path = String.Format("Auth/ExternalLogin?client_id={0}&provider={1}&redirect_uri={2}", client_id,
                                                                                                              provider,
                                                                                                              redirect_url);
                return new ExternalLoginContext(path);
            }
            catch
            {
                throw;
            }
        }

        public TokenResponseModel ExternalAuthenticate(ObtainLocalAccessTokenBindingModel model)
        {
            try
            {
                var content = new List<KeyValuePair<String, String>>
                {
                    new KeyValuePair<String, String>("grant_type", "external"),
                    new KeyValuePair<String, String>("provider", model.Provider),
                    new KeyValuePair<String, String>("access_token", model.AccessToken)
                };

                return PostTokenWithRetry<TokenResponseModel>(content);
            }
            catch
            {
                throw;
            }
        }

        public TokenResponseModel RefreshToken(RefreshTokenBindingModel model)
        {
            try
            {
                var content = new List<KeyValuePair<String, String>>
                {
                    new KeyValuePair<String, String>("grant_type", "refresh_token"),
                    new KeyValuePair<String, String>("refresh_token", model.RefreshToken)
                };

                return PostTokenWithRetry<TokenResponseModel>(content);
            }
            catch
            {
                throw;
            }
        }

        public VerifyTokenResponseModel VerifyToken(VerifyTokenRequest request)
        {
            try
            {
                return HttpUtility.Post<VerifyTokenResponseModel>("Auth/VerifyToken", null, CreateBearerToken(request.AccessToken));
            }
            catch
            {
                throw;
            }
        }

        public GenerateClientEncryptionTokenResponse GenerateClientEncryptionToken(GenerateClientEncryptionTokenRequest request)
        {
            try
            {
                return HttpUtility.Post<GenerateClientEncryptionTokenResponse>("Auth/ClientEncryptionToken", request);
            }
            catch
            {
                throw;
            }
        }

        public ActivateClientResponse ActivateClient(ActivateClientRequest model)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(model.DeviceIdentifier))
                {
                    model.DeviceIdentifier = GetInitiator();
                }

                return HttpUtility.Post<ActivateClientResponse>("Auth/ActivateClient", model);
            }
            catch
            {
                throw;
            }
        }

        public ActivateResponse Activate(ActivateRequest model)
        {
            try
            {
                return HttpUtility.Post<ActivateResponse>("Auth/Activate", model);
            }
            catch
            {
                throw;
            }
        }

        public ActivateTokenResponseModel GrantActivation(GrantActivationBindingModel model)
        {
            try
            {
                FormUrlEncodedContent content = new FormUrlEncodedContent(new List<KeyValuePair<String, String>>
                {
                    new KeyValuePair<String, String>("grant_type", "activation"),
                    new KeyValuePair<String, String>("userName", model.UserName),
                    new KeyValuePair<String, String>("token", model.Token),
                    new KeyValuePair<String, String>("client_id", _ClientID)
                });

                return PostTokenWithRetry<ActivateTokenResponseModel>(content);
            }
            catch
            {
                throw;
            }
        }

        public VerifyClientTokenResponseModel VerifyClientToken(VerifyTokenRequest request)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(request?.AccessToken))
                {
                    throw new UnauthorizedHttpException();
                }

                return HttpUtility.Post<VerifyClientTokenResponseModel>("Auth/VerifyClientToken", null, CreateBearerToken(request.AccessToken));
            }
            catch
            {
                throw;
            }
        }

        public LockUserResponseModel LockUser(LockUserBindingModel request)
        {
            try
            {
                try
                {
                    return HttpUtility.Post<LockUserResponseModel>("Auth/LockUser", request, _ClientAuthorizationToken);
                }
                catch (UnauthorizedHttpException)
                {
                    ResetClientToken();
                    return HttpUtility.Post<LockUserResponseModel>("Auth/LockUser", request, _ClientAuthorizationToken);
                }
            }
            catch
            {
                throw;
            }
        }

        public LockUserResponseModel UnLockUser(UnLockUserBindingModel request)
        {
            try
            {
                try
                {
                    return HttpUtility.Post<LockUserResponseModel>("Auth/UnLockUser", request, _ClientAuthorizationToken);
                }
                catch (UnauthorizedHttpException)
                {
                    ResetClientToken();
                    return HttpUtility.Post<LockUserResponseModel>("Auth/UnLockUser", request, _ClientAuthorizationToken);
                }
            }
            catch
            {
                throw;
            }
        }

        public Boolean SignOut(AuthorizationBindingModel model)
        {
            try
            {
                return HttpUtility.Post<Boolean>("Auth/Logout", null, CreateBearerToken(model.AccessToken));
            }
            catch
            {
                throw;
            }
        }

        public void Register(RegisterBindingModel model)
        {
            try
            {
                try
                {
                    HttpUtility.Post<Object>("Auth/Register", model, _ClientAuthorizationToken);
                }
                catch (UnauthorizedHttpException)
                {
                    ResetClientToken();
                    HttpUtility.Post<Object>("Auth/Register", model, _ClientAuthorizationToken);
                }
            }
            catch
            {
                throw;
            }
        }

        public TokenResponseModel RegisterAndLogin(RegisterBindingModel model)
        {
            try
            {
                Register(model);

                LoginBindingModel loginModel = new LoginBindingModel { UserName = model.UserName, Password = model.Password };
                return Authenticate(loginModel);
            }
            catch
            {
                throw;
            }
        }

        public void ChangePassword(ChangePasswordBindingModel model, string accessToken)
        {
            try
            {
                try
                {
                    HttpUtility.Post<object>("Auth/ChangePassword", model, CreateBearerToken(accessToken));
                }
                catch (UnauthorizedHttpException)
                {
                    ResetClientToken();
                    HttpUtility.Post<object>("Auth/ChangePassword", model, CreateBearerToken(accessToken));
                }
            }
            catch
            {
                throw;
            }
        }

        public Int64 CreateSession(CreateSessionBindingModel model)
        {
            try
            {
                try
                {
                    return HttpUtility.Post<Int64>("Auth/CreateSession", model, _ClientAuthorizationToken);
                }
                catch (UnauthorizedHttpException)
                {
                    ResetClientToken();
                    return HttpUtility.Post<Int64>("Auth/CreateSession", model, _ClientAuthorizationToken);
                }
            }
            catch
            {
                throw;
            }
        }

        public AuthorizedRolesResponseModel GetAuthorizedRoles(GetAuthorizedRolesBindingModel model)
        {
            try
            {
                return HttpUtility.Post<AuthorizedRolesResponseModel>("Auth/GetAuthorizedRoles", model, CreateBearerToken(model.AccessToken));
            }
            catch
            {
                throw;
            }
        }

        public LockInfoResponseModel GetLockoutInfo(GetLockoutInfoBindingModel model)
        {
            try
            {
                return HttpUtility.Post<LockInfoResponseModel>("Auth/LockoutInfo", model, CreateBearerToken(model.AccessToken));
            }
            catch
            {
                throw;
            }
        }

        public bool GetAuthorizationStatus(GetAuthorizedRolesBindingModel model)
        {
            try
            {
                return HttpUtility.Post<Boolean>("Auth/GetAuthorizationStatus", model, CreateBearerToken(model.AccessToken));
            }
            catch
            {
                throw;
            }
        }

        public bool ForgetPassword(ForgetPasswordBindingModel model)
        {
            try
            {
                try
                {
                    return HttpUtility.Post<Boolean>("Auth/ForgetPassword", model, _ClientAuthorizationToken);
                }
                catch (UnauthorizedHttpException)
                {
                    ResetClientToken();
                    return HttpUtility.Post<Boolean>("Auth/ForgetPassword", model, _ClientAuthorizationToken);
                }
            }
            catch
            {
                throw;
            }
        }

        public bool ResetPassword(ResetPasswordBindingModel model)
        {
            try
            {
                try
                {
                    return HttpUtility.Post<Boolean>("Auth/ResetPassword", model, _ClientAuthorizationToken);
                }
                catch (UnauthorizedHttpException)
                {
                    ResetClientToken();
                    return HttpUtility.Post<Boolean>("Auth/ResetPassword", model, _ClientAuthorizationToken);
                }
            }
            catch
            {
                throw;
            }
        }

        public bool SimpleResetPassword(SimpleResetPasswordBindingModel model)
        {
            try
            {
                try
                {
                    return HttpUtility.Post<Boolean>("Auth/SimpleResetPassword", model, _ClientAuthorizationToken);
                }
                catch (UnauthorizedHttpException)
                {
                    ResetClientToken();
                    return HttpUtility.Post<Boolean>("Auth/SimpleResetPassword", model, _ClientAuthorizationToken);
                }
            }
            catch
            {
                throw;
            }
        }

        #region Helpers

        private void ResetClientToken()
        {
            if (!String.IsNullOrEmpty(_ClientID) && !String.IsNullOrEmpty(_ClientSecret))
            {
                _AuthenticateClientResponse = AuthenticateClient(new AuthenticateClientRequest
                {
                    ClientID = _ClientID,
                    ClientSecret = _ClientSecret
                });

                _ClientAuthorizationToken = CreateBearerToken(_AuthenticateClientResponse.AccessToken);
            }
        }

        private void ResetClientBasicToken()
        {
            string clientSecret = EncryptClientSecret(_ClientID, _ClientSecret);
            _ClientBasicAuthorizationToken = CreateBasicToken(_ClientID, clientSecret);
        }

        private string EncryptClientSecret(string clientID, string clientSecret)
        {
            string result = clientSecret;

            if (_IsClientSecretEncrypted)
            {
                var clientEncryptionTokenResponse = GenerateClientEncryptionToken(new GenerateClientEncryptionTokenRequest
                {
                    ClientID = clientID
                });

                result = EncryptUtility.GetHash(result, clientEncryptionTokenResponse.Token);
            }

            return result;
        }

        private AuthorizationToken CreateBasicToken(string clientId, string clientSecret)
        {
            return CreateBasicToken(GenerateBasicAuthorizationToken(clientId, clientSecret));
        }

        private AuthorizationToken CreateBasicToken(string token)
        {
            return new AuthorizationToken(TokenTypeEnum.Basic, token);
        }

        private AuthorizationToken CreateBearerToken(string token)
        {
            return new AuthorizationToken(TokenTypeEnum.Bearer, token);
        }

        private AuthorizationToken CreateBasicAuthorization(AuthenticateClientRequest request)
        {
            string auturizationToken;

            if (!String.IsNullOrWhiteSpace(request?.ClientID) && !String.IsNullOrWhiteSpace(request?.ClientSecret))
            {
                string clientSecret = EncryptClientSecret(request.ClientID, request.ClientSecret);
                auturizationToken = GenerateBasicAuthorizationToken(request.ClientID, clientSecret);
            }
            else
            {
                auturizationToken = HttpContextProvider.Current?.GetToken();
            }

            if (String.IsNullOrWhiteSpace(auturizationToken))
            {
                throw new UnauthorizedHttpException();
            }
            else
            {
                var token = new AuthorizationToken(TokenTypeEnum.Basic, auturizationToken);
                return token;
            }
        }

        private string GenerateBasicAuthorizationToken(string clientId, string clientSecret)
        {
            return Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(clientId + ":" + clientSecret));
        }

        private string GetInitiator()
        {
#if NET48
            if (HttpContextProvider.Current != null && HttpContextProvider.Current.Request.Headers.AllKeys.Select(k => k.ToLower()).Contains("initiator"))
            {
                return HttpContextProvider.Current.Request.Headers.Get(HttpContextProvider.Current.Request.Headers.AllKeys.First(k => k.ToLower() == "initiator"));
            }
            else
            {
                return null;
            }
#else
            if (HttpContextProvider.Current != null && HttpContextProvider.Current.Request.Headers.Any(h => h.Key.ToLower() == "initiator"))
            {
                return HttpContextProvider.Current.Request.Headers.First(h => h.Key.ToLower() == "initiator").Value.FirstOrDefault();
            }
            else
            {
                return null;
            }
#endif
        }

        #endregion
    }
}
