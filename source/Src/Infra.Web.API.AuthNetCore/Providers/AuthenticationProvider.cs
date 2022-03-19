using DotFramework.Core;
using DotFramework.Infra.Security.Model;
using DotFramework.Infra.Web.API.Auth.Base;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotFramework.Infra.Web.API.Auth
{
    public class AuthenticationProvider : SingletonProvider<AuthenticationProvider>
    {
        #region Constructor

        private AuthenticationProvider()
        {
            _AuthenticationService = new AuthenticationService();
        }

        #endregion

        #region Private Members

        private IAuthenticationService _AuthenticationService;

        #endregion

        #region Public Methods

        public void Initialize(IAuthenticationService authenticationService)
        {
            _AuthenticationService = authenticationService;
        }

        #endregion

        #region Internal Methods

        public AuthenticateClientResponse AuthenticateClient(AuthenticateClientRequest request)
        {
            try
            {
                OnAuthenticateClientStarted(new AuthenticateClientStartedEventArgs(request));
                var response = _AuthenticationService.AuthenticateClient(request);
                OnAuthenticateClientFinished(new AuthenticateClientFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnLoginFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ObtainLocalAccessTokenResponse Login(LoginRequest request)
        {
            try
            {
                OnLoginStarted(new LoginStartedEventArgs(request));
                var response = _AuthenticationService.Login(request);
                OnLoginFinished(new LoginFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnLoginFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ObtainLocalAccessTokenResponse Reauthenticate(ReauthenticateRequest request)
        {
            try
            {
                OnReauthenticateStarted(new ReauthenticateStartedEventArgs(request));
                var response = _AuthenticationService.Reauthenticate(request);
                OnReauthenticateFinished(new ReauthenticateFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnReauthenticateFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public void Logout()
        {
            try
            {
                OnLogoutStarted(new LogoutStartedEventArgs());
                _AuthenticationService.Logout();
                OnLogoutFinished(new LogoutFinishedEventArgs());
            }
            catch (Exception ex)
            {
                OnLoginFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public RedirectResult GetExternalLogin(string provider, string redirect_url)
        {
            try
            {
                OnGetExternalLoginStarted(new GetExternalLoginStartedEventArgs(provider, redirect_url));
                var result = _AuthenticationService.GetExternalLogin(provider, redirect_url);
                //TODO
                //OnGetExternalLoginFinished(new GetExternalLoginFinishedEventArgs(provider, redirect_url, result));

                return result;
            }
            catch (Exception ex)
            {
                OnObtainLocalAccessTokenFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ObtainLocalAccessTokenResponse ObtainLocalAccessToken(ObtainLocalAccessTokenRequest request)
        {
            try
            {
                OnObtainLocalAccessTokenStarted(new ObtainLocalAccessTokenStartedEventArgs(request));
                var response = _AuthenticationService.ObtainLocalAccessToken(request);
                OnObtainLocalAccessTokenFinished(new ObtainLocalAccessTokenFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnObtainLocalAccessTokenFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ObtainLocalAccessTokenResponse RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                OnRefreshTokenStarted(new RefreshTokenStartedEventArgs(request));
                var response = _AuthenticationService.RefreshToken(request);
                OnRefreshTokenFinished(new RefreshTokenFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnRefreshTokenFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public AuthenticateResponseModel Authenticate(AuthenticateRequest request)
        {
            try
            {
                OnAuthenticateStarted(new AuthenticateStartedEventArgs(request));
                var response = _AuthenticationService.Authenticate(request);
                OnAuthenticateFinished(new AuthenticateFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnAuthenticateFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public GenerateClientEncryptionTokenResponse GenerateClientEncryptionToken(GenerateClientEncryptionTokenRequest request)
        {
            try
            {
                OnGenerateClientEncryptionTokenStarted(new GenerateClientEncryptionTokenStartedEventArgs(request));
                var response = _AuthenticationService.GenerateClientEncryptionToken(request);
                OnGenerateClientEncryptionTokenFinished(new GenerateClientEncryptionTokenFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnAuthenticateFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ActivateClientResponse ActivateClient(ActivateClientRequest request)
        {
            try
            {
                OnActivateClientStarted(new ActivateClientStartedEventArgs(request));
                var response = _AuthenticationService.ActivateClient(request);
                OnActivateClientFinished(new ActivateClientFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnActivateClientFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ActivateResponse Activate(ActivateRequest request)
        {
            try
            {
                OnActivateStarted(new ActivateStartedEventArgs(request));
                var response = _AuthenticationService.Activate(request);
                OnActivateFinished(new ActivateFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnActivateClientFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ActivateLocalAccessTokenResponse GrantActivation(GrantActivationRequest request)
        {
            try
            {
                OnGrantActivationStarted(new GrantActivationStartedEventArgs(request));
                var response = _AuthenticationService.GrantActivation(request);
                OnGrantActivationFinished(new GrantActivationFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnActivateClientFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public VerifyClientTokenResponseModel VerifyClientToken(VerifyTokenRequest request)
        {
            try
            {
                OnVerifyClientTokenStarted(new VerifyClientTokenStartedEventArgs(request));
                var response = _AuthenticationService.VerifyClientToken(request);
                OnVerifyClientTokenFinished(new VerifyClientTokenFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnVerifyClientTokenFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public UserDataResponseModel Authorize(AuthorizeRequest request)
        {
            try
            {
                OnAuthorizeStarted(new AuthorizeStartedEventArgs(request));
                var response = _AuthenticationService.Authorize(request);
                OnAuthorizeFinished(new AuthorizeFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnAuthorizeFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public UserDataResponseModel GetUserData()
        {
            try
            {
                OnGetUserDataStarted(new GetUserDataStartedEventArgs());
                var result = _AuthenticationService.GetUserData();
                OnGetUserDataFinished(new GetUserDataFinishedEventArgs(result));

                return result;
            }
            catch (Exception ex)
            {
                OnGetUserDataFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ClientDataResponseModel GetClientData()
        {
            try
            {
                OnGetClientDataStarted(new GetClientDataStartedEventArgs());
                var result = _AuthenticationService.GetClientData();
                OnGetClientDataFinished(new GetClientDataFinishedEventArgs(result));

                return result;
            }
            catch (Exception ex)
            {
                OnGetClientDataFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public AuthenticateResponseModel ValidateToken()
        {
            try
            {
                OnValidateTokenStarted(new ValidateTokenStartedEventArgs());
                var result = _AuthenticationService.ValidateToken();
                OnValidateTokenFinished(new ValidateTokenFinishedEventArgs(result));

                return result;
            }
            catch (Exception ex)
            {
                OnGetUserDataFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ObtainLocalAccessTokenResponse Register(RegisterRequest request)
        {
            try
            {
                OnRegisterStarted(new RegisterStartedEventArgs(request));
                var response = _AuthenticationService.Register(request);
                OnRegisterFinished(new RegisterFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnRegisterFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ChangePasswordResponse ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                OnChangePasswordStarted(new ChangePasswordStartedEventArgs(request));
                var response = _AuthenticationService.ChangePassword(request);
                OnChangePasswordFinished(new ChangePasswordFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnForgetPasswordFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ForgetPasswordResponse ForgetPassword(ForgetPasswordRequest request)
        {
            try
            {
                OnForgetPasswordStarted(new ForgetPasswordStartedEventArgs(request));
                var response = _AuthenticationService.ForgetPassword(request);
                OnForgetPasswordFinished(new ForgetPasswordFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnForgetPasswordFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        public ResetPasswordResponse ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                OnResetPasswordStarted(new ResetPasswordStartedEventArgs(request));
                var response = _AuthenticationService.ResetPassword(request);
                OnResetPasswordFinished(new ResetPasswordFinishedEventArgs(request, response));

                return response;
            }
            catch (Exception ex)
            {
                OnResetPasswordFailed(new ErrorEventArgs(ex));
                throw;
            }
        }

        #endregion

        #region Events

        #region AuthenticateClient

        public event EventHandler<AuthenticateClientStartedEventArgs> AuthenticateClientStarted;

        protected void OnAuthenticateClientStarted(AuthenticateClientStartedEventArgs e)
        {
            AuthenticateClientStarted?.Invoke(this, e);
        }

        public event EventHandler<AuthenticateClientFinishedEventArgs> AuthenticateClientFinished;

        protected void OnAuthenticateClientFinished(AuthenticateClientFinishedEventArgs e)
        {
            AuthenticateClientFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> AuthenticateClientFailed;

        protected void OnAuthenticateClientFailed(ErrorEventArgs e)
        {
            AuthenticateClientFailed?.Invoke(this, e);
        }

        #endregion

        #region Login

        public event EventHandler<LoginStartedEventArgs> LoginStarted;

        protected void OnLoginStarted(LoginStartedEventArgs e)
        {
            LoginStarted?.Invoke(this, e);
        }

        public event EventHandler<LoginFinishedEventArgs> LoginFinished;

        protected void OnLoginFinished(LoginFinishedEventArgs e)
        {
            LoginFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> LoginFailed;

        protected void OnLoginFailed(ErrorEventArgs e)
        {
            LoginFailed?.Invoke(this, e);
        }

        #endregion

        #region Reauthenticate

        public event EventHandler<ReauthenticateStartedEventArgs> ReauthenticateStarted;

        protected void OnReauthenticateStarted(ReauthenticateStartedEventArgs e)
        {
            ReauthenticateStarted?.Invoke(this, e);
        }

        public event EventHandler<ReauthenticateFinishedEventArgs> ReauthenticateFinished;

        protected void OnReauthenticateFinished(ReauthenticateFinishedEventArgs e)
        {
            ReauthenticateFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> ReauthenticateFailed;

        protected void OnReauthenticateFailed(ErrorEventArgs e)
        {
            ReauthenticateFailed?.Invoke(this, e);
        }

        #endregion

        #region Logout

        public event EventHandler<LogoutStartedEventArgs> LogoutStarted;

        protected void OnLogoutStarted(LogoutStartedEventArgs e)
        {
            LogoutStarted?.Invoke(this, e);
        }

        public event EventHandler<LogoutFinishedEventArgs> LogoutFinished;

        protected void OnLogoutFinished(LogoutFinishedEventArgs e)
        {
            LogoutFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> LogoutFailed;

        protected void OnLogoutFailed(ErrorEventArgs e)
        {
            LogoutFailed?.Invoke(this, e);
        }

        #endregion

        #region GetExternalLogin

        public event EventHandler<GetExternalLoginStartedEventArgs> GetExternalLoginStarted;

        protected void OnGetExternalLoginStarted(GetExternalLoginStartedEventArgs e)
        {
            GetExternalLoginStarted?.Invoke(this, e);
        }

        public event EventHandler<GetExternalLoginFinishedEventArgs> GetExternalLoginFinished;

        protected void OnGetExternalLoginFinished(GetExternalLoginFinishedEventArgs e)
        {
            GetExternalLoginFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> GetExternalLoginFailed;

        protected void OnGetExternalLoginFailed(ErrorEventArgs e)
        {
            GetExternalLoginFailed?.Invoke(this, e);
        }

        #endregion

        #region ObtainLocalAccessToken

        public event EventHandler<ObtainLocalAccessTokenStartedEventArgs> ObtainLocalAccessTokenStarted;

        protected void OnObtainLocalAccessTokenStarted(ObtainLocalAccessTokenStartedEventArgs e)
        {
            ObtainLocalAccessTokenStarted?.Invoke(this, e);
        }

        public event EventHandler<ObtainLocalAccessTokenFinishedEventArgs> ObtainLocalAccessTokenFinished;

        protected void OnObtainLocalAccessTokenFinished(ObtainLocalAccessTokenFinishedEventArgs e)
        {
            ObtainLocalAccessTokenFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> ObtainLocalAccessTokenFailed;

        protected void OnObtainLocalAccessTokenFailed(ErrorEventArgs e)
        {
            ObtainLocalAccessTokenFailed?.Invoke(this, e);
        }

        #endregion

        #region RefreshToken

        public event EventHandler<RefreshTokenStartedEventArgs> RefreshTokenStarted;

        protected void OnRefreshTokenStarted(RefreshTokenStartedEventArgs e)
        {
            RefreshTokenStarted?.Invoke(this, e);
        }

        public event EventHandler<RefreshTokenFinishedEventArgs> RefreshTokenFinished;

        protected void OnRefreshTokenFinished(RefreshTokenFinishedEventArgs e)
        {
            RefreshTokenFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> RefreshTokenFailed;

        protected void OnRefreshTokenFailed(ErrorEventArgs e)
        {
            RefreshTokenFailed?.Invoke(this, e);
        }

        #endregion

        #region Authenticate

        public event EventHandler<AuthenticateStartedEventArgs> AuthenticateStarted;

        protected void OnAuthenticateStarted(AuthenticateStartedEventArgs e)
        {
            AuthenticateStarted?.Invoke(this, e);
        }

        public event EventHandler<AuthenticateFinishedEventArgs> AuthenticateFinished;

        protected void OnAuthenticateFinished(AuthenticateFinishedEventArgs e)
        {
            AuthenticateFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> AuthenticateFailed;

        protected void OnAuthenticateFailed(ErrorEventArgs e)
        {
            AuthenticateFailed?.Invoke(this, e);
        }

        #endregion

        #region GenerateClientEncryptionToken

        public event EventHandler<GenerateClientEncryptionTokenStartedEventArgs> GenerateClientEncryptionTokenStarted;

        protected void OnGenerateClientEncryptionTokenStarted(GenerateClientEncryptionTokenStartedEventArgs e)
        {
            GenerateClientEncryptionTokenStarted?.Invoke(this, e);
        }

        public event EventHandler<GenerateClientEncryptionTokenFinishedEventArgs> GenerateClientEncryptionTokenFinished;

        protected void OnGenerateClientEncryptionTokenFinished(GenerateClientEncryptionTokenFinishedEventArgs e)
        {
            GenerateClientEncryptionTokenFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> GenerateClientEncryptionTokenFailed;

        protected void OnGenerateClientEncryptionTokenFailed(ErrorEventArgs e)
        {
            GenerateClientEncryptionTokenFailed?.Invoke(this, e);
        }

        #endregion

        #region ActivateClient

        public event EventHandler<ActivateClientStartedEventArgs> ActivateClientStarted;

        protected void OnActivateClientStarted(ActivateClientStartedEventArgs e)
        {
            ActivateClientStarted?.Invoke(this, e);
        }

        public event EventHandler<ActivateClientFinishedEventArgs> ActivateClientFinished;

        protected void OnActivateClientFinished(ActivateClientFinishedEventArgs e)
        {
            ActivateClientFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> ActivateClientFailed;

        protected void OnActivateClientFailed(ErrorEventArgs e)
        {
            ActivateClientFailed?.Invoke(this, e);
        }

        #endregion

        #region Activate

        public event EventHandler<ActivateStartedEventArgs> ActivateStarted;

        protected void OnActivateStarted(ActivateStartedEventArgs e)
        {
            ActivateStarted?.Invoke(this, e);
        }

        public event EventHandler<ActivateFinishedEventArgs> ActivateFinished;

        protected void OnActivateFinished(ActivateFinishedEventArgs e)
        {
            ActivateFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> ActivateFailed;

        protected void OnActivateFailed(ErrorEventArgs e)
        {
            ActivateFailed?.Invoke(this, e);
        }

        #endregion

        #region GrantActivation

        public event EventHandler<GrantActivationStartedEventArgs> GrantActivationStarted;

        protected void OnGrantActivationStarted(GrantActivationStartedEventArgs e)
        {
            GrantActivationStarted?.Invoke(this, e);
        }

        public event EventHandler<GrantActivationFinishedEventArgs> GrantActivationFinished;

        protected void OnGrantActivationFinished(GrantActivationFinishedEventArgs e)
        {
            GrantActivationFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> GrantActivationFailed;

        protected void OnGrantActivationFailed(ErrorEventArgs e)
        {
            GrantActivationFailed?.Invoke(this, e);
        }

        #endregion

        #region VerifyClientToken

        public event EventHandler<VerifyClientTokenStartedEventArgs> VerifyClientTokenStarted;

        protected void OnVerifyClientTokenStarted(VerifyClientTokenStartedEventArgs e)
        {
            VerifyClientTokenStarted?.Invoke(this, e);
        }

        public event EventHandler<VerifyClientTokenFinishedEventArgs> VerifyClientTokenFinished;

        protected void OnVerifyClientTokenFinished(VerifyClientTokenFinishedEventArgs e)
        {
            VerifyClientTokenFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> VerifyClientTokenFailed;

        protected void OnVerifyClientTokenFailed(ErrorEventArgs e)
        {
            VerifyClientTokenFailed?.Invoke(this, e);
        }

        #endregion

        #region Authorize

        public event EventHandler<AuthorizeStartedEventArgs> AuthorizeStarted;

        protected void OnAuthorizeStarted(AuthorizeStartedEventArgs e)
        {
            AuthorizeStarted?.Invoke(this, e);
        }

        public event EventHandler<AuthorizeFinishedEventArgs> AuthorizeFinished;

        protected void OnAuthorizeFinished(AuthorizeFinishedEventArgs e)
        {
            AuthorizeFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> AuthorizeFailed;

        protected void OnAuthorizeFailed(ErrorEventArgs e)
        {
            AuthorizeFailed?.Invoke(this, e);
        }

        #endregion

        #region GetUserData

        public event EventHandler<GetUserDataStartedEventArgs> GetUserDataStarted;

        protected void OnGetUserDataStarted(GetUserDataStartedEventArgs e)
        {
            GetUserDataStarted?.Invoke(this, e);
        }

        public event EventHandler<GetUserDataFinishedEventArgs> GetUserDataFinished;

        protected void OnGetUserDataFinished(GetUserDataFinishedEventArgs e)
        {
            GetUserDataFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> GetUserDataFailed;

        protected void OnGetUserDataFailed(ErrorEventArgs e)
        {
            GetUserDataFailed?.Invoke(this, e);
        }

        #endregion

        #region GetClientData

        public event EventHandler<GetClientDataStartedEventArgs> GetClientDataStarted;

        protected void OnGetClientDataStarted(GetClientDataStartedEventArgs e)
        {
            GetClientDataStarted?.Invoke(this, e);
        }

        public event EventHandler<GetClientDataFinishedEventArgs> GetClientDataFinished;

        protected void OnGetClientDataFinished(GetClientDataFinishedEventArgs e)
        {
            GetClientDataFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> GetClientDataFailed;

        protected void OnGetClientDataFailed(ErrorEventArgs e)
        {
            GetClientDataFailed?.Invoke(this, e);
        }

        #endregion

        #region ValidateToken

        public event EventHandler<ValidateTokenStartedEventArgs> ValidateTokenStarted;

        protected void OnValidateTokenStarted(ValidateTokenStartedEventArgs e)
        {
            ValidateTokenStarted?.Invoke(this, e);
        }

        public event EventHandler<ValidateTokenFinishedEventArgs> ValidateTokenFinished;

        protected void OnValidateTokenFinished(ValidateTokenFinishedEventArgs e)
        {
            ValidateTokenFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> ValidateTokenFailed;

        protected void OnValidateTokenFailed(ErrorEventArgs e)
        {
            ValidateTokenFailed?.Invoke(this, e);
        }

        #endregion

        #region ChangePassword

        public event EventHandler<ChangePasswordStartedEventArgs> ChangePasswordStarted;

        protected void OnChangePasswordStarted(ChangePasswordStartedEventArgs e)
        {
            ChangePasswordStarted?.Invoke(this, e);
        }

        public event EventHandler<ChangePasswordFinishedEventArgs> ChangePasswordFinished;

        protected void OnChangePasswordFinished(ChangePasswordFinishedEventArgs e)
        {
            ChangePasswordFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> ChangePasswordFailed;

        protected void OnChangePasswordFailed(ErrorEventArgs e)
        {
            ChangePasswordFailed?.Invoke(this, e);
        }

        #endregion

        #region Register

        public event EventHandler<RegisterStartedEventArgs> RegisterStarted;

        protected void OnRegisterStarted(RegisterStartedEventArgs e)
        {
            RegisterStarted?.Invoke(this, e);
        }

        public event EventHandler<RegisterFinishedEventArgs> RegisterFinished;

        protected void OnRegisterFinished(RegisterFinishedEventArgs e)
        {
            RegisterFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> RegisterFailed;

        protected void OnRegisterFailed(ErrorEventArgs e)
        {
            RegisterFailed?.Invoke(this, e);
        }

        #endregion

        #region ForgetPassword

        public event EventHandler<ForgetPasswordStartedEventArgs> ForgetPasswordStarted;

        protected void OnForgetPasswordStarted(ForgetPasswordStartedEventArgs e)
        {
            ForgetPasswordStarted?.Invoke(this, e);
        }

        public event EventHandler<ForgetPasswordFinishedEventArgs> ForgetPasswordFinished;

        protected void OnForgetPasswordFinished(ForgetPasswordFinishedEventArgs e)
        {
            ForgetPasswordFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> ForgetPasswordFailed;

        protected void OnForgetPasswordFailed(ErrorEventArgs e)
        {
            ForgetPasswordFailed?.Invoke(this, e);
        }

        #endregion

        #region ResetPassword

        public event EventHandler<ResetPasswordStartedEventArgs> ResetPasswordStarted;

        protected void OnResetPasswordStarted(ResetPasswordStartedEventArgs e)
        {
            ResetPasswordStarted?.Invoke(this, e);
        }

        public event EventHandler<ResetPasswordFinishedEventArgs> ResetPasswordFinished;

        protected void OnResetPasswordFinished(ResetPasswordFinishedEventArgs e)
        {
            ResetPasswordFinished?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> ResetPasswordFailed;

        protected void OnResetPasswordFailed(ErrorEventArgs e)
        {
            ResetPasswordFailed?.Invoke(this, e);
        }

        #endregion

        #endregion
    }
}