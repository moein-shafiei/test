using DotFramework.Core;
using DotFramework.Infra.Security;
using DotFramework.Infra.Security.Model;
using DotFramework.Infra.Web;
using DotFramework.Infra.Web.API;
#if NETFRAMEWORK
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using System.Web.Http.Results;
#else
using Microsoft.AspNetCore.Mvc;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DotFramework.Infra.Web.API.Auth.Base.Providers;
using System.IO;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class AuthenticationService : IAuthenticationService
    {
        #region IAuthenticationService

        public virtual AuthenticateClientResponse AuthenticateClient(AuthenticateClientRequest request)
        {
            try
            {
                var identity = AuthService.Instance.AuthenticateClient(request);
                return CreateClientIdentity(identity);
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ObtainLocalAccessTokenResponse Login(LoginRequest request)
        {
            try
            {
                var tokenResponse = AuthService.Instance.Authenticate(new LoginBindingModel
                {
                    UserName = request.UserName,
                    Password = request.Password
                });

                return Authenticate(tokenResponse.AccessToken, tokenResponse.RefreshToken, request.Initiator);
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ObtainLocalAccessTokenResponse Reauthenticate(ReauthenticateRequest request)
        {
            try
            {
                var accessToken = (AuthContextProvider.AuthContext.User.Identity as ClaimsIdentity).FindFirst(CustomClaimTypes.SSOAccessToken)?.Value;
                return Authenticate(accessToken, request.RefreshToken, request.Initiator);
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void Logout()
        {
            try
            {
                var accessToken = (AuthContextProvider.AuthContext.User.Identity as ClaimsIdentity).FindFirst(CustomClaimTypes.SSOAccessToken)?.Value;
                AuthService.Instance.SignOut(new AuthorizationBindingModel { AccessToken = accessToken });

                TokenProviderFactory.Instance.TokenProvider.Revoke(AuthContextProvider.AuthContext.AccessToken);
            }
            catch (UnauthorizedHttpException)
            {

            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual RedirectResult GetExternalLogin(string provider, string redirect_url)
        {
            try
            {
                var context = AuthService.Instance.ExternalLogin(ApplicationManager.Instance.ApplicationCode, provider, redirect_url);

#if NETFRAMEWORK
                return new RedirectResult(new Uri(context.RedirectUrl), AuthContextProvider.AuthContext.Request);
#else
                return new RedirectResult(context.RedirectUrl); 
#endif
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ObtainLocalAccessTokenResponse ObtainLocalAccessToken(ObtainLocalAccessTokenRequest request)
        {
            try
            {
                var tokenResponse = AuthService.Instance.ExternalAuthenticate(new ObtainLocalAccessTokenBindingModel
                {
                    AccessToken = request.AccessToken,
                    Provider = request.Provider,
                    ClientID = ApplicationManager.Instance.ApplicationCode
                });

                return Authenticate(tokenResponse.AccessToken, tokenResponse.RefreshToken, request.Initiator);
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ObtainLocalAccessTokenResponse RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                var tokenResponse = AuthService.Instance.RefreshToken(new RefreshTokenBindingModel
                {
                    RefreshToken = request.RefreshToken
                });

                return Authenticate(tokenResponse.AccessToken, tokenResponse.RefreshToken, request.Initiator);
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                if (ex.ErrorResult.Error == "invalid_grant")
                {
                    throw new UnauthorizedHttpException();
                }
                else
                {
                    throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual AuthenticateResponseModel Authenticate(AuthenticateRequest request)
        {
            try
            {
                var verifyTokenResponse = AuthService.Instance.VerifyToken(new VerifyTokenRequest
                {
                    AccessToken = request.AccessToken
                });

                if (verifyTokenResponse == null)
                {
                    throw new UnauthorizedHttpException();
                }

                AuthenticateResponseModel response = CreateIdentity(verifyTokenResponse, request.AccessToken, request.Initiator);
                return response;
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual GenerateClientEncryptionTokenResponse GenerateClientEncryptionToken(GenerateClientEncryptionTokenRequest request)
        {
            try
            {
                return AuthService.Instance.GenerateClientEncryptionToken(request);
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ActivateClientResponse ActivateClient(ActivateClientRequest request)
        {
            try
            {
                return AuthService.Instance.ActivateClient(request);
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ActivateResponse Activate(ActivateRequest request)
        {
            try
            {
                return AuthService.Instance.Activate(request);
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ActivateLocalAccessTokenResponse GrantActivation(GrantActivationRequest request)
        {
            try
            {
                var response = AuthService.Instance.GrantActivation(new GrantActivationBindingModel
                {
                    Initiator = request.Initiator,
                    Token = request.Token,
                    UserName = request.UserName
                });

                var identity = CreateIdentity<ActivateLocalAccessTokenResponse>(response, response.AccessToken, request.Initiator);

                identity.ResetPasswordToken = response.ResetPasswordToken;

                return identity;
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual VerifyClientTokenResponseModel VerifyClientToken(VerifyTokenRequest request)
        {
            try
            {
                var claims = TokenProviderFactory.Instance.TokenProvider.UnProtect(request.AccessToken);
                request.AccessToken = claims.FirstOrDefault(c => c.Type == CustomClaimTypes.SSOAccessToken)?.Value;

                return AuthService.Instance.VerifyClientToken(request);
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual UserDataResponseModel Authorize(AuthorizeRequest request)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual UserDataResponseModel GetUserData()
        {
            try
            {
                if (AuthContextProvider.AuthContext?.UserType == UserTypes.User)
                {
                    var user = AuthContextProvider.AuthContext?.User;

                    if (user != null)
                    {
                        return new UserDataResponseModel
                        {
                            UserName = user.Identity.Name,
                            Email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                            FullName = user.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.DisplayName)?.Value,
                            FirstName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value,
                            LastName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value,
                            ProfilePicture = user.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.ProfilePicture)?.Value,
                            Roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToListOrDefault()
                        };
                    }
                    else
                    {
                        throw new UnauthorizedHttpException();
                    }
                }
                else
                {
                    throw new UnauthorizedHttpException();
                }
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ClientDataResponseModel GetClientData()
        {
            try
            {
                if (AuthContextProvider.AuthContext?.UserType == UserTypes.Client)
                {
                    var user = AuthContextProvider.AuthContext?.User;

                    if (user != null)
                    {
                        return new ClientDataResponseModel
                        {
                            ClientIdentifier = user.Identity.Name,
                            ClientName = user.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.ClientName)?.Value
                        };
                    }
                    else
                    {
                        throw new UnauthorizedHttpException();
                    }
                }
                else
                {
                    throw new UnauthorizedHttpException();
                }
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual AuthenticateResponseModel ValidateToken()
        {
            try
            {
                var accessToken = (AuthContextProvider.AuthContext.User.Identity as ClaimsIdentity).FindFirst(CustomClaimTypes.SSOAccessToken)?.Value;
                return Authenticate(new AuthenticateRequest { AccessToken = accessToken, Initiator = AuthContextProvider.AuthContext.Initiator });
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ObtainLocalAccessTokenResponse Register(RegisterRequest request)
        {
            try
            {
                var tokenResponse = AuthService.Instance.RegisterAndLogin(new RegisterBindingModel
                {
                    UserName = String.IsNullOrWhiteSpace(request.UserName) ? request.Email : request.UserName,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = request.Password,
                    ConfirmPassword = request.ConfirmPassword
                });

                return Authenticate(tokenResponse.AccessToken, tokenResponse.RefreshToken, request.Initiator);
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ChangePasswordResponse ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                var accessToken = (AuthContextProvider.AuthContext.User.Identity as ClaimsIdentity).FindFirst(CustomClaimTypes.SSOAccessToken)?.Value;

                AuthService.Instance.ChangePassword(new ChangePasswordBindingModel
                {
                    UserName = request.UserName,
                    ConfirmPassword = request.ConfirmPassword,
                    NewPassword = request.NewPassword,
                    OldPassword = request.OldPassword
                }, accessToken);

                return new ChangePasswordResponse();
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ForgetPasswordResponse ForgetPassword(ForgetPasswordRequest request)
        {
            try
            {
                AuthService.Instance.ForgetPassword(new ForgetPasswordBindingModel
                {
                    UserName = request.UserName,
                    RedirectUrl = request.RedirectUrl,
                    TemplateKeyValues = request.TemplateKeyValues
                });

                return new ForgetPasswordResponse();
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual ResetPasswordResponse ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                AuthService.Instance.ResetPassword(new ResetPasswordBindingModel
                {
                    UserName = request.UserName,
                    Token = request.Token,
                    NewPassword = request.NewPassword,
                    ConfirmPassword = request.ConfirmPassword
                });

                return new ResetPasswordResponse();
            }
            catch (HttpException<AuthErrorResult> ex)
            {
                throw new ApiCustomException(ex.ErrorResult.ErrorDescription, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Protected Methods

        protected ObtainLocalAccessTokenResponse Authenticate(string accessToken, string refreshToken, string initiator)
        {
            var authenticateRequest = new AuthenticateRequest
            {
                AccessToken = accessToken,
                Initiator = initiator
            };

            AuthenticateResponseModel authenticateResponse = Authenticate(authenticateRequest);

            var response = new ObtainLocalAccessTokenResponse
            {
                AccessToken = authenticateResponse.AccessToken,
                GroupToken = authenticateResponse.GroupToken,
                RefreshToken = refreshToken,
                TokenType = authenticateResponse.TokenType,
                UserName = authenticateResponse.UserName,
                Email = authenticateResponse.Email,
                FullName = authenticateResponse.FullName,
                FirstName = authenticateResponse.FirstName,
                LastName = authenticateResponse.LastName,
                IssuedAt = authenticateResponse.IssuedAt,
                ExpiresIn = authenticateResponse.ExpiresIn,
                ExpiresAt = authenticateResponse.ExpiresAt,
                ExternalUserID = authenticateResponse.ExternalUserID,
                ProfilePicture = authenticateResponse.ProfilePicture,
                Friends = authenticateResponse.Friends,
                Roles = authenticateResponse.Roles,
                FirstTimeLogin = authenticateResponse.FirstTimeLogin,
                Initiator = initiator
            };

            return response;
        }

        protected AuthenticateResponseModel CreateIdentity(IUserIdentity identity, string ssoAccessToken, string initiator)
        {
            return CreateIdentity<AuthenticateResponseModel>(identity, ssoAccessToken, initiator);
        }

        protected TUserIdentity CreateIdentity<TUserIdentity>(IUserIdentity identity, string ssoAccessToken, string initiator) where TUserIdentity : IUserIdentity, IAccessToken, new()
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, identity.UserName));
            claims.Add(new Claim(CustomClaimTypes.GroupToken, identity.GroupToken));
            claims.Add(new Claim(CustomClaimTypes.UserType, UserTypes.User));
            claims.Add(new Claim(ClaimTypes.Email, identity.Email));
            claims.Add(new Claim(CustomClaimTypes.SSOAccessToken, ssoAccessToken));

            if (!String.IsNullOrEmpty(initiator))
            {
                claims.Add(new Claim(CustomClaimTypes.Initiator, initiator));
            }

            if (!String.IsNullOrEmpty(identity.FullName))
            {
                claims.Add(new Claim(CustomClaimTypes.DisplayName, identity.FullName));
            }

            if (!String.IsNullOrEmpty(identity.FirstName))
            {
                claims.Add(new Claim(ClaimTypes.GivenName, identity.FirstName));
            }

            if (!String.IsNullOrEmpty(identity.LastName))
            {
                claims.Add(new Claim(ClaimTypes.Surname, identity.LastName));
            }

            if (!String.IsNullOrEmpty(identity.ProfilePicture))
            {
                claims.Add(new Claim(CustomClaimTypes.ProfilePicture, identity.ProfilePicture));
            }

            if (identity.Roles != null)
            {
                foreach (var role in identity.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            if (CustomClaims != null)
            {
                foreach (var claim in CustomClaims)
                {
                    claims.Add(claim);
                }
            }

            var baseTicket = TokenProviderFactory.Instance.TokenProvider.GetBaseTicket(claims);

            TUserIdentity response = new TUserIdentity
            {
                AccessToken = baseTicket.ProtectedToken,
                GroupToken = identity.GroupToken,
                TokenType = baseTicket.TokenType,
                UserName = identity.UserName,
                Email = identity.Email,
                FullName = identity.FullName,
                FirstName = identity.FirstName,
                LastName = identity.LastName,
                IssuedAt = baseTicket.IssuedAt,
                ExpiresIn = baseTicket.ExpiresIn,
                ExpiresAt = baseTicket.ExpiresAt,
                ExternalUserID = identity.ExternalUserID,
                ProfilePicture = identity.ProfilePicture,
                Friends = identity.Friends,
                Roles = identity.Roles,
                FirstTimeLogin = identity.FirstTimeLogin,
                Initiator = initiator,
                Properties = CustomClaims?.ToDictionary(c => c.Type, c => c.Value)
            };

            return response;
        }

        protected AuthenticateClientResponse CreateClientIdentity(AuthenticateClientResponse identity)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, identity.ClientIdentifier));
            claims.Add(new Claim(CustomClaimTypes.UserType, UserTypes.Client));
            claims.Add(new Claim(CustomClaimTypes.ClientName, identity.ClientName));
            claims.Add(new Claim(CustomClaimTypes.SSOAccessToken, identity.AccessToken));

            if (CustomClientClaims != null)
            {
                foreach (var claim in CustomClientClaims)
                {
                    claims.Add(claim);
                }
            }

            var baseTicket = TokenProviderFactory.Instance.TokenProvider.GetBaseTicket(claims);

            AuthenticateClientResponse response = new AuthenticateClientResponse
            {
                AccessToken = baseTicket.ProtectedToken,
                TokenType = baseTicket.TokenType,
                ClientIdentifier = identity.ClientIdentifier,
                ClientName = identity.ClientName,
                IssuedAt = baseTicket.IssuedAt,
                ExpiresIn = baseTicket.ExpiresIn,
                ExpiresAt = baseTicket.ExpiresAt,
                Properties = CustomClaims?.ToDictionary(c => c.Type, c => c.Value)
            };

            return response;
        }

        protected virtual IEnumerable<Claim> CustomClaims
        {
            get
            {
                return null;
            }
        }

        protected virtual IEnumerable<Claim> CustomClientClaims
        {
            get
            {
                return null;
            }
        }

        #endregion
    }
}