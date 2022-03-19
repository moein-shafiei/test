using DotFramework.Infra.Security.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
#if NETFRAMEWORK
using System.Web.Http.Results;
#else
using Microsoft.AspNetCore.Mvc;
#endif

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public interface IAuthenticationService
    {
        AuthenticateClientResponse AuthenticateClient(AuthenticateClientRequest request);
        ObtainLocalAccessTokenResponse Login(LoginRequest request);
        ObtainLocalAccessTokenResponse Reauthenticate(ReauthenticateRequest request);
        void Logout();
        RedirectResult GetExternalLogin(string provider, string redirect_url); 
        ObtainLocalAccessTokenResponse ObtainLocalAccessToken(ObtainLocalAccessTokenRequest request);
        ObtainLocalAccessTokenResponse RefreshToken(RefreshTokenRequest request);
        AuthenticateResponseModel Authenticate(AuthenticateRequest request);
        GenerateClientEncryptionTokenResponse GenerateClientEncryptionToken(GenerateClientEncryptionTokenRequest request);
        ActivateClientResponse ActivateClient(ActivateClientRequest request);
        ActivateResponse Activate(ActivateRequest request);
        ActivateLocalAccessTokenResponse GrantActivation(GrantActivationRequest request);
        VerifyClientTokenResponseModel VerifyClientToken(VerifyTokenRequest request);
        UserDataResponseModel Authorize(AuthorizeRequest request);
        UserDataResponseModel GetUserData();
        ClientDataResponseModel GetClientData();
        AuthenticateResponseModel ValidateToken();
        ObtainLocalAccessTokenResponse Register(RegisterRequest request);
        ForgetPasswordResponse ForgetPassword(ForgetPasswordRequest request);
        ChangePasswordResponse ChangePassword(ChangePasswordRequest request);
        ResetPasswordResponse ResetPassword(ResetPasswordRequest request);
    }
}